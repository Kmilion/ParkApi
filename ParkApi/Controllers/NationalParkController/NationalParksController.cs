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
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NationalParksController : Controller
    {
        private readonly INationalParksManager _nationalParksManager;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParksManager nationalParksManager, IMapper mapper)
        {
            _nationalParksManager = nationalParksManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of national parks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<NationalParkRes>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetNationalParks()
        {
            try
            {
                ICollection<NationalParkDTO> parksDTOList = _nationalParksManager.GetNationalParks();

                ICollection<NationalParkRes> parkVMList = _mapper.Map<ICollection<NationalParkRes>>(parksDTOList);

                return Ok(parkVMList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get individual national park.
        /// </summary>
        /// <param name="id">National Park ID</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetNationalPark")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NationalParkRes))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public IActionResult GetNationalPark(int id)
        {
            try
            {
                NationalParkDTO parkDTO = _nationalParksManager.GetNationalPark(id);

                if (parkDTO == null)
                {
                    return NotFound();
                }

                NationalParkRes parkVM = _mapper.Map<NationalParkRes>(parkDTO);

                return Ok(parkVM);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Register a national park.
        /// </summary>
        /// <param name="nationalParkVM">National park data</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NationalParkRes))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNationalPark([FromBody] NationalParkReq nationalParkVM)
        {
            try
            {
                if (nationalParkVM == null)
                {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                NationalParkDTO parkDTO = _mapper.Map<NationalParkDTO>(nationalParkVM);

                NationalParkRes nationalParkRes = _mapper.Map<NationalParkRes>(_nationalParksManager.CreateNationalPark(parkDTO));

                return CreatedAtRoute("GetNationalPark", new { id = nationalParkRes.Id }, nationalParkRes);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Update a national park.
        /// </summary>
        /// <param name="id">National park ID</param>
        /// <param name="nationalParkVM">National park data</param>
        /// <returns></returns>
        [HttpPatch("{id:int}", Name = "UpdateNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateNationalPark(int id, [FromBody] NationalParkReq nationalParkVM)
        {
            try
            {
                if (nationalParkVM == null || id != nationalParkVM.Id)
                {
                    return BadRequest(ModelState);
                }

                NationalParkDTO parkDTO = _mapper.Map<NationalParkDTO>(nationalParkVM);

                _nationalParksManager.UpdateNationalPark(parkDTO);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);

            }
        }

        /// <summary>
        /// Delete a national park.
        /// </summary>
        /// <param name="id">National park ID</param>
        /// <returns></returns>
        [HttpDelete("{id:int}", Name = "DeleteNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int id)
        {
            try
            {
                if (!_nationalParksManager.NationalParkExists(id))
                {
                    return NotFound();
                }

                _nationalParksManager.DeleteNationalPark(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
