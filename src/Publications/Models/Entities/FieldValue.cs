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

        public int PublicationId { get; set; }
        public Publication Publication { get; set; }

        public int PublicationFeildId { get; set; }
        public int PublicationFieldId { get; set; }
        public PublicationField PublicationField { get; set; }
    }
}
