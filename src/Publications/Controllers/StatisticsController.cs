using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Publications.Helpers;
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
            ViewBag.Statistics = statistics;
            return View(statistics);
        }

        public IActionResult SaveToMsWord(StatisticsViewModel statistics)
        {
            var filepath = _service.GenerateReport(statistics, ReportTypeEnum.MsWord);

            byte[] filedata = System.IO.File.ReadAllBytes(filepath);


            return File(filedata, "application/x-msdownload", filepath);
        }

        public IActionResult SaveToPdf(StatisticsViewModel statistics)
        {
            var filepath = _service.GenerateReport(statistics, ReportTypeEnum.Pdf);

            byte[] filedata = System.IO.File.ReadAllBytes(filepath);


            return File(filedata, "application/x-msdownload", filepath);
        }

        public IActionResult SaveToExcell(StatisticsViewModel statistics)
        {
            
            var filepath = _service.GenerateReport(statistics, ReportTypeEnum.Xlsx);

            byte[] filedata = System.IO.File.ReadAllBytes(filepath);


            return File(filedata, "application/x-msdownload", filepath);
        }
    }
}