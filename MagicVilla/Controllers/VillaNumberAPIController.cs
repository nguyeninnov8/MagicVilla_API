using AutoMapper;
using Azure;
using MagicVilla.Models;
using MagicVilla.Models.Dto;
using MagicVilla.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla.Controllers
{
    /// <summary>
    /// API controller for managing villa numbers.
    /// </summary>
    [ApiController]
    [Route("api/villaNumberAPI")]
    public class VillaNumberAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IVillaNumberRepository _villaNumberRepo;
        private readonly IVillaRepository _villaRepo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for VillaNumberAPIController.
        /// </summary>
        /// <param name="villaNumberRepo">The repository for villa numbers.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public VillaNumberAPIController(IVillaNumberRepository villaNumberRepo, IMapper mapper, IVillaRepository villaRepo)
        {
            _villaNumberRepo = villaNumberRepo;
            _mapper = mapper;
            _villaRepo = villaRepo;
            this._response = new();
        }

        /// <summary>
        /// Get all villa numbers.
        /// </summary>
        /// <returns>A list of all villa numbers.</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<APIResponse>>> GetVillaNumbers()
        {
            try
            {
                // Retrieve all villa numbers from the repository
                IEnumerable<VillaNumber> villaNumbers = await _villaNumberRepo.GetAllAsync();

                // Map the villa numbers to DTOs
                _response.Result = _mapper.Map<IEnumerable<VillaNumberDTO>>(villaNumbers);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a BadRequest response
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
                return BadRequest(_response);
            }
            // Return the API response with the villa numbers
            return Ok(_response);
        }

        /// <summary>
        /// Get a specific villa number by its ID.
        /// </summary>
        /// <param name="villaNo">The ID of the villa number to retrieve.</param>
        /// <returns>The requested villa number.</returns>
        [HttpGet("{villaNo:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int villaNo)
        {
            try
            {
                // Check if the villa number ID is valid
                if (villaNo <= 0)
                {
                    return BadRequest();
                }

                // Retrieve the villa number from the repository
                VillaNumber villaNumber = await _villaNumberRepo.GetAsync(vn => vn.VillaNo == villaNo, tracked: false);

                // Check if the villa number exists
                if (villaNumber == null)
                {
                    return BadRequest();
                }

                // Map the villa number to a DTO
                var villaNumberDto = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.Result = villaNumberDto;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a BadRequest response
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
                return BadRequest(_response);
            }

            // Return the API response with the villa number
            return Ok(_response);
        }

        /// <summary>
        /// Create a new villa number.
        /// </summary>
        /// <param name="villaNumber">The data for the new villa number.</param>
        /// <returns>The newly created villa number.</returns>
        [HttpPost(Name = "Create Villa")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumer([FromBody] VillaNumberCreateDTO villaNumber)
        {
            try
            {
                // Check if the villa number data is provided
                if (villaNumber == null)
                {
                    return BadRequest();
                }

                // Check if the villa number already exists
                if (await _villaNumberRepo.GetAsync(u => u.VillaNo == villaNumber.VillaNo) != null)
                {
                    ModelState.AddModelError("Villa Exist Error", "Villa number already exists");
                    return BadRequest(ModelState);
                }

                // Check if the Villa Id does not exist
                if (await _villaRepo.GetAsync(u => u.Id == villaNumber.villaId) == null)
                {
                    ModelState.AddModelError("Villa Id Error", "Villa Id does not exist");
                    return BadRequest(ModelState);
                }

                // Map the DTO to a model and create the villa number
                var villaNumberModel = _mapper.Map<VillaNumber>(villaNumber);
                await _villaNumberRepo.CreateAsync(villaNumberModel);

                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumberModel);
                _response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a BadRequest response
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
                return BadRequest(_response);
            }

            // Return the API response with the newly created villa number
            return CreatedAtRoute("GetVillaNumber", new { villaNo = villaNumber.VillaNo }, _response);
        }

        /// <summary>
        /// Update an existing villa number.
        /// </summary>
        /// <param name="villaNo">The ID of the villa number to update.</param>
        /// <param name="villaNumber">The updated data for the villa number.</param>
        
        [HttpPut("{villaNo:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int villaNo, [FromBody] VillaNumberUpdateDTO villaNumber)
        {
            try
            {
                // Check if the villa number ID and data are valid
                if (villaNo == 0 || villaNumber == null || villaNo != villaNumber.VillaNo)
                {
                    return BadRequest();
                }

                // Check if the villa number already exists
                if (await _villaNumberRepo.GetAsync(u => u.VillaNo == villaNumber.VillaNo) == null)
                {
                    ModelState.AddModelError("Villa Exist Error", "Villa number already exists");
                    return BadRequest(ModelState);
                }

                // Check if the Villa Id does not exist
                if (await _villaRepo.GetAsync(u => u.Id == villaNumber.villaId) == null)
                {
                    ModelState.AddModelError("Villa Id Error", "Villa Id does not exist");
                    return BadRequest(ModelState);
                }

                // Check if the villa number exists
                var villaNumberModel = _mapper.Map<VillaNumber>(villaNumber);
                // Update the villa number
                await _villaNumberRepo.UpdateAsync(villaNumberModel);

                // Return a NoContent response and the updated villa number
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.Result = villaNumberModel;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Delete a villa number by its ID.
        /// </summary>
        /// <param name="villaNo">The ID of the villa number to delete.</param>
        [HttpDelete("{villaNo:int}", Name = "DeleteVillaNumber")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int villaNo)
        {
            try
            {
                // Check if the villa number ID is valid
                if (villaNo == 0)
                {
                    return BadRequest();
                }

                // Retrieve the villa number from the repository
                VillaNumber villaNumber = await _villaNumberRepo.GetAsync(vn => vn.VillaNo == villaNo);


                // Check if the villa number exists
                if (villaNumber == null)
                {
                    return NotFound();
                }

                // Delete the villa number
                await _villaNumberRepo.DeleteAsync(villaNumber);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = villaNumber;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Partially update a villa number by its ID.
        /// </summary>
        /// <param name="villaNo">The ID of the villa number to update.</param>
        /// <param name="jsonPatch">The JSON patch document containing the update operations.</param>
        
        [HttpPatch("{villaNo:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> PartiallyUpdateVillaNumber(int villaNo,
            [FromBody] JsonPatchDocument<VillaNumberUpdateDTO> jsonPatch)
        {
            try
            {
                // Check if the villa number ID and JSON patch document are valid
                if (villaNo == 0 || jsonPatch == null)
                {
                    return BadRequest();
                }

                // Retrieve the villa number from the repository
                var exisitingVillaNumber = await _villaNumberRepo.GetAsync(vn => vn.VillaNo == villaNo);

                // Check if the villa number exists
                if (exisitingVillaNumber == null)
                {
                    return NotFound();
                }

                // Apply the JSON patch to the villa number
                VillaNumberUpdateDTO villaNumberUpdate = _mapper.Map<VillaNumberUpdateDTO>(exisitingVillaNumber);

                jsonPatch.ApplyTo(villaNumberUpdate, ModelState);

                // Validate the updated villa number
                VillaNumber villaNumberModel = _mapper.Map<VillaNumber>(villaNumberUpdate);
                // Update the villa number
                await _villaNumberRepo.UpdateAsync(villaNumberModel);
                return Ok(villaNumberModel);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
                return BadRequest(_response);
            }
        }
    }
}
