using System.ComponentModel.DataAnnotations;

namespace FinalProjectCatalogue.Dtos
{
    public class GradeToAddDto
    {
        /// <summary>
        /// Value of the grade
        /// </summary>
        [Range(1, 10)]
        public int Value { get; set; }

        /// <summary>
        /// Student ID
        /// </summary>
        [Range(1, int.MaxValue)]
        public int StudentId { get; set; }

        /// <summary>
        /// Subject ID
        /// </summary>
        [Range(1, int.MaxValue)]
        public int subjectId { get; set; }
    }
}
