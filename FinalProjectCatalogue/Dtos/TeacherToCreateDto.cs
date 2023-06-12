using FinalProjectCatalogue.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectCatalogue.Dtos
{
    public class TeacherToCreateDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "the name cannot be empty")]
        public string Name { get; set; }
        [Range(0, 4)]
        public Rank rank;

    }
}
