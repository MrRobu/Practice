using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.ViewModels
{
    public class FormsViewModel : Conductor<object>
    {
        public void LoadForm1View()
        {
            ActivateItem(new Form1ViewModel());
        }

        public void LoadForm2View()
        {
            ActivateItem(new Form2ViewModel());
        }

        public void LoadForm3View()
        {
            ActivateItem(new Form3ViewModel());
        }
    }
}
