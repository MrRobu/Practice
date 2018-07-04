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

            var bandIds = (from ab in Artist.Bands select ab.ID);
            Bands = new ObservableCollection<Band>();
            foreach (var b in Band.All())
            {
                if (!bandIds.Contains(b.ID))
                    Bands.Add(b);
            }

            var genreIds = (from ag in Artist.Genres select ag.ID);
            Genres = new ObservableCollection<Genre>();
            foreach (var g in Genre.All())
            {
                if (!genreIds.Contains(g.ID))
                    Genres.Add(g);
            }
        }

        #region Artist
        public Artist Artist { get; set; }

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
