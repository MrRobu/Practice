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
    public class FooUsersViewModel : Screen
    {
        private List<FooUser> _fooUsers;

        public FooUsersViewModel()
        {
            FooUsers = FooUser.All();
            FilterFooUser = new FooUser();
        }

        public void LoadFooUserCreateView()
        {
            ShellViewModel.Instance.ActivateItem(new FooUserCreateViewModel());
        }

        public void LoadFooUserEditView()
        {
            if (SelectedFooUser != null)
                ShellViewModel.Instance.ActivateItem(new FooUserEditViewModel((int)SelectedFooUser.ID));
        }

        public FooUser FilterFooUser { get; set; }

        public void Filter()
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    if (FilterFooUser.ID != null)
                    {
                        FooUsers = new List<FooUser> { FooUser.Find((int)FilterFooUser.ID) };
                    }

                    string sql = "SELECT * FROM FooUser";

                    List<string> filters = new List<string>();

                    DynamicParameters parameters = new DynamicParameters();

                    if (FilterFooUser.ID != null)
                    {
                        filters.Add("ID = ?");
                        parameters.Add("ID", FilterFooUser.ID);
                    }

                    if (FilterFooUser.UserName != "")
                    {
                        filters.Add("UserName ILIKE ?");
                        parameters.Add("UserName", "%" + FilterFooUser.UserName + "%");
                    }

                    if (FilterFooUser.FirstName != "")
                    {
                        filters.Add("FirstName ILIKE ?");
                        parameters.Add("FirstName", "%" + FilterFooUser.FirstName + "%");
                    }

                    if (FilterFooUser.LastName != "")
                    {
                        filters.Add("LastName ILIKE ?");
                        parameters.Add("LastName", "%" + FilterFooUser.LastName + "%");
                    }

                    if (FilterFooUser.Email != "")
                    {
                        filters.Add("Email ILIKE ?");
                        parameters.Add("Email", "%" + FilterFooUser.Email + "%");
                    }

                    if (filters.Count > 0)
                        sql += " WHERE " + string.Join(" AND ", filters);

                    FooUsers = connection.Query<FooUser>(sql, parameters).ToList();
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public List<FooUser> FooUsers
        {
            get
            {
                return _fooUsers;
            }
            set
            {
                _fooUsers = value;
                NotifyOfPropertyChange(() => FooUsers);
            }
        }

        public FooUser SelectedFooUser { get; set; }
    }
}
