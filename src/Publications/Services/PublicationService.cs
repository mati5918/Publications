using Publications.Models;
using Publications.Models.Entities;
using Publications.Models.ViewModels;
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
        public Publication GetPublicationById(int? id)
        {
            try
            {
                return (from p in db.Publications where p.PublicationId == id select p).ToList()[0];
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<PublicationVM> GetAllPublications()
        {
            List<Publication> publications = db.Publications.ToList();
            List<PublicationVM> publicationsVM = new List<PublicationVM>();
            foreach (Publication item in publications)
            {
                publicationsVM.Add(new PublicationVM()
                {
                    Authors = GetAllAuthorsOfPublication(item.PublicationId),
                    Id = item.PublicationId,
                    Type = GetTypeOfPublication(item.PublicationId),
                    CreationDate = GetCreationDateFromPublication(item.PublicationId),
                    Title = GetTitleOfPublication(item.PublicationId)
                });
            }
            return publicationsVM;
        }
        private string GetAuthorById(int id)
        {
            List<Author> authors = (from author in db.Authors where author.AuthorId == id select author).ToList();
            return authors[0].FirstName + " " + authors[0].SecondName;
        }
        private string GetTypeOfPublication(int publicationId)
        {
            List<Publication> publications = (from publication in db.Publications where publication.PublicationId == publicationId select publication).ToList();
            return GetPublicationTemplateByTemplateId(publications[0].TemplateId).Name;
        }
        private DateTime GetCreationDateFromPublication(int id)
        {
            List<Publication> publications = (from pub in db.Publications where pub.PublicationId == id select pub).ToList();
            return publications[0].CreationDate;
        }
        private List<string> GetAllAuthorsOfPublication(int publicationId)
        {
            List<AuthorPublication> AuthorPublications = (from ap in db.AuthorPublications where ap.PublicationId == publicationId select ap).ToList();
            List<string> authors = new List<string>();
            foreach (AuthorPublication item in AuthorPublications)
            {
                authors.Add(GetAuthorById(item.AuthorId));
            }
            return authors;            
        }
        private PublicationTemplate GetPublicationTemplateByTemplateId(int id)
        {
            return (from temp in db.PublicationTemplates where temp.PublicationTemplateId == id select temp).ToList()[0];
        }
        private string GetTitleOfPublication(int publicationId)
        {
            return (from publication in db.Publications where publication.PublicationId == publicationId select publication).ToList()[0].Title;
        }
        private IEnumerable<FieldValueVM> GetFieldValueVMFromPublication(Publication publication)
        {
            int id = publication.PublicationId;
            List<FieldValue> fieldValue = (from fv in db.FieldValues join p in db.Publications on fv.PublicationId equals p.PublicationId where p.PublicationId == id select fv).ToList();
            return null;
        }
        private string GetTypeOfPublication(Publication publication)
        {
            List<PublicationTemplate> pt = (from t in db.PublicationTemplates where t.PublicationTemplateId == publication.TemplateId select t).ToList();
            if (pt.Count == 0)
            {
                return "";
            }
            else
            {
                return pt[0].Name;
            }
        }
        private PublicationField findFieldByName(string name)
        {
            foreach (PublicationField item in db.PublicationFields)
            {
                if (item.Name.Equals(name))
                {
                    return item;
                }
            }
            return null;
        }
        public  IEnumerable<PublicationTemplate> GetAllPublicationTemplate()
        {
            return db.PublicationTemplates;
        }
        public bool AddPublication(SavePublicationVM savePublication)
        {
            try
            {
                Publication pub = new Publication() { CreationDate = DateTime.Now, Title = savePublication.Title, TemplateId = savePublication.TemplateId };
                List<FieldValue> filedsValue = new List<FieldValue>();
                foreach (FieldValueVM item in savePublication.FieldsValue)
                {
                    PublicationField publicationField = findFieldByName(item.Name);
                    db.FieldValues.Add(new FieldValue() { Publication = pub, Value = item.Value, PublicationField = publicationField });
                }
                db.Publications.Add(pub);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public PublicationVM ParsePublicationToPublicationVM(Publication publication)
        {
            if (publication != null)
            {
                return new PublicationVM() { Id = publication.PublicationId, Title = publication.Title, Type = GetTypeOfPublication(publication), CreationDate = publication.CreationDate, FieldValues = new List<FieldValueVM>() };
            }
            else
            {
                return new PublicationVM();
            }
        }

    }
}
