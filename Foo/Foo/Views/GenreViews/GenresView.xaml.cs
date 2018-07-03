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
    /// Interaction logic for GenresView.xaml
    /// </summary>
    public partial class GenresView : UserControl
    {
        public GenresView()
        {
            InitializeComponent();
        }

        public void GenreDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenreDataGrid.SelectedItem == null)
                LoadGenreEditView.IsEnabled = false;
            else
                LoadGenreEditView.IsEnabled = true;
        }

        private void FilterGenre_ID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) || e.Text.Length == 0;
        }
    }
}
