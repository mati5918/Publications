using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.Statistisc
{
    public class AuthorViewModel
    {
        public int AuthorId { get; set; }
        public string FullName { get; set; }
        public AcademicDegree AcademicDegree { get; set; }
    }
}
