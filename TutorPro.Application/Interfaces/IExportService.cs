using Microsoft.AspNetCore.Mvc;
using TutorPro.Application.Models;

namespace TutorPro.Application.Interfaces
{
    public interface IExportService
    {
        Task<MemoryStream> ExportToExcel(List<WaitlistUsers> users);
    }
}
