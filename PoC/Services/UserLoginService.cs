using Microsoft.EntityFrameworkCore;
using PoC.Context;
using PoC.Models;

namespace PoC.Services
{
    public interface IUserLoginService
    {
        Task<List<UserLogin>> GetAlbumesAsync();
    }
    public class UserLoginService : IUserLoginService
    {
        private readonly ShareLoginDatabaseContext _context;
        public UserLoginService(ShareLoginDatabaseContext context) => _context = context;

        public async Task<List<UserLogin>> GetAlbumesAsync()
        {
            var albumes = new List<UserLogin>();

            try
            {
                albumes = await _context.UserLogin.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return albumes;
        }
    }
}
