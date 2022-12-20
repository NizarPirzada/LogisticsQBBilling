using AutoMapper;
using FTData.Model.Entities;
using FTDTO.DriverType;
using FTDTO.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineToonAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Don't use ForMember method if you want to show Id's
            CreateMap<DriverType, DriverTypeDTO>()
               .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<Ticket, TicketDTO>()
               .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<VwTicketLineItem, TicketLineItemDto>()
              .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}
