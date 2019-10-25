using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Norris.Data.Data.Entities
{
    public class User : IdentityUser
    {
        public List<Friends> Friends { get; set; }
        public List<GameSession> WhiteGameSessions { get; set; }
        public List<GameSession> BlackGameSessions { get; set; }
        public bool IsInLobby { get; set; }
    }
}
