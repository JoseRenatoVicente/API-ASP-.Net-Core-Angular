using AutoMapper;
using Padaria.Domain;
using Padaria.WebAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
             /*.ForMember(dest => dest.Produto, opt => {
              opt.MapFrom(src => src.))
                */
            CreateMap<Venda, VendaDto>().ReverseMap();
               
        }
    }
}
