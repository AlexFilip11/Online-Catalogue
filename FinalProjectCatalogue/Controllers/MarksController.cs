using FinalProjectCatalogue.Data.DAL;
using FinalProjectCatalogue.Dtos;
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
            DataAccessLayerSeed.Instance.GradeStudent(mark.Value, mark.StudentId, mark.subjectId);
            return Ok();
        }
        /*
        /// <summary>
        /// Get all grades
        /// </summary>
        [HttpGet("all-marks/{studentId}")]
        public ActionResult<MarksToGetDto> GetAllMarksForAStudent([Range(1, int.MaxValue)] int studentId)
        {
            var marks = DataAccessLayerSeed.Instance.GetAllMarksForAStudent(studentId);
            return marks.;
            
        }


        public IEnumerable<MarksToGetDto> GetGrades([Range(1, int.MaxValue)] int studentId)
        {
            var allGrades = DataAccessLayerSeed.Instance.GetAllMarksForAStudent(studentId);
            return allGrades.Select(s=>s)
        }*/
    }
}

