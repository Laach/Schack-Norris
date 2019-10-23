using System.Collections.Generic;
using Norris.Data.Data.Entities;

namespace Norris.Data.Models.DTO{
  public class UserFriendsDTO{
    public IEnumerable<User> OnlineFriends  {get; set;}
    public IEnumerable<User> OfflineFriends {get; set;}

    public UserFriendsDTO(){
      OnlineFriends = new List<User>();
      OfflineFriends = new List<User>();
    }
  }
}