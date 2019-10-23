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
        //Expects a gameid and returns the boardstate,a log and the color the of the active player
        GameStateDTO GetGamestate(string id);
        UserListDTO GetFriendList(string userID);      
        UserListDTO GetUserSearchResult(string searchterm);
        ViewUserModel GetUserData(string userID);
        GameStateDTO AddNewMove(NewMoveDTO newMove);
        string AddNewGame(string player1ID, string player2ID);
        bool AddFriend(string currentUserID, string friendUserID);
        UserListDTO GetPlayerLobby();
        bool IsActivePlayer(string gameID, string userID);
        
    }
}
