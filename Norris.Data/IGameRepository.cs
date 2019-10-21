using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Norris.Data.Models;
using Norris.Data.Models.DTO;

namespace Norris.Data
{
    public interface IGameRepository
    {
        GameStateDTO GetGamestate(GameID id);
        UserListDTO GetFriendList(int userID);      
        UserListDTO GetUserSearchResult(string searchterm);
        ViewUserModel GetUserData(int userID);
        GameStateDTO AddNewMove(NewMoveDTO newMove);
        string AddNewGame(string player1ID, string player2ID);
        bool AddFriend(int currentUserID, int friendUserID);
        UserListDTO GetPlayerLobby();
        
    }
}
