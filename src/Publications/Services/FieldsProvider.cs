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
                .Select(f => new SelectListItem { Text = f.Name, Value = f.PublicationFieldId.ToString() })
                .ToList();
        }
    }
}
