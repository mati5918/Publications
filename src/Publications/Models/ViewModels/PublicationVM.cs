using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.ViewModels
{
    public class PublicationVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
