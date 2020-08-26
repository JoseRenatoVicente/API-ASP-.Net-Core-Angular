using AutoMapper;
using Padaria.Domain.Entities;
using Padaria.Domain.Entities.Identity;
using Padaria.Application.ViewModels;

namespace Padaria.Application.AutoMapper
{
    //Aqui temos que referenciar os domains com os Dtos
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoPublicDto>().ReverseMap();

            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
            CreateMap<Pedido, PedidoViewModel>().ReverseMap();
            CreateMap<Venda, VendaViewModel>().ReverseMap();

            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, UserLoginViewModel>().ReverseMap();

        }
    }
}
