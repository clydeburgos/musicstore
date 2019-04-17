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
        private MusicStoreDBContext dbContext;
        public TrackService(MusicStoreDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Track>> GetAll() {
            return await this.dbContext.Track
                .Include(t => t.Genre)
                .Include(t => t.Album)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<Track> Get(int id) {
            return await this.dbContext.Track.SingleOrDefaultAsync(t => t.TrackId == id);
        }

        public async Task Save(Track track) {
            var trackData = await Get(track.TrackId);
            if (trackData == null)
            {
                this.dbContext.Add(track);
            }
            else
            {
                //update
            }
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id) {
            var trackData = await Get(id);
            if (trackData != null)
            {
                this.dbContext.Track.Remove(trackData);
                await this.dbContext.SaveChangesAsync();
            }
        }
    }
}
