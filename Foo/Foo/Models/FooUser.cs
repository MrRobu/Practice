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
    public class FooUser
    {
        public int? ID { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        
        // TODO: Password encryption
        public string Password { get; set; }

        private List<Playlist> _playlists;

        public List<Playlist> Playlists
        {
            get
            {
                try
                {
                    using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        return _playlists = connection.Query<Playlist>($"SELECT * FROM Playlist WHERE FooUserID = {ID}").ToList();
                    }
                }
                catch (OdbcException error)
                {
                    MessageBox.Show(error.Message);
                }

                return null;
            }
            set => _playlists = value;
        }

        public static List<FooUser> All()
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    var fooUsers = connection.Query<FooUser>("SELECT * FROM FooUser").ToList();
                    return fooUsers;
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }

            return null;
        }

        public static FooUser Find(int id)
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    var fooUser = connection.Query<FooUser>($"SELECT * FROM FooUser WHERE ID = {id}").First();
                    return fooUser;
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
                    connection.Execute($"DELETE FROM FooUser WHERE Id = {ID};");
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
                        @UserName = UserName,
                        @FirstName = FirstName,
                        @LastName = LastName,
                        @Email = Email,
                        @Password = Password
                    };

                    if (ID == 0)
                    {
                        sql = $"INSERT INTO FooUser VALUES ( ?, ?, ?, ?, ? )";
                    }
                    else
                    {
                         sql = $"UPDATE FooUser SET UserName = ?, FirstName = ?, LastName = ?, Email = ?, Password = ? WHERE ID = {ID}";
                    }

                    connection.Execute(sql, obj);
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
