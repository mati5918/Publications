using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.ViewModels
{
    public class TemplateVM
    {
        public int PublicationTemplateId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<FieldVM> Fields { get; set; } = new List<FieldVM>();
    }
}
