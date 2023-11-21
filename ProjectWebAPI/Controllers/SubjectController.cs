using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Repo;
using ProjectWebAPI.DTO;
using ProjectWebAPI.Entity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly ILogger<SubjectController> _logger;

        public SubjectController(oExamDbContext context, ILogger<SubjectController> logger)
        {
            unitOfWork = new UnitOfWork(context);
            _logger = logger;
        }

        // GET: api/Subject
        [HttpGet,Route("GetAll")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<SubjectDTO>> GetSubjects()
        {
            try
            {
                var subjects = unitOfWork.SubjectRepoImplObject.GetAll();
                return Ok(subjects);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Subject/5
        [HttpGet,Route("GetById/{id}")]
        [AllowAnonymous]
        public ActionResult<SubjectDTO> GetSubject(int id)
        {
            try
            {
                var subject = unitOfWork.SubjectRepoImplObject.GetById(id);

                if (subject == null)
                {
                    return NotFound();
                }

                return Ok(subject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet,Route("GetByName/{name}")]
        [AllowAnonymous]
        public ActionResult<SubjectDTO> GetSubjectByName(string name)
        {
            try
            {
                var subject = unitOfWork.SubjectRepoImplObject.GetByName(name);

                if (subject == null)
                {
                    return NotFound();
                }

                return Ok(new SubjectDTO
                {
                    SubjectID = subject.SubjectID,
                    SubjectName = subject.SubjectName,

                });
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/Subject
        [HttpPost,Route("Add")]
        [Authorize(Roles = "Admin")]
        public ActionResult<SubjectDTO> PostSubject(SubjectDTO subjectDTO)
        {
            try
            {
                unitOfWork.SubjectRepoImplObject.Add(subjectDTO);
                unitOfWork.SaveAll();

                return CreatedAtAction(nameof(GetSubject), new { id = subjectDTO.SubjectID }, subjectDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);

            }
        }

        // PUT: api/Subject/5
        [HttpPut,Route("Update/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult PutSubject(int id, SubjectDTO subjectDTO)
        {
            try
            {
                if (id != subjectDTO.SubjectID)
                {
                    return BadRequest();
                }

                bool success = unitOfWork.SubjectRepoImplObject.Update(id, subjectDTO);

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

        // DELETE: api/Subject/5
        [HttpDelete,Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteSubject(int id)
        {
            try
            {
                bool success = unitOfWork.SubjectRepoImplObject.Delete(id);

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
