using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Models
{
    public class ArtistModel
    {
        public int Id { get; set; }

        public string Stagename { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Country { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
