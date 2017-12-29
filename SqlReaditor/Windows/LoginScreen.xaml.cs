using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
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
using System.Windows.Shapes;
using SqlReaditor.Helpers;

namespace SqlReaditor.Windows
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void BtnCreateUser_OnClickCreateUser_OnClick(object sender, RoutedEventArgs e)
        {
            CreateUserScreen newCreateUserScreen = new CreateUserScreen();
            newCreateUserScreen.Show();
            this.Close();

        }

        private void BtnSubmit_OnClick(object sender, RoutedEventArgs e)
        {
            
            ConnectionStringHolder.Username = txtUsername.Text;

            string connectionString =
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SqlRUsers;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection connection = new SqlConnection(connectionString);


            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();      
                }
                string querySalt = "SELECT Salt FROM Users WHERE UserName=@Username";
                SqlCommand sqlCmdSalt = new SqlCommand(querySalt, connection);
                sqlCmdSalt.CommandType = CommandType.Text;
                sqlCmdSalt.Parameters.AddWithValue("@Username", txtUsername.Text);

                var salt = Convert.ToString(sqlCmdSalt.ExecuteScalar());

                string queryConn = "SELECT ConnectionString FROM Users WHERE UserName=@Username";
                SqlCommand sqlCmdConn = new SqlCommand(queryConn, connection);
                sqlCmdConn.CommandType = CommandType.Text;
                sqlCmdConn.Parameters.AddWithValue("@Username", txtUsername.Text);

                ConnectionStringHolder.ConString = Crypting.Decrypt(Convert.ToString(sqlCmdConn.ExecuteScalar()));


                

                Hashing hashing = new Hashing();
                var hashedPassword = hashing.GetHash(salt, txtPassword.Password);

                string query = "SELECT COUNT(1) FROM Users WHERE UserName=@Username AND Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                sqlCmd.Parameters.AddWithValue("@Password", hashedPassword);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                

                if (count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or Password is incorrect.");
                }
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
    }
}
