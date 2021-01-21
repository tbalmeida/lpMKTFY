using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.InteropServices;

namespace MKTFY.App.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<UserVM> GetUserByEmail(string email)
        {
            List<string> a = new List<string>();
            var result = await _context.Users.FirstAsync(item => item.Email == email);

            var model = new UserVM(result);
            return model;
        }

        public async Task<int> ListingCount(string userId, bool activeOnly, [Optional] int? status)
        {
            var lists = _context.Listings
                .Include(item => item.ListingStatus)
                .AsQueryable();

            // filters the user, always
            lists = lists.Where(x => x.UserId == userId);

            // Filter active only
            if (activeOnly)
                lists = lists.Where(x => x.ListingStatus.IsActive == true);

            // Filter specific status
            if (status.HasValue && status > 0)
                lists = lists.Where(x => x.ListingStatusId == status);

            // retrieves only the record count
            var results =  await lists.CountAsync();

            return results;
        }

        public async Task<UserVM> GetById(string guid)
        {
            var result = await _context.Users
                .Include(item => item.City)
                .Include(item => item.City.Province)
                .FirstOrDefaultAsync(user => user.Id == guid);

            return new UserVM(result);
        }
    }
}
