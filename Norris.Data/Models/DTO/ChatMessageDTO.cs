using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Norris.Data.Models.DTO
{
    public class ChatMessageDTO
    {
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
