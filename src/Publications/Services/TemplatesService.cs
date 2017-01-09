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
            return context.PublicationTemplates.Include(t => t.Publications).Select(t => MapEntityToVM(t, false));
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
                        Description = vm.Description,
                        IsActive = vm.IsActive
                    };
                    context.PublicationTemplates.Add(templateEntity);
                    context.SaveChanges();
                }
                else
                {
                    templateEntity = context.PublicationTemplates.FirstOrDefault(t => t.PublicationTemplateId == vm.TemplateId);
                    if(templateEntity != null)
                    {
                        templateEntity.Name = vm.Name;
                        templateEntity.IsActive = vm.IsActive;
                        templateEntity.Description = vm.Description;
                        templateEntity.ModifiedDate = DateTime.Now;
                        context.Entry(templateEntity).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                if (templateEntity != null)
                {
                    var newFields = vm.Fields.Where(f => f.AttachId == -1);
                    var oldFields = vm.Fields.Where(f => f.AttachId != -1);
                    var fieldsEnitites = context.FieldsTemplates.Where(ft => ft.TemplateId == vm.TemplateId).ToList();
                    foreach(var field in fieldsEnitites)
                    {
                        SaveFieldVM fieldVm = oldFields.FirstOrDefault(f => f.AttachId == field.FieldTemplateId);
                        if(fieldVm != null)
                        {
                            field.FieldId = fieldVm.FieldId;
                            context.Entry(field).State = EntityState.Modified;
                        }
                        else
                        {
                            context.FieldsTemplates.Remove(field);
                        }
                    }
                    context.SaveChanges();
                    foreach (SaveFieldVM field in newFields) //save new fields
                    {
                        FieldTemplate attachEntity = new FieldTemplate
                        {
                            TemplateId = templateEntity.PublicationTemplateId,
                            FieldId = field.FieldId
                        };
                        context.FieldsTemplates.Add(attachEntity);
                    }
                    context.SaveChanges();
                }
                vm.TemplateId = templateEntity.PublicationTemplateId;
                res = true;
            }
            catch { }

            return res;
        }

        public void Remove(int id)
        {
            PublicationTemplate entity = context.PublicationTemplates.FirstOrDefault(t => t.PublicationTemplateId == id);
            if (entity != null)
            {
                context.PublicationTemplates.Remove(entity);
                context.SaveChanges();
            }
        }

        private TemplateVM MapEntityToVM(PublicationTemplate entity, bool includeFields)
        {
            TemplateVM res = new TemplateVM
            {
                CreationDate = entity.CreationDate,
                Description = entity.Description,
                ModifiedDate = entity.ModifiedDate,
                Name = entity.Name,
                TemplateId = entity.PublicationTemplateId,
                IsActive = entity.IsActive,
                PublicationsCount = entity.Publications.Count()
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
                    .Include(t => t.Publications)
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

        public TemplateVM CopyTemplate(int id)
        {
            TemplateVM res = GetTemplateById(id);
            res.TemplateId = -1;
            res.PublicationsCount = 0;
            foreach(var field in res.Fields)
            {
                field.AttachId = -1;
            }
            return res;
        }

        public int? AddNewField(AddFieldVM vm)
        {
            int? res = null;
            try
            {
                if (vm.Id == -1)
                {
                    PublicationField field = new PublicationField
                    {
                        Name = vm.Name,
                        Type = vm.Type
                    };
                    if (field.Type == FieldType.Select && vm.SelectValues != null)
                    {
                        Dictionary<int, string> values = new Dictionary<int, string>();
                        int i = 0;
                        foreach (string value in vm.SelectValues)
                        {
                            values.Add(i++, value);
                        }
                        field.FieldData = Newtonsoft.Json.JsonConvert.SerializeObject(values);
                    }
                    context.PublicationFields.Add(field);
                    context.SaveChanges();
                    res = field.PublicationFieldId;
                }
                else
                {
                    PublicationField field = context.PublicationFields.FirstOrDefault(f => f.PublicationFieldId == vm.Id);
                    if(field != null)
                    {
                        field.Name = vm.Name;
                        if (field.Type == FieldType.Select && vm.SelectValues != null)
                        {
                            Dictionary<int, string> values = new Dictionary<int, string>();
                            int i = 0;
                            foreach (string value in vm.SelectValues)
                            {
                                values.Add(i++, value);
                            }
                            field.FieldData = Newtonsoft.Json.JsonConvert.SerializeObject(values);
                        }
                        context.Entry(field).State = EntityState.Modified;
                        context.SaveChanges();
                        res = field.PublicationFieldId;
                    }
                }
            }
            catch { }
            return res;
        }

        public bool isFieldNameValid(AddFieldVM vm)
        {
            return !context.PublicationFields.Any(f => f.Type == vm.Type && f.Name.ToLower() == vm.Name.ToLower() && f.PublicationFieldId != vm.Id);
        }

        public bool IsTemplateNameValid(SaveTemplateVM vm)
        {
            return !context.PublicationTemplates.Any(t => t.Name.ToLower() == vm.Name.ToLower() && t.PublicationTemplateId != vm.TemplateId);
        }

        public List<string> GetSelectFieldValues(int id)
        {
            List<string> res = new List<string>();
            var select = context.PublicationFields.FirstOrDefault(f => f.PublicationFieldId == id);
            if(select != null)
            {
                Dictionary<int, string> fieldData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, string>>(select.FieldData);
                res.AddRange(fieldData.Values);
            }
            return res;
        }
    }
}
