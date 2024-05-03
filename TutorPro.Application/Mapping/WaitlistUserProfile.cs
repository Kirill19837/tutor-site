using AutoMapper;
using TutorPro.Application.Models;
using TutorPro.Application.Models.RequestModel;

namespace TutorPro.Application.Mapping
{
    public class WaitlistUserProfile : Profile
    {
        public WaitlistUserProfile()
        {
            CreateMap<AddWailtListUserModel, WaitlistUsers>().ReverseMap();
        }
    }
}
