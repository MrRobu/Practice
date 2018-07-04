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
    public class Band
    {
        private ObservableCollection<Album> _albums;
        private ObservableCollection<Artist> _artists;
        private ObservableCollection<Genre> _genres;

        #region DB
        public int? ID { get; set; }
        
        public string Name { get; set; }

        public string Origin { get; set; }
        #endregion

        public ObservableCollection<Album> Albums
        {
            get
            {
                if (_albums != null) return _albums;

                try
                {
                    using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        if (ID == null) return null;

                        string sql = $"SELECT Al.ID, AL.Title FROM Band B INNER JOIN BandAlbum BA ON BA.BandID = B.ID INNER JOIN Album Al ON Al.ID = BA.AlbumID WHERE B.ID = {ID};";

                        return _albums = new ObservableCollection<Album>(connection.Query<Album>(sql));
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show($"Class: Band\n Property: Albums\n Error: {error.Message}");
                }

                return null;
            }
            set => _albums = value;
        }

        public ObservableCollection<Artist> Artists
        {
            get
            {
                if (_artists != null) return _artists;

                if (ID == null) return null;

                try
                {
                    using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        string sql = $"SELECT * FROM Band B INNER JOIN BandArtist BA ON BA.BandID = B.ID INNER JOIN Artist A ON A.ID = BA.ArtistID WHERE B.ID = {ID};";

                        return _artists = new ObservableCollection<Artist>(connection.Query<Artist>(sql));
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show($"Class: Band\n Property: Albums\n Error: {error.Message}");
                }

                return null;
            }
            set => _artists = value;
        }

        public ObservableCollection<Genre> Genres
        {
            get
            {
                if (_genres != null) return _genres;

                if (ID == null) return null;

                try
                {
                    using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        

                        string sql = $"SELECT G.ID, G.Title FROM Band B INNER JOIN BandGenre BG ON BG.BandID = B.ID INNER JOIN Genre G ON G.ID = BG.GenreID WHERE B.ID = {ID};";

                        return _genres = new ObservableCollection<Genre>(connection.Query<Genre>(sql));
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show($"Class: Band\n Property: Genres\n Error: {error.Message}");
                }

                return null;
            }
            set => _genres = value;
        }

        public List<BandReview> Reviews
        {
            get
            {
                try
                {
                    using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        if (ID == null) return null;

                        string sql = $"SELECT * FROM BandReview WHERE BandID = {ID};";

                        return new List<BandReview>(connection.Query<BandReview>(sql));
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show($"Class: Band\n Property: Reviews\n Error: {error.Message}");
                }

                return null;
            }
        }

        public bool Delete()
        {
            try
            {
                using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    connection.Execute($"DELETE FROM Band WHERE ID = {ID};");
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show($"Class: Band\n Method: Delete\n Error: {error.Message}");
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
                        @Name = Name,
                        @Origin = Origin
                    };
                        
                    if (ID == 0)
                    {
                        sql = $"INSERT INTO Band(Name, Origin) VALUES ( ?, ? );";
                    }
                    else
                    {
                        sql = $"UPDATE Band SET Name = ?, Origin = ? WHERE ID = {ID};";
                    }

                    connection.Execute(sql,obj);

                    // Artists
                    if(Artists != null)
                    {
                        List<String> values = new List<String>();

                        foreach (var artist in Artists)
                        {
                            values.Add($"({ID}, {artist.ID})");
                        }

                        sql = $"DELETE FROM BandArtist WHERE BandID = {ID}; INSERT INTO BandArtist VALUES {string.Join(",", values)};";

                        connection.Execute(sql);
                    }

                    // Genres
                    if (Genres != null)
                    {
                        List<String> values = new List<String>();

                        foreach (var genre in Genres)
                        {
                            values.Add($"({ID}, {genre.ID})");
                        }

                        sql = $"DELETE FROM BandGenre WHERE BandID = {ID}; INSERT INTO BandGenre VALUES {string.Join(",", values)};";

                        connection.Execute(sql);
                    }
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show($"Class: Band\n Method: Delete\n Error: {error.Message}");
                return false;
            }

            return true;
        }

        public static List<Band> All()
        {
            try
            {
                using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    var bands = connection.Query<Band>("SELECT * FROM Band").ToList();
                    return bands;
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show($"Class: Band\n Method: All\n Error: {error.Message}");
            }

            return null;
        }

        public static Band Find(int id)
        {
            try
            {
                using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    var band = connection.Query<Band>($"SELECT * FROM Band WHERE ID = {id}").First();
                    return band;
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show($"Class: Band\n Method: Find\n Error: {error.Message}");
            }

            return null;
        }
    }
}
