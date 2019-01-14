using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentalSystem.Models.ViewClasses
{
    public class MovieViewClass
    {
        public Guid Id { get; set; }

        public string PozaURL { get; set; }

        public string Nume { get; set; }

        public int IsRented { get; set; }

        public Guid RenterId { get; set; }

    }
}