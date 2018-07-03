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
    /// Interaction logic for BandsView.xaml
    /// </summary>
    public partial class BandsView : UserControl
    {
        public BandsView()
        {
            InitializeComponent();
        }

        public void BandDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BandDataGrid.SelectedItem == null)
                LoadBandEditView.IsEnabled = false;
            else
                LoadBandEditView.IsEnabled = true;
        }

        private void FilterBand_ID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) || e.Text.Length == 0;
        }
    }
}
