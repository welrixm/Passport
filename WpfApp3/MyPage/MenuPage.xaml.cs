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
            if (Navigation.AutoUser.Id == 32)
            {
                History.Visibility = Visibility.Visible;
            }
            else History.Visibility = Visibility.Collapsed;


            //if (Navigation.AutoUser.Id == 14 ||  Navigation.AutoUser.Id == 15)
            //{
            //    History1.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    History1.Visibility = Visibility.Collapsed;
            //}

        }

       

        private void GroupBtn_Click(object sender, RoutedEventArgs e)
        {
          
            Navigation.NextPage(new Nav("Форма записи на посещение мероприятия", new PersonalPage()));
        }

      

        private void PersonBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate( new GroupPage());
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("История заявок", new PassListOage()));
        }

        //private void History1_Click(object sender, RoutedEventArgs e)
        //{
        //    Navigation.NextPage(new Nav("История одобренных заявок", new ListEmployee()));
        //}
    }
}
