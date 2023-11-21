using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Repo;
using ProjectWebAPI.DTO;
using System.Collections.Generic;
using ProjectWebAPI.Entity;
using Microsoft.AspNetCore.Authorization;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestStructureController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly ILogger<TestStructureController> _logger;

        public TestStructureController(oExamDbContext context, ILogger<TestStructureController> logger)
        {
            unitOfWork = new UnitOfWork(context);
            _logger = logger;
        }

        // GET: api/TestStructure
        [HttpGet,Route("GetAll")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<TestStructureDTO>> GetTestStructures()
        {
            try
            {
                var testStructures = unitOfWork.TestStructureRepoImplObject.GetAll();
                return Ok(testStructures);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);

            }
        }

        // GET: api/TestStructure/5
        [HttpGet,Route("GetById/{id}")]
        [AllowAnonymous]
        public ActionResult<TestStructureDTO> GetTestStructure(int id)
        {
            try
            {
                var testStructure = unitOfWork.TestStructureRepoImplObject.GetById(id);

                if (testStructure == null)
                {
                    return NotFound();
                }

                return Ok(testStructure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);

            }
        }

        // POST: api/TestStructure
        [HttpPost,Route("Add")]
        [Authorize(Roles = "Admin")]
        public ActionResult<TestStructureDTO> PostTestStructure(TestStructureDTO testStructureDTO)
        {
            try
            {
                unitOfWork.TestStructureRepoImplObject.Add(testStructureDTO);
                unitOfWork.SaveAll();

                return CreatedAtAction(nameof(GetTestStructure), new { id = testStructureDTO.TestID }, testStructureDTO);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/TestStructure/5
        [HttpPut,Route("Update/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult PutTestStructure(int id, TestStructureDTO testStructureDTO)
        {
            try
            {
                if (id != testStructureDTO.TestID)
                {
                    return BadRequest();
                }

                bool success = unitOfWork.TestStructureRepoImplObject.Update(id, testStructureDTO);

                if (!success)
                {
                    return NotFound();
                }

                unitOfWork.SaveAll();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);

            }
        }

        // DELETE: api/TestStructure/5
        [HttpDelete,Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteTestStructure(int id)
        {
            try
            {

                bool success = unitOfWork.TestStructureRepoImplObject.Delete(id);

                if (!success)
                {
                    return NotFound();
                }

                unitOfWork.SaveAll();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);

            }
        }
    }
}
