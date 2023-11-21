using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.DTO;
using ProjectWebAPI.Entity;
using ProjectWebAPI.Repo;
using System;
using System.Collections.Generic;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(oExamDbContext context, IMapper mapper, ILogger<OrganizationController> logger)
        {
            unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet, Route("GetAll")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<OrganizationDTO>> GetOrganizations()
        {
            try
            {
                var organizations = unitOfWork.OrganizationRepoImplObject.GetAll();
                var organizationsDto = _mapper.Map<List<OrganizationDTO>>(organizations);
                return Ok(organizationsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet, Route("GetById/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<OrganizationDTO> GetOrganization(int id)
        {
            try
            {
                var organization = unitOfWork.OrganizationRepoImplObject.GetById(id);

                if (organization == null)
                {
                    return NotFound();
                }

                var organizationDto = _mapper.Map<OrganizationDTO>(organization);
                return Ok(organizationDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost, Route("Add")]
        [Authorize(Roles = "Admin")]
        public ActionResult<OrganizationDTO> PostOrganization(OrganizationDTO organizationDTO)
        {
            try
            {
                var organization = _mapper.Map<Organization>(organizationDTO);
                unitOfWork.OrganizationRepoImplObject.Add(organizationDTO);
                unitOfWork.SaveAll();

                var createdOrganizationDto = _mapper.Map<OrganizationDTO>(organization);
                return CreatedAtAction(nameof(GetOrganization), new { id = createdOrganizationDto.Id }, createdOrganizationDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut, Route("Update/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult PutOrganization(int id, OrganizationDTO organizationDTO)
        {
            try
            {
                if (id != organizationDTO.Id)
                {
                    return BadRequest();
                }

                var success = unitOfWork.OrganizationRepoImplObject.Update(id, organizationDTO);

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

        [HttpDelete, Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteOrganization(int id)
        {
            try
            {
                var success = unitOfWork.OrganizationRepoImplObject.Delete(id);

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
