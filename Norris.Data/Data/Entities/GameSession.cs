using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Norris.Data.Data.Entities;
using System.ComponentModel.DataAnnotations;
using Norris.Data.Models;

namespace Norris.Data.Data.Entities
{
    public class GameSession
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        public string PlayerWhiteID { get; set; }
        [Required]
        public string PlayerBlackID { get; set; }
        [Required]
        public User PlayerWhite { get; set; }
        [Required]
        public User PlayerBlack { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string Board { get; set; }
        [Required]
        public string Log { get; set; }
        public bool IsWhitePlayerTurn { get; set; }

    }
}
