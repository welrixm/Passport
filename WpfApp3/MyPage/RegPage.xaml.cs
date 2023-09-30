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
                var useru = DBConnect.db.Visitor.Where(x => x.Name == namee && x.Email == login && x.Password == password).FirstOrDefault();
                if (useru == null)
                {
                    DBConnect.db.Visitor.Add(new Visitor
                    {
                        Email = login,
                        Password = password,
                        Name = namee,
                    });
                    if (password.Length < 8 && login.Length < 8)
                    {

                        bool symbol = false;
                        bool number = false;
                        bool IsAllUpper = false;
                        for (int i = 0; i < password.Length; i++)
                        {

                            if (password[i] >= '0' && password[i] <= '9') number = true;
                            if (password[i] == '!' || password[i] == '@' || password[i] == '#' || password[i] == '$' || password[i] == '%' || password[i] == '^') symbol = true;
                            if (Char.IsUpper(password[i])) IsAllUpper = true;
                        }

                        if (!symbol)
                            MessageBox.Show("Добавьте один из следующих символов: ! @ # $ % ^");
                        else if (!number)
                            MessageBox.Show("Добавьте хотя бы одну цифру");
                        else if (!IsAllUpper)
                            MessageBox.Show("Добавьте одну прописную букву");
                        if (symbol && number && IsAllUpper)
                        {
                            MessageBox.Show("Пользователь зарегистрирован");
                            DBConnect.db.SaveChanges();
                            NavigationService.Navigate(new AutoPage());
                        }
                        //  MessageBox.Show("Пользователь успешно зарегестрирован!");
                    }
                    else
                    {
                        MessageBox.Show("Пароль слишком короткий, требуется минимум 6 символов!");
                    }
                    

                }
            }
        }

        private void OtmenaBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.BackPage();
        }
    }
}
