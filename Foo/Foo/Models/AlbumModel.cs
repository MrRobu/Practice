using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Models
{
    class AlbumModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ArtistId { get; set; }

        public int BandId { get; set; }
    }
}
