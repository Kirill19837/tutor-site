namespace TutorPro.Application.Models
{
    public class MaterialCardView
    {
        public string? Title { get; set; }
        public string? Text { get; set; }
        public List<string>? Tags { get; set; }
        public string? ImageUrl { get; set; }
        public string? LinkUrl { get; set; }
        public DateTime? DateOfRealeaseUpdate { get; set; }
        public int NumberOfUse { get; set; }
    }
}
