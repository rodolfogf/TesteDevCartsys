using AutoMapper;
using TesteDevCartsys.Controllers.Dtos;
using TesteDevCartsys.Models;

namespace TesteDevCartsys.Profiles
{
    public class TesteDevCartsysProfile : Profile
    {
        public TesteDevCartsysProfile()
        {
            CreateMap<CreateContatoDto, Contato>();
            CreateMap<UpdateContatoDto, Contato>();
        }
    }
}
