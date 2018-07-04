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
    public class PlaylistCreateViewModel : Screen
    {
        public PlaylistCreateViewModel()
        {
            Playlist = new Playlist();
            SelectedFooUser = FooUsers.Find(u => u.ID == Playlist.FooUserID);

            Tracks = new ObservableCollection<Track>(Track.All());
        }

        #region Playlist
        public Playlist Playlist { get; set; }

        public void SavePlaylist()
        {
            List<string> errors = new List<string>();

            if (Playlist.Title == "")
                errors.Add("Title is mandatory!");
            if (Playlist.FooUser == null)
                errors.Add("FooUser is mandatory!");

            if (errors.Count == 0)
            {
                if (SelectedFooUser != null)
                    Playlist.FooUser = SelectedFooUser;

                if (Playlist.Save())
                {
                    ShellViewModel.Instance.ActivateItem(new PlaylistsViewModel());
                    MessageBox.Show("Playlist successfully saved!");
                }
            }
            else
            {
                MessageBox.Show(string.Join("\n", errors));
            }
        }
        #endregion

        #region FooUser
        public List<FooUser> FooUsers { get; } = FooUser.All();

        public FooUser SelectedFooUser { get; set; }
        #endregion

        #region Tracks
        public ObservableCollection<Track> Tracks { get; set; }

        public Track TrackToAdd { get; set; }

        public Track TrackToRemove { get; set; }

        public void RemoveTrack()
        {
            if (TrackToRemove != null)
            {
                Tracks.Add(TrackToRemove);
                Playlist.Tracks.Remove(TrackToRemove);
            }
        }

        public void AddTrack()
        {
            if (TrackToAdd != null)
            {
                Playlist.Tracks.Add(TrackToAdd);
                Tracks.Remove(TrackToAdd);
            }
        }
        #endregion
    }
}
