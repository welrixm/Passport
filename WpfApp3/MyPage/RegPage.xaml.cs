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
using WpfApp3.Componens;
using WpfApp3.MyPage;

namespace WpfApp3.MyPage
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTb.Text.Trim();
            string password = PasswordTb.Text.Trim();
            string namee = NameTb.Text.Trim();
                if (login.Length > 0 && password.Length>0 && namee.Length>0)
            {
                var useru = BdConectn.db.Visitor.Where(x => x.Name == namee && x.Email == login && x.Password == password).FirstOrDefault();
                if (useru == null)
                {
                    BdConectn.db.Visitor.Add(new Visitor
                    {
                        Email = login,
                        Password = password,
                        Name = namee,
                    });
                    MessageBox.Show("Пользователь зарегистрирован");
                    BdConectn.db.SaveChanges();
                    NavigationService.Navigate(new AutoPage());

                }
            }
        }
    }
}
