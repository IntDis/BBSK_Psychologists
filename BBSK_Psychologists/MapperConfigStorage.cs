using AutoMapper;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Requests;
using BBSK_Psycho.Models.Responses;
namespace BBSK_Psycho;

public class MapperConfigStorage: Profile
{
	public MapperConfigStorage()
	{
		CreateMap<ClientRegisterRequest, Client>();
		CreateMap<Client, ClientResponse>();
		CreateMap<Comment, CommentResponse>();

        CreateMap<OrderCreateRequest, Order>()
            .ForMember(dest => dest.Client, opt => opt.Ignore())
            .ForMember(dest => dest.Psychologist, opt => opt.Ignore());

        CreateMap<Order, OrderResponse>()
            .ForMember(dest => dest.psychologistResponse, opt => opt.MapFrom(src => src.Psychologist));

        CreateMap<Psychologist, PsychologistResponse>();

        CreateMap<OrderCreateRequest, Order>();

        CreateMap<Order, AllOrdersResponse>();

        CreateMap<ApplicationForPsychologistSearch, SearchResponse>();
        CreateMap<SearchRequest, ApplicationForPsychologistSearch>();
    }
}
