namespace TutorPro.Application.Models.RequestModel
{
    public class GetMaterialsRequestModel
    {
        public string? SearchText { get; set; }
        public string? Subject { get; set; }
        public List<CategoryItem>? CategoryItems { get; set; }
        public int Sort { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }

    public class CategoryItem
    {
        public string? Category { get; set; }
        public List<string>? Items { get; set; }
    }

}
