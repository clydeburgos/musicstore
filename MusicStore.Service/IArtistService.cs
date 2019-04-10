using MusicStore.Data.Models;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service
{
    public interface IArtistService
    {
        Task<SearchResultModel> Search(string keyword);
        Task<string> GetArtistPhoto(string artistName);
        Task<IEnumerable<Artist>> GetAll();
        Task<Artist> Get(int id);
        Task Save(Artist artist);
        Task Delete(int id);
    }
}
