using Caliburn.Micro;
using Dapper;
using Foo.Models;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Foo.ViewModels
{
    public class BandsViewModel : Screen
    {
        private List<Band> _bands;

        public BandsViewModel()
        {
            Bands = Band.All();
            FilterBand = new Band();
        }

        public void LoadBandCreateView()
        {
            ShellViewModel.Instance?.ActivateItem(new BandCreateViewModel());
        }

        public void LoadBandEditView()
        {
            if (SelectedBand != null)
                ShellViewModel.Instance.ActivateItem(new BandEditViewModel((int)SelectedBand.ID));
        }

        public Band FilterBand { get; set; }

        public void Filter()
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    if (FilterBand.ID != null)
                    {
                        Bands = new List<Band> { Band.Find((int)FilterBand.ID) };
                    }

                    string sql = "SELECT * FROM Band";

                    List<string> filters = new List<string>();

                    DynamicParameters parameters = new DynamicParameters();

                    if (FilterBand.ID != null)
                    {
                        filters.Add("ID = ?");
                        parameters.Add("ID", FilterBand.ID);
                    }

                    if (FilterBand.Name != "")
                    {
                        filters.Add("Name ILIKE ?");
                        parameters.Add("Name", "%" + FilterBand.Name + "%");
                    }

                    if (FilterBand.Origin != "")
                    {
                        filters.Add("Origin ILIKE ?");
                        parameters.Add("Origin", "%" + FilterBand.Origin + "%");
                    }

                    if (filters.Count > 0)
                        sql += " WHERE " + string.Join(" AND ", filters);

                    Bands = connection.Query<Band>(sql, parameters).ToList();
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public List<Band> Bands
        {
            get
            {
                return _bands;
            }
            set
            {
                _bands = value;
                NotifyOfPropertyChange(() => Bands);
            }
        }

        public Band SelectedBand { get; set; }
    }
}
