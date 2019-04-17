using MusicStore.Data.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace MusicStore.Service
{
    public class AlbumService : IAlbumService
    {
        private MusicStoreDBContext dbContext;
        public AlbumService(MusicStoreDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Album>> GetAll()
        {
            return await this.dbContext.Album
                .Include(a => a.Track)
                .Include(a => a.Artist)
                .OrderBy(t => t.Title)
                .ToListAsync();
        }

        public async Task<Album> Get(int id)
        {
            return await this.dbContext.Album.SingleOrDefaultAsync(t => t.AlbumId == id);
        }
        public async Task Save(Album album)
        {
            var albumData = await Get(album.AlbumId);
            if (albumData == null)
            {
                this.dbContext.Add(album);
            }
            else
            {
                //update
            }
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var albumData = await Get(id);
            if (albumData != null)
            {
                this.dbContext.Album.Remove(albumData);
                await this.dbContext.SaveChangesAsync();
            }
        }
    }
}
