using Caliburn.Micro;
using Foo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Foo.ViewModels
{
    public class ArtistCreateViewModel : Screen
    {
        public ArtistCreateViewModel()
        {
            Artist = new Artist();

            Bands = new ObservableCollection<Band>(Band.All());

            Genres = new ObservableCollection<Genre>(Genre.All());
        }

        #region Artist
        public Artist Artist { get; set; }

        public void SaveArtist()
        {
            List<string> errors = new List<string>();

            if (Artist.FirstName == "")
                errors.Add("Firstname is mandatory!");
            if (Artist.LastName == "")
                errors.Add("Lastname is mandatory!");

            if (errors.Count == 0 && Artist.Save())
            {
                ShellViewModel.Instance.ActivateItem(new ArtistsViewModel());
                MessageBox.Show("Artist successfully saved!");
            }
            else
            {
                MessageBox.Show(string.Join("\n", errors));
            }
        }
        #endregion

        #region Bands
        public ObservableCollection<Band> Bands { get; set; }

        public Band BandToAdd { get; set; }

        public Band BandToRemove { get; set; }

        public void AddArtist()
        {
            if (BandToAdd == null) return;

            Artist.Bands.Add(BandToAdd);
            Bands.Remove(BandToAdd);
        }

        public void RemoveBand()
        {
            if (BandToRemove == null) return;

            Bands.Add(BandToRemove);
            Artist.Bands.Remove(BandToRemove);
        }
        #endregion

        #region Genres
        public ObservableCollection<Genre> Genres { get; set; }

        public Genre GenreToAdd { get; set; }

        public Genre GenreToRemove { get; set; }

        public void AddGenre()
        {
            if (GenreToAdd == null) return;

            Artist.Genres.Add(GenreToAdd);
            Genres.Remove(GenreToAdd);
        }

        public void RemoveGenre()
        {
            if (GenreToRemove == null) return;

            Genres.Add(GenreToRemove);
            Artist.Genres.Remove(GenreToRemove);
        }
        #endregion
    }
}
