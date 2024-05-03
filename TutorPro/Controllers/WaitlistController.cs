using Microsoft.AspNetCore.Mvc;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;
using TutorPro.Application.Models.RequestModel;
using Umbraco.Cms.Web.Common.Controllers;

namespace TutorPro.Controllers
{
    [Route("api/[controller]")]
    public class WaitlistController : UmbracoApiController
    {
        private readonly IWaitlistUserService _waitlistUserService;
        private readonly IExportService _exportService;
        private readonly ILogger<WaitlistController> _logger;

        public WaitlistController(IWaitlistUserService waitlistUserService, IExportService exportService, ILogger<WaitlistController> logger)
        {
            _waitlistUserService = waitlistUserService;
            _exportService = exportService;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<WaitlistUsers>>> GetWaitlistUsers()
        {
           return Ok(await _waitlistUserService.GetWaitlistUsers());
        }

        [HttpGet("deleted")]
        public async Task<ActionResult<List<WaitlistUsers>>> GetDeletedWaitlistUsers()
        {
            return Ok(await _waitlistUserService.GetDeletedWaitlistUsers());
        }

        [HttpPost("")]
        public async Task<ActionResult> AddWaitlistUser([FromBody] AddWailtListUserModel model)
        {
            await _waitlistUserService.AddWaitlistUser(model);

            return Ok();
        }

        [HttpPost("remove")]
        public async Task<ActionResult> RemoveWaitlistUser(int id)
        {
            await _waitlistUserService.RemoveWaitlistUserById(id);

            return Ok();
        }

        [HttpPost("range_remove")]
        public async Task<ActionResult> RemoveWaitlistUserRange([FromBody] int[] ids)
        {
            await _waitlistUserService.RemoveWaitlistUserByIdRange(ids.ToList());

            return Ok();
        }

        [HttpPost("hard_remove")]
        public async Task<ActionResult> HardRemoveWaitlistUser(int id)
        {
            await _waitlistUserService.HardRemoveWaitlistUserById(id);

            return Ok();
        }

        [HttpPost("hard_range_remove")]
        public async Task<ActionResult> HardRemoveWaitlistUserRange([FromBody] int[] ids)
        {
            await _waitlistUserService.HardRemoveWaitlistUserByIdRange(ids.ToList());

            return Ok();
        }

        [HttpPost("range_restore")]
        public async Task<ActionResult> RestoreWaitlistUserRange([FromBody] int[] ids)
        {
            await _waitlistUserService.RestoreWaitlistUserByIdRange(ids.ToList());

            return Ok();
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportWaitlistUsers()
        {
            try
            {
                var users = await _waitlistUserService.GetWaitlistUsers();
                var fileStream = await _exportService.ExportToExcel(users);
              
                if (fileStream == null || fileStream.Length == 0)
                {
                    return NotFound();
                }
                
                return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "waitlist_users.xlsx");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting waitlist users");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
