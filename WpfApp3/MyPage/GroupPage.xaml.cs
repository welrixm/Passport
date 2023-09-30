using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
            
            SubdivisionCb.ItemsSource = DBConnect.db.Subdivision.ToList();
            SubdivisionCb.DisplayMemberPath = "Name";
            VisitPurposeCb.ItemsSource = DBConnect.db.GoalVisit.ToList();
            VisitPurposeCb.DisplayMemberPath = "Name";
            DateEndTb.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string firstname = LastNameTb.Text.Trim();
            string secondname = NameTb.Text.Trim();
            string email = MailTb.Text.Trim();
            string note = PrimTb.Text.Trim();
            string series = CerTb.Text.Trim();
            string number = NumTb.Text.Trim();
            string datebirth = DateTb.Text.Trim();


            //string patron = PadingTb.Text.Trim();
            //string phone = PHONEBT.Text.Trim();
            //string organiz = OrgTb.Text.Trim();
            if (firstname.Length == 0 && secondname.Length == 0 && email.Length == 0 && note.Length == 0 && 
                series.Length ==0 && number.Length ==0 && datebirth.Length ==0)
            {
                MessageBox.Show("Пусто");
            }
            else
            {
                var us = DBConnect.db.Visitor.Where(x => x.LastName == LastNameTb.Text.Trim() && x.Name == NameTb.Text.Trim() && x.Patronimic == PadingTb.Text.Trim()).FirstOrDefault();
                if (us == null)
                {
                    
                    //if (firstname.Length == 0 && secondname.Length == 0 && email.Length == 0 && note.Length == 0 &&
                    //    series.Length == 0 && number.Length == 0 && datebirth.Length == 0)
                    //{
                    //    MessageBox.Show("Пусто");
                    //}
                  
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
                                                        PassportNum = NumTb.Text.Trim(),
                                                        Photo = image,
                                                    };
                    
                    
                    string subdiv = SubdivisionCb.Text.Trim();
                    if (subdiv.Length == 0)
                    {
                        MessageBox.Show("Пусто");
                    }
                    else
                    {
                        if(series.Length < 4 && number.Length < 6)
                        {
                            MessageBox.Show("Длина серии и номера паспорта неверны");
                        }
                        else
                        {
                            var sub = SubdivisionCb.SelectedItem as Subdivision;
                            var ifo = DBConnect.db.Employee.ToList().FirstOrDefault(x => x.SubdivisionId == sub.Id);
                            DBConnect.db.VisitorPass.Add(new VisitorPass
                            {

                                Visitor = vis,
                                Pass = new Pass
                                {
                                    DesiredStartDate = (DateTime)DateNewTb.SelectedDate,
                                    DesiredEndDate = (DateTime)DateEndTb.SelectedDate,
                                    Employee = ifo,
                                    GoalVisit = VisitPurposeCb.SelectedItem as GoalVisit,
                                },
                            });
                            MessageBox.Show("Успешно создано");
                            DBConnect.db.SaveChanges();
                        }
                        
                    }
                    
                }

                else
                {
                    var sub = SubdivisionCb.SelectedItem as Subdivision;
                    var ifo = DBConnect.db.Employee.ToList().FirstOrDefault(x => x.SubdivisionId == sub.Id);
                    MessageBox.Show("Такой пользователь уже существует");
                    DBConnect.db.VisitorPass.Add(new VisitorPass
                    {
                        Visitor = us,
                        Pass = new Pass
                        {
                            DesiredStartDate = (DateTime)DateNewTb.SelectedDate,
                            DesiredEndDate = (DateTime)DateEndTb.SelectedDate,
                            Employee = ifo,
                            GoalVisit = VisitPurposeCb.SelectedItem as GoalVisit,
                        },
                    });
                    MessageBox.Show("заявка на существующего посетителя создана");
                    DBConnect.db.SaveChanges();
                }
            }
            
        }

        private void SubdivisionCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sub = SubdivisionCb.SelectedItem as Subdivision;
            var ifo = DBConnect.db.Employee.ToList().FirstOrDefault(x => x.SubdivisionId == sub.Id);
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
           
             //|| DateNewTb.SelectedDate == DateEndTb.SelectedDate
          
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
            if (MailTb.Text.EndsWith("@gmail.com"))
            {
                
            }
           
            else
            {
                MessageBox.Show("Неверный адрес электронной почты. ");
                MailTb.Text = string.Empty;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private void DateTb_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime date = (DateTime)DateTb.SelectedDate;
                var tod = DateTime.Today;



                var mind = new DateTime((tod.Year - 16), tod.Month, tod.Day);
                if (date >= mind)
                {
                    MessageBox.Show("Вам должно быть не меньше 16 лет", "Уведомление");
                    DateTb.SelectedDate = null;
                }

                //Up();
            }
            catch { }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            //DateNewTb.Text = "";
            //DateEndTb.Text = "";
            VisitPurposeCb.Text = "";
            //SubdivisionCb.ItemsSource = "";
            NSPTb.Text = "";
            LastNameTb.Text = "";
            NameTb.Text = "";
            PadingTb.Text = "";
            PHONEBT.Text = "";
            MailTb.Text = "";
            OrgTb.Text = "";
            PrimTb.Text = "";
            DateTb.Text = "";
            CerTb.Text = "";
            NumTb.Text = "";
            

        }

        private void DateNewTb_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateEndTb.IsEnabled = false;
            if (DateNewTb.SelectedDate != null && DateNewTb.SelectedDate < DateTime.Today.AddDays(1) || DateNewTb.SelectedDate > DateTime.Today.AddDays(15))
            {
                MessageBox.Show("Можно выбрать дату, начиная с завтрашнего дня и не позже 15 дней вперед");
               // DateNewTb.SelectedDate = null;
            }
            else if (DateNewTb.SelectedDate == null)
            {
                DateEndTb.IsEnabled = false;
            }
            else
            {
                DateEndTb.IsEnabled = true;
            }
        }

        private void DateEndTb_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = (DateTime)DateNewTb.SelectedDate;

            if (DateEndTb.SelectedDate != null && DateEndTb.SelectedDate > date.AddDays(15))
            {
                MessageBox.Show("Нельзя выбрать дату окончания, если промежуток с началом больше 15 дней");
               // DateEndTb.SelectedDate = null;
            }
            try
            {
                DateTime dateEnd = (DateTime)DateEndTb.SelectedDate;
                if ( dateEnd < date )
                {
                    MessageBox.Show("Дата окончания должна равняться дате начала или быть позже даты начала");
                    //DateEndTb.SelectedDate = null;
                }
            }
            catch
            {

            }


        }
    }
    
}
