using Aqar.Data.DataLayer;
using Aqar.Infrastructure.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Aqar.Infrastructure.Repositories.Admin
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AqarDbContext _context;
        private readonly IMapper _mapper;
        public AdminRepository(AqarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Data.Model.Estate>> GetPendingEstates()
        {
            var pendingEstates = await _context.Estates.Where(e => !e.SeenByAdmin).ToListAsync();
            return pendingEstates;
        }

        public async Task<Data.Model.Estate> ApproveEstate(int estateId)
        {
            var estate = await _context.Estates.FirstOrDefaultAsync(e => e.Id == estateId);

            if (estate == null) throw new ServiceValidationException("لا بوجد عقار");

            estate.SeenByAdmin = true;

            _context.SaveChanges();

            return estate;
        }

    }
}