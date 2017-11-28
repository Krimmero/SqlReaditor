using System;
using System.Collections.Generic;
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
    }
}
