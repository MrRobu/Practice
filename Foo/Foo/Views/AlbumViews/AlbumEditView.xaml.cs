using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for AlbumEditView.xaml
    /// </summary>
    public partial class AlbumEditView : UserControl
    {
        public AlbumEditView()
        {
            InitializeComponent();
        }

        private void BandComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BandComboBox.SelectedItem != null)
                ArtistComboBox.SelectedItem = null;
        }

        private void ArtistComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ArtistComboBox.SelectedItem != null)
                BandComboBox.SelectedItem = null;
        }
    }
}
