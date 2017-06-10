using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Data;

namespace Nursing_home_manager.Classes
{
    public class Sqlconnect
    {

        public SqlConnection Con { get; set; }
        private string conString { get; set; }
        public ConnectionState State { get; internal set; }

        public void conOpen()
        {
            //conString = "data source= DESKTOP-4IM9OL5\\SQLEXPRESS;integrated security=true;initial catalog=exemplo1"; 
            conString = "Server= tcp: 193.136.175.33\\SQLSERVER2012,8293;Database=p1g6;User Id=p1g6; Password = passwordbd123;";
            Con = new SqlConnection(conString);//
            try
            {
                Con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DataBase cant open."+ ex, "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void conClose()
        {
            Con.Close();
        }

    }

}
