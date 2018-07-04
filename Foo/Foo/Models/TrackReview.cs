using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Models
{
    public class TrackReview
    {
        private Track _track;
        private FooUser _fooUser;

        public int? ID { get; set; }

        public int TrackID { get; set; }

        public int FooUserID { get; set; }

        public string Content { get; set; }

        public Track Track
        {
            get
            {
                if (_track != null) return _track;

                return _track = Track.Find(TrackID);
            }
            set
            {
                _track = value;
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
