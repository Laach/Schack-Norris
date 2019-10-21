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
        GameStateDTO GetGamestate(string id);
        UserListDTO GetFriendList(int userID);      
        UserListDTO GetUserSearchResult(string searchterm);
        ViewUserModel GetUserData(int userID);
        GameStateDTO AddNewMove(NewMoveDTO newMove);
        GameID AddNewGame(int player1ID, int player2ID);
        bool AddFriend(int currentUserID, int friendUserID);
        UserListDTO GetPlayerLobby();
        
    }
}
