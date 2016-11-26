using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Publications.Services;
using Publications.Models.ViewModels;
using Publications.Models;

namespace Publications.Controllers
{
    public class TemplatesController : Controller
    {
        private TemplatesService service;

        public TemplatesController(TemplatesService service)
        {
            this.service = service;
        }

        public IActionResult Browse()
        {
            return View(service.GetAllTemplates());
        }

        public IActionResult Add()
        {
            ViewBag.IsNewTemplate = true;
            return View("Details", new TemplateVM());
        }

        public IActionResult AddField()
        {
            return PartialView("TemplateRow", new FieldVM());
        }

        public IActionResult GetFieldsByType(FieldType type)
        {
            return PartialView("FieldsList", new FieldVM { PublicationFieldId = -1, Type = type });
        }
    }
}