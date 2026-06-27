using DAL.Models;

namespace Repository.Interfaces
{
    public interface IRegistrationRepository : IRepository<Registration>
    {
        Task<Registration?> GetByEmailAsync(string email);
        Task<IEnumerable<Registration>> GetActiveRegistrationsAsync();
    }
}
