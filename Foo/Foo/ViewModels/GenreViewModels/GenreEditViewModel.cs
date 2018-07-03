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
    public class GenreEditViewModel : Screen
    {
        public GenreEditViewModel(int id)
        {
            Genre = Genre.Find(id);
        }

        #region Genre
        public Genre Genre { get; }

        public void DeleteGenre()
        {
            if (Genre.Delete())
            {
                MessageBox.Show("Genre successfully deleted!");
                ShellViewModel.Instance.ActivateItem(new GenresViewModel());
            }
            else
                MessageBox.Show("Genre could not be deleted!");
        }

        public void UpdateGenre()
        {
            List<string> errors = new List<string>();

            if (Genre.Title == "")
                errors.Add("Title is mandatory!");

            if (errors.Count == 0 && Genre.Save())
            {
                ShellViewModel.Instance.ActivateItem(new GenresViewModel());
                MessageBox.Show("Genre successfully updated!");
            }
            else
            {
                MessageBox.Show(string.Join("\n", errors));
            }
        }
        #endregion
    }
}
