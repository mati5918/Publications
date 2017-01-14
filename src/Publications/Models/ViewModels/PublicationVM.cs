using Publications.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.ViewModels
{
    public class PublicationVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public List<string> BrachesOfKnowledge { get; set; }
        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
        public List<FieldValueVM> FieldValues { get; set; }
    }

    public class SavePublicationVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TemplateId { get; set; }
        public List<FieldValueVM> FieldsValue{ get; set; } 
        public List<Author> Authors { get; set; }
        public List<BranchOfKnowledge> BranchesOfKnowledge { get; set; }
    }
}
