using Dapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Foo.Models
{
    public class Album
    {
        private ObservableCollection<Track> _tracks;
        private Artist _artist;
        private Band _band;
        private string _owner;

        #region DB
        public int? ID { get; set; }

        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }
        #endregion

        public ObservableCollection<Track> Tracks
        {
            get
            {
                if (_tracks != null) return _tracks;

                try
                {
                    using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        if (ID == null) return null;

                        string sql = $"SELECT T.* FROM Track T INNER JOIN AlbumTrack AT ON AT.TrackID = T.ID INNER JOIN Album Al ON Al.ID = AT.AlbumID WHERE Al.ID = {ID};";

                        return _tracks = new ObservableCollection<Track>(connection.Query<Track>(sql));
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show($"Class: Album\n Property: Tracks\n Error: {error.Message}");
                }

                return null;
            }
            set
            {
                _tracks = value;
            }
        }

        public Artist Artist
        {
            get
            {
                if (_artist != null) return _artist;

                try
                {
                    using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        string sql = $"SELECT A.* FROM Artist A INNER JOIN ArtistAlbum AA ON AA.ArtistID = A.ID INNER JOIN Album Al ON Al.ID = AA.AlbumID WHERE Al.ID = {ID} LIMIT 1";

                        return _artist = connection.Query<Artist>(sql).FirstOrDefault();
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show($"Class: Album\n Property: Artist\n Error: {error.Message}");
                }

                return null;
            }
            set => _artist = value;
        }

        public Band Band
        {
            get
            {
                if (_band != null) return _band;

                try
                {
                    using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        string sql = $"SELECT B.* FROM Band B INNER JOIN BandAlbum BA ON BA.BandID = B.ID INNER JOIN Album Al ON Al.ID = BA.AlbumID WHERE Al.ID = {ID} LIMIT 1";

                        return _band = connection.Query<Band>(sql).FirstOrDefault();
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show($"Class: Album\n Property: Band\n Error: {error.Message}");
                }

                return null;
            }
            set => _band = value;
        }

        public String Owner
        {
            get
            {
                if (Artist != null)
                    return Artist.StageName;
                else if (Band != null)
                    return Band.Name;
                else
                    return null;
            }
            set
            {
                _owner = value;
            }
        }

        public List<AlbumReview> Reviews
        {
            get
            {
                try
                {
                    using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        if (ID == null) return null;

                        string sql = $"SELECT * FROM AlbumReview WHERE AlbumID = {ID};";

                        return new List<AlbumReview>(connection.Query<AlbumReview>(sql));
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show($"Class: Album\n Property: Reviews\n Error: {error.Message}");
                }

                return null;
            }
        }

        public static List<Album> Available()
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    string sql = 
                        "SELECT * FROM Album WHERE ID NOT IN (SELECT DISTINCT AlbumID FROM BandAlbum)" +
                        "AND ID NOT IN (SELECT DISTINCT AlbumID FROM ArtistAlbum)";

                    var albums = connection.Query<Album>(sql).ToList();
                    return albums;
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }

            return null;
        }

        public static List<Album> All()
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    var albums = connection.Query<Album>("SELECT * FROM Album").ToList();
                    return albums;
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }

            return null;
        }

        public static Album Find(int id)
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    var album = connection.Query<Album>($"SELECT * FROM Album WHERE ID = {id}").First();
                    return album;
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }

            return null;
        }

        public bool Delete()
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    connection.Execute($"DELETE FROM Album WHERE ID = {ID};");
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
                return false;
            }

            return true;
        }

        public bool Save()
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    if (ID == 0)
                    {
                        connection.Execute($"INSERT INTO Album(Title, ReleaseDate) VALUES ('{Title}', '{ReleaseDate?.ToShortDateString()}');");
                    }
                    else
                    {
                        connection.Execute($"UPDATE Album SET Title = '{Title}', ReleaseDate = '{ReleaseDate?.ToShortDateString()}';");
                    }

                    connection.Execute($"");
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
                return false;
            }

            return true;
        }
    }
}
