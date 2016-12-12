using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Publications.Services;

namespace Publications.Controllers
{
    public class PublicationController : Controller
    {
        PublicationService service;
        public PublicationController(PublicationService service)
        {
            this.service = service;
        }
        public IActionResult PublicationList()
        {
            var publicationList = service.GetAllPublications();
            return View(publicationList);
        }
    }
}