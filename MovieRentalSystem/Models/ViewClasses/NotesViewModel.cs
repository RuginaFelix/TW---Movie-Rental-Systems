using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentalSystem.Models.ViewClasses
{
    public class NotesViewModel
    {
        public NotesViewModel()
        {

            Id = Guid.Empty;
            AuthorId = Guid.Empty;
            ReciverId = Guid.Empty;
            AuthorName = "";
            ReciverName = "";
            Mesaj = "";

        }

        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }

        public Guid ReciverId { get; set; }

        public string AuthorName { get; set; }

        public string ReciverName { get; set; }

        public string Mesaj { get; set; }

    }
}