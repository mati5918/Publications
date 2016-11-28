using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.Entities
{
    public class AuthorPublication
    {
        public int AuthorPublicationId { get; set; }
        public int AuthorId { get; set; }
        public int PublicationId { get; set; }
        public Publication Publication { get; set; }
        public Author Author { get; set; }
    }
}
