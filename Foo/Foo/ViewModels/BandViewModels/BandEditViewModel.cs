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
    class BandEditViewModel : Screen
    {
        public BandEditViewModel(int id)
        {
            Band = Band.Find(id);

            var artistIds = (from ba in Band.Artists select ba.ID);
            Artists = new ObservableCollection<Artist>();
            foreach(var a in Artist.All())
            {
                if (!artistIds.Contains(a.ID))
                    Artists.Add(a);
            }

            var genreIds = (from bg in Band.Genres select bg.ID);
            Genres = new ObservableCollection<Genre>();
            foreach (var g in Genre.All())
            {
                if (!genreIds.Contains(g.ID))
                    Genres.Add(g);
            }
        }

        #region Band
        public Band Band { get; set; }

        public void DeleteArtist()
        {
            if (Band.Delete())
            {
                MessageBox.Show("Band successfully deleted!");
                ShellViewModel.Instance.ActivateItem(new ArtistsViewModel());
            }
            else
                MessageBox.Show("Band could not be deleted!");
        }

        public void UpdateBand()
        {
            List<string> errors = new List<string>();

            if (Band.Name == "")
                errors.Add("Name is mandatory!");

            if (errors.Count == 0 && Band.Save())
            {
                ShellViewModel.Instance.ActivateItem(new BandsViewModel());
                MessageBox.Show("Band successfully updated!");
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
