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
        private MusicStoreDBContext _dbContext;
        private BingImageSearchService searchService;

        public ArtistService(MusicStoreDBContext dbContext, BingImageSearchService searchService) {
            _dbContext = dbContext;
            this.searchService = searchService;
        }

        public async Task<SearchResultModel> Search(string keyword) {
            var results = await this.searchService.Search(keyword);
            return results;
        }

        public async Task<Artist> Get(int id) {
            return await _dbContext.Artist.SingleOrDefaultAsync(a => a.ArtistId == id);
        }

        public async Task<string> GetArtistPhoto(string artistName) {
            dynamic artistSearch = await Search(artistName);
            var artistFirstResult = Newtonsoft.Json.JsonConvert.DeserializeObject(artistSearch.jsonResult)["value"][0];
            string photo = artistFirstResult["contentUrl"];
            return photo;
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            var data = await _dbContext.Artist
                .Include(t => t.Album)
                .OrderBy(t => t.Name)
                .Take(5)
                .ToListAsync();

            return data.Select(a => new Artist() {
                ArtistId = a.ArtistId,
                Name = a.Name,
                PhotoUrl = string.IsNullOrEmpty(a.PhotoUrl) ? GetArtistPhoto(a.Name).Result : a.PhotoUrl
            });
        }

        public async Task Save(Artist artist) {
            var artistData = await Get(artist.ArtistId);
            if (artistData == null)
            {
                _dbContext.Add(artist);
            }
            else
            {
                //update
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var artistData = await Get(id);
            if (artistData != null)
            {
                _dbContext.Artist.Remove(artistData);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
