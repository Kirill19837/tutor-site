namespace TutorPro.Application.Models.ResponseModel
{
    public class BlogResponse
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<BlogView> Blogs { get; set; } = new List<BlogView>();
    }
}
