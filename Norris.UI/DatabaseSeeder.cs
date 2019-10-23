using Microsoft.EntityFrameworkCore;
using Norris.Data.Data;
using Norris.Data.Data.Entities;
using Norris.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Norris.UI
{
    //Adds 2 Users
    //Sets both Users to each others friends
    //Adds 1 Gamesession in which both userser are players
    public class DatabaseSeeder
    {
        private readonly NContext _context;
        private readonly GameRepository repo;
       public DatabaseSeeder(NContext context)
        {
            
            _context = context;
            //Uses the GameRepo to add new Games and add friends
            repo = new GameRepository(context);
        }

        public void SeedData()
        {
            //Checkes if database is empty of Users
            var IsNotEmpty = _context.Users.Any();
            //Only adds data if database is empty 
            if (!IsNotEmpty)
            {
                AddNewUsers();
                _context.SaveChanges();
                repo.AddNewGame(
                    "d6c04d35-9f84-4095-a257-10e447dd194e",
                    "d31b954c-687e-4f4e-8ef4-38a3039b603b"
                    );
                repo.AddFriend(
                    "d6c04d35-9f84-4095-a257-10e447dd194e",
                    "d31b954c-687e-4f4e-8ef4-38a3039b603b"
                    );
                _context.SaveChanges();
            }

        }

        //Creates and add 2 Users to the Database
        private void AddNewUsers()
        {
            
            var test1 = new User
            {
                Id = "d6c04d35-9f84-4095-a257-10e447dd194e",
                AccessFailedCount = 0,
                ConcurrencyStamp = "330b5e81-0750-4c84-8505-cf09e429e4d6",
                Email = "test1@test1.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                NormalizedEmail = "TEST1@TEST1.COM",
                NormalizedUserName = "TEST1",
                PasswordHash = "AQAAAAEAACcQAAAA" +
                               "EN3dCY1fbjemhlrU" +
                               "1XNuhLfqaOBXO1ua" +
                               "EhBshy2Uyh25rsYx" +
                               "wEDkvQZVwQ1BQDQCUw==",
                PhoneNumberConfirmed = false,
                SecurityStamp = "628edb52-be6a-40c7-a773-4272639a4024",
                TwoFactorEnabled = false,
                UserName = "test1"
            };

            var test2 = new User
            {
                Id = "d31b954c-687e-4f4e-8ef4-38a3039b603b",
                AccessFailedCount = 0,
                ConcurrencyStamp = "70b6edd3-bd78-4244-98a9-4d6413707ebc",
                Email = "test2@test2.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                NormalizedEmail = "TEST2@TEST2.COM",
                NormalizedUserName = "TEST2",
                PasswordHash = "AQAAAAEAACcQAAA" +
                   "AEEiMSw8XSP43mn" +
                   "ruK9r94BkIva14A" +
                   "SvH4KFrHMmEqF15" +
                   "mmZvezIpwgzRE+" +
                   "Tukun4JA ==",
                PhoneNumberConfirmed = false,
                SecurityStamp = "2a355eca-c2dc-432d-98ec-aa23a818b6c3",
                TwoFactorEnabled = false,
                UserName = "test2"
            };
            _context.Users.Add(test1);
            _context.Users.Add(test2);
        }
    }
}
