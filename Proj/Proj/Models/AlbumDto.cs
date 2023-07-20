using System;
using System.Collections.Generic;
using System.Text;

namespace Proj.Models
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }

        public string Name { get; set; }
        public string Artist { get; set; }
        public string Image { get; set; }
    }
}
