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
    public class Track
    {
        private Genre _genre;
        private Album _album;

        #region DB
        public int? ID { get; set; }

        public string Title { get; set; }

        public float? Length { get; set; }

        public int? AlbumID { get; set; }

        public int? GenreID { get; set; }
        #endregion

        public Genre Genre
        {
            get
            {
                if (_genre != null) return _genre;

                if (GenreID == null) return null;

                try
                {
                    using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        return _genre = Genre.Find((int)GenreID);
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show($"Class: Track\n Property: Genre\n Error: {error.Message}");
                }

                return null;
            }
            set => _genre = value;
        }

        public Album Album
        {
            get
            {
                if (_album != null) return _album;

                if (AlbumID == null) return null;

                try
                {
                    using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        return _album = Album.Find((int)AlbumID);
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show($"Class: Track\n Property: Album\n Error: {error.Message}");
                }

                return null;
            }
            set => _album = value;
        }

        public static List<Track> Available()
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    string sql =
                        "SELECT * FROM Track WHERE ID NOT IN (SELECT DISTINCT TrackID FROM AlbumTrack)" +
                        "AND ID NOT IN (SELECT DISTINCT TrackID FROM AlbumTrack)";

                    var tracks = connection.Query<Track>(sql).ToList();
                    return tracks;
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }

            return null;
        }

        public static List<Track> All()
        {
            try
            {
                using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    var tracks = connection.Query<Track>("SELECT * FROM Track").ToList();
                    return tracks;
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }

            return null;
        }

        public static Track Find(int id)
        {
            try
            {
                using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    var track = connection.Query<Track>($"SELECT * FROM Track WHERE ID = {id}").First();
                    return track;
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show($"Class: Track\nMethod: Find\nError: {error.Message}");
            }

            return null;
        }

        public bool Delete()
        {
            try
            {
                using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    connection.Execute($"DELETE FROM Track WHERE ID = {ID};");
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show($"Class: Track\nMethod: Delete\nError: {error.Message}");
                return false;
            }

            return true;
        }

        public bool Save()
        {
            try
            {
                using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    string sql;

                    object obj = new
                    {
                        @Title = Title,
                        @Length = Length,
                        @AlbumID = Album?.ID,
                        @GenreID = Genre?.ID
                    };

                    if (ID == 0)
                    {
                        sql = $"INSERT INTO Track VALUES ( ?, ?, ?, ?);";                        
                    }
                    else
                    {
                        sql = $"UPDATE Track SET Title = ?, Length = ?, AlbumID = ?, GenreID = ? WHERE ID = {ID};";
                    }

                    connection.Execute(sql, obj);
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show($"Class: Track\nMethod: Save\nError: {error.Message}");
                return false;
            }

            return true;
        }
    }
}
