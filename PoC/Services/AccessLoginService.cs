using Microsoft.EntityFrameworkCore;
using PoC.Context;
using PoC.Models;

namespace PoC.Services
{
    public interface IAccessLoginService
    {
        Task<List<int>> GetAllAccessLogin(int id);
    }
    public class AccessLoginService : IAccessLoginService
    {
        private readonly ShareLoginDatabaseContext _context;
        public AccessLoginService(ShareLoginDatabaseContext context) => _context = context;

        public async Task<List<int>> GetAllAccessLogin(int id)
        {
            var accessLogins = new List<int>();
            
            try
            {
                  accessLogins = await _context.UserLogin
                .Where(y => y.IdUser == id)
                .Select(x => x.IdAccessLogin).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return accessLogins;
        }
    }
}
