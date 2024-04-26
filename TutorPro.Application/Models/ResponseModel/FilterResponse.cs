namespace TutorPro.Application.Models.ResponseModel
{
    public class FilterResponse
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<MaterialCardView>? Materials { get; set; }
    }
}
