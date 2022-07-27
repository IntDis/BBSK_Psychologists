using AutoMapper;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Requests;
using BBSK_Psycho.Models.Responses;

namespace BBSK_Psycho;

public class MapperConfigStorage : Profile
{
    public MapperConfigStorage()
    {
        CreateMap<ClientRegisterRequest, Client>();
        CreateMap<Client, ClientResponse>();
        CreateMap<Comment, CommentResponse>();
        CreateMap<Psychologist, PsychologistResponse>();
        CreateMap<Psychologist, GetAllPsychologistsResponse>();
        CreateMap<AddPsychologistRequest, Psychologist>()
            .ForMember(x => x.Educations, s => s.MapFrom(x => x.Educations!.Select(education => new Education() { EducationData = education })))
            .ForMember(x => x.Problems, s => s.MapFrom(x => x.Problems!.Select(problemName => new Problem() { ProblemName = problemName })))
            .ForMember(x => x.TherapyMethods, s => s.MapFrom(x => x.TherapyMethods!.Select(therapyMethod => new TherapyMethod() { Method = therapyMethod })));
        CreateMap<UpdatePsychologistRequest, Psychologist>()
            .ForMember(x => x.Educations, s => s.MapFrom(x => x.Educations!.Select(education => new Education() { EducationData = education })))
            .ForMember(x => x.Problems, s => s.MapFrom(x => x.Problems!.Select(problemName => new Problem() { ProblemName = problemName })))
            .ForMember(x => x.TherapyMethods, s => s.MapFrom(x => x.TherapyMethods!.Select(therapyMethod => new TherapyMethod() { Method = therapyMethod })));
        CreateMap<Comment, GetCommentsByPsychologistIdResponse>();
        CreateMap<CommentRequest, Comment>()
            .ForMember(x => x.Psychologist, s => s.MapFrom((x => (new Psychologist { Id= x.PsychologistId}))))
            .ForMember(x => x.Client, s => s.MapFrom((x => (new Client { Id= x.ClientId}))));
        CreateMap<Comment, CommentResponse>();

        CreateMap<OrderCreateRequest, Order>();
        CreateMap<OrderResponse, Order>();

        CreateMap<OrderCreateRequest, Client>()

            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientId));

        CreateMap<OrderCreateRequest, Psychologist>()

            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PsychologistId));

    }
}
