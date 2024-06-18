using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionDto
    {
        [MinLength(3,ErrorMessage ="Code has to be minimum of 3 characters.")]
		[MaxLength(3, ErrorMessage = "Code has to be maximum of 3 characters.")]
		public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
