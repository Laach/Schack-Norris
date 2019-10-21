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

        public GameID AddNewGame(int playerWhiteID, int playerBlackID)
        {
            GameSession newgame = new GameSession();
            newgame.Id = Guid.NewGuid().ToString();

            string[,] DefaultGameState = new string[8, 8] {{"br","bn","bb","bq","bk","bb","bn","br"},
                                                           {"bp","bp","bp","bp","bp","bp","bp","bp"},
                                                           {"ee","ee","ee","ee","ee","ee","ee","ee"},
                                                           {"ee","ee","ee","ee","ee","ee","ee","ee"},
                                                           {"ee","ee","ee","ee","ee","ee","ee","ee"},
                                                           {"ee","ee","ee","ee","ee","ee","ee","ee"},
                                                           {"wp","wp","wp","wp","wp","wp","wp","wp"},
                                                           {"wr","wn","wb","wq","wk","wb","wn","wr"}};
            context.GameSessions.Add(newgame);
        }   




            
        }

        public GameStateDTO AddNewMove(NewMoveDTO newMove)
        {
            throw new NotImplementedException();
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

        public GameStateDTO GetGamestate(GameID id)
        {
            GameSession Game = (GameSession)context.GameSessions.Where(e => e.Id.Equals(id));
            var pieces = Game.Board.Split(',').ToList();
            var board = new string[8,8];
            int k = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = pieces.ElementAt(k++);
                }
            }
            return new GameStateDTO {
                Log = Game.Log.Split(',').ToList(),
                Board = board,
                ActivePlayerColor = Game.IsWhitePlayerTurn ? 'w' : 'b'
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
