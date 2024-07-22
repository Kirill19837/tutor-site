namespace TutorPro.Application.Models.UmbracoModel
{
    public class MaterialCard
    {
        public string? Title { get; set; }
        public string? Text { get; set; }
        public List<string>? Tags { get; set; }
        public string? ImageUrl { get; set; }
        public string? Url { get; set; }
        public int ViewsNumber { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
