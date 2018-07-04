using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Foo.Views
{
    /// <summary>
    /// Interaction logic for PlaylistsViewModel.xaml
    /// </summary>
    public partial class PlaylistsView : UserControl
    {
        public PlaylistsView()
        {
            InitializeComponent();
        }

        public void PlaylistDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlaylistDataGrid.SelectedItem == null)
                LoadPlaylistEditView.IsEnabled = false;
            else
                LoadPlaylistEditView.IsEnabled = true;
        }

        private void FilterPlaylist_ID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) || e.Text.Length == 0;
        }

        private void FooUsers_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Back)
            {
                FooUsers.SelectedItem = null;
            }
        }
    }
}
