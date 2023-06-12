using FinalProjectCatalogue.Data.Models;

namespace FinalProjectCatalogue.Dtos
{
    public class TeacherToGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Rank rank { get; set; }
    }
}
