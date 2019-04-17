﻿using System;
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
    public class ArtistController : Controller
    {
        private readonly IArtistService artistService;
        private readonly IMapper mapper;
        public ArtistController(IArtistService artistService, IMapper mapper)
        {
            this.artistService = artistService;
            this.mapper = mapper;
        }

        [Route("api/artist")]
        [HttpGet]
        public async Task<IActionResult> Search(string artist)
        {
            dynamic data = await this.artistService.Search(artist);
            var firstItem = Newtonsoft.Json.JsonConvert.DeserializeObject(data.jsonResult)["value"][0];
            return Ok(firstItem);
        }
        [EnableQuery]
        [Route("api/artists")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await this.artistService.GetAll();
            var dataModel = this.mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistModel>>(data);
            return Ok(dataModel);
        }
    }
}