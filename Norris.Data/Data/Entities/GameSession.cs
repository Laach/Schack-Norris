using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Norris.Data.Models.Board.Enums;
using Norris.Data.Data.Entities;
using Norris.Data.Models.Board;
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
        public string Player1ID { get; set; }
        [Required]
        public string Player2ID { get; set; }
        [Required]
        public User Player1 { get; set; }
        [Required]
        public User Player2 { get; set; }
        [Required]
        public GameStatus Status { get; set; }
        [Required]
        public string Board { get; set; }
        [Required]
        public string Log { get; set; }
    }
}
