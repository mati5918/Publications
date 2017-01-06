using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Publications.Models;
using Publications.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models
{

    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if(context.PublicationFields.Count() == 0)
                {
                    context.PublicationFields.Add(new PublicationField
                    {
                        Type = FieldType.String,
                        Name = "Test1"
                    });
                    context.PublicationFields.Add(new PublicationField
                    {
                        Type = FieldType.String,
                        Name = "Test2"
                    });
                    context.PublicationFields.Add(new PublicationField
                    {
                        Type = FieldType.Number,
                        Name = "Liczba stron"
                    });
                    context.PublicationFields.Add(new PublicationField
                    {
                        Type = FieldType.Number,
                        Name = "Czas trwania"
                    });

                    context.SaveChanges();
                }
                else
                {
                    //context.PublicationFields.RemoveRange(context.PublicationFields);
                    //context.SaveChanges();
                }
                //context.PublicationTemplates.RemoveRange(context.PublicationTemplates);
                //context.SaveChanges();
            }
        }
    }
}
