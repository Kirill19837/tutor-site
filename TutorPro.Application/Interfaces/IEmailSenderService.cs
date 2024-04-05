using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorPro.Application.Models;

namespace TutorPro.Application.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(FormRequestDTO formRequest, string subject, CancellationToken cancellation = default, bool isHtml = true);
    }
}
