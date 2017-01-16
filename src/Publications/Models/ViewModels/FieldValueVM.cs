using Microsoft.AspNetCore.Http;
using Publications.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.ViewModels
{
    public class FieldValueVM
    { 
        public int FieldId { get; set; }
        public FieldType FieldType { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool isChecked { get; set; }
    }
    public class FileProperties
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
    public class TemplateFieldValueVM
    {
        public List<PublicationTemplate> publicationTemplates { get; set; }
        public List<FieldValueVM> FieldValuesVM { get; set; }
    }
}
