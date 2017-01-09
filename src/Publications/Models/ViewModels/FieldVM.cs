using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.ViewModels
{
    public class FieldVM
    {
        public int FieldId { get; set; }
        public string Name { get; set; }
        public FieldType Type { get; set; }
        public int AttachId { get; set; }
    }

    public class SaveFieldVM
    {
        public int FieldId { get; set; }
        public int AttachId { get; set; }
    }

    public class AddFieldVM
    {
        public string Name { get; set; }
        public FieldType Type { get; set; }
        public int Id { get; set; }
        public List<string> SelectValues { get; set; }
    }
}
