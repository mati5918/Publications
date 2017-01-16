using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Publications.Models.Entities;

namespace Publications.Models.Statistisc
{
    public class StatisticsFilter
    {
        public List<AuthorViewModel> Authors { get; set; }
        public List<BranchOfKnowledge> BranchesOfKnowledge { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartOfTimeAmount { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndOfTimeAmount { get; set; }

        public int KnowledgeBranchId { get; set; }
        public int AuthorId { get; set; }
    }
}