using Microsoft.EntityFrameworkCore;
using Norris.Data.Data;
using Norris.Data.Data.Entities;
using Norris.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Norris.UI
{
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
            var IsNotEmpty = _context.GameSessions.Any();
            if (IsNotEmpty)
            {

                var temp = _context.GameSessions.Include(c => c.Chatlog).AsEnumerable<GameSession>();
                foreach (var game in temp)
                {
                    if (game.Chatlog != null)
                    {
                        foreach (var chat in game.Chatlog.ToList())
                        {
                            _context.GameSessions.Where(u => u == game).FirstOrDefault().Chatlog.Remove(chat);
                        }
                    }
                }
                _context.SaveChanges();
                _context.GameSessions.RemoveRange(temp);
                _context.SaveChanges();
            }

            IsNotEmpty = _context.Users.Any();
            if (IsNotEmpty)
            {
                var temp2 = _context.Users.Include(g => g.Friends).AsEnumerable<User>();
                foreach (var user in temp2)
                {
                    if (user.Friends != null)
                    {
                        foreach (var friend in user.Friends.ToList())
                        {
                            _context.Users.Where(u => u == user).FirstOrDefault().Friends.Remove(friend);
                        }
                    }
                }
                _context.SaveChanges();
                _context.Users.RemoveRange(temp2);
                _context.SaveChanges();
            }
            //Add cappe, alex, nick, emil, and philip as users
            AddNewUsers();
            _context.SaveChanges();

            //cappe and philip are friends with everyone else. Everyone else are only friends with cappe and philip
            repo.AddFriend("19cd7126-2f69-4b1d-9326-d92fcb438f2d",
                            "08e03d0c-bfb1-437a-a877-1d370bd92cc5");

            repo.AddFriend("b6d5de24-98f5-4e29-9fe8-5419f5140a02",
                            "08e03d0c-bfb1-437a-a877-1d370bd92cc5");

            repo.AddFriend("7e3fc3e1-5049-4567-96a2-a50db094cc3d",
                            "19cd7126-2f69-4b1d-9326-d92fcb438f2d");

            repo.AddFriend("b6d5de24-98f5-4e29-9fe8-5419f5140a02",
                            "19cd7126-2f69-4b1d-9326-d92fcb438f2d");

            repo.AddFriend( "bc64fdac-65bf-4e91-b9cf-cdbc6c542e2c",
                            "19cd7126-2f69-4b1d-9326-d92fcb438f2d");

            repo.AddFriend("b6d5de24-98f5-4e29-9fe8-5419f5140a02",
                            "7e3fc3e1-5049-4567-96a2-a50db094cc3d");

            repo.AddFriend("bc64fdac-65bf-4e91-b9cf-cdbc6c542e2c",
                            "b6d5de24-98f5-4e29-9fe8-5419f5140a02");


            //Some games
            string gameId = repo.AddNewGame("19cd7126-2f69-4b1d-9326-d92fcb438f2d",
                            "08e03d0c-bfb1-437a-a877-1d370bd92cc5");
            _context.GameSessions.Find(gameId).MovesCounter = 15;
            _context.GameSessions.Find(gameId).Board =      
                    "br,ee,ee,ee,ee,ee,ee,br," +
                    "bp,ee,bp,bk,ee,bp,ee,ee," +
                    "ee,ee,bp,ee,ee,ee,ee,bp," +
                    "ee,ee,ee,bp,ee,ee,wb,bb," +
                    "ee,ee,ee,ee,ee,ee,ee,ee," +
                    "ee,ee,ee,wb,ee,ee,ee,ee," +
                    "wp,wp,wp,ee,wr,bb,wp,wp," +
                    "wr,wn,ee,ee,ee,ee,ee,wk";
            _context.GameSessions.Find(gameId).IsWhitePlayerTurn = true;

            gameId = repo.AddNewGame("b6d5de24-98f5-4e29-9fe8-5419f5140a02",
                            "08e03d0c-bfb1-437a-a877-1d370bd92cc5");
            _context.GameSessions.Find(gameId).MovesCounter = 23;
            _context.GameSessions.Find(gameId).Board =
                    "ee,ee,ee,ee,ee,wb,ee,ee," +
                    "ee,ee,ee,ee,ee,ee,wr,ee," +
                    "ee,br,ee,ee,ee,ee,ee,bk," +
                    "ee,br,ee,bp,ee,wr,ee,ee," +
                    "bp,ee,ee,ee,ee,ee,wp,bp," +
                    "wp,ee,wp,ee,ee,ee,bn,ee," +
                    "ee,wp,ee,ee,ee,ee,wp,ee," +
                    "ee,ee,ee,ee,ee,ee,wk,ee,";
            _context.GameSessions.Find(gameId).IsWhitePlayerTurn = true;

            gameId = repo.AddNewGame("7e3fc3e1-5049-4567-96a2-a50db094cc3d",
                            "19cd7126-2f69-4b1d-9326-d92fcb438f2d");

            _context.GameSessions.Find(gameId).MovesCounter = 18;
            _context.GameSessions.Find(gameId).Board =
                    "br,bk,ee,ee,ee,ee,ee,br," +
                    "bp,bp,ee,wq,ee,bp,ee,bp," +
                    "ee,bq,ee,bp,ee,wn,ee,ee," +
                    "ee,ee,ee,ee,ee,ee,ee,ee," +
                    "ee,ee,ee,ee,ee,ee,ee,ee," +
                    "ee,ee,ee,ee,ee,ee,wp,ee," +
                    "wp,wp,ee,ee,ee,wp,wb,wp," +
                    "ee,ee,wr,ee,ee,ee,wk,ee,";
            _context.GameSessions.Find(gameId).IsWhitePlayerTurn = false;

            gameId = repo.AddNewGame("b6d5de24-98f5-4e29-9fe8-5419f5140a02",
                            "19cd7126-2f69-4b1d-9326-d92fcb438f2d");

            _context.GameSessions.Find(gameId).MovesCounter = 16;
            _context.GameSessions.Find(gameId).Board =
                    "ee,ee,ee,ee,ee,br,bk,ee," +
                    "bp,bp,ee,ee,ee,ee,bp,bp," +
                    "ee,ee,bp,ee,ee,ee,bn,ee," +
                    "ee,ee,ee,bp,ee,ee,wn,ee," +
                    "ee,ee,wp,ee,ee,wp,wk,ee," +
                    "ee,ee,ee,wb,ee,ee,wp,ee," +
                    "wp,wp,ee,ee,ee,ee,ee,bq," +
                    "wr,ee,wb,wq,ee,wr,ee,ee,";
            _context.GameSessions.Find(gameId).IsWhitePlayerTurn = false;

            gameId = repo.AddNewGame("bc64fdac-65bf-4e91-b9cf-cdbc6c542e2c",
                            "19cd7126-2f69-4b1d-9326-d92fcb438f2d");

            _context.GameSessions.Find(gameId).MovesCounter = 14;
            _context.GameSessions.Find(gameId).Board =
                    "br,ee,ee,ee,ee,br,ee,bk," +
                    "ee,bp,bp,bq,wn,bp,bp,bp," +
                    "bp,ee,bp,ee,bb,ee,ee,ee," +
                    "ee,ee,ee,bn,ee,ee,wb,wq," +
                    "ee,ee,ee,bp,wr,ee,ee,ee," +
                    "ee,ee,ee,ee,ee,ee,ee,ee," +
                    "wp,wp,wp,ee,ee,wp,wp,wp," +
                    "ee,ee,ee,ee,ee,ee,wk,ee,";
            _context.GameSessions.Find(gameId).IsWhitePlayerTurn = true;

            gameId = repo.AddNewGame("b6d5de24-98f5-4e29-9fe8-5419f5140a02",
                            "7e3fc3e1-5049-4567-96a2-a50db094cc3d");

            _context.GameSessions.Find(gameId).MovesCounter = 12;
            _context.GameSessions.Find(gameId).Board =
                    "br,ee,bb,bk,ee,ee,bn,br," +
                    "bp,ee,ee,bp,ee,bp,wn,bp," +
                    "bn,ee,ee,wb,ee,ee,ee,ee," +
                    "ee,bp,ee,wn,wp,ee,ee,wp," +
                    "ee,ee,ee,ee,ee,ee,wp,ee," +
                    "ee,ee,ee,wp,ee,wq,ee,ee," +
                    "wp,ee,wp,ee,wk,ee,ee,ee," +
                    "bq,ee,ee,ee,ee,ee,bb,ee,";
            _context.GameSessions.Find(gameId).IsWhitePlayerTurn = true;

            gameId = repo.AddNewGame("bc64fdac-65bf-4e91-b9cf-cdbc6c542e2c",
                            "b6d5de24-98f5-4e29-9fe8-5419f5140a02");

            _context.GameSessions.Find(gameId).MovesCounter = 3;
            _context.GameSessions.Find(gameId).Board =
                    "ee,ee,ee,ee,ee,ee,ee,ee," +
                    "ee,ee,ee,ee,ee,ee,ee,ee," +
                    "ee,ee,ee,ee,ee,bk,ee,ee," +
                    "ee,wr,ee,ee,ee,ee,ee,ee," +
                    "wr,ee,ee,ee,ee,ee,ee,ee," +
                    "ee,ee,ee,ee,ee,ee,ee,ee," +
                    "ee,ee,ee,ee,ee,ee,ee,ee," +
                    "ee,ee,ee,ee,wk,ee,ee,ee,";
            _context.GameSessions.Find(gameId).IsWhitePlayerTurn = true;

            //b6d5de24-98f5-4e29-9fe8-5419f5140a02

            gameId = repo.AddNewGame("bc64fdac-65bf-4e91-b9cf-cdbc6c542e2c",
                            "b6d5de24-98f5-4e29-9fe8-5419f5140a02");

            _context.GameSessions.Find(gameId).MovesCounter = 30;
            _context.GameSessions.Find(gameId).Board =
                "br,bn,bb,bq,bk,bb,bn,br," +
                "ee,bp,bp,bp,bp,wq,bp,bp," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "bp,ee,wb,ee,wp,ee,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "wp,wp,wp,wp,ee,wp,wp,wp," +
                "wr,wn,wb,ee,wk,ee,wn,wr";
            _context.GameSessions.Find(gameId).IsActive = false;
            _context.GameSessions.Find(gameId).IsWhitePlayerTurn = false;


            gameId = repo.AddNewGame("7e3fc3e1-5049-4567-96a2-a50db094cc3d",
                            "19cd7126-2f69-4b1d-9326-d92fcb438f2d");

            _context.GameSessions.Find(gameId).MovesCounter = 75;
            _context.GameSessions.Find(gameId).Board =
                "ee,ee,ee,ee,bk,ee,ee,ee," +
                "ee,bb,ee,ee,wp,ee,ee,ee," +
                "ee,ee,ee,ee,wk,wn,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,bp," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,ee";
            _context.GameSessions.Find(gameId).IsActive = false;
            _context.GameSessions.Find(gameId).IsWhitePlayerTurn = false;


            gameId = repo.AddNewGame("19cd7126-2f69-4b1d-9326-d92fcb438f2d",
                            "7e3fc3e1-5049-4567-96a2-a50db094cc3d");

            _context.GameSessions.Find(gameId).MovesCounter = 46;
            _context.GameSessions.Find(gameId).Board =
                "ee,ee,bk,wq,ee,ee,bn,br," +
                "bp,bp,bp,ee,ee,bp,bp,ee," +
                "ee,ee,bq,ee,ee,ee,ee,ee," +
                "ee,ee,ee,ee,bp,ee,ee,ee," +
                "ee,wp,ee,ee,ee,ee,ee,bp," +
                "wp,ee,ee,ee,wp,ee,ee,ee," +
                "ee,ee,ee,ee,wb,wp,wp,wp," +
                "ee,ee,ee,wr,ee,ee,wk,ee";
            _context.GameSessions.Find(gameId).IsActive = false;
            _context.GameSessions.Find(gameId).IsWhitePlayerTurn = false;


            gameId = repo.AddNewGame("19cd7126-2f69-4b1d-9326-d92fcb438f2d",
                            "bc64fdac-65bf-4e91-b9cf-cdbc6c542e2c");

            _context.GameSessions.Find(gameId).MovesCounter = 54;
            _context.GameSessions.Find(gameId).Board =
                "ee,bk,ee,ee,ee,ee,ee,br," +
                "wq,bp,bp,ee,ee,ee,ee,br," +
                "ee,bn,ee,ee,ee,ee,bn,ee," +
                "ee,wp,ee,ee,ee,ee,bp,ee," +
                "ee,ee,ee,ee,wp,ee,ee,bp," +
                "ee,ee,ee,bq,wp,ee,wp,wb," +
                "ee,ee,ee,ee,ee,ee,ee,wp," +
                "wr,ee,ee,ee,ee,ee,wk,ee";
            _context.GameSessions.Find(gameId).IsActive = false;
            _context.GameSessions.Find(gameId).IsWhitePlayerTurn = false;



            _context.SaveChanges();
            

        }

        private void AddNewUsers()
        {
            using (var reader = new StreamReader(@"us-500.csv"))
            {
                reader.ReadLine();
                int LFG = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    line = line.Replace("\\", "");
                    line = line.Replace("\"", "");
                    var values = line.Split(',');
                    string username = values[0] + values[1];
                    User user = new User
                    {                        
                        Id = Guid.NewGuid().ToString(),
                        AccessFailedCount = 0,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Email = values[10],
                        EmailConfirmed = false,
                        LockoutEnabled = true,
                        NormalizedEmail = values[11].ToUpper(),
                        NormalizedUserName = username.ToUpper(),
                        PasswordHash = "AQAAAAEAACcQAAAAEA7iUp/6A4MnDJlrrwhldY0l8zmjI0QIc8eZNk2BiydK59mSTgd/czQYaQ150AwVtA==",
                        PhoneNumberConfirmed = false,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        TwoFactorEnabled = false,
                        UserName = username
                    };
                    if (LFG++ % 50 == 0)
                    {
                        user.IsInLobby=true;
                    }
                    _context.Users.Add(user);
                }
            }

            //password: Mediumcook1!
            var cappe = new User
            {
                Id = "19cd7126-2f69-4b1d-9326-d92fcb438f2d",
                AccessFailedCount = 0,
                ConcurrencyStamp = "8878ab0d-53d9-455e-bd2b-415f61eb3edf",
                Email = "cappe@male.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                NormalizedEmail = "CAPPE@MALE.COM",
                NormalizedUserName = "CAPPE",
                PasswordHash = "AQAAAAEAACcQAAAAEKRLoVQcXq3dPzAGrHmMb1sV02HDmiILMNawgqJLLUQXJCw5bwKjRxd23ISIX+d24g==",
                PhoneNumberConfirmed = false,
                SecurityStamp = "5db147e1-f023-4dd2-8612-3f115a04b6db",
                TwoFactorEnabled = false,
                UserName = "cappe"
            };
            _context.Users.Add(cappe);

            //password: XXXRedhead1!
            var alex = new User
            {
                Id = "bc64fdac-65bf-4e91-b9cf-cdbc6c542e2c",
                AccessFailedCount = 0,
                ConcurrencyStamp = "017b2f4e-84b5-4b42-a5a9-4ea4f1647fb9",
                Email = "alex@male.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                NormalizedEmail = "ALEX@MALE.COM",
                NormalizedUserName = "ALEX",
                PasswordHash = "AQAAAAEAACcQAAAAEOBSLL9MI3bvDbiOVJinWutxLmJFCcrd2rGZc6Qw2F5uPi6K/tfBQKJaODGl8pvf+A==",
                PhoneNumberConfirmed = false,
                SecurityStamp = "2eb6cb9f-4f8b-4968-8ce7-2594f374b45e",
                TwoFactorEnabled = false,
                UserName = "alex"
            };
            _context.Users.Add(alex);

            //password: Password1!
            var nick = new User
            {
                Id = "7e3fc3e1-5049-4567-96a2-a50db094cc3d",
                AccessFailedCount = 0,
                ConcurrencyStamp = "4bfdf30d-294e-479b-ac6f-bf44efac6732",
                Email = "nick@male.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                NormalizedEmail = "NICK@MALE.COM",
                NormalizedUserName = "NICK",
                PasswordHash = "AQAAAAEAACcQAAAAEIB5IOWx9+gUXS+vBCNxtfKzoFEVaF5QCKSEmvdIJERICJVwvIhJw4pJDw2rnXMnLw==",
                PhoneNumberConfirmed = false,
                SecurityStamp = "86373a87-e6d3-4ed1-8322-b0a3d0383bef",
                TwoFactorEnabled = false,
                UserName = "nick"
            };
            _context.Users.Add(nick);

            //password: STHLMlove1!
            var emil = new User
            {
                Id = "08e03d0c-bfb1-437a-a877-1d370bd92cc5",
                AccessFailedCount = 0,
                ConcurrencyStamp = "d860197b-e594-42bf-9422-6a8dfcd811dc",
                Email = "emil@male.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                NormalizedEmail = "EMIL@MALE.COM",
                NormalizedUserName = "EMIL",
                PasswordHash = "AQAAAAEAACcQAAAAEA7iUp/6A4MnDJlrrwhldY0l8zmjI0QIc8eZNk2BiydK59mSTgd/czQYaQ150AwVtA==",
                PhoneNumberConfirmed = false,
                SecurityStamp = "5c6657eb-24ae-4e74-9d59-9b0c03655987",
                TwoFactorEnabled = false,
                UserName = "emil"
            };
            _context.Users.Add(emil);

            //password: Kattungar1!
            var philip = new User
            {
                Id = "b6d5de24-98f5-4e29-9fe8-5419f5140a02",
                AccessFailedCount = 0,
                ConcurrencyStamp = "ea6cba8f-ca15-4b97-a8e0-d9906ae57c7d",
                Email = "philip@male.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                NormalizedEmail = "PHILIP@MALE.COM",
                NormalizedUserName = "PHILIP",
                PasswordHash = "AQAAAAEAACcQAAAAEBf0Z2hB+1rXo5bsWU4poxpficZ/CyK4/STEpN6/wegH00+6bxjzjCnvr8il6ilAVw==",
                PhoneNumberConfirmed = false,
                SecurityStamp = "413732b7-c15c-4419-8f6f-5fdb337ec9fd",
                TwoFactorEnabled = false,
                UserName = "philip"
            };
            _context.Users.Add(philip);
        }
    }
}
