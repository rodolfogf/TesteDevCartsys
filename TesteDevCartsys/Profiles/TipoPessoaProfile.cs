using AutoMapper;
using TesteDevCartsys.Data.Dtos;
using TesteDevCartsys.Models;

namespace TesteDevCartsys.Profiles
{
    public class TipoPessoaProfile : Profile
    {
        public TipoPessoaProfile()
        {
            CreateMap<CreateTipoPessoaDto, TipoPessoa>();
            CreateMap<UpdateTipoPessoaDto, TipoPessoa>();
            CreateMap<TipoPessoa, ReadTipoPessoaDto>();
        }
    }
}
