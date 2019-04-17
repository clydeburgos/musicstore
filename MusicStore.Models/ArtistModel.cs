using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore.Models
{
    public class ArtistModel
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public IEnumerable<AlbumModel> Album { get; set; }
    }
}
