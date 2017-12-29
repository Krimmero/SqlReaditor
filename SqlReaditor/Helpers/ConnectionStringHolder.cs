using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlReaditor.Helpers
{
    public static class ConnectionStringHolder
    {
        private static string _conString;
        private static string _username;

        public static string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public static string ConString
        {
            get { return _conString; }
            set { _conString = value; }
        }
    }
}
