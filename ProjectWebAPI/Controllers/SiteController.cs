using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Repo;
using ProjectWebAPI.DTO;
using ProjectWebAPI.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly ILogger<SiteController> _logger;

        public SiteController(oExamDbContext context, ILogger<SiteController> logger)
        {
            unitOfWork = new UnitOfWork(context);
            _logger = logger;
        }
        // GET: api/Site
        [HttpGet,Route("GetAll")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<SiteDTO>> GetSites()
        {
            try
            {
                var sites = unitOfWork.SiteRepoImplObject.GetAll();
                return Ok(sites);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);

            }
        }

        // GET: api/Site/5
        [HttpGet,Route("GetSite/{id}")]
        [Authorize(Roles = "Admin, User")]
        public ActionResult<SiteDTO> GetSite(int id)
        {
            try
            {
                var site = unitOfWork.SiteRepoImplObject.GetById(id);

                if (site == null)
                {
                    return NotFound();
                }

                return Ok(site);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet,Route("GetByName/{name}")]
        [Authorize(Roles = "Admin, User")]
        public ActionResult<SiteDTO> GetSiteByName(string name)
        {
            try
            {
                var site = unitOfWork.SiteRepoImplObject.GetByName(name);

                if (site == null)
                {
                    return NotFound();
                }

                return Ok(new SiteDTO
                {
                    SiteID = site.SiteID,
                    SiteName = site.SiteName,

                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);

            }
        }

        // POST: api/Site
        [HttpPost,Route("Add")]
        [Authorize(Roles = "Admin")]
        public ActionResult<SiteDTO> PostSite(SiteDTO siteDTO)
        {
            try
            {
                unitOfWork.SiteRepoImplObject.Add(siteDTO);
                unitOfWork.SaveAll();

                return CreatedAtAction(nameof(GetSite), new { id = siteDTO.SiteID }, siteDTO);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/Site/5
        [HttpPut,Route("Update/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult PutSite(int id, SiteDTO siteDTO)
        {
            try
            {
                if (id != siteDTO.SiteID)
                {
                    return BadRequest();
                }

                bool success = unitOfWork.SiteRepoImplObject.Update(id, siteDTO);

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

        // DELETE: api/Site/5
        [HttpDelete,Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteSite(int id)
        {
            try
            {
                bool success = unitOfWork.SiteRepoImplObject.Delete(id);

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
