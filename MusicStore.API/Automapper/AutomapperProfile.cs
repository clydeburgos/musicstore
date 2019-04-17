using AutoMapper;
using MusicStore.Data.Models;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.API.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile() {
            CreateMap<Artist, ArtistModel>().ReverseMap();
            CreateMap<Album, AlbumModel>().ReverseMap();
            CreateMap<Track, TrackModel>().ReverseMap();
            CreateMap<Genre, GenreModel>().ReverseMap();
        }
    }
}
