using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Publications.Services;
using Publications.Models.Entities;
using Publications.Models.ViewModels;

namespace Publications.Controllers
{
    public class PublicationController : Controller
    {
        PublicationService publicationService;
        public PublicationController(PublicationService publicationService)
        {
            this.publicationService = publicationService;
        }
        public IActionResult PublicationList()
        {
            var publicationList = publicationService.GetAllPublications();
            return View(publicationList);
        }
        public IActionResult Details(int? publicationId)
        {
            Publication publication = publicationService.GetPublicationById(publicationId);
            PublicationVM publicationVM = publicationService.ParsePublicationToPublicationVM(publication);
            return View(publicationVM);
        }

        public IActionResult FieldValueRow(int templateId)
        {
            IEnumerable<FieldValueVM> fieldValues = publicationService.GenerateNewFieldValue(templateId);
            return PartialView(fieldValues);
        }


        public IActionResult AddPublication()
        {
            return View(new SavePublicationVM());
        }
        public IActionResult ShowTemplatesList()
        {
            return PartialView("TemplatesSelectList", publicationService.GetAllPublicationTemplate());
        }
        [HttpPost]
        public IActionResult Add(SavePublicationVM savePublication)
        {
            bool isDone = publicationService.AddPublication(savePublication);
            if (isDone)
            {
                return Json(new { success = true, message = "Zapisano pomyœlnie!" });
            }
            else
            {
                return Json(new { success = false, message = "Wyst¹pi³ b³¹d w zapisie!" });
            }

        }
    }
}