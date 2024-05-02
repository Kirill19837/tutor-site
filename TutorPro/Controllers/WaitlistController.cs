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

        public WaitlistController(IWaitlistUserService waitlistUserService)
        {
            _waitlistUserService = waitlistUserService;
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

        [HttpPost]
        public async Task<ActionResult> AddWaitlistUser([FromBody] AddWailtListUserModel model)
        {
            await _waitlistUserService.AddWaitlistUser(model);

            return Ok();
        }

        [HttpPost]
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

        [HttpDelete]
        public async Task<ActionResult> HardRemoveWaitlistUser(int id)
        {
            await _waitlistUserService.HardRemoveWaitlistUserById(id);

            return Ok();
        }
    }
}
