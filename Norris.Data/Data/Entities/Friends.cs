using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Norris.Data.Data.Entities
{
    public class Friends
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string FriendID { get; set; }
        public User Friend { get; set; }
    }
}
