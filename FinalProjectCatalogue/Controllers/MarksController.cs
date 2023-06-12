using FinalProjectCatalogue.Data.DAL;
using FinalProjectCatalogue.Dtos;
using FinalProjectCatalogue.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectCatalogue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        /// <summary>
        /// Grade a student
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public IActionResult AddNota([FromBody] GradeToAddDto mark)
        {
            DataAccessLayerSingleton.Instance.GradeStudent(mark.Value, mark.StudentId, mark.subjectId);
            return Ok();
        }
        /// <summary>
        /// Get all marks for a student
        /// </summary>
        [HttpGet]
        public IEnumerable<MarksToGetDto> GetAllMarksForAStudent([Range(1, int.MaxValue)] int id)
        {
            var allMarks = DataAccessLayerSingleton.Instance.GetAllMarksForAStudent(id);
            return allMarks.Select(s => s.ToDto()).ToList();
        }
        

        /*
        /// <summary>
        /// Get all marks for a subject
        /// </summary>
        [HttpGet]

        public IEnumerable<MarksToGetDto> GetAllMarksForASubject([Range(0, int.MaxValue)] int id)
        {
            var allMarks = DataAccessLayerSeed.Instance.GetAllMarksForASubject(id);
            return allMarks.Select(s=>s.ToDto()).ToList();
        }
        */
    }
}

