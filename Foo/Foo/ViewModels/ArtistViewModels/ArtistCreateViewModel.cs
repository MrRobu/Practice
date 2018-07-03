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
    public class ArtistCreateViewModel : Screen
    {
        public Artist Artist { get; set; }

        public ArtistCreateViewModel()
        {
            Artist = new Artist();
        }

        public void SaveArtist()
        {
            List<string> errors = new List<string>();

            if (Artist.FirstName == "")
                errors.Add("Firstname is mandatory!");
            if (Artist.LastName == "")
                errors.Add("Lastname is mandatory!");

            if (errors.Count == 0)
            {
                Artist.Save();
                ShellViewModel.Instance.ActivateItem(new ArtistsViewModel());
                MessageBox.Show("Artist successfully created!");
            }
            else
            {
                MessageBox.Show(string.Join("\n", errors));
            }
        }
    }
}
