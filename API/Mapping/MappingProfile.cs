using API.Param.Setup;
using API.RequestModel.Setup;
using AutoMapper;
namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<QuestionSetupParam, QuestionSetupRequestModel>().ReverseMap();

        }
    }
}
