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
            listBox.Items.Add(GetSelectedCellValue());
        }
        private void btnLoadDB_Click(object sender, RoutedEventArgs e)
        {
            lblUser.Content = ConnectionStringHolder.Username;
           


            //StackPanel stack = new StackPanel();
            //stack.HorizontalAlignment = HorizontalAlignment.Left;
            
            //stack.Height = 400;
            //stack.Width = 400;
            //stack.Background = Brushes.White;
            //mainGrid.Children.Add(stack);

            //Button btn = new Button();
            //btn.Height = 100;
            //btn.Width = 100;
            //btn.Content = "Button";
            //btn.Foreground = Brushes.Blue;
            //btn.Background = Brushes.Red;
            //stack.Children.Add(btn);

            //Label label = new Label();
            //label.Height = 40;
            //label.Width = 70;
            //label.Foreground = Brushes.Black;
            //label.Content = "This is a label";
            //stack.Children.Add(label);
             
            string connectionString =
               ConnectionStringHolder.ConString;
            SqlConnection connection = new SqlConnection(connectionString);




            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                else
                {
                    MessageBox.Show("Database not found");
                }

                
                var tables = connection.GetSchema("Tables");

                dataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new System.Windows.Data.Binding { Source = tables });


                var cellInfo = dataGrid.SelectedCells[0];
                
                var content = cellInfo.Column.GetCellContent(cellInfo.Item);
                SelectedItem.FrameworkElement = content;



                //foreach (DataRow table in tables.Rows)
                //{
                //    listBox.Items.Add(table.ToString());
                //}


                //string query = "select table_schema from information_schema.tables where table_type='base table'";
                //SqlCommand sqlCmd = new SqlCommand(query, connection);
                //sqlCmd.CommandType = CommandType.Text;












                //String query =
                //    "SELECT ConnectionString FROM Users WHERE UserName=@Username";
                //SqlCommand sqlCmd = new SqlCommand(query, connection);
                //sqlCmd.CommandType = CommandType.Text;
                //sqlCmd.Parameters.AddWithValue("@Username", .Text);
                //sqlCmd.Parameters.AddWithValue("@ConnectionString", encryptedConnectionString);
                //sqlCmd.Parameters.AddWithValue("@Password", hash);
                //sqlCmd.Parameters.AddWithValue("@Salt", salt);


                //if (sqlCmd.ExecuteNonQuery() != 0)
                //{
                //    LoginScreen login = new LoginScreen();
                //    login.Show();
                //    this.Close();
                //}
                //else
                //{
                //    MessageBox.Show("Failed creating new user!");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public string GetSelectedCellValue()
        {
            DataGridCellInfo cellInfo = dataGrid.SelectedCells[0];
            if (cellInfo == null) return null;

            DataGridBoundColumn column = cellInfo.Column as DataGridBoundColumn;
            if (column == null) return null;

            FrameworkElement element = new FrameworkElement() { DataContext = cellInfo.Item };
            BindingOperations.SetBinding(element, TagProperty, column.Binding);

            return element.Tag.ToString();
        }

    }
}
