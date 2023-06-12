using FinalProjectCatalogue.Data.Models;
using Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectCatalogue.Data.DAL
{
    internal class DataAccessLayerMarks
    {
        private readonly CatalogueDbContext ctx;
        public DataAccessLayerMarks(CatalogueDbContext ctx)
        {
            this.ctx = ctx;
        }
        public void GradeStudent(int studentId, int gradeValue, int subjectId)
        {
            if(ctx.Students.Any(s=> s.Id != studentId))
            {
                throw new InvalidIdException($"invalid subject id {subjectId}");
            }
            if(ctx.Subjects.Any(s=> s.Id != subjectId))
            {
                throw new InvalidIdException($"invalid subject id {subjectId}");
            }
            ctx.Marks.Add(new Mark { Value = gradeValue, SubjectId = subjectId, StudentId = studentId, DataAndTime = DateTime.Now });
            ctx.SaveChanges();
        }
        public List<bool> GetAllMarksForAStudent(int studentId)
        {
            if(ctx.Students.Any(s=>s.Id != studentId)) 
            {
                throw new InvalidIdException($"invalid student id {studentId}");
            }
            if(!ctx.Marks.Any(m=> m.StudentId == studentId))
            {
                throw new InvalidIdException($"invalid no marks for student id {studentId}");
            }
            return ctx.Marks.Select(m => m.StudentId == studentId).ToList();
            
            
        }
        public List<bool> GetAllMarksForASubject(int studentId, int subjectId) 
        {
            if (ctx.Students.Any(s => s.Id != studentId))
            {
                throw new InvalidIdException($"invalid student id {studentId}");
            }
            if(ctx.Subjects.Any(s=> s.Id != subjectId))
            {
                throw new InvalidIdException($"invalid subject id {subjectId}");
            }
            if (!ctx.Marks.Any(m => m.StudentId == studentId))
            {
                throw new InvalidIdException($"invalid no marks for student id {studentId}");
            }
            return ctx.Marks.Where(s=> s.SubjectId == subjectId).Select(m => m.StudentId == studentId).ToList();
        }
        public List<string> GetAverageGradeForAStudent(int studentId)
        {
            var student = ctx.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                throw new InvalidIdException($"Invalid student ID: {studentId}");
            }

            var marks = ctx.Marks.Include(m => m.Subject)
                                 .Where(m => m.StudentId == studentId)
                                 .ToList();

            if (marks.Count == 0)
            {
                throw new InvalidIdException($"No marks found for student ID: {studentId}");
            }

            var averages = marks.GroupBy(m => m.Subject.Name)
                                .Select(g => new
                                {
                                    SubjectName = g.Key,
                                    AverageGrade = g.Average(m => m.Value)
                                })
                                .ToList();

            var result = averages.Select(a => $"{student.Name} {a.SubjectName} {a.AverageGrade}")
                                 .ToList();

            return result;
            /*List<string> averages = new List<string>();
            if (!ctx.Students.Any(s => s.Id == studentId))
            {
                throw new InvalidIdException($"invalid student id {studentId}");
            }
            if (!ctx.Marks.Where(s => s.StudentId == studentId).Any())
            {
                throw new InvalidIdException($"invalid no marks for student id {studentId}");
            }
            foreach (var mark in ctx.Marks.Where(m=>m.StudentId==studentId))
            {
                foreach(var subject in ctx.Subjects.Include(s=>s.Marks))
                {
                    averages.Add($"{ctx.Students.Where(s => s.Id == studentId).Single()} {subject.Name.Single()} {ctx.Marks.Average(m=>m.Value)}");
                }
            }
            foreach (var subject in ctx.Subjects.Where(s=> s.StudentId == studentId).Distinct())
            {
                var average = 0;
                var count = 0;
                foreach (var mark in ctx.Marks.Where(s => s.SubjectId == subject.Id))
                {
                    average += mark.Value;
                    count++;
                }
                averages.Add( $"{ctx.Students.Where(s=> s.Id == studentId).Single()} +{subject.Name.Single()} +{average / count} /n");
            }
            return averages;*/
        }
       /* public List<string> GetAllStudentsOrderedByGrades()
        {
            List<string> averages = new List<string>();
            foreach(var student in ctx.Students.Average(s=> s.Marks.AddRange())
            {
                student.Marks.Average(m => m.StudentId);
            }
        }*/

    }
}
