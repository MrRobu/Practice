using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;
using System.Windows;
using Dapper;
using Foo.Models;

namespace Foo.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public ShellViewModel()
        {
            Instance = this;
            ActivateItem(new DashboardViewModel());
        }

        public void LoadDashboardView()
        {
            ActivateItem(new DashboardViewModel());
        }

        public void LoadArtistsView()
        {
            ActivateItem(new ArtistsViewModel());
        }

        public void LoadAlbumsView()
        {
            ActivateItem(new AlbumsViewModel());
        }

        public void LoadBandsView()
        {
            ActivateItem(new BandsViewModel());
        }

        public void LoadTracksView()
        {
            ActivateItem(new TracksViewModel());
        }

        public void LoadPlaylistsView()
        {
            ActivateItem(new PlaylistsViewModel());
        }

        public void LoadGenresView()
        {
            ActivateItem(new GenresViewModel());
        }

        public void LoadFooUsersView()
        {
            ActivateItem(new FooUsersViewModel());
        }

        public void LoadSettingsView()
        {
            ActivateItem(new SettingsViewModel());
        }

        public static ShellViewModel Instance { get; private set; }
    }
}