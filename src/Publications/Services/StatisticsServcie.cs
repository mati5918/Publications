using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Publications.Models;
using Publications.Models.Entities;
using Publications.Models.Statistisc;
using Publications.Models.ViewModels;

namespace Publications.Services
{
    public class StatisticsServcie
    {
        private readonly ApplicationDbContext _context;

        public StatisticsServcie(ApplicationDbContext context)
        {
            _context = context;
        }


        public List<AuthorViewModel> GetAllAuthors()
        {
            return _context.Authors.Select(a=>new AuthorViewModel() {FullName = a.FirstName+ " "+a.SecondName, AuthorId = a.AuthorId, AcademicDegree = a.AcademicDegree}).ToList(); 
        }

        public List<BranchOfKnowledge> GetAllBranchesOfKnowledge()
        {
            return _context.BranchOfKnowledges.ToList();
        }

        public StatisticsViewModel GetStatistics(StatisticsFilter filter)
        {
            if(filter.AuthorId==null&&filter.KnowledgeBranchId==null)
               return GetOveralStatistics(filter.StartOfTimeAmount, filter.EndOfTimeAmount);

            if(filter.AuthorId == null && filter.KnowledgeBranchId != null)
                return GetBranchOfKnowledgeStatistics(filter.StartOfTimeAmount, filter.EndOfTimeAmount,filter.KnowledgeBranchId.Value);

            if (filter.AuthorId != null && filter.KnowledgeBranchId == null)
                return GetAuthorStatistics(filter.StartOfTimeAmount, filter.EndOfTimeAmount, filter.AuthorId.Value);

            return GetAuthorInBranchOfKnowledgeStatistics(filter.StartOfTimeAmount, filter.EndOfTimeAmount, filter.AuthorId.Value, filter.KnowledgeBranchId.Value);
        }

        private StatisticsViewModel GetAuthorInBranchOfKnowledgeStatistics(DateTime startOfTimeAmount, DateTime endOfTimeAmount, int authorId, int branchOfKnowledgeId)
        {
            var author = _context.Authors.First(a => a.AuthorId == authorId);

            var branchOfKnowledge = _context.BranchOfKnowledges.First(b => b.BranchOfKnowledgeId == branchOfKnowledgeId);

            var publications = _context.Publications.Where(p =>
                        p.CreationDate > startOfTimeAmount &&
                        p.CreationDate < endOfTimeAmount &&
                        p.AuthorPublication.Any(ap => ap.AuthorId == authorId) &&
                        p.BranchOfKnowledgePublication.Any(bp => bp.BranchOfKnowledgeId == branchOfKnowledgeId));

            var allPublicationsCount = _context.Publications.Count();
            if (allPublicationsCount == 0)
                allPublicationsCount = 1;

            return new StatisticsViewModel
            {
                AuthorName = author.FirstName+" "+author.SecondName,
                KnowledgeBranchName =branchOfKnowledge.Name, 
                PercentOfAllPublications = 100*publications.Count()/(double)allPublicationsCount, 
                PublicationsCount = publications.Count(),
                PublicationsPerKonwledgeBranch = null,
                TimeAmount = $"Od {startOfTimeAmount:d} do {endOfTimeAmount:d}",
                PercentOfPublicationsPerKonwledgeBranch = null
            };
        }

        private StatisticsViewModel GetAuthorStatistics(DateTime startOfTimeAmount, DateTime endOfTimeAmount, int authorId)
        {
            var author = _context.Authors.First(a => a.AuthorId == authorId);

            var publications = _context.Publications.Where(p =>
                        p.CreationDate > startOfTimeAmount &&
                        p.CreationDate < endOfTimeAmount &&
                        p.AuthorPublication.Any(ap => ap.AuthorId == authorId));
            var allPublicationsCount = _context.Publications.Count();
            if (allPublicationsCount == 0)
                allPublicationsCount = 1;

            var result = new StatisticsViewModel()
            {
              AuthorName = author.FirstName + " " + author.SecondName,
              KnowledgeBranchName = null,
              PercentOfAllPublications = 100*publications.Count()/(double)allPublicationsCount,  
              PublicationsCount = publications.Count(),  
              PublicationsPerKonwledgeBranch = CalculatePublicationsPerKnowledgeBranch(publications),
              TimeAmount = $"Od {startOfTimeAmount:d} do {endOfTimeAmount:d}",
              PercentOfPublicationsPerKonwledgeBranch = CalculatePublicationsPerKnowledgeBranchPercentage(publications)
            };
            return result;
        }

        public StatisticsViewModel GetOveralStatistics(DateTime startOfTimeAmount, DateTime endOfTimeAmount)
        {
            var publications = _context.Publications.Where(p =>
            p.CreationDate > startOfTimeAmount &&
            p.CreationDate < endOfTimeAmount);
            var allPublicationsCount = _context.Publications.Count();
            if (allPublicationsCount == 0)
                allPublicationsCount = 1;

            var result = new StatisticsViewModel()
            {
                AuthorName = null,
                KnowledgeBranchName = null, 
                PercentOfAllPublications = 100*publications.Count()/(double)allPublicationsCount,
                PublicationsCount = publications.Count(),
                PublicationsPerKonwledgeBranch = CalculatePublicationsPerKnowledgeBranch(publications),
                TimeAmount = $"Od {startOfTimeAmount:d} do {endOfTimeAmount:d}",
                PercentOfPublicationsPerKonwledgeBranch =CalculatePublicationsPerKnowledgeBranchPercentage(publications)

            };
            return result;
        }

        public StatisticsViewModel GetBranchOfKnowledgeStatistics(DateTime startOfTimeAmount, DateTime endOfTimeAmount, int branchOfKnowledgeId)
        {

            var branchOfKnowledge = _context.BranchOfKnowledges.First(b => b.BranchOfKnowledgeId == branchOfKnowledgeId);

            var publications = _context.Publications.Where(p =>
                        p.CreationDate > startOfTimeAmount &&
                        p.CreationDate < endOfTimeAmount &&
                        p.BranchOfKnowledgePublication.Any(bp => bp.BranchOfKnowledgeId == branchOfKnowledgeId));

            var allPublicationsCount = _context.Publications.Count();
            if (allPublicationsCount == 0)
                allPublicationsCount = 1;

            var result = new StatisticsViewModel()
            {
                AuthorName = null,
                KnowledgeBranchName =branchOfKnowledge.Name,
                PercentOfAllPublications = publications.Count()/(double)allPublicationsCount,
                PublicationsCount = publications.Count(),
                PublicationsPerKonwledgeBranch = null,
                TimeAmount = $"Od {startOfTimeAmount:d} do {endOfTimeAmount:d}",
                PercentOfPublicationsPerKonwledgeBranch = null

            };
            return result;
        }

        private Dictionary<string, int> CalculatePublicationsPerKnowledgeBranch(IQueryable<Publication> publications)
        {
            var knowledgeBranches = _context.BranchOfKnowledges;
            var result=new Dictionary<string, int>();
            foreach (var branchOfKnowledge in knowledgeBranches)
            {
                 result.Add(branchOfKnowledge.Name,publications.Count(p=>p.BranchOfKnowledgePublication.Any(bp=>bp.BranchOfKnowledgeId==branchOfKnowledge.BranchOfKnowledgeId)));
            }
            return result;
        }

        private Dictionary<string, double> CalculatePublicationsPerKnowledgeBranchPercentage(IQueryable<Publication> publications)
        {
            var knowledgeBranches = _context.BranchOfKnowledges;
            
            var result = new Dictionary<string, double>();
            foreach (var branchOfKnowledge in knowledgeBranches)
            {
                result.Add(branchOfKnowledge.Name, 100* publications.Count(p => p.BranchOfKnowledgePublication.Any(bp => bp.BranchOfKnowledgeId == branchOfKnowledge.BranchOfKnowledgeId))/ (double)_context.Publications.Count(p => p.BranchOfKnowledgePublication.Any(bp => bp.BranchOfKnowledgeId == branchOfKnowledge.BranchOfKnowledgeId)));
            }
            return result;
        }

        public string GenerateReport(StatisticsViewModel statistics, string fileType)
        {
            return "";
        }
    }
}
