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
    public class AlbumsViewModel : Screen
    {
        private List<Album> _albums;

        public AlbumsViewModel()
        {
            Albums = Album.All();
            FilterAlbum = new Album();
        }

        public void LoadAlbumCreateView()
        {
            ShellViewModel.Instance.ActivateItem(new AlbumCreateViewModel());
        }

        public void LoadAlbumEditView()
        {
            if (SelectedAlbum != null)
                ShellViewModel.Instance.ActivateItem(new AlbumEditViewModel((int)SelectedAlbum.ID));
        }

        public Album FilterAlbum { get; set; }

        public void Filter()
        {
            try
            {
                using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    if (FilterAlbum.ID != null)
                    {
                        Albums = new List<Album> { Album.Find((int)FilterAlbum.ID) };
                    }

                    string sql = "SELECT * FROM Album";

                    List<string> filters = new List<string>();

                    DynamicParameters parameters = new DynamicParameters();

                    if (FilterAlbum.ID != null)
                    {
                        filters.Add("ID = ?");
                        parameters.Add("ID", FilterAlbum.ID);
                    }

                    if (FilterAlbum.Title != "")
                    {
                        filters.Add("Title ILIKE ?");
                        parameters.Add("Title", "%" + FilterAlbum.Title + "%");
                    }

                    if (FilterAlbum.ReleaseDate != null)
                    {
                        filters.Add("ReleaseDate = ?");
                        parameters.Add("ReleaseDate", FilterAlbum.ReleaseDate);
                    }

                    if (filters.Count > 0)
                        sql += " WHERE " + string.Join(" AND ", filters);

                    Albums = connection.Query<Album>(sql, parameters).ToList();
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public List<Album> Albums
        {
            get
            {
                return _albums;
            }
            set
            {
                _albums = value;
                NotifyOfPropertyChange(() => Albums);
            }
        }

        public Album SelectedAlbum { get; set; }
    }
}
