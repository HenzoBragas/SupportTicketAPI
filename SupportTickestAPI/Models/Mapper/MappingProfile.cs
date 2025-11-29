using AutoMapper;
using SupportTickestAPI.Models.DTO;
using SupportTickestAPI.Models.Ticket;
using SupportTickestAPI.Models.User;

namespace SupportTickestAPI.Models.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TicketRequest, TicketsModel>();
        CreateMap<TicketsModel, TicketResponse>();
        
        
        CreateMap<UserRequest, UserModel>();
        CreateMap<UserModel, UserResponse>();

    }
    
}