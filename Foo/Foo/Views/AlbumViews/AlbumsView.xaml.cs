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
    /// Interaction logic for AlbumsView.xaml
    /// </summary>
    public partial class AlbumsView : UserControl
    {
        public AlbumsView()
        {
            InitializeComponent();
        }

        public void AlbumDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AlbumDataGrid.SelectedItem == null)
                LoadAlbumEditView.IsEnabled = false;
            else
                LoadAlbumEditView.IsEnabled = true;
        }

        private void FilterAlbum_ID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) || e.Text.Length == 0;
        }
    }
}
