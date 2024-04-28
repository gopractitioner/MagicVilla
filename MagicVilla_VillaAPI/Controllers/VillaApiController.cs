using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers;

//[Route("api/[controller]")]
[Route("api/VillaApi")]
[ApiController]
public class VillaApiController : ControllerBase
{
    [HttpGet]
    public IEnumerable<VillaDto> GetVillas()
    {
        return new List<VillaDto>
        {
            new VillaDto { Id = 1, Name = "Villa 1" },
            new VillaDto { Id = 2, Name = "Villa 2" }
        };
    }
}