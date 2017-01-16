using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Publications.Helpers;
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
            var result = _context.Authors.Select(a => new AuthorViewModel() { FullName = a.FirstName + " " + a.SecondName, AuthorId = a.AuthorId, AcademicDegree = a.AcademicDegree }).ToList();
            result.Add(new AuthorViewModel { FullName = "Brak wyboru", AuthorId = 0 });
            return result;
        }

        public List<BranchOfKnowledge> GetAllBranchesOfKnowledge()
        {
            var result = _context.BranchOfKnowledges.ToList();
            result.Add(new BranchOfKnowledge { BranchOfKnowledgeId = 0, Name = "Brak wyboru" });
            return result;
        }

        public StatisticsViewModel GetStatistics(StatisticsFilter filter)
        {
            if (filter.StartOfTimeAmount == null)
                filter.StartOfTimeAmount = DateTime.MinValue;
            if (filter.EndOfTimeAmount == null)
                filter.EndOfTimeAmount = DateTime.MaxValue;

            if (filter.AuthorId == 0 && filter.KnowledgeBranchId == 0)
                return GetOveralStatistics(filter.StartOfTimeAmount.Value, filter.EndOfTimeAmount.Value);

            if (filter.AuthorId == 0 && filter.KnowledgeBranchId != 0)
                return GetBranchOfKnowledgeStatistics(filter.StartOfTimeAmount.Value, filter.EndOfTimeAmount.Value, filter.KnowledgeBranchId);

            if (filter.AuthorId != 0 && filter.KnowledgeBranchId == 0)
                return GetAuthorStatistics(filter.StartOfTimeAmount.Value, filter.EndOfTimeAmount.Value, filter.AuthorId);

            return GetAuthorInBranchOfKnowledgeStatistics(filter.StartOfTimeAmount.Value, filter.EndOfTimeAmount.Value, filter.AuthorId, filter.KnowledgeBranchId);
        }

        private StatisticsViewModel GetAuthorInBranchOfKnowledgeStatistics(DateTime startOfTimeAmount, DateTime endOfTimeAmount, int authorId, int branchOfKnowledgeId)
        {
            var author = _context.Authors.First(a => a.AuthorId == authorId);

            var branchOfKnowledge = _context.BranchOfKnowledges.First(b => b.BranchOfKnowledgeId == branchOfKnowledgeId);

            var publications = _context.Publications.Where(p =>
                        p.CreationDate.Date >= startOfTimeAmount.Date &&
                        p.CreationDate.Date <= endOfTimeAmount.Date &&
                        p.AuthorPublication.Any(ap => ap.AuthorId == authorId) &&
                        p.BranchOfKnowledgePublication.Any(bp => bp.BranchOfKnowledgeId == branchOfKnowledgeId));

            var allPublicationsCount = _context.Publications.Count();
            if (allPublicationsCount == 0)
                allPublicationsCount = 1;

            var result = new StatisticsViewModel
            {
                AuthorName = author.FirstName + " " + author.SecondName,
                KnowledgeBranchName = branchOfKnowledge.Name,
                PercentOfAllPublications = 100 * publications.Count() / (double)allPublicationsCount,
                PublicationsCount = publications.Count(),
                PublicationsPerKonwledgeBranch = new List<PublicationsPerKnowledgeBranch>(),
                TimeAmount = $"Od {startOfTimeAmount:d} do {endOfTimeAmount:d}"
            };
            if (startOfTimeAmount.Year == DateTime.MinValue.Year && endOfTimeAmount.Year == DateTime.MaxValue.Year)
                result.TimeAmount = null;
            return result;
        }

        private StatisticsViewModel GetAuthorStatistics(DateTime startOfTimeAmount, DateTime endOfTimeAmount, int authorId)
        {
            var author = _context.Authors.First(a => a.AuthorId == authorId);

            var publications = _context.Publications.Where(p =>
                        p.CreationDate.Date >= startOfTimeAmount.Date &&
                        p.CreationDate.Date <= endOfTimeAmount.Date &&
                        p.AuthorPublication.Any(ap => ap.AuthorId == authorId));
            var allPublicationsCount = _context.Publications.Count();
            if (allPublicationsCount == 0)
                allPublicationsCount = 1;

            var result = new StatisticsViewModel()
            {
                AuthorName = author.FirstName + " " + author.SecondName,
                KnowledgeBranchName = null,
                PercentOfAllPublications = 100 * publications.Count() / (double)allPublicationsCount,
                PublicationsCount = publications.Count(),
                PublicationsPerKonwledgeBranch = GetPublicationsPerKnowledgeBranch(publications),
                TimeAmount = $"Od {startOfTimeAmount:d} do {endOfTimeAmount:d}",
            };
            if (startOfTimeAmount.Year == DateTime.MinValue.Year && endOfTimeAmount.Year == DateTime.MaxValue.Year)
                result.TimeAmount = null;
            return result;
        }

        public StatisticsViewModel GetOveralStatistics(DateTime startOfTimeAmount, DateTime endOfTimeAmount)
        {
            var publications = _context.Publications.Where(p =>
            p.CreationDate.Date >= startOfTimeAmount.Date &&
            p.CreationDate.Date <= endOfTimeAmount.Date);
            var allPublicationsCount = _context.Publications.Count();
            if (allPublicationsCount == 0)
                allPublicationsCount = 1;

            var result = new StatisticsViewModel()
            {
                AuthorName = null,
                KnowledgeBranchName = null,
                PercentOfAllPublications = -1,
                PublicationsCount = publications.Count(),
                PublicationsPerKonwledgeBranch = GetPublicationsPerKnowledgeBranch(),
                TimeAmount = $"Od {startOfTimeAmount:d} do {endOfTimeAmount:d}",
            };
            if (startOfTimeAmount.Year == DateTime.MinValue.Year && endOfTimeAmount.Year == DateTime.MaxValue.Year)
                result.TimeAmount = null;
            return result;
        }

        public StatisticsViewModel GetBranchOfKnowledgeStatistics(DateTime startOfTimeAmount, DateTime endOfTimeAmount, int branchOfKnowledgeId)
        {
            var branchOfKnowledge = _context.BranchOfKnowledges.First(b => b.BranchOfKnowledgeId == branchOfKnowledgeId);

            var publications = _context.Publications.Where(p =>
                        p.CreationDate.Date >= startOfTimeAmount.Date &&
                        p.CreationDate.Date <= endOfTimeAmount.Date &&
                        p.BranchOfKnowledgePublication.Any(bp => bp.BranchOfKnowledgeId == branchOfKnowledgeId));

            var allPublicationsCount = _context.Publications.Count();
            if (allPublicationsCount == 0)
                allPublicationsCount = 1;

            var result = new StatisticsViewModel()
            {
                AuthorName = null,
                KnowledgeBranchName = branchOfKnowledge.Name,
                PercentOfAllPublications = 100 * publications.Count() / (double)allPublicationsCount,
                PublicationsCount = publications.Count(),
                PublicationsPerKonwledgeBranch = new List<PublicationsPerKnowledgeBranch>(),
                TimeAmount = $"Od {startOfTimeAmount:d} do {endOfTimeAmount:d}",
            };
            if (startOfTimeAmount.Year == DateTime.MinValue.Year && endOfTimeAmount.Year == DateTime.MaxValue.Year)
                result.TimeAmount = null;
            return result;
        }

        private List<PublicationsPerKnowledgeBranch> GetPublicationsPerKnowledgeBranch(IQueryable<Publication> publications)
        {
            var knowledgeBranches = _context.BranchOfKnowledges;
            var result = new List<PublicationsPerKnowledgeBranch>();
            foreach (var branchOfKnowledge in knowledgeBranches)
            {
                var item = new PublicationsPerKnowledgeBranch
                {
                    KnowledgeBranchName = branchOfKnowledge.Name,
                    PublicationsCount = publications.Count(p => p.BranchOfKnowledgePublication.Any(bp => bp.BranchOfKnowledgeId == branchOfKnowledge.BranchOfKnowledgeId)),
                    PublicationsPercentage = 100 * publications.Count(p => p.BranchOfKnowledgePublication.Any(bp => bp.BranchOfKnowledgeId == branchOfKnowledge.BranchOfKnowledgeId)) / (double)_context.Publications.Count(p => p.BranchOfKnowledgePublication.Any(bp => bp.BranchOfKnowledgeId == branchOfKnowledge.BranchOfKnowledgeId))
                };

                if (item.PublicationsCount != 0)
                    result.Add(item);
            }
            return result;
        }

        private List<PublicationsPerKnowledgeBranch> GetPublicationsPerKnowledgeBranch()
        {
            var knowledgeBranches = _context.BranchOfKnowledges;
            var result = new List<PublicationsPerKnowledgeBranch>();
            var publications = _context.Publications;
            foreach (var branchOfKnowledge in knowledgeBranches)
            {
                var item = new PublicationsPerKnowledgeBranch
                {
                    KnowledgeBranchName = branchOfKnowledge.Name,
                    PublicationsCount = publications.Count(p => p.BranchOfKnowledgePublication.Any(bp => bp.BranchOfKnowledgeId == branchOfKnowledge.BranchOfKnowledgeId)),
                    PublicationsPercentage = 100 * publications.Count(p => p.BranchOfKnowledgePublication.Any(bp => bp.BranchOfKnowledgeId == branchOfKnowledge.BranchOfKnowledgeId)) / (double)_context.Publications.Count()
                };
                if (item.PublicationsCount != 0)
                    result.Add(item);
            }
            return result;
        }

        public string GenerateReport(StatisticsViewModel statistics, ReportTypeEnum fileType)
        {
            var filename = FileFormatHelper.GenerateXlsx(statistics);
            return filename;
        }
    }
}