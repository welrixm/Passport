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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp3.MyPage;
using WpfApp3.Componens;

namespace WpfApp3.MyPage
{
    /// <summary>
    /// Логика взаимодействия для AutoPage.xaml
    /// </summary>
    public partial class AutoPage : Page
    {
        public AutoPage()
        {
            InitializeComponent();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("", new RegPage()));
        }

        private void AutoBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTb.Text.Trim();
            string password = PasswordTb.Password.Trim();
            if (login.Length> 0 && password.Length>0)
            {
               
                var user = DBConnect.db.Visitor.Where(x => x.Email == login && x.Password == password).FirstOrDefault();
                if (user == null)
                {
                    MessageBox.Show("Пользователя не существует");
                }
                else 
                {
                    
                Navigation.isAuth = true;
                    Navigation.AutoUser = user;
                Navigation.NextPage(new Nav("", new HomePage()));
              
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните поля");
            }
          //  var Userid= BdConectn.db
        }
    }
}
