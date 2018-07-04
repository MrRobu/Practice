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
    public class Playlist
    {
        private ObservableCollection<Track> _tracks;
        private FooUser _fooUser;

        public int? ID { get; set; }

        public string Title { get; set; }

        public int? FooUserID { get; set; }

        public FooUser FooUser
        {
            get
            {
                if (_fooUser != null) return _fooUser;

                if (FooUserID == null) return null;

                try
                {
                    using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        return _fooUser = FooUser.Find((int)FooUserID);
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show($"Class: Playlist\n Property: FooUser\n Error: {error.Message}");
                }

                return null;
            }
            set => _fooUser = value;
        }

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

                        string sql = $"SELECT T.* FROM Track T INNER JOIN PlaylistTrack PT ON PT.TrackID = T.ID INNER JOIN Playlist P ON P.ID = PT.PlaylistID WHERE P.ID = {ID};";

                        return _tracks = new ObservableCollection<Track>(connection.Query<Track>(sql));
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show($"Class: Playlist\n Property: Tracks\n Error: {error.Message}");
                }

                return null;
            }
            set => _tracks = value;
        }

        public static List<Playlist> All()
        {
            try
            {
                using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    var playlists = connection.Query<Playlist>("SELECT * FROM Playlist").ToList();
                    return playlists;
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }

            return null;
        }

        public static Playlist Find(int id)
        {
            try
            {
                using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    var playlist = connection.Query<Playlist>($"SELECT * FROM Playlist WHERE ID = {id}").First();
                    return playlist;
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
                using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    connection.Execute($"DELETE FROM Playlist WHERE ID = {ID};");
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
                using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    string sql;
                    object obj = new
                    {
                        @Title = Title,
                        FooUserID = FooUser.ID
                    };

                    if (ID == 0)
                    {
                        sql = $"INSERT INTO Playlist VALUES ( ?, ?);";
                    }
                    else
                    {
                        sql = $"UPDATE Playlist SET Title = ?, FooUserID = ? WHERE ID = {ID}";
                    }

                    connection.Execute(sql, obj);

                    // Tracks
                    if (Tracks != null)
                    {
                        List<String> values = new List<String>();

                        foreach (var track in Tracks)
                        {
                            values.Add($"({track.ID},{ID})");
                        }

                        sql = $"DELETE FROM PlaylistTrack WHERE PlaylistID = {ID}; INSERT INTO PlaylistTrack VALUES {string.Join(",", values)};";

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
    }
}
