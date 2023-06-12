using FinalProjectCatalogue.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectCatalogue.Dtos
{
    public class SubjectToCreateDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "the name cannot be empty")]
        public string Name { get; set; }
    }
}
