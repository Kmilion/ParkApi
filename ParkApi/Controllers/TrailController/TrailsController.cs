using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkApi.Controllers.NationalParkController.ViewModels;
using Services.DTOs;
using Services.Managers.IManagers;
using System;
using System.Collections.Generic;

namespace ParkApi.Controllers
{
    [Route("api/trails")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ParkOpenAPISpecTrails")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TrailsController : Controller
    {
        private readonly ITrailsManager _trailsManager;
        private readonly IMapper _mapper;

        public TrailsController(ITrailsManager trailsManager, IMapper mapper)
        {
            _trailsManager = trailsManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of trails.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<TrailRes>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTrails()
        {
            try
            {
                ICollection<TrailDTO> trailsDTOList = _trailsManager.GetTrails();

                ICollection<TrailRes> trailResList = _mapper.Map<ICollection<TrailRes>>(trailsDTOList);

                return Ok(trailResList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get individual trail.
        /// </summary>
        /// <param name="id">Trail ID</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetTrail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrailRes))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int id)
        {
            try
            {
                TrailDTO parkDTO = _trailsManager.GetTrail(id);

                if (parkDTO == null)
                {
                    return NotFound();
                }

                TrailRes parkVM = _mapper.Map<TrailRes>(parkDTO);

                return Ok(parkVM);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get list of trails in national park.
        /// </summary>
        /// <param name="nationalParkId">National park ID</param>
        /// <returns></returns>
        [HttpGet("GetTrailsInNationalPark/{nationalParkId:int}", Name = "GetTrailsInNationalPark")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<TrailRes>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrailsInNationalPark(int nationalParkId)
        {
            try
            {
                ICollection<TrailDTO> parkDTOList = _trailsManager.GetTrailsInNationalPark(nationalParkId);

                if (parkDTOList == null)
                {
                    return NotFound();
                }

                ICollection<TrailRes> parkResList = _mapper.Map<ICollection<TrailRes>>(parkDTOList);

                return Ok(parkResList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Register a trail.
        /// </summary>
        /// <param name="trailReq">Trail data</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrailRes))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail(TrailReq trailReq)
        {
            try
            {
                if (trailReq == null)
                {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                TrailDTO trailDTO = _mapper.Map<TrailDTO>(trailReq);
                trailDTO.DateCreated = DateTime.Now;

                TrailRes trailRes = _mapper.Map<TrailRes>(_trailsManager.CreateTrail(trailDTO));

                return CreatedAtRoute("GetTrail", new { id = trailRes.Id }, trailRes);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Update a trail.
        /// </summary>
        /// <param name="id">National trail ID</param>
        /// <param name="trailReq">National trail data</param>
        /// <returns></returns>
        [HttpPatch("{id:int}", Name = "UpdateTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int id, TrailReq trailReq)
        {
            try
            {
                if (trailReq == null || id != trailReq.Id)
                {
                    return BadRequest(ModelState);
                }

                TrailDTO parkDTO = _mapper.Map<TrailDTO>(trailReq);

                _trailsManager.UpdateTrail(parkDTO);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);

            }
        }

        /// <summary>
        /// Delete a trail.
        /// </summary>
        /// <param name="id">Trail ID</param>
        /// <returns></returns>
        [HttpDelete("{id:int}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int id)
        {
            try
            {
                if (!_trailsManager.TrailExists(id))
                {
                    return NotFound();
                }

                _trailsManager.DeleteTrail(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
