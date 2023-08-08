using Aqar.Core.DTOS.ApiBase;

namespace Aqar.Infrastructure.Repositories.CommonRepository
{
    public interface ICommonRepository
    {
        UserModel GetUserRole(UserModel userModel);
    }
}