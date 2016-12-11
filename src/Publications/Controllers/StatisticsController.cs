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

        private readonly StatisticsServcie _service;

        public StatisticsController(StatisticsServcie service)
        {
            _service = service;
        }

        public IActionResult Statistics()
        {
            var statisticsFilter = new StatisticsFilter()
            {
              Authors = _service.GetAllAuthors(),
              BranchesOfKnowledge = _service.GetAllBranchesOfKnowledge()
            };

            return View(statisticsFilter);
        }

        public IActionResult ShowStatistics(StatisticsFilter filter)
        {
            var statistics = _service.GetStatistics(filter);
            return View(statistics);
        }
    }
}