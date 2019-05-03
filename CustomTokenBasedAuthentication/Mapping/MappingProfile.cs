using AutoMapper;
using CustomTokenBasedAuthentication.Business.Models;
using CustomTokenBasedAuthentication.Database.Models;

namespace CustomTokenBasedAuthentication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>();
        }
    }
}
