using AutoMapper;
using MagicVilla.Data;
using MagicVilla.Models;
using MagicVilla.Models.Dto;
using MagicVilla.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace MagicVilla.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IVillaRepository _villaDb;
        private readonly IMapper _mapper;

        public VillaAPIController(IVillaRepository villaDb, IMapper mapper)
        {
            _villaDb = villaDb;
            _mapper = mapper;
            this._response = new();
        }

        // GET api/VillaAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APIResponse>>> GetVillas()
        {
            try
            {
                // Retrieve all villas from the database
                IEnumerable<Villa> villas = await _villaDb.GetAllAsync();
                // Map the villas to VillaDTO objects
                _response.Result = _mapper.Map<IEnumerable<VillaDTO>>(villas);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }
            return Ok(_response);
        }

        // GET api/VillaAPI/{Id}
        [HttpGet("{Id:int}", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<APIResponse>>> GetVilla(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    return BadRequest();
                }

            // Retrieve the villa with the specified Id from the database
                var villa = await _villaDb.GetAsync(v => v.Id == Id);

                if (villa == null)
                {
                    return NotFound();
                }

                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                    
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }

            // Map the villa to a VillaDTO object
            return Ok(_response);
        }

        // POST api/VillaAPI
        [HttpPost(Name = "CreateVilla")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                 
                // Check if a villa with the same name already exists
                if (await _villaDb.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("Custom Error", "Villa name already exists");
                    return BadRequest(ModelState);
                }

                // Map the VillaCreateDTO to a Villa object
                Villa model = _mapper.Map<Villa>(createDTO);
                _response.Result = model;
                _response.StatusCode = HttpStatusCode.Created;

                // Add the villa to the database
                await _villaDb.CreateAsync(model);
                await _villaDb.SaveAsync();

                // Return the created villa as a VillaDTO object
                return CreatedAtRoute("GetVilla", new { Id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }

            return NotFound();
        }

        // DELETE api/VillaAPI/{Id}
        [HttpDelete("{Id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest();
                }

                // Retrieve the villa with the specified Id from the database
                var villa = await _villaDb.GetAsync(v => v.Id == Id);
                _response.StatusCode = HttpStatusCode.Accepted;
                _response.IsSuccess = true;
                if (villa == null)
                {
                    return NotFound();
                }

                // Remove the villa from the database
                await _villaDb.DeleteAsync(villa);
                await _villaDb.SaveAsync();
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }

            return Accepted(_response);
        }

        // PUT api/VillaAPI/{Id}
        [HttpPut("{Id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int Id, [FromBody] VillaUpdateDTO villa)
        {
            try
            {
                if (villa == null)
                {
                    return BadRequest(villa);
                }

                if (Id != villa.Id)
                {
                    return BadRequest();
                }

                // Map the VillaUpdateDTO to a Villa object
                Villa model = _mapper.Map<Villa>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

                // Update the villa in the database
                await _villaDb.UpdateAsync(model);
                await _villaDb.SaveAsync();

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            } 
            return Ok(_response);
        }

        // PATCH api/VillaAPI/{Id}
        [HttpPatch("{Id:int}", Name = "PartiallyUpdateVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> PartiallyUpdateVilla(int Id, JsonPatchDocument<VillaUpdateDTO> patchDoc)
        {
            try
            {
                // Retrieve the existing villa with the specified Id from the database
                var existingVilla = await _villaDb.GetAsync(v => v.Id == Id, tracked: false);

                // Map the existing villa to a VillaUpdateDTO object
                VillaUpdateDTO villaUpdateDTO = _mapper.Map<VillaUpdateDTO>(existingVilla);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

                if (existingVilla == null)
                {
                    return NotFound();
                }

                // Apply the patch document to the VillaUpdateDTO object
                patchDoc.ApplyTo(villaUpdateDTO, ModelState);

                // Map the VillaUpdateDTO object to a Villa object
                Villa model = _mapper.Map<Villa>(villaUpdateDTO);

                // Update the villa in the database
                await _villaDb.UpdateAsync(model);
                await _villaDb.SaveAsync();

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }
           
            return Ok(_response);
        }
    }
}
