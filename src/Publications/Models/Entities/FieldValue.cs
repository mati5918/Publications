using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.Entities
{
    public class FieldValue
    {
        public int FieldValueId { get; set; }
        public string Value { get; set; }
        public List<Publication> Publications { get; set; }
        public List<PublicationField> PublicationFields { get; set; }
    }
}
