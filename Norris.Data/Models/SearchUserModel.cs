using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Norris.Data.Data.Entities;

namespace Norris.Data.Models
{
    public class SearchUserModel
    {
        public List<User> SearchResult { get; set; }
    }
}
