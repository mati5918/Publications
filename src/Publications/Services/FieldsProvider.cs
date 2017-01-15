using Microsoft.AspNetCore.Mvc.Rendering;
using Publications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Services
{
    public class FieldsProvider
    {
        private ApplicationDbContext context;

        public FieldsProvider(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<SelectListItem> GetFieldsByType(FieldType type)
        {
            return context.PublicationFields.Where(f => f.Type == type)
                .OrderBy(f => f.Name)
                .Select(f => new SelectListItem { Text = f.Name, Value = f.PublicationFieldId.ToString() })             
                .ToList();
        }

        public Dictionary<int, string> GetSelectValues()
        {
            Dictionary<int, string> res = new Dictionary<int, string>();
            var selects = context.PublicationFields.Where(f => f.Type == FieldType.Select).ToList();
            foreach(var select in selects)
            {
                Dictionary<int, string> fieldData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int,string>>(select.FieldData);
                res.Add(select.PublicationFieldId, string.Join(",", fieldData.Values));
            }
            return res;
        }

        public IEnumerable<SelectListItem> GetSelectValues(string name)
        {
            List<SelectListItem> res = new List<SelectListItem>();
            var select = context.PublicationFields.FirstOrDefault(f => f.Name == name);
            if(select != null)
            {
                Dictionary<int, string> fieldData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, string>>(select.FieldData);
                if (fieldData != null)
                {
                    foreach (var item in fieldData)
                    {
                        res.Add(new SelectListItem {Value = item.Key.ToString(), Text = item.Value});
                    }
                }
            }
            return res;
        }

        public string GetSelectValue(string name, int value)
        {
            string res = string.Empty;
            var select = context.PublicationFields.FirstOrDefault(f => f.Name == name);
            if (select != null)
            {
                Dictionary<int, string> fieldData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, string>>(select.FieldData);
                if (fieldData != null)
                {
                    fieldData.TryGetValue(value, out res);
                }
            }
            return res ?? string.Empty;
        }
    }
}
