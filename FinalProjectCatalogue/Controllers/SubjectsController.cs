using Data.Exceptions;
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
    public class SubjectsController : ControllerBase
    {
        /// <summary>
        /// Shows all subjects
        /// </summary>
        [HttpGet]
        public IEnumerable<SubjectsToGetDto> GetAllSubjects()
        {
            var allSubjects = DataAccessLayerSingleton.Instance.GetAllSubjects();
            return allSubjects.Select(s => s.ToDto()).ToList();
        }
        /// <summary>
        /// Add a new subject
        /// </summary>
        [HttpPost]
        public SubjectsToGetDto AddASubject([FromBody] SubjectToCreateDto subjectToCreate)
        {
            var subject = DataAccessLayerSingleton.Instance.AddSubject(subjectToCreate.ToEntity()).ToDto();
            return subject;
        }
        /// <summary>
        /// Delete a subject by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteSubject(int id)
        {
            if (id == 0)
            {
                return BadRequest("id cannot be 0");
            }
            try
            {
                DataAccessLayerSingleton.Instance.DeleteSubject(id);
            }
            catch (InvalidIdException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok();
        }

    }
}
