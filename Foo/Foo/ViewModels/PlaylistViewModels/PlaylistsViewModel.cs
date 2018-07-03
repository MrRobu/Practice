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
    public class PlaylistsViewModel : Screen
    {
        private List<Playlist> _playlists;

        public PlaylistsViewModel()
        {
            Playlists = Playlist.All();
            FilterPlaylist = new Playlist();
        }

        public void LoadPlaylistCreateView()
        {
            ShellViewModel.Instance.ActivateItem(new PlaylistCreateViewModel());
        }

        public void LoadPlaylistEditView()
        {
            if (SelectedPlaylist != null)
                ShellViewModel.Instance.ActivateItem(new BandEditViewModel((int)SelectedPlaylist.ID));
        }

        public Playlist FilterPlaylist { get; set; }

        public void Filter()
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    if (FilterPlaylist.ID != null)
                    {
                        Playlists = new List<Playlist> { Playlist.Find((int)FilterPlaylist.ID) };
                    }

                    string sql = "SELECT * FROM Artist";

                    List<string> filters = new List<string>();

                    DynamicParameters parameters = new DynamicParameters();

                    if (FilterPlaylist.ID != null)
                    {
                        filters.Add("ID = ?");
                        parameters.Add("ID", FilterPlaylist.ID);
                    }

                    if (FilterPlaylist.Title != "")
                    {
                        filters.Add("Title ILIKE ?");
                        parameters.Add("Title", "%" + FilterPlaylist.Title + "%");
                    }

                    //if (FilterPlaylist.FooUser.UserName != "")
                    //{
                    //    filters.Add("Origin ILIKE ?");
                    //    parameters.Add("Origin", "%" + FilterBand.Origin + "%");
                    //}

                    if (filters.Count > 0)
                        sql += " WHERE " + string.Join(" AND ", filters);

                    Playlists = connection.Query<Playlist>(sql, parameters).ToList();
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public List<Playlist> Playlists
        {
            get
            {
                return _playlists;
            }
            set
            {
                _playlists = value;
                NotifyOfPropertyChange(() => Playlists);
            }
        }

        public Playlist SelectedPlaylist { get; set; }
    }
}
