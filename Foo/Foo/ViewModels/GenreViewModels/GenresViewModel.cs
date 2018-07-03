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
    public class GenresViewModel : Screen
    {
        private List<Genre> _genres;

        public GenresViewModel()
        {
            Genres = Genre.All();
            FilterGenre = new Genre();
        }

        public void LoadGenreCreateView()
        {
            ShellViewModel.Instance.ActivateItem(new GenreCreateViewModel());
        }

        public void LoadGenreEditView()
        {
            if (SelectedGenre != null)
                ShellViewModel.Instance.ActivateItem(new GenreEditViewModel((int)SelectedGenre.ID));
        }

        public Genre FilterGenre { get; set; }

        public void Filter()
        {
            try
            {
                using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    if (FilterGenre.ID != null)
                    {
                        Genres = new List<Genre> { Genre.Find((int)FilterGenre.ID) };
                    }

                    string sql = "SELECT * FROM Artist";

                    List<string> filters = new List<string>();

                    DynamicParameters parameters = new DynamicParameters();

                    if (FilterGenre.ID != null)
                    {
                        filters.Add("ID = ?");
                        parameters.Add("ID", FilterGenre.ID);
                    }

                    if (FilterGenre.Title != "")
                    {
                        filters.Add("Title ILIKE ?");
                        parameters.Add("Title", "%" + FilterGenre.Title + "%");
                    }

                    if (filters.Count > 0)
                        sql += " WHERE " + string.Join(" AND ", filters);

                    Genres = connection.Query<Genre>(sql, parameters).ToList();
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public List<Genre> Genres
        {
            get
            {
                return _genres;
            }
            set
            {
                _genres = value;
                NotifyOfPropertyChange(() => Genres);
            }
        }

        public Genre SelectedGenre { get; set; }
    }
}
