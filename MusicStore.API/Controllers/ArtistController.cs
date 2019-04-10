using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Service;

namespace MusicStore.API.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;
        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [Route("api/artist")]
        [HttpGet]
        public async Task<IActionResult> Search(string keyword)
        {
            dynamic data = await _artistService.Search(keyword);
            var firstItem = Newtonsoft.Json.JsonConvert.DeserializeObject(data.jsonResult)["value"][0];
            return Ok(firstItem);
        }

        [Route("api/artists")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _artistService.GetAll();
            return Ok(data);
        }
    }
}