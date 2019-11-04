using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Norris.Data.Data.Entities;
using Norris.Data;
using Norris.Data.Models;
using Norris.Data.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Norris.Data{
    public static class UserActivity{
        private static Dictionary<string, DateTime> LastActivity = 
        new Dictionary<string, DateTime>();


        public static void RefreshUser(string userid){
        LastActivity[userid] = DateTime.Now;
        }

        public static bool IsOnline(string userid){

        DateTime lastSeen;
        if(!LastActivity.TryGetValue(userid, out lastSeen)){
            return false; // User has never been logged in.
        }

        var timeoutLimit = new TimeSpan(0, 10, 0);
        DateTime limit = DateTime.Now.Subtract(timeoutLimit);

        if(DateTime.Compare(lastSeen, limit) > 0){
            return true;
        }
        return false;

        }
        public static void Logout(string userid)
        {
            LastActivity.Remove(userid);
        }

    }
}