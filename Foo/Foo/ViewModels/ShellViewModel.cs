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
            ActivateItem(new DashboardViewModel());
        }

        public void LoadDashboardView()
        {
            ActivateItem(new DashboardViewModel());
        }

        public void LoadMusicView()
        {
            ActivateItem(new MusicViewModel());
        }

        public void LoadFormsView()
        {
            ActivateItem(new FormsViewModel());
        }

        public void LoadReportsView()
        {
            ActivateItem(new ReportsViewModel());
        }

        public void LoadSearchView()
        {
            ActivateItem(new SearchViewModel());
        }

        public void LoadSettingsView()
        {
            ActivateItem(new SettingsViewModel());
        }
    }
}