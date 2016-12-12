using Publications.Models;
using Publications.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Services
{
    public class FieldValueService
    {
        private ApplicationDbContext context;

        public FieldValueService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddFieldValue(FieldValue fieldValue) {
            context.Add(fieldValue);
        }

    }
}
