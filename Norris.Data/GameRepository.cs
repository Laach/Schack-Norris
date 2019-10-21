using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Norris.Data.Models;
using Norris.Data.Models.DTO;
using Norris.Data.Data.Entities;
using Norris.Data.Data;

namespace Norris.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly NContext context;
        public GameRepository(NContext context)
        {
            this.context = context;
        }
        public bool AddFriend(int currentUserID, int friendUserID)
        {
            
            throw new NotImplementedException();
        }

        public GameID AddNewGame(int player1ID, int player2ID)
        {
            throw new NotImplementedException();
        }

        public GameStateDTO AddNewMove(NewMoveDTO newMove)
        {
            throw new NotImplementedException();
        }

        public UserListDTO GetFriendList(int userID)
        {
            var test = new UserListDTO
            {
               Users = new List<User>()

            };
            test.Users.Add(new User
            {
                UserName = "FriendsUser1",
                Id = "1"
            });
            return test;
        }

        public GameStateDTO GetGamestate(GameID id)
        {
            throw new NotImplementedException();
        }

        public UserListDTO GetPlayerLobby()
        {
            var test = new UserListDTO
            {
                Users = new List<User>()

            };
            test.Users.Add(new User
            {
                UserName = "SlUtsUckEr69",
                Id = "1"
            });
            test.Users.Add(new User
            {
                UserName = "DucKLoVer420",
                Id = "3"
            });
            return test;
        }

        public ViewUserModel GetUserData(int userID)
        {
            throw new NotImplementedException();
        }

        public UserListDTO GetUserSearchResult(string searchterm)
        {
            var test = new UserListDTO
            {
                Users = new List<User>()

            };
            test.Users.Add(new User
            {
                UserName = "TestUser1",
                Id = "1"
            }) ;
            return test;
        }
    }
}
