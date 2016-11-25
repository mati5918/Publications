using Publications.Models;
using Publications.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Services
{
    public class TemplatesService
    {
        private ApplicationDbContext context;

        public TemplatesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<TemplateGeneralVM> GetAllTemplates()
        {
            return context.PublicationTemplates.Select(t => new TemplateGeneralVM
            {
                CreationDate = t.CreationDate,
                Description = t.Description,
                ModifiedDate = t.ModifiedDate,
                Name = t.Name,
                PublicationTemplateId = t.PublicationTemplateId
            });
        }
    }
}
