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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SqlReaditor.Helpers;

namespace SqlReaditor.Windows
{
    /// <summary>
    /// Interaction logic for CreateUserScreen.xaml
    /// </summary>
    public partial class CreateUserScreen : Window
    {
        public CreateUserScreen()
        {
            InitializeComponent();
        }

        private void BtnCreateUser_OnClick(object sender, RoutedEventArgs e)
        {
            string connectionString =
               @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SqlRUsers;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection connection = new SqlConnection(connectionString);
            Salting salting = new Salting();
            var salt = salting.GetSalt();
            Hashing hashing = new Hashing();
            var hash = hashing.GetHash(salt, txtCreatePassword.Password);

            var encryptedConnectionString = Crypting.Encrypt(txtConnectionString.Text);            

            try
            {
                if (connection.State == ConnectionState.Closed && txtConfirmPassword.Password.Equals(txtCreatePassword.Password))
                {
                    connection.Open();
                }
                else
                {
                    MessageBox.Show("Confirm Password does not match Password.");
                }
                String query =
                    "INSERT into Users (UserName,ConnectionString, Password, Salt) VALUES (@Username, @ConnectionString, @Password, @Salt)";
                SqlCommand sqlCmd = new SqlCommand(query, connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", txtCreateUsername.Text);
                sqlCmd.Parameters.AddWithValue("@ConnectionString", encryptedConnectionString);
                sqlCmd.Parameters.AddWithValue("@Password", hash);
                sqlCmd.Parameters.AddWithValue("@Salt", salt);
                

                if (sqlCmd.ExecuteNonQuery() != 0)
                {
                    LoginScreen login = new LoginScreen();
                    login.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed creating new user!");
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
