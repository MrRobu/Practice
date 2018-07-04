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
    /// Interaction logic for TrackEditView.xaml
    /// </summary>
    public partial class TrackEditView : UserControl
    {
        public TrackEditView()
        {
            InitializeComponent();
        }

        //private void Albums_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Back)
        //        Albums.SelectedItem = null;
        //}

        //private void Genres_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Back)
        //        Genres.SelectedItem = null;
        //}
    }
}
