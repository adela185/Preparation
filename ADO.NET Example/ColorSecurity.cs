using ADO.NET_Class_Library_Ex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADO.NET_Example
{
    public class ColorSecurity
    {
        public static bool Login(string username, string password)
        {
            using(TestDBEntities entities = new TestDBEntities())
            {
                return entities.Users.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}