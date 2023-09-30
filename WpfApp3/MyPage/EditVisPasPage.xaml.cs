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

namespace WpfApp3.MyPage
{
    /// <summary>
    /// Логика взаимодействия для EditVisPasPage.xaml
    /// </summary>
    public partial class EditVisPasPage : Page
    {
        //public VisitorPass visi {  get; set; }
        Componens.VisitorPass visitorPass;
        public EditVisPasPage( VisitorPass _visitorPass)
        {
            
            InitializeComponent();
            visitorPass = _visitorPass;
            DataContext = visitorPass;
            //VisCb.ItemsSource = BdConectn.db.Visitor.ToList();
            //VisCb.DisplayMemberPath = "Name" + "LastName";
            //var us = VisCb.SelectedItem as Visitor;

            StatexsCb.ItemsSource = DBConnect.db.ExecutionStage.ToList();
            StatexsCb.DisplayMemberPath = "Name";
            var st = (StatexsCb.SelectedItem as ExecutionStage);
            //StatexsCb = StatexsCb.SelectedIndex(1);
      




        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            visitorPass.ExecutionStage = StatexsCb.SelectedItem as ExecutionStage;
            MessageBox.Show("Успешно сохранено");
            DBConnect.db.SaveChanges();
        }

        private void StatexsCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatexsCb.SelectedIndex == 0) 
            {

                Otcas.Visibility = Visibility.Collapsed;
            }
            else
                Otcas.Visibility = Visibility.Visible;
        }
    }
}
