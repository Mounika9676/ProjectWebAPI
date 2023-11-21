using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectWebAPI.DTO;
using ProjectWebAPI.Entity;

namespace ProjectWebAPI.Repo
{
        public class SubjectRepoImpl : IRepo<SubjectDTO>
        {
            private readonly oExamDbContext context;

            public SubjectRepoImpl(oExamDbContext ctx)
            {
                context = ctx;
            }

        public bool Add(SubjectDTO item)
        {
            Subject subject = new Subject
            {
                SiteID = item.SiteID,
                SubjectName = item.SubjectName
            };

            context.Subjects.Add(subject);
            context.SaveChanges();
            return true;
        }

        public List<SubjectDTO> GetAll()
        {
            var result = context.Subjects
                .Select(s => new SubjectDTO
                {
                    SubjectID = s.SubjectID,
                    SiteID = s.SiteID,
                    SubjectName = s.SubjectName
                })
                .ToList();

            return result;
        }

        public SubjectDTO GetById(int subjectId)
        {
            var subject = context.Subjects
                .Include(s => s.Site) // Include related Site data if needed
                .FirstOrDefault(s => s.SubjectID == subjectId);

            if (subject == null)
            {
                return null;
            }

            return new SubjectDTO
            {
                SubjectID = subject.SubjectID,
                SiteID = subject.SiteID,
                SubjectName = subject.SubjectName
            };
        }
        public Subject GetByName(string name)
        {
            return context.Subjects.FirstOrDefault(s => s.SubjectName == name);
        }

        public bool Update(int subjectId, SubjectDTO updatedItem)
        {
            var subject = context.Subjects.Find(subjectId);

            if (subject == null)
            {
                return false; // Subject not found
            }

            subject.SiteID = updatedItem.SiteID;
            subject.SubjectName = updatedItem.SubjectName;

            context.SaveChanges();
            return true;
        }

        public bool Delete(int subjectId)
        {
            var subject = context.Subjects.Find(subjectId);

            if (subject == null)
            {
                return false; // Subject not found
            }

            context.Subjects.Remove(subject);
            context.SaveChanges();
            return true;
        }
    }
}
