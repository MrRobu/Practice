using Caliburn.Micro;
using Dapper;
using Foo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Foo.ViewModels
{
    public class ArtistEditViewModel : Screen
    {
        public ArtistEditViewModel(int id)
        {
            Artist = Artist.Find(id);
        }

        #region Artist
        public Artist Artist { get; }

        public void DeleteArtist()
        {
            if (Artist.Delete())
            {
                MessageBox.Show("Artist successfully deleted!");
                ShellViewModel.Instance.ActivateItem(new ArtistsViewModel());
            }
            else
                MessageBox.Show("Artist could not be deleted!");
        }

        public void UpdateArtist()
        {
            List<string> errors = new List<string>();

            if (Artist.FirstName == "")
                errors.Add("Firstname is mandatory!");
            if (Artist.LastName == "")
                errors.Add("Lastname is mandatory!");

            if (errors.Count == 0 && Artist.Save())
            {
                ShellViewModel.Instance.ActivateItem(new ArtistsViewModel());
                MessageBox.Show("Artist successfully updated!");
            }
            else
            {
                MessageBox.Show(string.Join("\n", errors));
            }
        }
        #endregion

        #region Bands
        public List<Playlist> Bands { get; } = Playlist.All();

        public Playlist BandToAdd { get; set; }

        public Playlist BandToRemove { get; set; }

        public void AddBand()
        {
            if (BandToAdd == null || Artist.Bands.Contains(BandToAdd))
                return;

            Artist.Bands.Add(BandToAdd);
        }

        public void RemoveBand()
        {
            if (BandToRemove == null) return;

            Artist.Bands.Remove(BandToRemove);
        }
        #endregion

        #region Genres
        public List<Genre> Genres { get; } = Genre.All();

        public Genre GenreToAdd { get; set; }

        public Genre GenreToRemove { get; set; }

        public void AddGenre()
        {
            if (GenreToAdd == null || Artist.Genres.Contains(GenreToAdd))
                return;

            Artist.Genres.Add(GenreToAdd);
        }

        public void RemoveGenre()
        {
            if (GenreToRemove == null) return;

            Artist.Genres.Remove(GenreToRemove);
        }
        #endregion

        #region Albums
        public List<Album> Albums { get; } = Album.Available();

        public Album AlbumToAdd { get; set; }

        public Album AlbumToRemove { get; set; }

        public void AddAlbum()
        {
            if (AlbumToAdd == null || Artist.Albums.Contains(AlbumToAdd))
                return;

            Artist.Albums.Add(AlbumToAdd);
        }

        public void RemoveAlbum()
        {
            if (AlbumToRemove == null) return;

            Artist.Albums.Remove(AlbumToRemove);
        }
        #endregion
    }
}
