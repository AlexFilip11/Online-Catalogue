using FinalProjectCatalogue.Data.Models;
using FinalProjectCatalogue.Dtos;
using System.Runtime.CompilerServices;

namespace FinalProjectCatalogue.Utils
{
    public static class TeacherUtils
    {
        public static TeacherToGetDto ToDto(this Teacher teacher)
        {
            if (teacher == null)
            {
                return null;
            }

            return new TeacherToGetDto { Id = teacher.Id, Name = teacher.Name, rank = teacher.Rank };
        }

        public static Teacher ToEntity(this TeacherToCreateDto teacher)
        {
            if (teacher == null)
            {
                return null;
            }

            return new Teacher
            {
                Name = teacher.Name,
                Rank = teacher.rank,
            };
        }
        public static Teacher ToEntity(this TeacherToPromoteDto teacher)
        {
            if(teacher == null)
            {
                return null;
            }

            return new Teacher
            {
                Rank= teacher.rank,
            };
        }
        
    }
}
