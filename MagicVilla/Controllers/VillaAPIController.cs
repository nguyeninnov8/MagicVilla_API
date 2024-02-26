using AutoMapper;
using MagicVilla.Data;
using MagicVilla.Models;
using MagicVilla.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public VillaAPIController(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villas = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VillaDTO>>(villas));
        }

        [HttpGet("{Id:int}", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVilla(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }

            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == Id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost(Name = "CreateVilla")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }

            if ((await _db.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null))
            {
                ModelState.AddModelError("Custom Error", "Villa name already exists");
                return BadRequest(ModelState);
            }

            //if (villa.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            Villa model = _mapper.Map<Villa>(createDTO);
            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { Id = model.Id }, model);
        }

        [HttpDelete("{Id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteVilla(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }

            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == Id);

            if (villa == null)
            {
                return NotFound();
            }

            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();

            return Accepted();
        }

        [HttpPut("{Id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<VillaDTO>> UpdateVilla(int Id, [FromBody] VillaUpdateDTO villa)
        {
            if (villa == null)
            {
                return BadRequest(villa);
            }

            if (Id != villa.Id)
            {
                return BadRequest();
            }

            Villa model = _mapper.Map<Villa>(villa);

            _db.Villas.Update(model);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("{Id:int}", Name = "PartiallyUpdateVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<VillaDTO>> PartiallyUpdateVilla(int Id, JsonPatchDocument<VillaUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var existingVilla = await _db.Villas.FirstOrDefaultAsync(v => v.Id == Id);

            VillaUpdateDTO villaUpdateDTO = _mapper.Map<VillaUpdateDTO>(existingVilla);

            if (existingVilla == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(villaUpdateDTO, ModelState);
            
            Villa model = _mapper.Map<Villa>(villaUpdateDTO);
        
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(existingVilla);
        }
    }
}
