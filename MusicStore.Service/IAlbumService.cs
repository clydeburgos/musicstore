using MusicStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service
{
    public interface IAlbumService
    {
        Task<IEnumerable<Album>> GetAll();
        Task<Album> Get(int id);
        Task Save(Album album);
        Task Delete(int id);
    }
}
