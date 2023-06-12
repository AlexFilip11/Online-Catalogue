using FinalProjectCatalogue.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public interface IDalStudents
    {
        IEnumerable<Student> GetAllStudents();
        public Student GetStudentByStudentId(int studentId);
        public Student CreateStudent(Student newStudent);
        public void DeleteStudent(int studentId);
        public Student UpdateStudent(Student studentToUpdate);
        public bool UpdateOrCreateStudentAddress(int studentId, Address newAddress);


    }
}
