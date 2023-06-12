using FinalProjectCatalogue.Data.Models;

namespace FinalProjectCatalogue.Data.Models
{
    public class Teacher
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public Address? Address { get; set; }
        public Rank Rank { get; set; }
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<Mark> Marks { get; set; } = new List<Mark>();
    }
}
