using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Data;

public class VillaStore
{
    public static List<VillaDto> VillaList= new List<VillaDto>
    {
        new VillaDto { Id = 1, Name = "Villa 1", Sqft = 100, Occupancy = 4},
        new VillaDto { Id = 2, Name = "Villa 2", Sqft = 100, Occupancy = 3}
    };
}