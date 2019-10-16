using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Norris.Data.Models;
using Norris.Data.Data.Entities;

namespace Norris.Data
{
    public class GameRepository : IGameRepositroy
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

        public FriendListModel GetFriendList(int userID)
        {
            return new FriendListModel
            {


            };
        }

        public GameStateModel GetGamestate(GameID id)
        {
            throw new NotImplementedException();
        }

        public ViewUserModel GetUserData(int userID)
        {
            throw new NotImplementedException();
        }

        public SearchUserModel GetUserSearchResult(string searchterm)
        {
            var test = new SearchUserModel
            {
                SearchResult = new List<User>()

            };
            test.SearchResult.Add(new User
            {
                UserName = "TestUser1",
                Id = "1"
            }) ;
            return test;
        }
    }
}
