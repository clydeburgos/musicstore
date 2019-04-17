using MusicStore.Data.Models;
using MusicStore.Models;
using MusicStore.Service.AzureServices;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service
{
    public class ArtistService : IArtistService
    {
        private MusicStoreDBContext dbContext;
        private BingImageSearchService searchService;

        public ArtistService(MusicStoreDBContext dbContext, BingImageSearchService searchService) {
            this.dbContext = dbContext;
            this.searchService = searchService;
        }

        public async Task<SearchResultModel> Search(string keyword) {
            var results = await this.searchService.Search(keyword);
            return results;
        }

        public async Task<Artist> Get(int id) {
            return await this.dbContext.Artist.SingleOrDefaultAsync(a => a.ArtistId == id);
        }

        public async Task<string> GetArtistPhoto(string artistName) {
            dynamic artistSearch = await Search(artistName);
            var artistFirstResult = Newtonsoft.Json.JsonConvert.DeserializeObject(artistSearch.jsonResult)["value"][0];
            string photo = artistFirstResult["contentUrl"];
            return photo;
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            return await this.dbContext.Artist
                .Include(a => a.Album)
                .Include("Album.Track")
                .ToListAsync();
        }

        public async Task Save(Artist artist) {
            var artistData = await Get(artist.ArtistId);
            if (artistData == null)
            {
                this.dbContext.Add(artist);
            }
            else
            {
                //update
            }
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var artistData = await Get(id);
            if (artistData != null)
            {
                this.dbContext.Artist.Remove(artistData);
                await this.dbContext.SaveChangesAsync();
            }
        }
    }
}
