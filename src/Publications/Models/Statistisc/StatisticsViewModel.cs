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
        public Dictionary<string, int> PublicationsPerKonwledgeBranch { get; set; }
        public Dictionary<string, double> PercentOfPublicationsPerKonwledgeBranch { get; set; }
    }
}
