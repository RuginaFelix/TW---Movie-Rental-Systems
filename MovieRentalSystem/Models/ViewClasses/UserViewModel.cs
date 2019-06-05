using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentalSystem.Models.ViewClasses
{
    public class UserViewModel
    {
        public string Email { get; set; }

        public string Parola { get; set; }

        public string ConfirmaParola { get; set; }

        public int UserType { get; set; }

    }
}