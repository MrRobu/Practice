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
    public class TrackCreateViewModel : Screen
    {
        public TrackCreateViewModel()
        {
            Track = new Track();
        }

        #region Track
        public Track Track { get; set; }

        public void SaveTrack()
        {
            List<string> errors = new List<string>();

            if (Track.Title == "")
                errors.Add("Title is mandatory!");

            if (errors.Count == 0)
            {
                if (SelectedAlbum != null)
                    Track.Album = SelectedAlbum;

                if (SelectedGenre != null)
                    Track.Genre = SelectedGenre;

                if (Track.Save())
                {
                    ShellViewModel.Instance.ActivateItem(new TracksViewModel());
                    MessageBox.Show("Track successfully saved!");
                }
            }
            else
            {
                MessageBox.Show(string.Join("\n", errors));
            }
        }
        #endregion

        #region Album
        public List<Album> Albums { get; } = Album.All();

        public Album SelectedAlbum { get; set; }
        #endregion

        #region Genre
        public List<Genre> Genres { get; } = Genre.All();

        public Genre SelectedGenre { get; set; }
        #endregion
    }
}
