using Microsoft.AspNetCore.Mvc;
using Publications.Models.Statistisc;
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

        public IActionResult GenerateReport(StatisticsViewModel statistics, string fileType)
        {
            var filepath=_service.GenerateReport(statistics, fileType);
          
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);


            return File(filedata, "application/x-msdownload", filepath);
        }
    }
}