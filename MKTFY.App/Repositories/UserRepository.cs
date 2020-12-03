﻿using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    }
}
