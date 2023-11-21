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
    public class QuestionBankController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly ILogger<QuestionBankController> _logger;

        public QuestionBankController(oExamDbContext context, ILogger<QuestionBankController> logger)
        {
            unitOfWork = new UnitOfWork(context);
            _logger = logger;
        }

        // GET: api/QuestionBank
        [HttpGet,Route("GetAll")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<QuestionBankDTO>> GetQuestions()
        {
            try
            {
                var questions = unitOfWork.QuestionBankRepoImplObject.GetAll();
                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // GET: api/QuestionBank/5
        [HttpGet,Route("GetById/{id}")]
        [AllowAnonymous]
        public ActionResult<QuestionBankDTO> GetQuestion(int id)
        {
            try
            {
                var question = unitOfWork.QuestionBankRepoImplObject.GetById(id);

                if (question == null)
                {
                    return NotFound();
                }

                return Ok(question);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // GET: api/QuestionBank/BySubjectID/1
        [HttpGet,Route("GetBySubjectID/{subjectId}")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<QuestionBankDTO>> GetQuestionsBySubjectID(int subjectId)
        {
            try
            {
                var questions = unitOfWork.QuestionBankRepoImplObject.GetBySubjectID(subjectId);
                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // GET: api/QuestionBank/ByQuestion/keyword
        [HttpGet,Route("GetByQuestion/{keyword}")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<QuestionBankDTO>> GetQuestionsByKeyword(string keyword)
        {
            try
            {
                var questions = unitOfWork.QuestionBankRepoImplObject.GetByQuestion(keyword);
                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // POST: api/QuestionBank
        [HttpPost,Route("Add")]
        [Authorize(Roles = "Admin")]
        public ActionResult<QuestionBankDTO> PostQuestion(QuestionBankDTO questionDTO)
        {
            try
            {
                unitOfWork.QuestionBankRepoImplObject.Add(questionDTO);
                unitOfWork.SaveAll();

                return CreatedAtAction(nameof(GetQuestion), new { id = questionDTO.QuestionID }, questionDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT: api/QuestionBank/5
        [HttpPut,Route("Update/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult PutQuestion(int id, QuestionBankDTO questionDTO)
        {
            try
            {
                if (id != questionDTO.QuestionID)
                {
                    return BadRequest();
                }

                bool success = unitOfWork.QuestionBankRepoImplObject.Update(id, questionDTO);

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

        // DELETE: api/QuestionBank/5
        [HttpDelete,Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteQuestion(int id)
        {
            try
            {
                bool success = unitOfWork.QuestionBankRepoImplObject.Delete(id);

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
