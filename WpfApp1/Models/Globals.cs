using System.Collections.Generic;
using WpfApp1.Models;

namespace WpfApp1
{
    class Globals
    {
        public static User LoggedInUser { get; set; }
        public static List<FailedUserLogin> FailedLogins = new List<FailedUserLogin>();
        public static string DBurl = @"data source=.\;initial catalog=db_project2;integrated security=true";
    }
}