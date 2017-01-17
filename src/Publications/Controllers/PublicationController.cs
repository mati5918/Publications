using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Publications.Services;
using Publications.Models.Entities;
using Publications.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Publications.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using System.Text;

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

        public IActionResult FieldValueRow(int? publicationId, int? templateId)
        {
            IEnumerable<FieldValueVM> fieldValues;
            if (publicationId != null && publicationId != 0)
            {
                Publication pub = publicationService.GetPublicationById(publicationId);
                fieldValues = publicationService.GetFieldValueVmOfPublication(pub);
            }
            else
            {
                fieldValues = publicationService.GenerateNewFieldValue(templateId);
            }
            return PartialView(fieldValues);
        }
        public IActionResult AuthorRow()
        {
            return PartialView("AuthorRow", new Author());
        }
        public IActionResult BranchRow()
        {
            return PartialView("BranchRow", new BranchOfKnowledge());
        }
        [Authorize]
        public IActionResult AddPublication()
        {
            return View(new SavePublicationVM() { isExist = false });
        }
        [Authorize(Roles = "Admin")]
        public IActionResult EditPublication(int? publicationId)
        {
            SavePublicationVM sp = publicationService.GetSavePublicationVMById(publicationId);
            if (sp != null)
            {
                sp.isExist = true;
                return View("AddPublication", sp);
            }
            else
            {
                return Json("publication: notexist");
            }
        }
        public IActionResult ShowTemplatesList(int publicationId)
        {
            TemplateFieldValueVM tfv = new TemplateFieldValueVM() { publicationId = publicationId, publicationTemplates = publicationService.GetAllPublicationTemplate().ToList() };
            return PartialView("TemplatesSelectList", tfv);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Add(SavePublicationStringVM savePublicationString)
        {
            SavePublicationVM savePublication = new SavePublicationVM
            {
                Id = savePublicationString.publicationId,
                TemplateId = savePublicationString.TemplateId,
                Title = savePublicationString.Title,
                BranchesOfKnowledge = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BranchOfKnowledge>>(savePublicationString.BranchesOfKnowledge),
                Authors = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Author>>(savePublicationString.Authors),
                FieldsValue = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FieldValueVM>>(savePublicationString.FieldsValue)

            }; //TODO move mapping to service (or not move :D)
            foreach (var file in Request.Form.Files)
            {
                if (file.Length > 0)
                {
                    string path = @"PublicationsFiles\" + Guid.NewGuid();
                    string name = file.FileName;
                    FileProperties fileProperties = new FileProperties() { FilePath = path, FileName = name };
                    if (!Directory.Exists("PublicationsFiles"))
                    {
                        Directory.CreateDirectory("PublicationsFiles");
                    }
                    using (FileStream fs = System.IO.File.Create(path))
                    {
                        file.CopyTo(fs);
                    }
                    FieldValueVM fieldValue = new FieldValueVM() { FieldType = FieldType.File, Name = file.Name, Value = Newtonsoft.Json.JsonConvert.SerializeObject(fileProperties), isChecked = false };
                    savePublication.FieldsValue.Add(fieldValue);
                }

            }
            bool isDone;
            if (savePublication.Id == 0)
            {
                isDone = publicationService.AddPublication(savePublication);
            }
            else
            {
                isDone = publicationService.EditPublication(savePublication);
            }
            if (isDone)
            {
                return Json(new { success = true, message = "Zapisano pomyślnie!" });
            }
            else
            {
                return Json(new { success = false, message = "Wystąpił błąd w zapisie!" });
            }

        }
        public FileResult download(int? FieldViewId)
        {
            FileProperties fileProperties = publicationService.GetFileInformationById(FieldViewId);
            string fileName = fileProperties.FileName;
            string filePath = fileProperties.FilePath;
            string fileText = System.IO.File.ReadAllText(filePath);
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
            FileResult fr = File(Encoding.UTF8.GetBytes(fileText), "application/pdf", fileName);
            return fr;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Remove(int? publicationId)
        {
            publicationService.RemovePublication(publicationId);            
            return Json("");
        }
    }
}