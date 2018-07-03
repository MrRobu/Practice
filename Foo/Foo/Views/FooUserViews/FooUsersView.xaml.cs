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
    /// Interaction logic for FooUsersView.xaml
    /// </summary>
    public partial class FooUsersView : UserControl
    {
        public FooUsersView()
        {
            InitializeComponent();
        }

        public void FooUserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FooUserDataGrid.SelectedItem == null)
                LoadFooUserEditView.IsEnabled = false;
            else
                LoadFooUserEditView.IsEnabled = true;
        }

        private void FilterFooUser_ID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) || e.Text.Length == 0;
        }
    }
}
