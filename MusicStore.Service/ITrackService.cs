using MusicStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service
{
    public interface ITrackService
    {
        Task<IEnumerable<Track>> GetAll();
        Task Save(Track track);
        Task Delete(int id);
    }
}
