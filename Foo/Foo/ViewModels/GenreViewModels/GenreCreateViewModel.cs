using Caliburn.Micro;
using Foo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Foo.ViewModels
{
    public class GenreCreateViewModel : Screen
    {
        public GenreCreateViewModel()
        {
            Genre = new Genre();
        }

        #region Genre
        public Genre Genre { get; }

        public void SaveGenre()
        {
            List<string> errors = new List<string>();

            if (Genre.Title == "")
                errors.Add("Title is mandatory!");

            if (errors.Count == 0 && Genre.Save())
            {
                ShellViewModel.Instance.ActivateItem(new GenresViewModel());
                MessageBox.Show("Genre successfully saved!");
            }
            else
            {
                MessageBox.Show(string.Join("\n", errors));
            }
        }
        #endregion
    }
}
