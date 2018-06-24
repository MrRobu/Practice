using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.ViewModels
{
    public class ReportsViewModel : Conductor<object>
    {
        public void LoadReport1View()
        {
            ActivateItem(new Report1ViewModel());
        }

        public void LoadReport2View()
        {
            ActivateItem(new Report2ViewModel());
        }

        public void LoadReport3View()
        {
            ActivateItem(new Report3ViewModel());
        }

        public void LoadReportArtistView()
        {
            ActivateItem(new ReportArtistViewModel());
        }
    }
}
