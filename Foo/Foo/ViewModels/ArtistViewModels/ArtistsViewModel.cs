using Caliburn.Micro;
using Dapper;
using Foo.Models;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Foo.ViewModels
{
    public class ArtistsViewModel : Screen
    {
        private List<Artist> _artists;

        public ArtistsViewModel()
        {
            Artists = Artist.All();
            FilterArtist = new Artist();
        }

        public void LoadArtistCreateView()
        {
            ShellViewModel.Instance?.ActivateItem(new ArtistCreateViewModel());
        }

        public void LoadArtistEditView()
        {
            if(SelectedArtist != null)
                ShellViewModel.Instance.ActivateItem(new ArtistEditViewModel((int)SelectedArtist.ID));
        }

        public Artist FilterArtist { get; set; }

        public void Filter()
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    if(FilterArtist.ID != null)
                    {
                        Artists = new List<Artist> { Artist.Find((int)FilterArtist.ID) };
                    }

                    string sql = "SELECT * FROM Artist";

                    List<string> filters = new List<string>();

                    DynamicParameters parameters = new DynamicParameters();

                    if (FilterArtist.ID != null)
                    {
                        filters.Add("ID = ?");
                        parameters.Add("ID", FilterArtist.ID);
                    }

                    if (FilterArtist.FirstName != "")
                    {
                        filters.Add("FirstName ILIKE ?");
                        parameters.Add("FirstName", "%" + FilterArtist.FirstName + "%");
                    }

                    if (FilterArtist.LastName != "")
                    {
                        filters.Add("LastName ILIKE ?");
                        parameters.Add("LastName", "%" + FilterArtist.LastName + "%");
                    }

                    if (FilterArtist.StageName != "")
                    {
                        filters.Add("StageName ILIKE ?");
                        parameters.Add("StageName", "%" + FilterArtist.StageName + "%");
                    }

                    if (FilterArtist.Origin != "")
                    {
                        filters.Add("Origin ILIKE ?");
                        parameters.Add("Origin", "%" + FilterArtist.Origin + "%");
                    }

                    if (FilterArtist.Birthdate != null)
                    {
                        filters.Add("Birthdate = ?");
                        parameters.Add("Birthdate", FilterArtist.Birthdate?.Date.ToShortDateString());
                    }

                    if (filters.Count > 0)
                        sql += " WHERE " + string.Join(" AND ", filters);

                    var artists = connection.Query<Artist>(sql, parameters ).ToList();
                    Artists = artists;
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public List<Artist> Artists
        {
            get
            {
                return _artists;
            }
            set
            {
                _artists = value;
                NotifyOfPropertyChange(() => Artists);
            }
        }

        public Artist SelectedArtist { get; set; }
    }
}
