using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Norris.Data.Models;

namespace Norris.Data
{
    public class GameRepository : IGameRepositroy
    {
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
