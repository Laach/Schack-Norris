using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Norris.Data.Data.Entities
{
    public class ChatMessage
    {
        [Key]
        public int MessageId { get; set; }
        [Required]
        public string  GameSessionID { get; set; }
        public string Username { get; set; }
        public string  Message { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
