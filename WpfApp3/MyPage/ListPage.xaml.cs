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
    /// Логика взаимодействия для PassListOage.xaml
    /// </summary>
    public partial class PassListOage : Page
    {
        public IEnumerable<Visitor> visitors
        {
            get { return (IEnumerable<Visitor>)GetValue(visitorsProperty); }
            set { SetValue(visitorsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for visitors.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty visitorsProperty =
            DependencyProperty.Register("visitors", typeof(IEnumerable<Visitor>), typeof(PassListOage));



        public IEnumerable<Pass> passes
        {
            get { return (IEnumerable<Pass>)GetValue(passesProperty); }
            set { SetValue(passesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for passes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty passesProperty =
            DependencyProperty.Register("passes", typeof(IEnumerable<Pass>), typeof(PassListOage));



        public IEnumerable<VisitorPass> VisitorPasses
        {
            get { return (IEnumerable<VisitorPass>)GetValue(VisitorPassesProperty); }
            set { SetValue(VisitorPassesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cispas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisitorPassesProperty =
            DependencyProperty.Register("VisitorPasses", typeof(IEnumerable<VisitorPass>), typeof(PassListOage));



        public PassListOage()
        {
            InitializeComponent();
            var db = DBConnect.db;
            db.Visitor.ToList();
            db.VisitorPass.ToList();
            db.Pass.ToList();
            VisitorPasses = db.VisitorPass.ToList();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var visitor = (sender as Button).DataContext as VisitorPass;
            NavigationService.Navigate(new EditVisPasPage(visitor));   

        }

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            var db = DBConnect.db;
            db.Visitor.ToList();
            db.VisitorPass.ToList();
            db.Pass.ToList();
            VisitorPasses = db.VisitorPass.Where(x => x.ExecutionStageId == 1).ToList();
        }

        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            var db = DBConnect.db;
            db.Visitor.ToList();
            db.VisitorPass.ToList();
            db.Pass.ToList();
            VisitorPasses = db.VisitorPass.Where(x => x.ExecutionStageId == 2).ToList();
        }
    }
}
