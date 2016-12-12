using Publications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Services
{
    public class PublicationService
    {
        private ApplicationDbContext db;

        public PublicationService(ApplicationDbContext context)
        {
            db = context;
        }





    }
}
