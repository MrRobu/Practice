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
    public class BandCreateViewModel : Screen
    {
        public BandCreateViewModel()
        {
            Band = new Band();

            Artists = new ObservableCollection<Artist>(Artist.All());
            
            Genres = new ObservableCollection<Genre>(Genre.All());
        }

        #region Band
        public Band Band { get; set; }

        public void SaveBand()
        {
            List<string> errors = new List<string>();

            if (Band.Name == "")
                errors.Add("Name is mandatory!");

            if (errors.Count == 0 && Band.Save())
            {
                ShellViewModel.Instance.ActivateItem(new BandsViewModel());
                MessageBox.Show("Band successfully saved!");
            }
            else
            {
                MessageBox.Show(string.Join("\n", errors));
            }
        }
        #endregion

        #region Artists
        public ObservableCollection<Artist> Artists { get; set; }

        public Artist ArtistToAdd { get; set; }

        public Artist ArtistToRemove { get; set; }

        public void AddArtist()
        {
            if (ArtistToAdd == null) return;

            Band.Artists.Add(ArtistToAdd);
            Artists.Remove(ArtistToAdd);
        }

        public void RemoveArtist()
        {
            if (ArtistToRemove == null) return;

            Artists.Add(ArtistToRemove);
            Band.Artists.Remove(ArtistToRemove);
        }
        #endregion

        #region Genres
        public ObservableCollection<Genre> Genres { get; set; }

        public Genre GenreToAdd { get; set; }

        public Genre GenreToRemove { get; set; }

        public void AddGenre()
        {
            if (GenreToAdd == null) return;

            Band.Genres.Add(GenreToAdd);
            Genres.Remove(GenreToAdd);
        }

        public void RemoveGenre()
        {
            if (GenreToRemove == null) return;

            Genres.Add(GenreToRemove);
            Band.Genres.Remove(GenreToRemove);
        }
        #endregion
    }
}
