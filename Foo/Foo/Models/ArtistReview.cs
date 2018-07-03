using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Foo.Models
{
    public class ArtistReview
    {
        private Artist _artist;
        private FooUser _fooUser;

        public int? ID { get; set; }

        public int ArtistID { get; set; }

        public int FooUserID { get; set; }

        public string Content { get; set; }

        public Artist Artist
        {
            get
            {
                if (_artist != null) return _artist;

                return _artist = Artist.Find(ArtistID);
            }
            set
            {
                _artist = value;
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
