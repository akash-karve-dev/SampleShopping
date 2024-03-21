using AutoMapper;
using User.Application.Dto.Output;

namespace User.Application.Mapper
{
    public class OutputProfile : Profile
    {
        public OutputProfile()
        {
            CreateMap<Domain.User.User, UserResponse>();
        }
    }
}