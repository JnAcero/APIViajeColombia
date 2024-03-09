using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViajeColombia.BussinesLogic.DTOs;
using ViajeColombia.Models;

namespace ViajeColombia.BussinesLogic.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Journey, JourneyDTO>()
                .ForMember(destiny => destiny.Fligths,
                    opt => opt.MapFrom(origin => origin.JourneyFlights));

            CreateMap<JourneyFlight, FlightDTO>()
                .ForMember(destiny => destiny.Origin,
                opt => opt.MapFrom(origin => origin.Fligth.Origin))
                 .ForMember(destiny => destiny.Destination,
                opt => opt.MapFrom(origin => origin.Fligth.Destination))
                 .ForMember(destiny => destiny.Price,
                opt => opt.MapFrom(origin => origin.Fligth.Price))
                  .ForMember(destiny => destiny.Transport,
                opt => opt.MapFrom(origin => origin.Fligth.Transport));

            CreateMap<Transport, TransportDTO>();
        }
    }
}
