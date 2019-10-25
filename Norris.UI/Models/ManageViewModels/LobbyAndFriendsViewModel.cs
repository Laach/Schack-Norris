using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Norris.Data.Data.Entities;
using Norris.Data.Models.DTO;

namespace Norris.UI.Models.ManageViewModels
{
    public class LobbyAndFriendsViewModel
    {
        public List<User> CurrentLobbyUsers { get; set; }
        public FriendsPartialViewModel Friends { get; set; }

    }
}
