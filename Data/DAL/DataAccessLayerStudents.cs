using Microsoft.EntityFrameworkCore;
using FinalProjectCatalogue.Data.Models;
using Data.Exceptions;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Data.DAL;

[assembly: InternalsVisibleTo("FinalProjectCatalogue")]

namespace FinalProjectCatalogue.Data.DAL
{
    internal class DataAccessLayerStudents :IDalStudents
    {
        private readonly CatalogueDbContext ctx;
        public DataAccessLayerStudents(CatalogueDbContext ctx) 
        {
            this.ctx = ctx;
        }

        public IEnumerable<Student> GetAllStudents() => ctx.Students.ToList();
       
        public Student GetStudentByStudentId(int studentId)
        {
            try
            {
                var student = ctx.Students.Single(s => s.Id == studentId);
                return student;
            }
            catch (Exception ex)
            {
                throw new InvalidIdException($"invalid no student id {studentId}");
            }
        }
        public Student CreateStudent(Student newStudent)
        {
            var student = new Student{Name = newStudent.Name, Surname = newStudent.Surname, Age=newStudent.Age};
            ctx.Students.Add(student);
            ctx.SaveChanges();
            return student;
        }

        public void DeleteStudent(int studentId) 
        { 
            var student = ctx.Students.Single(s => s.Id == studentId);
            if(student.Address!= null) 
            {
                ctx.Addresses.Remove(student.Address);
            }
            if(student.Marks!= null) 
            {
                ctx.Marks.RemoveRange(student.Marks);
            }
            ctx.Students.Remove(student);
            ctx.SaveChanges();
        }
        public Student UpdateStudent(Student studentToUpdate)
        {
            var student = ctx.Students.Single(s => s.Id == studentToUpdate.Id);
            student.Name = studentToUpdate.Name;
            student.Surname = studentToUpdate.Surname;
            student.Age = studentToUpdate.Age;
            ctx.SaveChanges();
            return student;

        }
        public bool UpdateOrCreateStudentAddress(int studentId, Address newAddress)
        {
            var student = ctx.Students.Include(s=> s.Address).Single(s => s.Id == studentId);
            if(!ctx.Students.Any(s=>s.Id == studentId))
            {
                throw new InvalidIdException($"invalid no student id {studentId}");
            }
            var created = false;
            if(student.Address==null)
            {
                student.Address = new Address();
                created = true;
            }
            student.Address.City= newAddress.City;
            student.Address.Street = newAddress.Street;
            student.Address.Number = newAddress.Number;
            ctx.SaveChanges();
            return created;
        }
        
    }
}
