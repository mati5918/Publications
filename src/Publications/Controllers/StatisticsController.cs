using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Publications.Models.Statistisc;
using Publications.Models;
using Publications.Services;

namespace Publications.Controllers
{
    public class StatisticsController : Controller
    {

        private  StatisticsServcie service;

        public StatisticsController(StatisticsServcie service)
        {
            this.service = service;
        }

        public IActionResult Statistics()
        {
            var statisticsFilter = new StatisticsFilter()
            {
              Authors = service.GetAllAuthors(),
              BranchesOfKnowledge = service.GetAllBranchesOfKnowledge()
            };

            return View(statisticsFilter);
        }
    }
}