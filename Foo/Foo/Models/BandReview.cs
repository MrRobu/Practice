using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Models
{
    public class BandReview
    {
        private Playlist _band;
        private FooUser _fooUser;

        public int? ID { get; set; }

        public int BandID { get; set; }

        public int FooUserID { get; set; }

        public string Content { get; set; }

        public Playlist Band
        {
            get
            {
                if (_band != null) return _band;

                return _band = Playlist.Find(BandID);
            }
            set
            {
                _band = value;
            }
        }

        public FooUser FooUser
        {
            get
            {
                if (_fooUser != null) return _fooUser;

                return _fooUser = FooUser.Find(FooUserID);
            }
            set
            {
                _fooUser = value;
            }
        }
    }
}
