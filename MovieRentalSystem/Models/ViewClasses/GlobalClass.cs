using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentalSystem.Models.ViewClasses
{
    public static class GlobalClass
    {
        private static Guid _globalVar = Guid.Empty;
        private static int _globalCounter = 0;
        private static int USER_TYPE = (int)USER_TYPE_ENUM.ADMIN;
        private static string User_Email = "";

        public static Guid UserId
        {
            get { return _globalVar; }
            set { _globalVar = value; }
        }
        public static int CartCounter
        {
            get { return _globalCounter; }
            set { _globalCounter = value; }
        }
        public static int UserType
        {
            get { return USER_TYPE; }
            set { USER_TYPE = value; }
        }
        public static string UserEmail
        {
            get { return User_Email; }
            set { User_Email = value; }
        }
    }
}