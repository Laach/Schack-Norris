using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Norris.Data.Models;
using Norris.Data.Models.DTO;
using Norris.Data.Data.Entities;
using Norris.Data.Data;

namespace Norris.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly NContext context;
        public GameRepository(NContext context)
        {
            this.context = context;
        }
        public bool AddFriend(int currentUserID, int friendUserID)
        {
            throw new NotImplementedException();
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

        public UserListDTO GetFriendList(int userID)
        {
            var test = new UserListDTO
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

        public ViewUserModel GetUserData(int userID)
        {
            throw new NotImplementedException();
        }

        public UserListDTO GetUserSearchResult(string searchterm)
        {
            var test = new UserListDTO
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
