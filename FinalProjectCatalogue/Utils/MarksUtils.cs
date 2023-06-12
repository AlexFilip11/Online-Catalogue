using FinalProjectCatalogue.Data.Models;
using FinalProjectCatalogue.Dtos;

namespace FinalProjectCatalogue.Utils
{
    public static class MarksUtils
    {
        public static MarksToGetDto ToDto(this Mark marks)
        {
            if (marks == null)
            {
                return null;
            }

            return new MarksToGetDto { value=marks.Value, DataAndTime= marks.DataAndTime, subject = marks.Subject };
        }
        public static Mark ToEntity(this MarksToGetDto mark)
        {
            return  new Mark
            {
                StudentId=mark.studentId,
                Value=mark.value,
                DataAndTime=mark.DataAndTime,
                Subject=mark.subject
            };
        }
    }
}
