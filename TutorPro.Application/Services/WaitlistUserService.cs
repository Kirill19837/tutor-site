using Microsoft.Extensions.Logging;

namespace TutorPro.Application.Services
{
    public class WaitlistUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WaitlistUserService> _logger;

        public WaitlistUserService(ApplicationDbContext context, ILogger<WaitlistUserService> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
