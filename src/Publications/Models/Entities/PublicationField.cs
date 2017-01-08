using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.Entities
{
    public class PublicationField
    {
        public int PublicationFieldId { get; set; }
        public string Name { get; set; }
        public FieldType Type { get; set; }
        public string FieldData { get; set; }

        public List<FieldTemplate> FieldsTemplates { get; set; }
    }
}
