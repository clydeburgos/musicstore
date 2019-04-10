using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Service;

namespace MusicStore.API.Controllers
{
    public class TrackController : Controller
    {
        private readonly ITrackService _trackService;
        public TrackController(ITrackService trackService) {
            _trackService = trackService;
        }

        [Route("api/tracks")]
        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _trackService.GetAll();
            return Ok(data);
        }

    }
}