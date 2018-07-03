using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Models
{
    public class AlbumReview
    {
        private Album _album;
        private FooUser _fooUser;

        public int? ID { get; set; }

        public int AlbumID { get; set; }

        public int FooUserID { get; set; }

        public string Content { get; set; }

        public Album Album
        {
            get
            {
                if (_album != null) return _album;

                return _album = Album.Find(AlbumID);
            }
            set
            {
                _album = value;
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
