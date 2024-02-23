using MagicVilla.Models.Dto;

namespace MagicVilla.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> Villas = new List<VillaDTO>
        {
            new VillaDTO { Id = 1, Name = "Villa 1" },
            new VillaDTO { Id = 2, Name = "Villa 2" }
        };
    }
}
