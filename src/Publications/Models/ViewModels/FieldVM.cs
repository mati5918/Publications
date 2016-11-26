using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.ViewModels
{
    public class FieldVM
    {
        public int PublicationFieldId { get; set; }
        public string Name { get; set; }
        public FieldType Type { get; set; }
    }
}
