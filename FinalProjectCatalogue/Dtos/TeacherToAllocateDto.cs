using FinalProjectCatalogue.Data.Models;

namespace FinalProjectCatalogue.Dtos
{
    public class TeacherToAllocateDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
