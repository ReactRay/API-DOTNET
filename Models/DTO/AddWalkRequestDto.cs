using System.ComponentModel.DataAnnotations;

namespace NZwalksAPI.Models.DTO
{
    public class AddWalkRequestDto
    {

        [Required]
        [MaxLength(100, ErrorMessage = "name too long")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Description is too long")]
        public string Description { get; set; }

        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
