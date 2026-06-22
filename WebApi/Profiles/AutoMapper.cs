using AutoMapper;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Profiles
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Usuario, UsuarioListarDto>().ReverseMap();
        }
    }
}
