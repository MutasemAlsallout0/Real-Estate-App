using Aqar.Core.DTOS.ApiBase;
using Aqar.Data.DataLayer;
using Aqar.Infrastructure.Exceptions;
using AutoMapper;

namespace Aqar.Infrastructure.Repositories.CommonRepository
{
    public class CommonRepository : ICommonRepository
    {
        private AqarDbContext _context;
        private IMapper _mapper;
        public CommonRepository(AqarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UserModel GetUserRole(UserModel userModel)
        {
            var dbUser = _context.Users
                                  .FirstOrDefault(x => x.Id == userModel.Id)
                                  ?? throw new ServiceValidationException("Invalid user id received");
            return _mapper.Map<UserModel>(dbUser);
        }
    }
}