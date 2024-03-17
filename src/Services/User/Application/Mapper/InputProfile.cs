using AutoMapper;
using User.Application.Dto.Input;

namespace User.Application.Mapper
{
    public class InputProfile : Profile
    {
        public InputProfile()
        {
            CreateMap<CreateUserDto, Domain.User>();
        }
    }
}