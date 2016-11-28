using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Publications.Models;
using Publications.Models.Entities;
using Publications.Models.Statistisc;
using Publications.Models.ViewModels;

namespace Publications.Services
{
    public class StatisticsServcie
    {
        private ApplicationDbContext context;

        public StatisticsServcie(ApplicationDbContext context)
        {
            this.context = context;
        }


        public List<AuthorViewModel> GetAllAuthors()
        {
            return new List<AuthorViewModel>()
            {
                new AuthorViewModel()
                {
                    AcademicDegree = AcademicDegree.Bacheler,
                    AuthorId = 1,
                    FullName = "Author Abc",
                }
            };
            //return context.Authors.Select(a=>new AuthorViewModel() {FullName = a.FirstName+ " "+a.SecondName, AuthorId = a.AuthorId, AcademicDegree = a.AcademicDegree}).ToList(); TODO:Add Authors to DB, uncomment
        }

        public List<BranchOfKnowledge> GetAllBranchesOfKnowledge()
        {
            return new List<BranchOfKnowledge>()
            {
                new BranchOfKnowledge()
                {
                    BranchOfKnowledgeId = 1,
                    Name = "IT"
                }
            };
            //return context.BranchOfKnowledges.ToList(); TODO:Add Branches to DB, uncomment
        }
    }
}
