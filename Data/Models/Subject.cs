using FinalProjectCatalogue.Data.Models;

namespace FinalProjectCatalogue.Data.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Teacher>? Teacher { get; set; } = new List<Teacher>();
        public List<Mark>? Marks { get; set; } = new List<Mark>();

    }
}
