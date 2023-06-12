using FinalProjectCatalogue.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProjectCatalogue.Data.DAL;
using Data.Exceptions;
using FinalProjectCatalogue.Data.Models;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Data.DAL;
using FinalProjectCatalogue.Utils;

namespace FinalProjectCatalogue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        /// <summary>
        /// Initialize Database 
        /// </summary>
        [HttpPost("seed")]
        public void Seed() =>
            DataAccessLayerSeed.Instance.Seed();

        /// <summary>
        /// Shows all students from the database
        /// </summary>
        [HttpGet]
        public IEnumerable<StudentToGetDto> GetAllStudents()
        {
            var allStudents = DataAccessLayerSeed.Instance.GetAllStudents();
            return allStudents.Select(s => s.ToDto()).ToList();
        }

        /// <summary>
        /// Gets a student by id
        /// </summary>
        /// <param name="id">id of the student</param>
        /// <returns>student data</returns>
        [HttpGet("/id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGetDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]

        public ActionResult<StudentToGetDto> GetStudentById([Range(1, int.MaxValue)] int id)
        {
            var student = DataAccessLayerSeed.Instance.GetStudentByStudentId(id);
            return student.ToDto();
        }

        /// <summary>
        /// Creates a student
        /// </summary>
        /// <param name="studentToCreate">student to create data</param>
        /// <returns>created student data</returns>
        [HttpPost]
        public StudentToGetDto CreateAStudent([FromBody] StudentToCreateDto studentToCreate)
        {
            var student = DataAccessLayerSeed.Instance.CreateStudent(studentToCreate.ToEntity()).ToDto();
            return student;
        }

        /// <summary>
        /// Updates a student
        /// </summary>
        /// <param name="studentToUpdate"></param>
        /// <returns></returns>
        [HttpPatch]
        public StudentToGetDto UpdateStudent([FromBody] StudentToUpdateDto studentToUpdate)
        {
            var student = DataAccessLayerSeed.Instance.UpdateStudent(studentToUpdate.ToEntity()).ToDto();
            return student;
        }

        /// <summary>
        /// Delete a student by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest("id cannot be 0");
            }
            try
            {
                DataAccessLayerSeed.Instance.DeleteStudent(id);
            }
            catch (InvalidIdException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Updates a student's address
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addressToUpdate"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult UpdateStudentAddress([FromRoute] int id, [FromBody] AddressToUpdateDto addressToUpdate)
        {

            if (DataAccessLayerSeed.Instance.UpdateOrCreateStudentAddress(id, addressToUpdate.ToEntity()))
            {
                return Created("succeess", null);
            }
            return Ok();
        }

    }
}
