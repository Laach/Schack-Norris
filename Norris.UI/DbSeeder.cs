using Microsoft.EntityFrameworkCore;
using Norris.Data.Data;
using Norris.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Norris.UI
{
    //Work in progress, has no use in current state
    public static class DbSeeder
    {
        public static void Seed(NContext context) 
        {
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


            context.Users.Add(test1);
            context.Users.Add(test2);
            context.SaveChanges();
            string DefaultGameState =
                "br,bn,bb,bq,bk,bb,bn,br," +
                "bp,bp,bp,bp,bp,bp,bp,bp," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "wp,wp,wp,wp,wp,wp,wp,wp," +
                "wr,wn,wb,wq,wk,wb,wn,wr";

            var testgame = new GameSession
            {
                Id = Guid.NewGuid().ToString(),
                Board = DefaultGameState,
                PlayerBlackID = "d6c04d35-9f84-4095-a257-10e447dd194e",
                PlayerBlack = context.Users.Where(e => e.Id.Equals("d6c04d35-9f84-4095-a257-10e447dd194e")).FirstOrDefault(),
                PlayerWhite = context.Users.Where(e => e.Id.Equals("d31b954c-687e-4f4e-8ef4-38a3039b603b")).FirstOrDefault(),
                PlayerWhiteID = "d31b954c-687e-4f4e-8ef4-38a3039b603b",
                IsActive = true,
                Log = "",
                IsWhitePlayerTurn = true
            };



        }
    }
}
