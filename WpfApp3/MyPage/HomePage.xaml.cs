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
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Li4noeBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GroupPage());

        }

        private void GroupBtn_Click(object sender, RoutedEventArgs e)
        {
          
            Navigation.NextPage(new Nav("", new PersonalPage()));
        }
    }
}
