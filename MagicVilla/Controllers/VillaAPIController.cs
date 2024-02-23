using MagicVilla.Data;
using MagicVilla.Models;
using MagicVilla.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDBContext _db;

        public VillaAPIController(ApplicationDBContext db)
        {
            _db = db;
        }


        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(_db.Villas.ToList());
        }

        [HttpGet("{Id:int}", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<VillaDTO>> GetVilla(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }

            var villa = _db.Villas.FirstOrDefault(v => v.Id == Id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villa)
        {
            if (villa == null)
            {
                return BadRequest(villa);
            }

            if (_db.Villas.FirstOrDefault(u => u.Name.Equals(villa.Name, StringComparison.CurrentCultureIgnoreCase)) == null)
            {
                ModelState.AddModelError("Custom Error", "Villa name already exists");
                return BadRequest(ModelState);
            }

            if (villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Villa model = new Villa()
            {
                Amenity = villa.Amenity ?? "",
                Details = villa.Details ?? "",
                ImageUrl = villa.ImageUrl ?? "",
                Name = villa.Name ?? "",
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft
            };

            _db.Villas.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetVilla", new { Id = villa.Id }, villa);
        }

        [HttpDelete("{Id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult DeleteVilla(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }

            var villa = _db.Villas.FirstOrDefault(v => v.Id == Id);

            if (villa == null)
            {
                return NotFound();
            }

            _db.Villas.Remove(villa);

            return Accepted();
        }

        [HttpPut("{Id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<VillaDTO> UpdateVilla(int Id, [FromBody] VillaDTO villa)
        {
            if (villa == null)
            {
                return BadRequest(villa);
            }

            if (Id != villa.Id)
            {
                return BadRequest();
            }

            Villa model = new Villa()
            {
                Amenity = villa.Amenity ?? "",
                Details = villa.Details ?? "",
                ImageUrl = villa.ImageUrl ?? "",
                Name = villa.Name ?? "",
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft
            };

            _db.Villas.Update(model);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPatch("{Id:int}", Name = "PartiallyUpdateVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<VillaDTO> PartiallyUpdateVilla(int Id, JsonPatchDocument<VillaDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var existingVilla = _db.Villas.FirstOrDefault(v => v.Id == Id);

            VillaDTO villaDTO = new()
            {
                Amenity = existingVilla.Amenity ?? "",
                Details = existingVilla.Details ?? "",
                ImageUrl = existingVilla.ImageUrl ?? "",
                Name = existingVilla.Name ?? "",
                Occupancy = existingVilla.Occupancy,
                Rate = existingVilla.Rate,
                Sqft = existingVilla.Sqft
            };

            if (existingVilla == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(villaDTO, ModelState);

            Villa model = new Villa()
            {
                Amenity = villaDTO.Amenity ?? "",
                Details = villaDTO.Details ?? "",
                ImageUrl = villaDTO.ImageUrl ?? "",
                Name = villaDTO.Name ?? "",
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };

            _db.Villas.Update(model);
            _db.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(existingVilla);
        }
    }
}
