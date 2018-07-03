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
    public class TrackEditViewModel : Screen
    {
        public TrackEditViewModel(int id)
        {
            Track = Track.Find(id);
        }

        #region Track
        public Track Track { get; }

        public void DeleteArtist()
        {
            if (Track.Delete())
            {
                MessageBox.Show("Track successfully deleted!");
                ShellViewModel.Instance.ActivateItem(new TracksViewModel());
            }
            else
                MessageBox.Show("Track could not be deleted!");
        }

        public void UpdateTrack()
        {
            List<string> errors = new List<string>();

            if (Track.Title == "")
                errors.Add("Title is mandatory!");

            if (errors.Count == 0 && Track.Save())
            {
                ShellViewModel.Instance.ActivateItem(new TracksViewModel());
                MessageBox.Show("Track successfully updated!");
            }
            else
            {
                MessageBox.Show(string.Join("\n", errors));
            }
        }
        #endregion

        #region Album
        public List<Album> Albums { get; } = Album.All();

        //public Album SelectedAlbum { get; set; }
        #endregion

        #region Genre
        public List<Genre> Genres { get; } = Genre.All();

        //public Genre SelectedGenre { get; set; }
        #endregion
    }
}
