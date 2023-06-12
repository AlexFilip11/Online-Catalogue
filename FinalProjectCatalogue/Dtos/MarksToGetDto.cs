using FinalProjectCatalogue.Data.Models;
using System.Data;

namespace FinalProjectCatalogue.Dtos
{
    public class MarksToGetDto
    {
        public int studentId { get; set; }
        public int value { get; set; }
        public DateTime DataAndTime { get; set; }
        public Subject subject { get; set; }
    }
}
