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

        public SqlConnection Con { get; set; }//the object
        private string conString { get; set; }//the string to store your connection parameters
        public ConnectionState State { get; internal set; }

        public void conOpen()
        {
            conString = "data source= DESKTOP-4IM9OL5\\SQLEXPRESS;integrated security=true;initial catalog=exemplo1"; //the same as you post in your post
            Con = new SqlConnection(conString);//
            try
            {
                Con.Open();//try to open the connection
            }
            catch (Exception ex)
            {
                MessageBox.Show("DataBase cant open."+ ex, "Nursing Home Manager", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void conClose()
        {
            Con.Close();//close the connection
        }

    }

}
