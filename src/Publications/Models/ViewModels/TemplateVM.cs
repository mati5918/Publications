using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.ViewModels
{
    public class TemplateVM
    {
        public int TemplateId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PublicationsCount { get; set; }
        public bool IsActive { get; set; }

        public List<FieldVM> Fields { get; set; } = new List<FieldVM>();
    }

    public class SaveTemplateVM
    {
        public int TemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<SaveFieldVM> Fields { get; set; } 
    }

}
