using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.Entities
{
    public class FieldTemplate
    {
        public int FieldTemplateId { get; set; }
        public int TemplateId { get; set; }
        public int FieldId { get; set; }

        public PublicationTemplate Template { get; set; }
        public PublicationField Field { get; set; }
    }
}
