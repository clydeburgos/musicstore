using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MusicStore.Data.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Track = new HashSet<Track>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; }
        [IgnoreDataMember]
        public ICollection<Track> Track { get; set; }
    }
}
