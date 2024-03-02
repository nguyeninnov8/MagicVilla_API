using System.ComponentModel.DataAnnotations;

namespace MagicVilla.Models.Dto
{
    public class VillaNumberUpdateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int villaId { get; set; }
        [Required]
        public string SpecialDetails { get; set; }
    }
}
