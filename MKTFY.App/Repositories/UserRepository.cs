using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.InteropServices;
using MKTFY.App.Exceptions;

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
            //List<string> a = new List<string>();

            var result = await _context.Users.FirstAsync(item => item.Email == email);
            int qtyListings = await ListingCount(result.Id, true);
            var model = new UserVM(result, qtyListings);

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

            var qtyListings = await ListingCount(guid, true);

            return new UserVM(result, qtyListings);
        }

        public async Task<UserVM> UpdateUser(UserUpdateVM src)
        {
            var currlUser = await _context.Users.FindAsync(src.Id);

            if (currlUser == null)
                throw new NotFoundException("User not found", src.Id );

            // updates the data
            currlUser.CityId = src.CityId;
            currlUser.Address = src.Address;
            currlUser.PostalCode = src.PostalCode;
            await _context.SaveChangesAsync();

            return await GetById(src.Id);
        }
    }
}
