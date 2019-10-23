using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Norris.Data.Models;
using Norris.Data.Models.DTO;
using Norris.Data.Data.Entities;
using Norris.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Norris.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly NContext context;
        public GameRepository(NContext context)
        {
            this.context = context;
        }
        public bool AddFriend(string currentUserID, string friendUserID)
        {
            User user = context.Users
              .Include(u => u.Friends)
              .Where(u => u.Id == currentUserID)
              .FirstOrDefault();
            if (user == null){return false;}
            if (user.Friends.Any(f => f.FriendID == friendUserID)){ return false; }

            User friend = context.Users
              .Include(u => u.Friends)
              .Where(u => u.Id == friendUserID)
              .FirstOrDefault();
            if (friend == null){return false;}

            Friends userfriend = new Friends{
              User = user,
              UserId = currentUserID,
              Friend = friend,
              FriendID = friendUserID,
            };
            Friends frienduser = new Friends{
              User = friend,
              UserId = friendUserID,
              Friend = user,
              FriendID = currentUserID,
            };

            if (user.Friends == null){
              user.Friends = new List<Friends>();
            }
            if(friend.Friends == null){
              friend.Friends = new List<Friends>();
            }
            user.Friends.Add(userfriend);
            friend.Friends.Add(frienduser);
            context.SaveChanges();
            
            return true;
        }

        public string AddNewGame(string playerWhiteID, string playerBlackID)
        {
            
            string DefaultGameState = 
                "br,bn,bb,bq,bk,bb,bn,br," +
                "bp,bp,bp,bp,bp,bp,bp,bp," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "wp,wp,wp,wp,wp,wp,wp,wp," +
                "wr,wn,wb,wq,wk,wb,wn,wr";
            //creates a new GameSession
            GameSession newgame = new GameSession
            {
                Id = Guid.NewGuid().ToString(),
                Board = DefaultGameState,
                PlayerBlackID = playerBlackID,
                PlayerBlack = context.Users
                    .Where(e => e.Id.Equals(playerBlackID))
                    .FirstOrDefault(),
                PlayerWhiteID = playerWhiteID,
                PlayerWhite = context.Users
                    .Where(e => e.Id.Equals(playerWhiteID))
                    .FirstOrDefault(),
                IsActive = true,
                Log = "",
                IsWhitePlayerTurn = true
            };

            //if any of the desired users didn't exist: error
            if (newgame.PlayerWhite == null || newgame.PlayerBlack == null)
            {
                throw new ArgumentException($"One of the PlayerID's was not found");
            }
            //Add the new gamesession to the database
            context.GameSessions.Add(newgame);
            context.SaveChanges();
            return newgame.Id;
        }

        public GameStateDTO AddNewMove(NewMoveDTO newMove)
        {
            //tries to fetch the requested Gameseesion from the Database
            var desiredgame = context.GameSessions
                .Where(u => u.Id == newMove.GameID)
                .FirstOrDefault();
            //if requested Gamesession does not exits: error
            if (desiredgame == null) throw new ArgumentException($"Game {newMove.GameID} not found");
            //converts a string[,] to a string
            string gameboard = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    gameboard = gameboard + newMove.CurrentBoard[i, j] + ",";
                }
            }
            //remove the last ','
            desiredgame.Board = gameboard.Substring(0, gameboard.Length - 1);

            var temp = desiredgame.Log;
            //if it the first move, no leading ',' should be added
            if (desiredgame.Log.Length > 0) { temp.Concat("," + newMove.From + newMove.To); }
            else                     { temp.Concat(newMove.From + newMove.To); }
            desiredgame.Log = temp;
            //Toggle the curent turn
            desiredgame.IsWhitePlayerTurn = desiredgame.IsWhitePlayerTurn ? false : true;

            //Save the changes to the database
            context.GameSessions.Update(desiredgame);
            var successfullUpdate = context.SaveChanges();
            //if no changes was made: error
            if (successfullUpdate == 0) { throw new Exception("Database did not update"); }

            //convert the log from string to List<string>
            var ListLog = desiredgame.Log.Split(',').ToList();
            
            return new GameStateDTO
            {
                Board = newMove.CurrentBoard,
                Log = ListLog,
                ActivePlayerColor = desiredgame.IsWhitePlayerTurn == true ? 'w' : 'b'
            };
        }

        public UserFriendsDTO GetFriendList(string userID)
        {
            var dto = new UserFriendsDTO();
            var user = context.Users
              .Include(u => u.Friends)
              .ThenInclude(f => f.Friend)
              .Where(u => u.Id == userID)
              .FirstOrDefault();
            if (user == null){
              throw new ArgumentException($"User {userID} not found");
            }  

            var friends = user
              ?.Friends
              .Select(f => f.Friend)
              .ToList();

            if(friends == null){
              dto.OnlineFriends = new List<User>();
              dto.OfflineFriends = new List<User>();
            }
            else{
              dto.OnlineFriends = 
                friends
                .Where(u => UserActivity.IsOnline(u.Id))
                .ToList();
              dto.OfflineFriends = 
                friends
                .Where(u => !UserActivity.IsOnline(u.Id))
                .ToList();
            }

            return dto;
        }

        public GameStateDTO GetGamestate(string id)
        {
            //Fetches the desired gamesession
                        GameSession desiredGame = context.GameSessions
                .Where(e => e.Id.Equals(id))
                .FirstOrDefault();
            //If the desired game was not found: error
            if (desiredGame == null) throw new ArgumentException($"Game {id} not found");

            //Coverts a string to a string[,]
            var piecesList = desiredGame.Board.Split(',').ToList();
            var board = new string[8, 8];
            int k = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = piecesList.ElementAt(k++);
                }
            }
            return new GameStateDTO {
                Log = desiredGame.Log.Split(',').ToList(),
                Board = board,
                ActivePlayerColor = desiredGame.IsWhitePlayerTurn == true ? 'w' : 'b'
            };

        }


        public IEnumerable<UserActiveGamesDTO> GetUserGameList(string userID){

            var sessions = context.GameSessions
              .Include(gs => gs.PlayerBlack)
              .Include(gs => gs.PlayerWhite)
              .Where(gs => gs.PlayerWhiteID == userID || gs.PlayerBlackID == userID)
              .ToList();

            if(sessions == null){
              throw new ArgumentException($"User {userID} not found");
            }

            var games = new List<UserActiveGamesDTO>();

            games = sessions.Select(s => new UserActiveGamesDTO{
                GameID = s.Id,
                OpponentName = s.PlayerWhiteID == userID ? s.PlayerBlack.UserName : s.PlayerWhite.UserName,
                IsMyTurn = s.PlayerWhiteID == userID ? s.IsWhitePlayerTurn : !s.IsWhitePlayerTurn
              })
              .ToList();

            return games;
        }

       
        public UserListDTO GetPlayerLobby()
        {
            //Mock data
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

        public ViewUserModel GetUserData(string userID)
        {
            //might be removable later?
            throw new NotImplementedException();
        }

        private static bool IsNotFriend(User other, User me){
          return me.Friends == null ? true : !me.Friends.Any(f => f.FriendID == other.Id);
        }

        public UserListDTO GetUserSearchResult(string userID, string searchterm)
        {
            var user = context.Users.Include(u => u.Friends).Where(u => u.Id == userID).FirstOrDefault();
            if(user == null){return null;}

            string search = searchterm + "%";
            UserListDTO users = new UserListDTO();
            users.Users = context.Users.Where(u => EF.Functions.Like(u.UserName, search) && IsNotFriend(u, user) && u.Id != user.Id).ToList();

            return users;
        }
        public bool IsActivePlayer(string gameID, string UserID)
        {
            var gamesession = context.GameSessions.Where(g => g.Id.Equals(gameID)).FirstOrDefault();
            if(gamesession.PlayerWhiteID == UserID && gamesession.IsWhitePlayerTurn)
            {
                return true;
            } else if(gamesession.PlayerBlackID == UserID && !gamesession.IsWhitePlayerTurn)
            {
                return true;
            }
            return false;
        }
        public char GetPlayerColor(string gameID, string UserID)
        {
            var gamesession = context.GameSessions.Where(g => g.Id.Equals(gameID)).FirstOrDefault();
            if (gamesession.PlayerWhiteID == UserID)
            {
                return 'w';
            }
            else 
            {
                return 'b';
            }
        }
    }
}
