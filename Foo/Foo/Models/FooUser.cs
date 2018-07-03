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
        public int? ID { get; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        
        // TODO: Password encryption
        public string Password { get; set; }

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
                    if (ID == 0)
                    {
                        connection.Execute($"INSERT INTO FooUser VALUES ('{UserName}', '{FirstName}', '{LastName}', '{Email}', '{Password}')");
                    }
                    else
                    {
                        connection.Execute($"UPDATE FooUser SET UserName = '{UserName}', FirstName = '{FirstName}', LastName = '{LastName}', Email = '{Email}', Password = '{Password}' WHERE ID = {ID}");
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
