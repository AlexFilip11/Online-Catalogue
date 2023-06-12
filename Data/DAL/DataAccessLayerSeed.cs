﻿using Data.Exceptions;
using FinalProjectCatalogue.Data.Models;
using FinalProjectCatalogue.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectCatalogue.Data.DAL
{
    public class DataAccessLayerSeed
    {
        #region singleton
        private DataAccessLayerSeed() 
        {

        }
        private static DataAccessLayerSeed instance;
        public static DataAccessLayerSeed Instance 
        { 
            get 
            {
                if(instance == null)
                {
                    instance = new DataAccessLayerSeed();
                }
                return instance;
            } 
        }
        #endregion
        #region seed
        public void Seed()
        {
            using var ctx = new CatalogueDbContext();
            var MsPop = new Teacher { Name = "Ms. Pop", Rank = Rank.Assistant, Address = new Address {City = "Oradea", Street="Iuliu Maniu", Number = 3 } };
            ctx.Add(MsPop);
            var MrVoicu = new Teacher { Name = "Mr. Voicu", Rank = Rank.Professor, Address = new Address { City = "Oradea", Street = "Pascal", Number = 30 } };
            ctx.Add(MrVoicu);
            var maths = new Subject { Name = "Maths", Teacher = new List<Teacher>() { MsPop, MrVoicu } };
            ctx.Add(maths);
            var MsVlaicu = new Teacher { Name = "Ms. Vlaicu", Rank = Rank.Associate, Address = new Address { City = "Marghita", Street = "1 Decembrie", Number = 17 } };
            ctx.Add(MsVlaicu);
            var English = new Subject { Name = "English", Teacher = new List<Teacher>() { MsVlaicu, MsPop } };
            ctx.Add(English);
            var MrCiupe = new Teacher { Name = "Mr. Ciupe", Rank = Rank.Instructor, Address = new Address { City = "Alesd", Street = "Republicii", Number = 81 } };
            ctx.Add(MrCiupe);
            var PE = new Subject { Name = "Physical Education", Teacher = new List<Teacher>() { MrCiupe} };
            ctx.Add(PE);
            var Literature = new Subject { Name = "Literature", Teacher = new List<Teacher>() { MsVlaicu } };
            ctx.Add(Literature);
            ctx.Add(new Student
            {
                Name = "Alex",
                Surname = "Filip",
                Age = 23,
                Address = new Address { City = "Marghita", Street = "Ierului", Number = 8 },
                Marks = new List<Mark>()
                { new Mark { Value = 10, DataAndTime = DateTime.Now, Subject = maths, Teacher=MsPop }, new Mark{Value = 9, DataAndTime= DateTime.Today, Subject = English, Teacher=MsVlaicu} }
            });
            ctx.Add(new Student
            {
                Name = "Alexandra",
                Surname = "Pop",
                Age = 20,
                Address = new Address { City = "Cluj", Street = "Republicii", Number = 13 },
                Marks = new List<Mark>()
                { new Mark { Value = 10, DataAndTime = DateTime.Now, Subject = PE, Teacher=MrCiupe }, new Mark{Value = 8, DataAndTime= DateTime.Today, Subject = Literature, Teacher=MsVlaicu} }
            });
            ctx.Add(new Student
            {
                Name = "Denis",
                Surname = "Farcas",
                Age = 21,
                Address = new Address { City = "Oradea", Street = "Cantemir", Number = 32 },
                Marks = new List<Mark>()
                { new Mark { Value = 6, DataAndTime = DateTime.Now, Subject = maths, Teacher=MsPop }, new Mark{Value = 9, DataAndTime= DateTime.Today, Subject = PE, Teacher=MrCiupe} }
            });
            ctx.Add(new Student
            {
                Name = "Dorel",
                Surname = "Manciu",
                Age = 24,
                Address = new Address { City = "Alesd", Street = "Principala", Number = 2 },
                Marks = new List<Mark>()
                { new Mark { Value = 10, DataAndTime = DateTime.Now, Subject = Literature, Teacher=MsVlaicu }, new Mark{Value = 4, DataAndTime= DateTime.Today, Subject = maths, Teacher=MrVoicu} }
            });
            ctx.Add(new Student
            {
                Name = "Diana",
                Surname = "Alb",
                Age = 22,
                Address = new Address { City = "Oradea", Street = "Mihai Eminescu", Number = 14 },
                Marks = new List<Mark>()
                { new Mark { Value = 10, DataAndTime = DateTime.Now, Subject = maths, Teacher=MrVoicu }, new Mark{Value = 10, DataAndTime= DateTime.Today, Subject = PE, Teacher=MrCiupe} }
            });
            ctx.SaveChanges();
        }
        #endregion
        #region students
        public IEnumerable<Student> GetAllStudents()
        {
            using var ctx = new CatalogueDbContext();
            return ctx.Students.ToList();
        }
        public Student GetStudentByStudentId(int studentId)
        {
            using var ctx = new CatalogueDbContext();
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
            using var ctx = new CatalogueDbContext();
            var student = new Student { Name = newStudent.Name, Surname = newStudent.Surname, Age = newStudent.Age };
            ctx.Students.Add(student);
            ctx.SaveChanges();
            return student;
        }
        public Student UpdateStudent(Student studentToUpdate)
        {
            using var ctx = new CatalogueDbContext();
            var student = ctx.Students.Single(s => s.Id == studentToUpdate.Id);
            student.Name = studentToUpdate.Name;
            student.Surname = studentToUpdate.Surname;
            student.Age = studentToUpdate.Age;
            ctx.SaveChanges();
            return student;

        }
        public void DeleteStudent(int studentId)
        {
            using var ctx = new CatalogueDbContext();
            var student = ctx.Students.Single(s => s.Id == studentId);
            if (student.Address != null)
            {
                ctx.Addresses.Remove(student.Address);
            }
            if (student.Marks != null)
            {
                ctx.Marks.RemoveRange(student.Marks);
            }
            ctx.Students.Remove(student);
            ctx.SaveChanges();
        }
        public bool UpdateOrCreateStudentAddress(int studentId, Address newAddress)
        {
            using var ctx = new CatalogueDbContext();
            var student = ctx.Students.Include(s => s.Address).Single(s => s.Id == studentId);
            if (!ctx.Students.Any(s => s.Id == studentId))
            {
                throw new InvalidIdException($"invalid no student id {studentId}");
            }
            var created = false;
            if (student.Address == null)
            {
                student.Address = new Address();
                created = true;
            }
            student.Address.City = newAddress.City;
            student.Address.Street = newAddress.Street;
            student.Address.Number = newAddress.Number;
            ctx.SaveChanges();
            return created;
        }
        #endregion
        #region subjects
        public Subject AddSubject(Subject newSubject)
        {
            using var ctx = new CatalogueDbContext();
            var subject = new Subject { Name = newSubject.Name, Teacher=null, Marks=null };
            ctx.Subjects.Add(subject);
            ctx.SaveChanges();
            return subject;
        }
        public List<Subject> GetAllSubjects()
        {
            using var ctx = new CatalogueDbContext();
            return ctx.Subjects.ToList();
        }
        /*public List<Subject> GetAllSubjectsOfASpecificStudent(int studentId)
        {
            using var ctx = new CatalogueDbContext();
            if (!ctx.Students.Any(s => s.Id == studentId))
            {
                throw new InvalidIdException($"invalid student id {studentId}");
            }
            return ctx.Subjects.Where(s => s.StudentId == studentId).Distinct().ToList();
        }*/
        public void DeleteSubject(int subjectId)
        {
            using var ctx = new CatalogueDbContext();
            var subject = ctx.Subjects.Single(s => s.Id == subjectId);
            if (subject.Marks != null)
            {
                ctx.Marks.RemoveRange(subject.Marks);
            }
            ctx.Subjects.Remove(subject);
            ctx.SaveChanges();
        }
        #endregion
        #region marks
        public void GradeStudent(int studentId, int gradeValue, int subjectId)
        {
            using var ctx = new CatalogueDbContext();
            if (!ctx.Students.Any(s => s.Id == studentId))
            {
                throw new InvalidIdException($"invalid student id {studentId}");
            }
            if (!ctx.Subjects.Any(s => s.Id == subjectId))
            {
                throw new InvalidIdException($"invalid subject id {subjectId}");
            }
            ctx.Marks.Add(new Mark { Value = gradeValue, SubjectId = subjectId, StudentId = studentId, DataAndTime = DateTime.Now });
            ctx.SaveChanges();
        }
        public List<bool> GetAllMarksForAStudent(int studentId)
        {
            using var ctx = new CatalogueDbContext();
            if (!ctx.Students.Any(s => s.Id == studentId))
            {
                throw new InvalidIdException($"invalid student id {studentId}");
            }
            if (!ctx.Marks.Any(m => m.StudentId == studentId))
            {
                throw new InvalidIdException($"invalid no marks for student id {studentId}");
            }
            return ctx.Marks.Select(m => m.StudentId == studentId).ToList();


        }
        public List<bool> GetAllMarksForASubject(int studentId, int subjectId)
        {
            using var ctx = new CatalogueDbContext();
            if (ctx.Students.Any(s => s.Id != studentId))
            {
                throw new InvalidIdException($"invalid student id {studentId}");
            }
            if (ctx.Subjects.Any(s => s.Id != subjectId))
            {
                throw new InvalidIdException($"invalid subject id {subjectId}");
            }
            if (!ctx.Marks.Any(m => m.StudentId == studentId))
            {
                throw new InvalidIdException($"invalid no marks for student id {studentId}");
            }
            return ctx.Marks.Where(s => s.SubjectId == subjectId).Select(m => m.StudentId == studentId).ToList();
        }
        public List<string> GetAverageGradeForAStudent(int studentId)
        {
            using var ctx = new CatalogueDbContext();
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
        }
        #endregion
        #region teachers
        public Teacher AddTeacher(Teacher newTeacher)
        {
            using var ctx = new CatalogueDbContext();
                var teacher = new Teacher { Name = newTeacher.Name };
                ctx.Teachers.Add(teacher);
            ctx.SaveChanges();
            return teacher;
        }
        public List<Teacher> GetAllTeacher()
        {
            using var ctx = new CatalogueDbContext();
            return ctx.Teachers.ToList();
        }
        public void DeleteTeacher(int teacherId)
        {
            using var ctx = new CatalogueDbContext();
            var teacher = ctx.Teachers.Single(t => t.Id == teacherId);
            ctx.Teachers.Remove(teacher);
            ctx.SaveChanges();
        }
        public bool UpdateOrCreateTeacherAddress(int teacherId, Address newAddress)
        {
            using var ctx = new CatalogueDbContext();
            var teacher = ctx.Teachers.Include(s => s.Address).Single(s => s.Id == teacherId);
            if (!ctx.Teachers.Any(s => s.Id == teacherId))
            {
                throw new InvalidIdException($"invalid no student id {teacherId}");
            }
            var created = false;
            if (teacher.Address == null)
            {
                teacher.Address = new Address();
                created = true;
            }
            teacher.Address.City = newAddress.City;
            teacher.Address.Street = newAddress.Street;
            teacher.Address.Number = newAddress.Number;
            ctx.SaveChanges();
            return created;
        }
        public void AllocateSubjectToTeacher(int teacherId, int subjectId)
        {
            using var ctx = new CatalogueDbContext();
            if (ctx.Teachers.Where(t => t.Id == teacherId).Where(s => s.Id == subjectId).Any())
            {
                throw new InvalidIdException($"invalid subject id {subjectId}");
            }
            var teacher = ctx.Teachers.Include(s => s.Subjects).Single(t => t.Id == teacherId);
            teacher.Subjects = new List<Subject>(subjectId);
            ctx.SaveChanges();
        }
        public Teacher PromoteTeacher(int teacherId,Teacher teacherToPromote)
        {
            using var ctx = new CatalogueDbContext();
            var teacher = ctx.Teachers.Single(s => s.Id == teacherId);
            teacher.Rank = teacherToPromote.Rank;
            ctx.SaveChanges();
            return teacher;
        }
        public List<string> GetAllGradesOfferedByATeacher(int teacherId)
        {
            using var ctx = new CatalogueDbContext();
            List<string> teachersGrades = new List<string>();
            foreach (var mark in ctx.Marks.Where(m => m.TeacherId == teacherId))
            {
                teachersGrades.Add($"{mark.Value} +{mark.DataAndTime} +{mark.StudentId}/n");
            }
            return teachersGrades;
        }
        #endregion
    }
}