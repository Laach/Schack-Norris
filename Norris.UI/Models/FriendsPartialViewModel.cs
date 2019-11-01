using Norris.Data.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Norris.UI.Models
{
    public class FriendsPartialViewModel
    {
        public UserFriendsDTO UserFriends {get; set;}
        public IEnumerable<UserActiveGamesDTO> UserGames { get; set; }
        public string ActiveGame { get; set; }
    }
}
