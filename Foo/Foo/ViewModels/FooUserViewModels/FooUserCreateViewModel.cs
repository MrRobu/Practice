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
    public class FooUserCreateViewModel : Screen
    {
        public FooUserCreateViewModel()
        {
            FooUser = new FooUser();
        }

        #region FooUser
        public FooUser FooUser { get; set; }

        public void UpdateFooUser()
        {
            List<string> errors = new List<string>();

            if (FooUser.UserName == "")
                errors.Add("User Name is mandatory!");
            if (FooUser.FirstName == "")
                errors.Add("First Name is mandatory!");
            if (FooUser.LastName == "")
                errors.Add("Last Name is mandatory!");
            if (FooUser.Email == "")
                errors.Add("Email is mandatory!");
            if (FooUser.Password == "")
                errors.Add("Password is mandatory!");

            if (errors.Count == 0 && FooUser.Save())
            {
                ShellViewModel.Instance.ActivateItem(new FooUsersViewModel());
                MessageBox.Show("Foo User successfully saved!");
            }
            else
            {
                MessageBox.Show(string.Join("\n", errors));
            }
        }
        #endregion

        #region Playlists
        public List<Playlist> Playlists { get; } = Playlist.All();
        #endregion
    }
}
