using FinalProjectCatalogue.Data.Models;
using FinalProjectCatalogue.Dtos;
using System.Runtime.CompilerServices;

namespace FinalProjectCatalogue.Utils
{
    public static class SubjectsUtils
    {
        public static SubjectsToGetDto ToDto(this Subject subject)
        {
            if (subject == null)
            {
                return null;
            }

            return new SubjectsToGetDto { Id = subject.Id, Name = subject.Name };
        }
        public static Subject ToEntity(this SubjectToCreateDto subject)
        {
            if (subject == null)
            {
                return null;
            }

            return new Subject
            {
                Name = subject.Name,
                Teacher = null,
                Marks = null
            };
        }
    }
}
