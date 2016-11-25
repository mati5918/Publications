using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Publications.Services;

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
    }
}