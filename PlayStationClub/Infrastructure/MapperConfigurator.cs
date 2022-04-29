using AutoMapper;
using PlayStationClub.Data.Entity;
using PlayStationClub.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayStationClub.Infrastructure
{
    public class MapperConfigurator : Profile
    {
        public MapperConfigurator()
        {
            CreateMap<Image, ImageViewModel>().ReverseMap();
            CreateMap<Room, RoomViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Game, GameViewModel>().ReverseMap();
        }
    }
}
