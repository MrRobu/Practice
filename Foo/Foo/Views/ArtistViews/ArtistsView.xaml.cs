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
    /// Interaction logic for ArtistsView.xaml
    /// </summary>
    public partial class ArtistsView : UserControl
    {
        public ArtistsView()
        {
            InitializeComponent();
        }

        public void ArtistDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ArtistDataGrid.SelectedItem == null)
                LoadArtistEditView.IsEnabled = false;
            else
                LoadArtistEditView.IsEnabled = true;
        }

        private void FilterArtist_ID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) || e.Text.Length == 0;
        }

        private void FilterArtist_Birthdate_KeyDown(object sender, KeyEventArgs e)
        {
            ((DatePicker)sender).Text = null; 
        }
    }
}
