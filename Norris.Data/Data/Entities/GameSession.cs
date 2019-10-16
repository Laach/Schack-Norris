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
    class GameSession
    {
        [Required]
        string Id;
        [Required]
        List<User> Players;
        [Required]
        GameStatus status;
        [Required]
        BoardModel BoardModel;
        [Required]
        GameLogModel Log;
    }
}
