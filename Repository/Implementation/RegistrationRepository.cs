using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Implementation
{
    public class RegistrationRepository : Repository<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Registration?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.Email == email);
        }

        public async Task<IEnumerable<Registration>> GetActiveRegistrationsAsync()
        {
            return await _dbSet.Where(r => r.IsActive).ToListAsync();
        }
    }
}
