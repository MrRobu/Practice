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
    public class AlbumEditViewModel : Screen
    {
        public AlbumEditViewModel(int id)
        {
            Album = Album.Find(id);

            if(Album.Artist != null)
                SelectedArtist = Artists.Find(a => a.ID == Album.Artist.ID);

            if(Album.Band != null)
                SelectedBand = Bands.Find(b => b.ID == Album.Band.ID);
        }

        #region Album
        public Album Album { get; }

        public void DeleteAlbum()
        {
            if (Album.Delete())
            {
                MessageBox.Show("Album successfully deleted!");
                ShellViewModel.Instance.ActivateItem(new AlbumsViewModel());
            }
            else
                MessageBox.Show("Album could not be deleted!");
        }

        public void UpdateAlbum()
        {
            List<string> errors = new List<string>();

            if (Album.Title == "")
                errors.Add("Title is mandatory!");

            if (errors.Count == 0)
            {
                if (SelectedArtist != null)
                {
                    Album.Artist = SelectedArtist;
                    Album.Band = null;
                }
                if (SelectedBand != null)
                {
                    Album.Band = SelectedBand;
                    Album.Artist = null;
                }

                if(Album.Save())
                {
                    ShellViewModel.Instance.ActivateItem(new AlbumsViewModel());
                    MessageBox.Show("Album successfully updated!");
                }
            }
            else
            {
                MessageBox.Show(string.Join("\n", errors));
            }
        }
        #endregion

        #region Artist
        public List<Artist> Artists{ get; } = Artist.All();

        private Artist _selectedArtist;
        public Artist SelectedArtist
        {
            get => _selectedArtist;
            set
            {
                _selectedArtist = value;
                NotifyOfPropertyChange(() => SelectedArtist);
            }
        }
        #endregion

        #region Band
        public List<Band> Bands { get; } = Band.All();

        private Band _selectedBand;
        public Band SelectedBand
        {
            get => _selectedBand;
            set
            {
                _selectedBand = value;
                NotifyOfPropertyChange(() => SelectedBand);
            }
        }
        #endregion
    }
}
