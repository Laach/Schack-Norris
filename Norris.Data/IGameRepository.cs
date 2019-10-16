using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Norris.Data.Models;
using Norris.Data.Models.Board;

namespace Norris.Data
{
    public interface IGameRepository
    {
        GameStateModel GetGamestate(GameID id);
        UserListModel GetFriendList(int userID);      
        UserListModel GetUserSearchResult(string searchterm);
        ViewUserModel GetUserData(int userID);
        GameStateModel AddNewMove(NewMoveModel newMove);
        GameID AddNewGame(int player1ID, int player2ID);
        bool AddFriend(int currentUserID, int friendUserID);
        UserListModel GetPlayerLobby();
        
    }
}
