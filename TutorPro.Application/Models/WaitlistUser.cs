namespace TutorPro.Application.Models
{
    public class WaitlistUsers
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Message { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
