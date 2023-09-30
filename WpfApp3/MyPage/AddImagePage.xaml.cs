using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using WpfApp3.Componens;
using WpfApp3.MyPage;


namespace WpfApp3.MyPage
{
    /// <summary>
    /// Логика взаимодействия для AddImagePage.xaml
    /// </summary>
    public partial class AddImagePage : Window
    {
        public static byte[] image { get; set; }
        public Visitor visitor { get; set; }
        public AddImagePage(Visitor _visitor)
        {
            visitor = _visitor;
            InitializeComponent();
            VisitTb.Text = visitor.LastName ;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFil = new OpenFileDialog()
            {
                Filter = "**.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg",
            };
            if (openFil.ShowDialog().GetValueOrDefault())
            {
                image = File.ReadAllBytes(openFil.FileName);
                PhoteImage.Source = new BitmapImage(new Uri(openFil.FileName));
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var patronid = visitor.Id;
            DBConnect.db.Document.Add(new Document
            {
                VisitorId = visitor.Id,
                Photo = image,
            }) ;
            DBConnect.db.SaveChanges();
            MessageBox.Show("Изображение добавлено");
            Close();
        }
    }
}
