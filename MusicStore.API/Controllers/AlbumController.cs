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
    public class AlbumController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly IMapper mapper;

        public AlbumController(IAlbumService albumService, IMapper mapper) {
            this.albumService = albumService;
            this.mapper = mapper;
        }

        [Route("api/albums")]
        [EnableQuery(MaxTop = 50, PageSize = 10)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await this.albumService.GetAll();
            var dataModel = this.mapper.Map<IEnumerable<Album>, IEnumerable<AlbumModel>>(data);
            return Ok(dataModel);
        }
    }
}