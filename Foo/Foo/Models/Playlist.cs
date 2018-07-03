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
    public class Playlist
    {
        private FooUser _fooUser;

        public int? ID { get; set; }

        public string Title { get; set; }

        public int FooUserID { get; set; }

        public FooUser FooUser
        {
            get
            {
                if (_fooUser != null) return _fooUser;

                if (ID == null) return _fooUser = new FooUser();

                try
                {
                    using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        return _fooUser = FooUser.Find(FooUserID);
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show($"Class: Playlist\n Property: FooUser\n Error: {error.Message}");
                }

                return null;
            }
            set
            {
                _fooUser = value;
            }
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
                    if (ID == 0)
                    {
                        connection.Execute($"INSERT INTO Playlist VALUES ('{Title}', {FooUserID});");
                    }
                    else
                    {
                        connection.Execute($"UPDATE Playlist SET Title = '{Title}', FooUserID = {FooUserID} WHERE ID = {ID}");
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
