using System;
using System.Collections.Generic;
using Publications.Models.Entities;

namespace Publications.Models.Statistisc
{
    public class StatisticsFilter
    {
        public List<AuthorViewModel> Authors { get; set; }
        public List<BranchOfKnowledge> BranchesOfKnowledge { get; set; }
        public DateTime StartOfTimeAmount { get; set; }
        public DateTime EndOfTimeAmount { get; set; }
        public int KnowledgeBranchId { get; set; }
        public int AuthorId { get; set; }
    }
}
