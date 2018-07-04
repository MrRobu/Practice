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
    /// Interaction logic for TracksView.xaml
    /// </summary>
    public partial class TracksView : UserControl
    {
        public TracksView()
        {
            InitializeComponent();
        }

        public void TrackDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TrackDataGrid.SelectedItem == null)
                LoadTrackEditView.IsEnabled = false;
            else
                LoadTrackEditView.IsEnabled = true;
        }

        private void FilterTrack_ID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) || e.Text.Length == 0;
        }

        private void Albums_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
                Albums.SelectedItem = null;
        }

        private void Genres_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
                Genres.SelectedItem = null;
        }
    }
}
