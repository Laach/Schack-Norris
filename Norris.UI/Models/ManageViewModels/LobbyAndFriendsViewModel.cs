using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Norris.Data.Data.Entities;

namespace Norris.UI.Models.ManageViewModels
{
    public class LobbyAndFriendsViewModel
    {
        public List<User> CurrentLobbyUsers { get; set; }
        public List<User> Friends { get; set; }

    }
}
