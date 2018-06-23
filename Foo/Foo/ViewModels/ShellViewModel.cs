using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;
using System.Windows;

namespace Foo.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        OdbcConnection connection;
        public ShellViewModel()
        {
            OpenConnection();
        }

        public void OpenConnection()
        {
            try
            {
                connection = new OdbcConnection(Helper.ConnectionString("postgres"));
                connection.Open();
                
                MessageBox.Show("Connection Open");
                connection.Close();
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
                if (connection?.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
