using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Data.Models;
using MusicStore.Models;
using MusicStore.Service;

namespace MusicStore.API.Controllers
{
    public class TrackController : Controller
    {
        private readonly ITrackService trackService;
        private readonly IMapper mapper;
        public TrackController(ITrackService trackService, IMapper mapper) {
            this.trackService = trackService;
            this.mapper = mapper;
        }

        [Route("api/tracks")]
        //[EnableQuery(PageSize = 10)]
        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await this.trackService.GetAll();
            return Ok(data);
        }

    }
}