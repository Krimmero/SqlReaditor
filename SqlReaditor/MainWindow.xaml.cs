using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SqlReaditor.Helpers;
using SqlReaditor.Windows;

namespace SqlReaditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        private void BtnList_OnClick(object sender, RoutedEventArgs e)
        {
            string connectionString =
                ConnectionStringHolder.ConString;
            string selected = listBox.SelectedItem.ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM " + selected;
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable(selected);
                sda.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                
            }
        }
        private void btnLoadDB_Click(object sender, RoutedEventArgs e)
        {
            lblUser.Content = ConnectionStringHolder.Username;
            

            string connectionString =
                ConnectionStringHolder.ConString;

            List<string> tableList = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'";
                SqlCommand cmd = new SqlCommand(query, connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tableList.Add(reader.GetString(0));
                }

                foreach (var table in tableList)
                {
                    listBox.Items.Add(table);
                }

                connection.Close();

              }
        }
    }
}
