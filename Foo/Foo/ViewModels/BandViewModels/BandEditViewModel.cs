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
    class BandEditViewModel : Screen
    {
        public BandEditViewModel(int id)
        {
            Band = Band.Find(id);
        }

        #region Band
        public Band Band { get; }

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
        public List<Artist> Artists { get; } = Artist.All();

        public Artist ArtistToAdd { get; set; }

        public Artist ArtistToRemove { get; set; }

        public void AddBand()
        {
            if (ArtistToAdd == null || Band.Artists.Contains(ArtistToAdd))
                return;

            Band.Artists.Add(ArtistToAdd);
        }

        public void RemoveBand()
        {
            if (ArtistToRemove == null) return;

            Band.Artists.Remove(ArtistToRemove);
        }
        #endregion

        #region Genres
        public List<Genre> Genres { get; } = Genre.All();

        public Genre GenreToAdd { get; set; }

        public Genre GenreToRemove { get; set; }

        public void AddGenre()
        {
            if (GenreToAdd == null || Band.Genres.Contains(GenreToAdd))
                return;

            Band.Genres.Add(GenreToAdd);
        }

        public void RemoveGenre()
        {
            if (GenreToRemove == null) return;

            Band.Genres.Remove(GenreToRemove);
        }
        #endregion

        #region Albums
        public List<Album> Albums { get; } = Album.Available();

        public Album AlbumToAdd { get; set; }

        public Album AlbumToRemove { get; set; }

        public void AddAlbum()
        {
            if (AlbumToAdd == null || Band.Albums.Contains(AlbumToAdd))
                return;

            Band.Albums.Add(AlbumToAdd);
        }

        public void RemoveAlbum()
        {
            if (AlbumToRemove == null) return;

            Band.Albums.Remove(AlbumToRemove);
        }
        #endregion
    }
}
