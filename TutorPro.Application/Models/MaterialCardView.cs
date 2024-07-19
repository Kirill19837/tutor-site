using Newtonsoft.Json;

namespace TutorPro.Application.Models
{
    public class MaterialCardView
    {
        [JsonProperty("name")]
        public string? Title { get; set; }
        [JsonProperty("description")]
        public string? Text { get; set; }
        [JsonProperty("tags")]
        public List<string>? Tags { get; set; }
        [JsonProperty("image")]
        public string? ImageUrl { get; set; }
        [JsonProperty("guid")]        
        public string? Guid { get; set; }
        [JsonProperty("views")]
        public int ViewsNumber { get; set; }
		[JsonProperty("createdAt")]
		public string? CreatedAt { get; set; }
		[JsonProperty("updatedAt")]
		public string? UpdatedAt {  get; set; } 
    }

    public class Result
    {
        [JsonProperty("result")]
        public InnerResult? Results { get; set; }

        [JsonIgnore]
        public List<MaterialCardView>? ResultData => Results?.Data;
    }

    public class InnerResult
    {
        [JsonProperty("data")]
        public List<MaterialCardView>? Data { get; set; }
    }
}
