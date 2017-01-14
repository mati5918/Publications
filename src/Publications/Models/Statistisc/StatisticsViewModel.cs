using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models.Statistisc
{
    public class StatisticsViewModel
    {
        public string AuthorName { get; set; }
        public string KnowledgeBranchName { get; set; }
        public string TimeAmount { get; set; }
        public int PublicationsCount { get; set; }
        public double PercentOfAllPublications { get; set; }
        public List<PublicationsPerKnowledgeBranch> PublicationsPerKonwledgeBranch { get; set; }
        
    }

    public class PublicationsPerKnowledgeBranch
    {
        public string KnowledgeBranchName { get; set; }
        public int PublicationsCount { get; set; }
        public double PublicationsPercentage { get; set; }
    }
}
