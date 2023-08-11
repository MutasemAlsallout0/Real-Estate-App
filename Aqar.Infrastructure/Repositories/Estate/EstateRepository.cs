using Aqar.Core.DTOS.ApiBase;
using Aqar.Core.DTOS.Estate;
using Aqar.Core.Enums;
using Aqar.Data.DataLayer;
using Aqar.Data.Model;
using Aqar.Infrastructure.Exceptions;
using Aqar.Infrastructure.Extensions;
using Aqar.Infrastructure.HelperServices.ImageHelper;
using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Aqar.Infrastructure.Repositories.Estate
{
    public class EstateRepository : IEstateRepository
    {
        private readonly AqarDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public EstateRepository(AqarDbContext context,
                               IMapper mapper,
                               IImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<List<Country>> GetCountries()
        {
            var countries = await _context.Countries.ToListAsync();


            return countries;
        }

        public async Task<List<City>> GetCities(int countryId)
        {
            var cities = await _context.Cities.Where(x => x.CountryId == countryId).ToListAsync();


            return cities;
        }

        public async Task<PaginatedList<GetEstateDto>> GetPaginatedDataAsync(int pageNumber, int pageSize, EstateType? estateType)
        {
            var query = _context.Estates.AsQueryable().Where(x => x.SeenByAdmin == true);

            if (estateType.HasValue)
            {
                query = query.Where(x => x.EstateType == estateType.Value);
            }

            var totalCount = await query.CountAsync();
            var pageData = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                                     .Select(x => new GetEstateDto
                                     {
                                         Id = x.Id,
                                         OwnerEstate = x.User.GetFullName(),
                                         UserImage = x.User.UserImage,
                                         EstateType = x.DisplayEstateType,
                                         ContractType = x.DisplayContractType,
                                         Price = x.Price,
                                         Area = x.Area,
                                         Description = x.Description,
                                         street = x.Street.Name,
                                         city = x.Street.City.Name,
                                         country = x.Street.City.Country.Name,
                                         MainImage = x.MainImage,
                                         CreateAt = x.CreateAt,
                                     })
                                     .ToListAsync();

            var paginatedList = new PaginatedList<GetEstateDto>(pageData, totalCount, pageNumber, pageSize);
            return paginatedList;
        }


        public async Task<GetEstateDto> GetEstate(UserModel currentUser, int Id)
        {
            var estate = await _context.Estates
                .Include(z => z.User)
                .Include(x => x.Street).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == Id && x.SeenByAdmin == true);

            if (estate == null)
                throw new ServiceValidationException("لا يوجد عقار!");

            var imagesUrls = await _context.Attachments
                .Where(ei => ei.EstateId == estate.Id)
                .Select(ei => ei.Image)
                .ToListAsync();

            var estateDTO = new GetEstateDto
            {
                Id = estate.Id,
                Images = imagesUrls,
                Description = estate.Description,
                Price = estate.Price,
                EstateType = estate.DisplayEstateType,
                ContractType = estate.DisplayContractType,
                street = estate.Street.Name,
                city = estate.Street.City.Name,
                country = estate.Street.City.Country.Name,
                OwnerEstate = estate.User.GetFullName(),
                Area = estate.Area,
                MainImage = estate.MainImage,
                UserImage = estate.User.UserImage,
                PhoneNumber = estate.User.PhoneNumber,
                 CreateAt = estate.CreateAt,

            };

            if (currentUser.Id == estate.User.Id)
            {
                estateDTO.IsOwner = true;
            }
            else
            {
                estateDTO.IsOwner = false;
            }
            return estateDTO;
        }


        public async Task<List<Attachment>> AddAttachment(List<Attachment> attachments)
        {
            await _context.Attachments.AddRangeAsync(attachments);
            await _context.SaveChangesAsync();
            return attachments;
        }


        public async Task<Data.Model.Estate> Create(UserModel currentUser, CreateEstateDTO input)
        {
            int newStreetId;

            if (_context.Streets.Any(x => x.Name.Equals(input.StreetName)))
            {
                var updateStreet = await _context.Streets.FirstOrDefaultAsync(x => x.Name.ToLower() == input.StreetName.ToLower());
                newStreetId = updateStreet.Id;
            }
            else
            {
                var newStreert = new Street
                {
                    CityId = input.CityId,
                    Name = input.StreetName
                };

                var newStreet = _context.Streets.Add(newStreert);
                await _context.SaveChangesAsync();

                newStreetId = newStreert.Id;
            }


            var estate = _mapper.Map<Data.Model.Estate>(input);
            estate.UserId = currentUser.Id;
            estate.StreetId = newStreetId;

            if (input.MainImage != null)
            {
                estate.MainImage = await _imageService.SaveImage(input.MainImage, "Images");
            }
            //estate.SeenByAdmin = false;
            estate.SeenByAdmin = true;

            await _context.Estates.AddAsync(estate);
            await _context.SaveChangesAsync();

            return estate;

        }

        public async Task<Data.Model.Estate> UpdateEstate(UpdateEstateDTO input)
        {
            var dbestate = await _context.Estates.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (dbestate == null) throw new ServiceValidationException("لا يوجد عقار!");

            dbestate.EstateType = input.EstateType;
            dbestate.ContractType = input.ContractType;
            dbestate.Price = input.Price;
            dbestate.Description = input.Description;
            dbestate.Area = input.Area;
            dbestate.StreetId = input.StreetId;

            var Result = _context.Estates.Update(dbestate);

            await _context.SaveChangesAsync();

            return dbestate;

        }

        public async Task<string> DeleteEstate(int id)
        {
            var dbestate = await _context.Estates.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);

            if (dbestate == null) throw new ServiceValidationException("لا يوجد عقار!");

            foreach (var estateImage in dbestate.Images.ToList())
            {
                var dbestateAttachment = await _context.Attachments.FirstOrDefaultAsync(x => x.Id == estateImage.Id);
                if (dbestateAttachment != null)
                {
                    _context.Attachments.Remove(dbestateAttachment);
                }
            }

            _context.Estates.Remove(dbestate);

            await _context.SaveChangesAsync();

            return "تم حذف العقار والمرفقات المرتبطة بنجاح!";
        }


    }
}