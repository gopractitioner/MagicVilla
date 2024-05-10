using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers;

//[Route("api/[controller]")]
[Route("api/VillaApi")]
[ApiController]
public class VillaApiController : ControllerBase
{
    private readonly ILogging _Logger;
    public VillaApiController(ILogging logger)
    {
        _Logger = logger;
    }
  
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDto>> GetVillas()
    {
        _Logger.Log("Getting all villas", "");
        return Ok(VillaStore.VillaList);
    }

    [HttpGet("{id}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDto> GetVilla(int id)
    {
        if (id == 0)
        {
            _Logger.Log($"Get villa Error: Id is {id}", "error");
            return BadRequest();
        }

        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        return Ok(villa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<VillaDto> AddVilla([FromBody] VillaDto villaDto)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        if (villaDto == null)
        {
            return BadRequest(villaDto);
        }

        if (villaDto.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        villaDto.Id = VillaStore.VillaList.Max(v => v.Id) + 1;
        //villa.Id = VillaStore.VillaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
        VillaStore.VillaList.Add(villaDto);
        return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
        //    the 2nd parameter should be an object  return CreatedAtRoute("GetVilla", villaDto.Id, villaDto);
    }
    [HttpDelete("{id}", Name = "DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        VillaStore.VillaList.Remove(villa);
        return NoContent();
    }

    [HttpPut("{id}", Name = "UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
    {
        if (villaDto == null || id != villaDto.Id)
        {
            return BadRequest();
        }
        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
        villa.Name = villaDto.Name;
        villa.Sqft = villaDto.Sqft;
        villa.Occupancy = villaDto.Occupancy;
        return NoContent();
    }
    [HttpPatch("{id}", Name = "PatchVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult PatchVilla(int id, JsonPatchDocument<VillaDto> patchDto)
    {
        if (patchDto == null || id == 0)
        {
            return BadRequest();
        }
        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
        if (villa == null)
        {
            return BadRequest();
        }
        patchDto.ApplyTo(villa, ModelState);
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }
        return NoContent();
    }
}