using AutoMapper;
using TesteDevCartsys.Data.Dtos;
using TesteDevCartsys.Models;

namespace TesteDevCartsys.Profiles
{
    public class TipoContatoProfile : Profile
    {
        public TipoContatoProfile()
        {
            CreateMap<CreateTipoContatoDto, TipoContato>();
            CreateMap<UpdateTipoContatoDto, TipoContato>();
            CreateMap<TipoContato, ReadTipoContatoDto>();
        }
    }
}
