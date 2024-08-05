using AutoMapper;
using TesteDevCartsys.Data.Dtos;
using TesteDevCartsys.Models;

namespace TesteDevCartsys.Profiles
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<CreatePessoaDto, Pessoa>();
            CreateMap<UpdatePessoaDto, Pessoa>();
            CreateMap<Pessoa, UpdatePessoaDto>();
            CreateMap<Pessoa, ReadPessoaDto>();
        }
    }
}
