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
            CreateMap<Contato, ReadContatoDto>().ForMember(
                contatoDto => contatoDto.TipoContato, 
                opt => opt.MapFrom(contato => contato.TipoContato));
        }
    }
}
