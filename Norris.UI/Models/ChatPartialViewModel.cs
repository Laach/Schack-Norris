using Norris.Data.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Norris.UI.Models
{
    public class ChatPartialViewModel
    {
        public IEnumerable<ChatMessageDTO> ChatMessages { get; set; }
        public string GameID { get; set; }
    }
}
