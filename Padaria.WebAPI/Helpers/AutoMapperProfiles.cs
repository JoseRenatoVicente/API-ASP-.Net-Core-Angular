using AutoMapper;
using Padaria.Domain.Entities;
using Padaria.Domain.Entities.Identity;
using Padaria.WebAPI.Dtos;
using System.Linq;


namespace Padaria.WebAPI.Helpers
{
    //Aqui temos que referenciar os domains com os Dtos
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Produto, ProdutoDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Pedido, PedidoDto>().ReverseMap();
            CreateMap<Venda, VendaDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();

        }
    }
}
