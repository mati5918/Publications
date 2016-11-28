using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.Entities
{
    public class BranchOfKnowledgePublication
    {
        public int BranchOfKnowledgePublicationId { get; set; }
        public int PublicationId { get; set; }
        public int BranchOfKnowledgeId { get; set; }

        public BranchOfKnowledge BranchOfKnowledge { get; set; }
        public Publication Publication { get; set; }
    }
}
