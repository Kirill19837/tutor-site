using TutorPro.Application.Models;
using TutorPro.Application.Models.RequestModel;

namespace TutorPro.Application.Interfaces
{
    public interface IWaitlistUserService
    {
        Task<List<WaitlistUsers>> GetWaitlistUsers();
        Task<List<WaitlistUsers>> GetDeletedWaitlistUsers();
        Task AddWaitlistUser(AddWailtListUserModel model);
        Task HardRemoveWaitlistUserByIdRange(List<int> ids);
        Task RemoveWaitlistUserByIdRange(List<int> ids);
        Task RemoveWaitlistUserById(int id);
        Task HardRemoveWaitlistUserById(int id);
        Task RestoreWaitlistUserByIdRange(List<int> ids);
    }
}
