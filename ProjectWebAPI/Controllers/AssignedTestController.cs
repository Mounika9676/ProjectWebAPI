using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Repo;
using ProjectWebAPI.DTO;
using System;
using System.Collections.Generic;
using ProjectWebAPI.Entity;
using Microsoft.AspNetCore.Authorization;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignedTestController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly ILogger<OrganizationController> _logger;

        public AssignedTestController(oExamDbContext context, ILogger<OrganizationController> logger)
        {
            unitOfWork = new UnitOfWork(context);
            _logger = logger;
        }

        // GET: api/AssignedTest
        [HttpGet,Route("GetAll")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<AssignedTestDTO>> GetAssignedTests()
        {
            try
            {
                var assignedTests = unitOfWork.AssignedTestRepoImplObject.GetAll();
                return Ok(assignedTests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // GET: api/AssignedTest/5
        [HttpGet,Route("GetById/{id}")]
        [AllowAnonymous]
        public ActionResult<AssignedTestDTO> GetAssignedTest(int id)
        {
            try
            {
                var assignedTest = unitOfWork.AssignedTestRepoImplObject.GetById(id);

                if (assignedTest == null)
                {
                    return NotFound();
                }

                return Ok(assignedTest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // POST: api/AssignedTest
        [HttpPost,Route("Add")]
        [Authorize(Roles = "Admin")]
        public ActionResult<AssignedTestDTO> PostAssignedTest(AssignedTestDTO assignedTestDTO)
        {
            try
            {
                unitOfWork.AssignedTestRepoImplObject.Add(assignedTestDTO);
                unitOfWork.SaveAll();

                return CreatedAtAction(nameof(GetAssignedTest), new { id = assignedTestDTO.AssignmentID }, assignedTestDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT: api/AssignedTest/5
        [HttpPut,Route("Update/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult PutAssignedTest(int id, AssignedTestDTO assignedTestDTO)
        {
            try
            {
                if (id != assignedTestDTO.AssignmentID)
                {
                    return BadRequest();
                }

                bool success = unitOfWork.AssignedTestRepoImplObject.Update(id, assignedTestDTO);

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
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // DELETE: api/AssignedTest/5
        [HttpDelete,Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteAssignedTest(int id)
        {
            try
            {
                bool success = unitOfWork.AssignedTestRepoImplObject.Delete(id);

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
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
