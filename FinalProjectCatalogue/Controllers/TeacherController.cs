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
    public class TeacherController : ControllerBase
    {
        /// <summary>
        /// Creates a teacher
        /// </summary>
        /// <param name="teacherToCreate">student to create data</param>
        /// <returns>created student data</returns>
        [HttpPost]
        public TeacherToGetDto CreateATeacher([FromBody] TeacherToCreateDto teacherToCreate)
        {
            var teacher = DataAccessLayerSeed.Instance.AddTeacher(teacherToCreate.ToEntity()).ToDto();
            return teacher;
        }
        /// <summary>
        /// Updates a teacher's address
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addressToUpdate"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult UpdateOrCreateTeacherAddress([FromRoute] int id, [FromBody] AddressToUpdateDto addressToUpdate)
        {

            if (DataAccessLayerSeed.Instance.UpdateOrCreateTeacherAddress(id, addressToUpdate.ToEntity()))
            {
                return Created("succeess", null);
            }
            return Ok();
        }
        /// <summary>
        /// Get all teachers from the database
        /// </summary>
        [HttpGet]
        public IEnumerable<TeacherToGetDto> GetAllTeachers()
        {
            var allTeachers = DataAccessLayerSeed.Instance.GetAllTeacher();
            return allTeachers.Select(s => s.ToDto()).ToList();
        }

        /// <summary>
        /// Updates a teacher's rank
        /// </summary>
        /// <param name="teacherToUpdate"></param>
        /// <returns></returns>
        [HttpPatch]
        public TeacherToGetDto PromoteTeacher([Range(1, int.MaxValue)]int id, [FromBody] TeacherToUpdateDto teacherToUpdate)
        {

            var teacher =DataAccessLayerSeed.Instance.PromoteTeacher(id, teacherToUpdate.ToEntity()).ToDto();
            return teacher; 
          
        }
    }
}
