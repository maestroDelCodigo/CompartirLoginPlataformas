using Microsoft.EntityFrameworkCore;
using PoC.Context;
using PoC.Models;

namespace PoC.Services
{
    public interface IUserService
    {
        Task<List<string>> GetAllAccessLoginsUser(int id, List<int> idAccessLoginUser);
        Task<List<int>> GetIdAccessLoginUser(List<int> accessLogins, User user);
    }
    public class UserService : IUserService
    {
        private readonly ShareLoginDatabaseContext _context;
        public UserService(ShareLoginDatabaseContext context) => _context = context;

        public async Task<List<String>> GetAllAccessLoginsUser(int id, List<int> idAccessLoginUser)
        {
            var accessLogin = new List<string>();

            try
            {
                accessLogin = await _context.AccessLogin
                                    .Where(a => idAccessLoginUser.Contains((int)a.IdAccessLogin))
                                    .Select(x => x.PlataformLink).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return accessLogin;
        }

        public async Task<List<int>> GetIdAccessLoginUser(List<int> accessLogins, User user)
        {
            var accessLoginUser = new List<int>();

            try
            {
                accessLoginUser = _context.UserLogin
                             .Where(p => accessLogins.Contains(p.IdAccessLogin))
                             .Where(y => user.IdUser == y.IdUser)
                             .Select(x => x.IdAccessLogin).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return accessLoginUser;
        }
    }
}
