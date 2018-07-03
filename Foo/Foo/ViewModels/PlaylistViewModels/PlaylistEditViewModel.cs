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
    public class PlaylistEditViewModel : Screen
    {
        public PlaylistEditViewModel(int id)
        {
            Playlist = Playlist.Find(id);
        }

        #region Playlist
        public Playlist Playlist { get; }

        public void DeletePlaylist()
        {
            if (Playlist.Delete())
            {
                MessageBox.Show("Playlist successfully deleted!");
                ShellViewModel.Instance.ActivateItem(new PlaylistsViewModel());
            }
            else
                MessageBox.Show("Playlist could not be deleted!");
        }

        public void UpdatePlaylist()
        {
            List<string> errors = new List<string>();

            if (Playlist.Title == "")
                errors.Add("Title is mandatory!");
            if (Playlist.FooUser.UserName == "")
                errors.Add("UserName is mandatory!");

            if (errors.Count == 0 && Playlist.Save())
            {
                ShellViewModel.Instance.ActivateItem(new PlaylistsViewModel());
                MessageBox.Show("Playlist successfully updated!");
            }
            else
            {
                MessageBox.Show(string.Join("\n", errors));
            }
        }
        #endregion

        public List<FooUser> FooUsers { get; } = FooUser.All(); 
    }
}
