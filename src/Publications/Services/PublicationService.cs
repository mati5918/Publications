using Publications.Models;
using Publications.Models.Entities;
using Publications.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return authors[0].AcademicDegree.ToString() + " " + authors[0].FirstName + " " + authors[0].SecondName;
        }
        private IEnumerable<string> GetAuthorsOfPublication(int? publicationId)
        {
            List<string> temp = new List<string>();
            List<Author> authors = (from p in db.Publications join pa in db.AuthorPublications on p.PublicationId equals pa.PublicationId join a in db.Authors on pa.AuthorId equals a.AuthorId where p.PublicationId == publicationId select a).ToList();
            foreach (Author item in authors)
            {
                StringBuilder author = new StringBuilder();
                author.Append(item.AcademicDegree.ToString());
                author.Append(" ");
                author.Append(item.FirstName);
                author.Append(" ");
                author.Append(item.SecondName);
                temp.Add(author.ToString());
            }
            return temp;
        }
        private List<FieldValueVM> GetFieldValueInPublication(int? publicationId)
        {
            List<FieldValue> FieldValues = (from p in db.Publications join fv in db.FieldValues on p.PublicationId equals fv.PublicationId where p.PublicationId == publicationId select fv).ToList();
            List<FieldValueVM> temp = new List<FieldValueVM>();
            foreach (FieldValue item in FieldValues)
            {
                temp.Add(new FieldValueVM() { Value = item.Value, Name = GetNameOfFieldValue(item), FieldType = GetTypeOfFieldValue(item), FieldId = item.FieldValueId });
            }
            return temp;
        }
        private string GetNameOfFieldValue(FieldValue fieldValue)
        {
            return (from fv in db.FieldValues join f in db.PublicationFields on fv.PublicationFieldId equals f.PublicationFieldId where fv.FieldValueId == fieldValue.FieldValueId select f).ToList()[0].Name;
        }
        private FieldType GetTypeOfFieldValue(FieldValue fieldValue)
        {
            return (from fv in db.FieldValues join f in db.PublicationFields on fv.PublicationFieldId equals f.PublicationFieldId where fv.FieldValueId == fieldValue.FieldValueId select f).ToList()[0].Type;
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
                    db.FieldValues.Add(new FieldValue() { Publication = pub, Value = item.FieldType != FieldType.Boolean?item.Value:item.isChecked.ToString(), PublicationField = publicationField });
                }
                db.Publications.Add(pub);
                foreach (var item in savePublication.Authors)
                {
                    if (!IsAuthorExist(item))
                    {
                        db.Authors.Add(item);
                        db.AuthorPublications.Add(new AuthorPublication() { Author = item, Publication = pub });
                    }
                    else
                    {
                        db.AuthorPublications.Add(new AuthorPublication() { Author = GetTheSameAuthor(item), Publication = pub });
                    }
                }
                foreach (var item in savePublication.BranchesOfKnowledge)
                {
                    if (!IsBranchExist(item))
                    {
                        db.BranchOfKnowledges.Add(item);
                        db.BranchOfKnowledgePublications.Add(new BranchOfKnowledgePublication() { BranchOfKnowledge = item, Publication = pub });
                    }
                    else
                    {
                        db.BranchOfKnowledgePublications.Add(new BranchOfKnowledgePublication() { BranchOfKnowledge = GetTheSameBranch(item), Publication = pub });
                    }
                }
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
                return new PublicationVM() { Id = publication.PublicationId, Title = publication.Title, Type = GetTypeOfPublication(publication), CreationDate = publication.CreationDate, FieldValues = GetFieldValueInPublication(publication.PublicationId), Authors = GetAllAuthorsOfPublication(publication.PublicationId) as List<string>, BrachesOfKnowledge = GetBranchesFromPublication(publication.PublicationId) };
            }
            else
            {
                return new PublicationVM();
            }
        }
        private List<string> GetBranchesFromPublication(int? publicationId)
        {
            List<string> temp = new List<string>();
            List<BranchOfKnowledge> branches = (from b in db.BranchOfKnowledges join bp in db.BranchOfKnowledgePublications on b.BranchOfKnowledgeId equals bp.BranchOfKnowledgeId join p in db.Publications on bp.PublicationId equals p.PublicationId where p.PublicationId == publicationId select b).ToList();
            foreach (BranchOfKnowledge item in branches)
            {
                temp.Add(item.Name);
            }
            return temp;
        }
        public List<FieldValueVM> GenerateNewFieldValue(int? templateId)
        {
            List<FieldValueVM> temp = new List<FieldValueVM>();
            List<PublicationField> fields = (from t in db.PublicationTemplates join tf in db.FieldsTemplates on t.PublicationTemplateId equals tf.TemplateId join f in db.PublicationFields on tf.FieldId equals f.PublicationFieldId where t.PublicationTemplateId == templateId select f).ToList();
            foreach (PublicationField item in fields)
            {
                temp.Add(new FieldValueVM() { FieldType = item.Type, Name = item.Name, Value = "" });
            }
            return temp;
        }
        private bool IsAuthorExist(Author author)
        {
            return (from a in db.Authors where a.AcademicDegree == author.AcademicDegree && a.FirstName == author.FirstName && a.SecondName == author.SecondName select a).ToList().Count != 0 ? true : false;
        }
        private bool IsBranchExist(BranchOfKnowledge branch)
        {
            return (from b in db.BranchOfKnowledges where b.Name == branch.Name select b).ToList().Count != 0 ? true : false;
        }
        private Author GetTheSameAuthor(Author author)
        {
            return (from a in db.Authors where a.AcademicDegree == author.AcademicDegree && a.FirstName == author.FirstName && a.SecondName == author.SecondName select a).ToList()[0];
        }
        private BranchOfKnowledge GetTheSameBranch(BranchOfKnowledge branch)
        {
            return (from b in db.BranchOfKnowledges where b.Name == branch.Name select b).ToList()[0];
        }
        public FileProperties GetFileInformationById(int? fieldValueId)
        {
            try
            {
                FieldValue fv = (from f in db.FieldValues where f.FieldValueId == fieldValueId select f).ToList()[0];
                return Newtonsoft.Json.JsonConvert.DeserializeObject<FileProperties>(fv.Value);
            }
            catch (Exception e)
            {
                return null;
            }
            
        }
        public bool RemovePublication(int? id)
        {
            try
            {
                Publication pub = (from p in db.Publications where p.PublicationId == id select p).ToList()[0];
                db.Publications.Remove(pub);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public SavePublicationVM GetSavePublicationVMById(int? id)
        {
            try
            {
                Publication pub = (from p in db.Publications where p.PublicationId == id select p).ToList()[0];
                return ParsePublicationToSavePublicationVM(pub);
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        private SavePublicationVM ParsePublicationToSavePublicationVM(Publication pub)
        {
            try
            {
                List<Author> a = GetAuthorsOfPublication(pub.PublicationId);
                List<BranchOfKnowledge> b = GetBranchesOfPublication(pub.PublicationId);
                return new SavePublicationVM() { Id = pub.PublicationId, TemplateId = pub.TemplateId, Title = pub.Title, Authors = a, BranchesOfKnowledge = b};
            }
            catch (Exception e)
            {
                return new SavePublicationVM();
            }
        }
        private List<Author> GetAuthorsOfPublication(int publicationId)
        {
            List<Author> authors = (from p in db.Publications join pa in db.AuthorPublications on p.PublicationId equals pa.PublicationId join a in db.Authors on pa.AuthorId equals a.AuthorId where p.PublicationId == publicationId select a).ToList();
            return authors;
        }
        private List<BranchOfKnowledge> GetBranchesOfPublication(int publicationId)
        {
            List<BranchOfKnowledge> branches = (from b in db.BranchOfKnowledges join bp in db.BranchOfKnowledgePublications on b.BranchOfKnowledgeId equals bp.BranchOfKnowledgeId join p in db.Publications on bp.PublicationId equals p.PublicationId where p.PublicationId == publicationId select b).ToList();
            return branches;
        }
    }
}
