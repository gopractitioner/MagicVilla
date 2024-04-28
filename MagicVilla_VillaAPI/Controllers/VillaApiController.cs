using MagicVilla_VillaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillaApiController : ControllerBase
{
    public IEnumerable<Villa> GetVillas()
    {
        return new List<Villa>
        {
            new Villa { Id = 1, Name = "Villa 1" },
            new Villa { Id = 2, Name = "Villa 2" }
        };
    }
}