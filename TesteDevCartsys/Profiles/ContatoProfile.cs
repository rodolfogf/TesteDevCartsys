using AutoMapper;
using TesteDevCartsys.Data.Dtos;
using TesteDevCartsys.Models;

namespace TesteDevCartsys.Profiles
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            CreateMap<CreateContatoDto, Contato>();
            CreateMap<UpdateContatoDto, Contato>();
            CreateMap<Contato, UpdateContatoDto>();
            CreateMap<Contato, ReadContatoDto>();
        }
    }
}
