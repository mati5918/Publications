using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Publications.Services;
using Publications.Models.ViewModels;
using Publications.Models;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public IActionResult Add()
        {
            ViewBag.IsNewTemplate = true;
            return View("Details", new TemplateVM { TemplateId = -1, IsActive = true });
        }
        [Authorize]
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
        [Authorize]
        public IActionResult CopyTemplate(int id)
        {
            ViewBag.IsCopy = true;
            ViewBag.IsNewTemplate = true;
            return View("Details", service.CopyTemplate(id));
        }
        [Authorize]
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
        [Authorize]
        [HttpPost]
        public IActionResult Save([FromBody] SaveTemplateVM vm)
        {
            if(!service.IsTemplateNameValid(vm))
            {
                return Json(new { success = false, message = "Nazwa szablonu musi byæ unikalna." });
            }
            bool isSaveSuccess = service.Save(vm);
            if (isSaveSuccess)
            {
                return Json(new { success = true, message = "Zapisano pomyœlnie!", id = vm.TemplateId});
            }
            else
            {
                return Json(new { success = false, message = "Wyst¹pi³ b³¹d w zapisie!" });
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult Remove(int id)
        {
            service.Remove(id);
            return Json("");
        }

        [HttpPost]
        public IActionResult GetSelectFieldValues(int id)
        {
            return Json(service.GetSelectFieldValues(id));
        }
    }
}