namespace Aqar.Infrastructure.Repositories.Admin
{
    public interface IAdminRepository
    {
        Task<Data.Model.Estate> ApproveEstate(int estateId);
        Task<List<Data.Model.Estate>> GetPendingEstates();
    }
}