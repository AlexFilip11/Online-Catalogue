using FinalProjectCatalogue.Data.Models;
using Data.Exceptions;

namespace FinalProjectCatalogue.Data.DAL
{
    internal class DataAccessLayerSubjects
    {
        private readonly CatalogueDbContext ctx;
        public DataAccessLayerSubjects(CatalogueDbContext ctx)
        {
            this.ctx = ctx;
        }
        public void AddSubject(Subject newSubject)
        {
            var subject = new Subject { Name = newSubject.Name };
            ctx.Subjects.Add(subject);
            ctx.SaveChanges();
        }
        public List<Subject> GetAllSubjects() => ctx.Subjects.ToList();
        public List<Subject> GetAllSubjectsOfASpecificStudent(int studentId)
        {
            if(!ctx.Students.Any(s=>s.Id == studentId))
            {
                throw new InvalidIdException($"invalid student id {studentId}");
            }
            return ctx.Marks.Where(s=> s.StudentId == studentId).Select(m => m.Subject).Distinct().ToList();
        }
        public void DeleteSubject(int subjectId)
        {
            var subject = ctx.Subjects.Single(s => s.Id == subjectId);
            if (subject.Marks != null)
            {
                ctx.Marks.RemoveRange(subject.Marks);
            }
            ctx.Subjects.Remove(subject);
            ctx.SaveChanges();
        }
    }
}
