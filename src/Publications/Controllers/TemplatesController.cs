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
            return View("Details", new TemplateVM {TemplateId = -1 });
        }

        public IActionResult AddField()
        {
            return PartialView("TemplateRow", new FieldVM() { AttachId = -1});
        }

        public IActionResult GetFieldsByType(FieldType type)
        {
            return PartialView("FieldsList", new FieldVM { FieldId = -1, Type = type });
        }

        public IActionResult Details(int? templateId)
        {
            TemplateVM vm = service.GetTemplateById(templateId);
            ViewBag.IsNewTemplate = false;
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddNewField([FromBody] AddFieldVM vm)
        {
            if (service.isFieldNameValid(vm))
            {
                int? newId = service.AddNewField(vm);
                if (newId.HasValue)
                {
                    return PartialView("TemplateRow", new FieldVM() { AttachId = -1, FieldId = newId.Value, Type = vm.Type });
                }
                else
                {
                    return StatusCode(500, "Wyst¹pi³ b³¹d w zapisie!");
                }
            }
            else
            {
                return StatusCode(500, "Istnieje pole o podanej nazwie.");
            }
            
        }

        [HttpPost]
        public IActionResult Save([FromBody] SaveTemplateVM vm)
        {
            bool isSaveSuccess = service.Save(vm);
            if (isSaveSuccess)
            {
                return Json(new { success = true, message = "Zapisano pomyœlnie!"});
            }
            else
            {
                return Json(new { success = false, message = "Wyst¹pi³ b³¹d w zapisie!" });
            }
        }
    }
}