using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для GroupPage.xaml
    /// </summary>
    public partial class GroupPage : Page
    {


        public Visitor vis { get; set; }
        public static byte[] image { get; set; }
        public GroupPage()
        {
            InitializeComponent();
            SubdivisionCb.ItemsSource = BdConectn.db.Subdivision.ToList();
            SubdivisionCb.DisplayMemberPath = "Title";
            VisitPurposeCb.ItemsSource = BdConectn.db.VisitPurpose.ToList();
            VisitPurposeCb.DisplayMemberPath = "Title";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            var us = BdConectn.db.Visitor.Where(x => x.LastName == LastNameTb.Text.Trim() && x.Name == NameTb.Text.Trim() && x.Patronimic == PadingTb.Text.Trim()).FirstOrDefault();
            if (us == null)
            {

                vis = new Visitor
                {
                    LastName = LastNameTb.Text.Trim(),
                    Name = NameTb.Text.Trim(),
                    Patronimic = PadingTb.Text.Trim(),
                    Phone = PHONEBT.Text.Trim(),
                    Email = MailTb.Text.Trim(),
                    Organization = OrgTb.Text.Trim(),
                    Note = PrimTb.Text.Trim(),
                    DateOfBirth = (DateTime)DateTb.SelectedDate,
                    PassportSeries = CerTb.Text.Trim(),
                    OassportNum = NumTb.Text.Trim(),
                    Photo = image,
                };
                var sub = SubdivisionCb.SelectedItem as Subdivision;
                var ifo = BdConectn.db.Employee.ToList().FirstOrDefault(x => x.SubdivisionId == sub.Id);
                BdConectn.db.VisitorPass.Add(new VisitorPass
                {

                    Visitor = vis,
                    Pass = new Pass
                    {
                        DesiredStartDate = (DateTime)DateNewTb.SelectedDate,
                        DesiredEndDate = (DateTime)DateEndTb.SelectedDate,
                        Employee = ifo,
                        VisitPurpose = VisitPurposeCb.SelectedItem as VisitPurpose,
                    },
                }) ;
                MessageBox.Show("заявка создана");
                BdConectn.db.SaveChanges();
            }

            else
            {
                var sub = SubdivisionCb.SelectedItem as Subdivision;
                var ifo = BdConectn.db.Employee.ToList().FirstOrDefault(x => x.SubdivisionId == sub.Id);
                MessageBox.Show("Такой посетитель уже существет");
                BdConectn.db.VisitorPass.Add(new VisitorPass
                {
                Visitor = us,
                Pass = new Pass {
                    DesiredStartDate = (DateTime)DateNewTb.SelectedDate,
                    DesiredEndDate = (DateTime)DateEndTb.SelectedDate,
                    Employee = ifo,
                    VisitPurpose = VisitPurposeCb.SelectedItem as VisitPurpose,
                },
                });
                MessageBox.Show("заявка на существующего посетителя создана");
                BdConectn.db.SaveChanges();
            } 
        }

        private void SubdivisionCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sub = SubdivisionCb.SelectedItem as Subdivision;
            var ifo = BdConectn.db.Employee.ToList().FirstOrDefault(x => x.SubdivisionId == sub.Id);
            NSPTb.Text = ifo.LastName + " " + ifo.Name + " " + ifo.Patronimyc;
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
                ImageVisitor.Source = new BitmapImage(new Uri(openFil.FileName));
            }
        }

        private void LastNameTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

      

        private void OrgTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
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

        private void CerTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void MailTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(MailTb.Text, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                e.Handled = true;
            }
        }

        private void PHONEBT_TextChanged(object sender, TextChangedEventArgs e)
        {
           
             
          
        }

        private void PHONEBT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }
        private bool IsNumeric(string value)
        {
            return int.TryParse(value, out int result); 
        }

        private void MailTb_KeyUp(object sender, KeyEventArgs e)
        {
            if (Regex.IsMatch(MailTb.Text, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                e.Handled = true;
            }
        }

        private void MailTb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MailTb.Text.EndsWith("@mail.ru"))
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
    }
    
}
