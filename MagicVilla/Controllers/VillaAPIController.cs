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
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.Villas);
        }

        [HttpGet("{Id:int}", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<VillaDTO>> GetVilla(int Id)
        {
            if(Id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.Villas.FirstOrDefault(v => v.Id == Id);

            if(villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villa)
        {
            if (villa == null)
            {
                return BadRequest(villa);
            }

            if(VillaStore.Villas.FirstOrDefault(u => u.Name.Equals(villa.Name, StringComparison.CurrentCultureIgnoreCase)) == null)
            {
                ModelState.AddModelError("Custom Error", "Villa name already exists");
                return BadRequest(ModelState);
            }

            if (villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villa.Id = VillaStore.Villas.OrderByDescending(u => u.Id).First().Id + 1;

            VillaStore.Villas.Add(villa);

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

            var villa = VillaStore.Villas.FirstOrDefault(v => v.Id == Id);

            if (villa == null)
            {
                return NotFound();
            }

            VillaStore.Villas.Remove(villa);

            return Accepted();
        }

        [HttpPut("{Id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<VillaDTO> UpdateVilla(int Id, [FromBody]VillaDTO villa)
        {
            if (villa == null)
            {
                return BadRequest(villa);
            }

            if (Id != villa.Id)
            {
                return BadRequest();
            }

            var existingVilla = VillaStore.Villas.FirstOrDefault(v => v.Id == Id);

            if (existingVilla == null)
            {
                return NotFound();
            }

            existingVilla.Name = villa.Name;
            existingVilla.Occupancy = villa.Occupancy;
            existingVilla.Sqft = villa.Sqft;

            return Ok(existingVilla);
        }

        [HttpPatch("{Id:int}", Name = "PartiallyUpdateVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<VillaDTO> PartiallyUpdateVilla(int Id,  JsonPatchDocument<VillaDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var existingVilla = VillaStore.Villas.FirstOrDefault(v => v.Id == Id);

            if (existingVilla == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(existingVilla, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(existingVilla);
        }
    }
}
