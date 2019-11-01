
using Microsoft.AspNetCore.Mvc;

namespace Norris.UI.Models{
  public class SidebarModel{
    public string ActiveGames    {get; set;}
    public string OnlineFriends  {get; set;}
    public string OfflineFriends {get; set;}
  }
}