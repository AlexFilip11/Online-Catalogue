using Microsoft.EntityFrameworkCore;
using FinalProjectCatalogue.Data.Models;
using Data.Exceptions;

namespace FinalProjectCatalogue.Data.DAL
{
    internal class DataAccessLayerTeachers
    {
        private readonly CatalogueDbContext ctx;
        public DataAccessLayerTeachers(CatalogueDbContext ctx)
        {
            this.ctx = ctx;
        }
        public void AddTeacher(Teacher newTeacher)
        {
            if(newTeacher.Address != null)
            {
                var teacher = new Teacher { Name = newTeacher.Name, Address = newTeacher.Address };
                ctx.Teachers.Add(teacher);
            }
            else
            {
                var teacher = new Teacher { Name = newTeacher.Name };
                ctx.Teachers.Add(teacher);
            }
            ctx.SaveChanges();
        }
        public List<Teacher> GetAllTeacher() => ctx.Teachers.ToList();
        public void DeleteTeacher(int teacherId)
        {
            var teacher = ctx.Teachers.Single(t=> t.Id == teacherId);
            ctx.Teachers.Remove(teacher);
            ctx.SaveChanges();
        }
        public void UpdateOrCreateTeacherAddress(int teacherId, Address newAddress)
        {
            var teacher = ctx.Teachers.Include(s => s.Address).Single(s => s.Id == teacherId);
            if (teacher.Address == null)
            {
                teacher.Address = new Address();
            }
            teacher.Address.City = newAddress.City;
            teacher.Address.Street = newAddress.Street;
            teacher.Address.Number = newAddress.Number;
            ctx.SaveChanges();
        }
        public void AllocateSubjectToTeacher(int teacherId, int subjectId)
        {
            if(ctx.Teachers.Where(t=> t.Id == teacherId).Where(s=> s.Id == subjectId).Any())
            {
                throw new InvalidIdException($"invalid subject id {subjectId}");
            }
            var teacher = ctx.Teachers.Include(s => s.Subjects).Single(t => t.Id == teacherId);
            teacher.Subjects = new List<Subject>(subjectId);
            ctx.SaveChanges();
        }
        public void PromoteTeacher(int teacherId, Rank newRank)
        {
            var teacher = ctx.Teachers.Single(t=> t.Id == teacherId);
            teacher.Rank = newRank;
        }
        public List<string> GetAllGradesOfferedByATeacher(int teacherId)
        {
            List<string> teachersGrades = new List<string>();
            foreach(var mark in ctx.Marks.Where(m=> m.TeacherId == teacherId))
            {
                teachersGrades.Add($"{mark.Value} +{mark.DataAndTime} +{mark.StudentId}/n");
            }
            return teachersGrades;
        }
    }
}
