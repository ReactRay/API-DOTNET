using System.ComponentModel.DataAnnotations;

namespace NZwalksAPI.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3,ErrorMessage ="Code must be minimum of 3 characters")]
        [MaxLength(3,ErrorMessage ="Code Must be Maximum of 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "name has to be maximum of 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
