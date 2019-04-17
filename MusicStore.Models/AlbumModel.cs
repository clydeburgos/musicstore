using System.Collections.Generic;

namespace MusicStore.Models
{
    public class AlbumModel
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }

        public ArtistModel Artist { get; set; }
        public IEnumerable<TrackModel> Track { get; set; }
    }
}