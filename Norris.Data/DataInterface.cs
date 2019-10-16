using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Norris.Data.Models;

namespace Norris.Data
{
    public class DataInterface
    {
        public static GameStateModel GetGamestate(GameID id) => throw new NotImplementedException();
        public static FriendListModel GetFriendList(int userID) => throw new NotImplementedException();
        public static SearchUserModel GetUserSearchResult(string searchterm) => throw new NotImplementedException();
        public static ViewUserModel GetUserData(int userID) => throw new NotImplementedException();
        public static GameStateModel AddNewMove(NewMoveModel newMove) => throw new NotImplementedException();
        public static GameID AddNewGame(int player1ID, int player2ID ) => throw new NotImplementedException();
        public static bool AddFriend(int currentUserID, int friendUserID) => throw new NotImplementedException();
    }
}
