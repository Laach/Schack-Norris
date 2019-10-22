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
            User user = context.Users.Where(u => u.Id == currentUserID).FirstOrDefault();
            if (user == null){return false;}

            User friend = context.Users.Where(u => u.Id == friendUserID).FirstOrDefault();
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

            GameSession newgame = new GameSession
            {
                Id = Guid.NewGuid().ToString(),
                Board = DefaultGameState,
                PlayerBlackID = playerBlackID,
                PlayerBlack = context.Users.Where(e => e.Id.Equals(playerBlackID)).FirstOrDefault(),
                PlayerWhite = context.Users.Where(e => e.Id.Equals(playerWhiteID)).FirstOrDefault(),
                PlayerWhiteID = playerWhiteID,
                IsActive = true,
                Log = "",
                IsWhitePlayerTurn = true
            };
            context.GameSessions.Add(newgame);
            context.SaveChanges();
            return newgame.Id;
        }







        public GameStateDTO AddNewMove(NewMoveDTO newMove)
        {
            return default;
        }

        public UserListDTO GetFriendList(string userID)
        {
            var test = new UserListDTO();
            test.Users = context.Users.Where(u => u.Id == userID).FirstOrDefault()?.Friends.Select(f => f.Friend).ToList();
            if(test.Users == null){
              test.Users = new List<User>();
            }
            return test;
        }

        public GameStateDTO GetGamestate(string id)
        {
            //GameSession Game = (GameSession)context.GameSessions.Where(e => e.Id.Equals(id));
            string DefaultGameState = 
                "br,bn,bb,bq,bk,bb,bn,br," +
                "bp,bp,bp,bp,bp,bp,bp,bp," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "ee,ee,ee,ee,ee,ee,ee,ee," +
                "wp,wp,wp,wp,wp,wp,wp,wp," +
                "wr,wn,wb,wq,wk,wb,wn,wr";

            //var pieces = Game.Board.Split(',').ToList();
            var pieces = DefaultGameState.Split(',').ToList();

            var board = new string[8, 8];
            int k = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = pieces.ElementAt(k++);
                }
            }
            return new GameStateDTO {
                //Log = Game.Log.Split(',').ToList(),
                Log = new List<string>(),
                Board = board,
                ActivePlayerColor = 'W'
            };

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

        public ViewUserModel GetUserData(string userID)
        {
            throw new NotImplementedException();
        }

        private static bool IsNotFriend(User other, User me){
          return other.Id != me.Id && me.Friends == null ? true : !me.Friends.Any(f => f.FriendID == other.Id);
        }

        public UserListDTO GetUserSearchResult(string userID, string searchterm)
        {
            var user = context.Users.Include(u => u.Friends).Where(u => u.Id == userID).FirstOrDefault();
            if(user == null){return null;}

            string search = "%" + searchterm + "%";
            UserListDTO users = new UserListDTO();
            users.Users = context.Users.Where(u => EF.Functions.Like(u.UserName, search) && IsNotFriend(u, user)).ToList();

            return users;
        }
    }
}
