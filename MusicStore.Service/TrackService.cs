using MusicStore.Data.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Service
{
    public class TrackService : ITrackService
    {
        private MusicStoreDBContext _dbContext;
        public TrackService(MusicStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Track>> GetAll() {
            return await _dbContext.Track
                .Include(t => t.Genre)
                .Include(t => t.Album)
                .Include(t => t.Album.Artist)
                .OrderBy(t => t.Name)
                .Take(50)
                .ToListAsync();
        }

        public async Task<Track> Get(int id) {
            return await _dbContext.Track.SingleOrDefaultAsync(t => t.TrackId == id);
        }

        public async Task Save(Track track) {
            var trackData = await Get(track.TrackId);
            if (trackData == null)
            {
                _dbContext.Add(track);
            }
            else
            {
                //update
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id) {
            var trackData = await Get(id);
            if (trackData != null)
            {
                _dbContext.Track.Remove(trackData);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
