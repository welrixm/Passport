using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using System.Data.SqlClient;
using WpfApp3.MyPage;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Win32;

namespace WpfApp3.MyPage
{
    /// <summary>
    /// Логика взаимодействия для PersonalPage.xaml
    /// </summary>
    public partial class PersonalPage : Page
    {
        private ObservableCollection<Visitor> _people = new ObservableCollection<Visitor>();
        public Visitor vis { get; set; }
        public PersonalPage()
        {
            InitializeComponent();
            SubdivisionCb.ItemsSource = BdConectn.db.Subdivision.ToList();
            SubdivisionCb.DisplayMemberPath = "Title";
            VisitPurposeCb.ItemsSource = BdConectn.db.VisitPurpose.ToList();
            VisitPurposeCb.DisplayMemberPath = "Title";
           
            var db = BdConectn.db;
            db.Visitor.ToList();
            db.VisitorPass.ToList();
            visitors = db.Visitor.ToList();
        }



        public IEnumerable<Visitor> visitors
        {
            get { return (IEnumerable<Visitor>)GetValue(visitorsProperty); }
            set { SetValue(visitorsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for visitors.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty visitorsProperty =
            DependencyProperty.Register("visitors", typeof(IEnumerable<Visitor>), typeof(PersonalPage));


        #region

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (FastNameTb.Text.Length > 0 && NameTb.Text.Length > 0 && PadingTb.Text.Length > 0)
            {
                if (PHONEBT.Text.Length > 0 && MailTb.Text.Length > 0 && OrgTb.Text.Length>0)
                {
                    if (DateTb != null)
                    {
                        if (CerTb.Text.Length > 0  && NumTb.Text.Length > 0 )
                        {

                            vis = new Visitor
                            {
                                LastName = FastNameTb.Text.Trim(),
                                Name = NameTb.Text.Trim(),
                                Patronimic = PadingTb.Text.Trim(),
                                Phone = PHONEBT.Text.Trim(),
                                Email = MailTb.Text.Trim(),
                                Organization = OrgTb.Text.Trim(),
                                Note = PrimTb.Text.Trim(),
                                DateOfBirth = (DateTime)DateTb.SelectedDate,
                                PassportSeries = CerTb.Text.Trim(),
                                OassportNum = NumTb.Text.Trim(),

                            };
                            BdConectn.db.Visitor.Add(vis);
                            BdConectn.db.SaveChanges();
                            MessageBox.Show("Пользователь создан");
                        }
                        else MessageBox.Show("Заполните поля серии и номера паспорта");

                    }
                    else MessageBox.Show("Заполните дату рождения");
                }
                else MessageBox.Show("вы не заполнили поля телефона или почты или организации");
            }
            else MessageBox.Show("Проверьте заполнение фио");
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var sub = SubdivisionCb.SelectedItem as Subdivision;
            var ifo = BdConectn.db.Employee.ToList().FirstOrDefault(x => x.SubdivisionId == sub.Id);
            var visi = BdConectn.db.Visitor.Where(x => x.LastName == FastNameTb.Text && x.Name == NameTb.Text && x.Patronimic == PadingTb.Text).FirstOrDefault();
            BdConectn.db.VisitorPass.Add(new VisitorPass 
            { Visitor = vis,
            Pass = new Pass 
            {

                DesiredStartDate = (DateTime)DateNewTb.SelectedDate,
                DesiredEndDate = (DateTime)DateEndTb.SelectedDate,
                Employee = ifo,
                VisitPurpose = VisitPurposeCb.SelectedItem as VisitPurpose,
            },
                
                });
            
            MessageBox.Show("Заявка создана");
            BdConectn.db.SaveChanges();
        }
        private void FastNameTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }

        }

        private void NameTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void PadingTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void PHONEBT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void OrgTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void CerTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void NumTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void AddImageBtn_Click(object sender, RoutedEventArgs e) =>
            new AddImagePage(vis).ShowDialog();

        private void SubdivisionCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sub = SubdivisionCb.SelectedItem as Subdivision;
            var ifo = BdConectn.db.Employee.ToList().FirstOrDefault(x => x.SubdivisionId == sub.Id);
            NSPTb.Text = ifo.LastName + " " + ifo.Name + " " + ifo.Patronimyc ;
        }

        private void MailTb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MailTb.Text.EndsWith("@mail.ru"))
            {

            }
            else if (MailTb.Text == string.Empty)
            {

            }
           

            else if (MailTb.Text.EndsWith("@gmail.com"))
            {

            }
            else if (MailTb.Text.EndsWith("@yandex.ru"))
            {

            }
            else if (MailTb.Text.EndsWith("@inbox.ru"))
            {

            }
            else if (MailTb.Text.EndsWith("@bk.ru"))
            {

            }
            else if (MailTb.Text.EndsWith("@hotmail.com"))
            {

            }
            
           

            else
            {
                MessageBox.Show("Неверный адрес электронной почты. Возможное окончание почты: @yandex.ru\r\n@mail.ru\r\n@inbox.ru\r\n@bk.ru\r\n@hotmail.com\r\n@gmail.com");
                MailTb.Text = string.Empty;
            }
        }
        #endregion
        public class Person
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Phone { get; set; }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFil = new OpenFileDialog()
            {
                Filter = "**.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg",
            };
            //if (openFil.ShowDialog().GetValueOrDefault())
            //{
            //    image = File.ReadAllBytes(openFil.FileName);
            //    ImageVisitor.Source = new BitmapImage(new Uri(openFil.FileName));
            //}
        }
    }

}
