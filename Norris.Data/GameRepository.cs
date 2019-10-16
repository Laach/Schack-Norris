using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Norris.Data.Models;
using Norris.Data.Data.Entities;

namespace Norris.Data
{
    public class GameRepository : IGameRepository
    {
        /*private readonly AppDbContext context;
        public GameRepository(AppDBContext context)
        {
          this.context = context;    
        }*/
        public bool AddFriend(int currentUserID, int friendUserID)
        {
            throw new NotImplementedException();
        }

        public GameID AddNewGame(int player1ID, int player2ID)
        {
            throw new NotImplementedException();
        }

        public GameStateModel AddNewMove(NewMoveModel newMove)
        {
            throw new NotImplementedException();
        }

        public UserListModel GetFriendList(int userID)
        {
            var test = new UserListModel
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

        public GameStateModel GetGamestate(GameID id)
        {
            throw new NotImplementedException();
        }

        public UserListModel GetPlayerLobby()
        {
            var test = new UserListModel
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

        public UserListModel GetUserSearchResult(string searchterm)
        {
            var test = new UserListModel
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
