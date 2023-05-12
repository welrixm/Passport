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
    /// Логика взаимодействия для PersonalPage.xaml
    /// </summary>
    public partial class PersonalPage : Page
    {
        public Visitor vis { get; set; }
        public PersonalPage()
        {
            InitializeComponent();
            SubdivisionCb.ItemsSource = BdConectn.db.Subdivision.ToList();
            SubdivisionCb.DisplayMemberPath = "Title";
            VisitPurposeCb.ItemsSource = BdConectn.db.VisitPurpose.ToList();
            VisitPurposeCb.DisplayMemberPath = "Title";
        }

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

    }
}
