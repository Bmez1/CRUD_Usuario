using AutoMapper;
using User.Domain.Dtos.Inputs;

namespace User.Domain.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserCreateInputDto, Entities.User>();   
        }
    }
}
