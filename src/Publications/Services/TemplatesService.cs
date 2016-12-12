using Publications.Models;
using Publications.Models.Entities;
using Publications.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Publications.Services
{
    public class TemplatesService
    {
        private ApplicationDbContext context;

        public TemplatesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<TemplateVM> GetAllTemplates()
        {
            return context.PublicationTemplates.Select(t => MapEntityToVM(t, false));
        }

        public bool Save(SaveTemplateVM vm)
        {
            bool res = false;
            try
            {
                PublicationTemplate templateEntity = null;
                if (vm.TemplateId == -1) //save new template
                {
                    templateEntity = new PublicationTemplate
                    {
                        CreationDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Name = vm.Name,
                        Description = vm.Description
                    };
                    context.PublicationTemplates.Add(templateEntity);
                    context.SaveChanges();
                }
                //TODO: modify existing template
                if (templateEntity != null)
                {
                    var newFields = vm.Fields.Where(f => f.AttachId == -1);
                    foreach (SaveFieldVM field in newFields) //save new fields
                    {
                        FieldTemplate attachEntity = new FieldTemplate
                        {
                            TemplateId = templateEntity.PublicationTemplateId,
                            FieldId = field.FieldId
                        };
                        context.FieldsTemplates.Add(attachEntity);
                    }
                    //TODO: mopdify existing fields
                    context.SaveChanges();
                }
                res = true;
            }
            catch { }

            return res;
        }

        private TemplateVM MapEntityToVM(PublicationTemplate entity, bool includeFields)
        {
            TemplateVM res = new TemplateVM
            {
                CreationDate = entity.CreationDate,
                Description = entity.Description,
                ModifiedDate = entity.ModifiedDate,
                Name = entity.Name,
                TemplateId = entity.PublicationTemplateId
            };
            if(includeFields)
            {
                foreach(FieldTemplate field in entity.FieldsTemplates)
                {
                    res.Fields.Add(new FieldVM
                    {
                        AttachId = field.FieldTemplateId,
                        FieldId = field.FieldId,
                        Type = field.Field.Type,
                        Name = field.Field?.Name
                    });
                }
            }
            return res;
        }

        public TemplateVM GetTemplateById(int? id)
        {
            TemplateVM res = new TemplateVM();
            if (id.HasValue)
            {
                PublicationTemplate templateEntity = context.PublicationTemplates
                    .Include(t => t.FieldsTemplates)
                        .ThenInclude(ft => ft.Field)
                    .FirstOrDefault(t => t.PublicationTemplateId == id.Value);
                if (templateEntity != null)
                {
                    res = MapEntityToVM(templateEntity, true);
                }
            }
            return res;
        }

        public int? AddNewField(AddFieldVM vm)
        {
            int? res = null;
            try
            {
                PublicationField field = new PublicationField
                {
                    Name = vm.Name,
                    Type = vm.Type
                };
                context.PublicationFields.Add(field);
                context.SaveChanges();
                res = field.PublicationFieldId;
            }
            catch { }
            return res;
        }

        public bool isFieldNameValid(AddFieldVM vm)
        {
            return !context.PublicationFields.Any(f => f.Type == vm.Type && f.Name.ToLower() == vm.Name.ToLower());
        }
    }
}
