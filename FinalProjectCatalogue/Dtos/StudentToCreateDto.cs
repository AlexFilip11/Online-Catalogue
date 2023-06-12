using System.ComponentModel.DataAnnotations;

namespace FinalProjectCatalogue.Dtos
{
    public class StudentToCreateDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "the name cannot be empty")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "the surname cannot be empty")]
        public string Surname { get; set; }
        [Range(0, 100)]
        public int Age { get; set; }
    }
}
