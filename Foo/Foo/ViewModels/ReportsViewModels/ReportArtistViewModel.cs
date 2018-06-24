using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foo.Models;
using System.Data.Odbc;
using Dapper;
using Dapper.Mapper;
using System.Windows;
using System.Data;

namespace Foo.ViewModels
{
    public class ReportArtistViewModel : Screen
    {
        public List<ArtistModel> ArtistList
        {
            get
            {
                try
                {
                    using (IDbConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                    {
                        var artists = connection.Query<ArtistModel>("SELECT * FROM Artist").ToList();
                        return artists;
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
}
