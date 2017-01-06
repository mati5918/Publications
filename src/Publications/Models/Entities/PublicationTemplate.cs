using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.Entities
{
    public class PublicationTemplate
    {
        public int PublicationTemplateId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public List<FieldTemplate> FieldsTemplates { get; set; }
        public List<Publication> Publications { get; set; }
    }
}
