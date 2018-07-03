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
        public int? ID { get; }

        public string Title { get; set; }

        public float Length { get; set; }

        public int AlbumID { get; set; }

        public int GenreID { get; set; }

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
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
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
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    var track = connection.Query<Track>($"SELECT * FROM Track WHERE ID = {id}").First();
                    return track;
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
                    connection.Execute($"DELETE FROM Track WHERE ID = {ID};");
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
                        connection.Execute($"INSERT INTO Track VALUES ('{Title}', '{Length}', '{AlbumID}');");
                    }
                    else
                    {
                        connection.Execute($"UPDATE Track SET Title = '{Title}', Duration = {Length}, AlbumID = '{AlbumID}';");
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
