﻿using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Data
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(DbContextClass dbContext)
        {
            DbContext = dbContext;
        }

        public DbContextClass DbContext { get; }

        public User GetOneUser(string id)
        {
            var result = DbContext.Users.Find(id);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public bool Login(LoginDTO loginDTO)
        {
            var user = DbContext.Users.Find(loginDTO.username);
            var password = BCrypt.Net.BCrypt.Verify(loginDTO.password, user.password);

            return user != null && password;

        }

        public bool Register(RegisterDTO user)
        {
            var searchUsers = GetOneUser(user.username);

            if (searchUsers != null)
            {
                return false;
            }

            var newPassword = BCrypt.Net.BCrypt.HashPassword(user.password);

            User newUser = new User() { username = user.username, email = user.email, password = newPassword, likes = 0};

            var result = DbContext.Users.Add(newUser);

            try{
                return DbContext.SaveChanges() > 0;
            }
            catch { 
                return false;
            }
        }
    } 
}
