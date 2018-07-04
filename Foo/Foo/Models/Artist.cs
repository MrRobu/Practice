using Caliburn.Micro;
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
    public class Artist
    {
        private ObservableCollection<Album> _albums;
        private ObservableCollection<Band> _bands;
        private ObservableCollection<Genre> _genres;

        public int? ID { get; set; }

        public string StageName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Origin { get; set; }

        public DateTime? Birthdate { get; set; }

        public string Name
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public List<ArtistReview> Reviews
        {
            get
            {
                try
                {
                    using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        if (ID == null) return null;

                        string sql = $"SELECT * FROM ArtistReview WHERE ArtistID = {ID};";

                        return new List<ArtistReview>(connection.Query<ArtistReview>(sql));
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show(error.Message);
                }

                return null;
            }
        }

        public ObservableCollection<Album> Albums
        {
            get
            {
                if (_albums != null) return _albums;

                try
                {
                    using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        if (ID == null) return null;

                        string sql = $"SELECT Al.ID, AL.Title FROM Artist A INNER JOIN ArtistAlbum AA ON AA.ArtistID = A.ID INNER JOIN Album Al ON Al.ID = AA.AlbumID WHERE A.ID = {ID};";

                        return _albums = new ObservableCollection<Album>(connection.Query<Album>(sql));
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show(error.Message);
                }

                return null;
            }
            set
            {
                _albums = value;
            }
        }

        public ObservableCollection<Band> Bands
        {
            get
            {
                if (_bands != null) return _bands;

                try
                {
                    using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        if (ID == null)
                            return null;
                        string sql = $"SELECT B.ID, B.Name FROM Artist A INNER JOIN BandArtist BA ON BA.ArtistID = A.ID INNER JOIN Band B ON B.ID = BA.BandID WHERE A.ID = {ID};";
                        return _bands = new ObservableCollection<Band>(connection.Query<Band>(sql));
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show(error.Message);
                }

                return null;
            }
            set
            {
                _bands = value;
            }
        }

        public ObservableCollection<Genre> Genres
        {
            get
            {
                if (_genres != null) return _genres;

                try
                {
                    using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        if (ID == null)
                            return null;
                        string sql = $"SELECT G.ID, G.Title FROM Artist A INNER JOIN ArtistGenre AG ON AG.ArtistID = A.ID INNER JOIN Genre G ON G.ID = AG.GenreID WHERE A.ID = {ID};";
                        return _genres = new ObservableCollection<Genre>(connection.Query<Genre>(sql));
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show(error.Message);
                }

                return null;
            }
            set
            {
                _genres = null;
            }
        }

        public bool Delete()
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    connection.Execute($"DELETE FROM Artist WHERE ID = {ID};");
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
                    string sql;

                    object obj = new
                    {
                        @StageName = StageName,
                        @FirstName = FirstName,
                        @LastName = LastName,
                        @Origin = Origin,
                        @Birthdate = Birthdate?.ToShortDateString()
                    };
                        
                    if (ID == null)
                    {
                        sql = $"INSERT INTO Artist(StageName, FirstName, LastName, Origin, Birthdate) VALUES ( ?, ?, ?, ?, ?)";
                    }
                    else
                    {
                        sql = $"UPDATE Artist SET StageName = ?, FirstName = ?, LastName = ?, Origin = ?, Birthdate = ? WHERE Id = {ID}";
                    }

                    connection.Execute(sql,obj);

                    // Bands
                    if (Bands != null)
                    {
                        List<String> values = new List<String>();

                        foreach(var band in Bands)
                        {
                            values.Add($"({band.ID},{ID})");
                        }

                        sql = $"DELETE FROM BandArtist WHERE ArtistID = {ID}; INSERT INTO BandArtist VALUES {string.Join(",", values)};";

                        connection.Execute(sql);
                    }

                    // Genres
                    if(Genres != null)
                    {
                        List<String> values = new List<String>();

                        foreach (var genre in Genres)
                        {
                            values.Add($"({ID},{genre.ID})");
                        }

                        sql = $"DELETE FROM ArtistGenre WHERE ArtistID = {ID}; INSERT INTO ArtistGenre VALUES {string.Join(", ", values)};";
                         
                        connection.Execute(sql);
                    }

                    if(Albums != null)
                    {
                        List<String> values = new List<String>();

                        foreach (var album in Albums)
                        {
                            values.Add($"({ID},{album.ID})");
                        }

                        sql = $"DELETE FROM ArtistAlbum WHERE ArtistID = {ID}; INSERT INTO ArtistAlbum VALUES {string.Join(", ", values)};";

                        connection.Execute(sql);
                    }
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
                return false;
            }

            return true;
        }

        public static List<Artist> All()
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    var artists = connection.Query<Artist>("SELECT * FROM Artist").ToList();
                    return artists;
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }

            return null;
        }

        public static Artist Find(int id)
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    var artist = connection.Query<Artist>($"SELECT * FROM Artist WHERE ID = ?", new { @ID = id }).FirstOrDefault();
                    return artist;
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }

            return null;
        }
    }
}
