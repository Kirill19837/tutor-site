namespace TutorPro.Application.Models
{
    public class FormRequestDTO
    {
        public string SenderName { get; set; } = "";
        public string SenderEmail { get; set; } = "";
        public string SenderPhone { get; set; } = "";
        public string SenderMessage { get; set; } = "";

        public List<string> AdditionalEmail { get; set; }
    }
}
