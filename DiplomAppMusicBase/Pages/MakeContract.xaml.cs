using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiplomAppMusicBase.Pages
{
    /// <summary>
    /// Логика взаимодействия для MakeContract.xaml
    /// </summary>
    public partial class MakeContract : Page
    {
        public MakeContract()
        {
            InitializeComponent();
            var uriMain = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMain);
            ContractFon.Background = new ImageBrush(bitmapMain);
            DataContext = MusicStudioBaseEntities.GetContext().Contracts.ToList();
        }

        private async void SaveContract_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            NameContractTB.IsEnabled = false;
            NameSingerTB.IsEnabled = false;
            FamiliaSingerTB.IsEnabled = false;
            PatronymicSingerTB.IsEnabled = false;
            MusicStudioTB.IsEnabled = false;
            NameProducerTB.IsEnabled = false;
            CountCompositionsTB.IsEnabled = false;
            ExperienceTB.IsEnabled = false;
            ProfitTB.IsEnabled = false;
            AddContractBT.Visibility = Visibility.Visible;
            RedContractBT.Visibility = Visibility.Visible;
            PrintContractBT.Visibility = Visibility.Visible;
            SaveContract.Visibility = Visibility.Hidden;
            BackContract.Visibility = Visibility.Hidden;
            ListContract.Visibility = Visibility.Hidden;
        }

        private async void BackContract_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            if (Manager.MFrame.CanGoBack)
            {
                Manager.MFrame.GoBack();
            }
            else
            {
                Manager.MFrame = null;
            }
        }

        private async void ListContract_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.ListContracts());
        }

        private async void PickerDateEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(500);
            if (PickerDateStart.SelectedDate.HasValue && PickerDateEnd.SelectedDate < PickerDateStart.SelectedDate)
            {
                PickerDateEnd.SelectedDate = PickerDateStart.SelectedDate;
            }

            DateEndTB.Text = PickerDateEnd.SelectedDate.ToString();
            PickerDateEnd.Visibility = Visibility.Collapsed;
        }

        private async void PickerDateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(500);
            if (PickerDateEnd.SelectedDate.HasValue && PickerDateStart.SelectedDate > PickerDateEnd.SelectedDate)
            {
                PickerDateEnd.SelectedDate = PickerDateStart.SelectedDate;
            }

            DateStartTB.Text = PickerDateStart.SelectedDate.ToString();
            PickerDateStart.Visibility = Visibility.Collapsed;
        }

        private async void PickerDayBirthday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(500);
            DayBirthdayTB.Text = PickerDayBirthday.SelectedDate.ToString();
            PickerDayBirthday.Visibility = Visibility.Collapsed;
        }

        private async void AddContractBT_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(NameContractTB.Text))
                errors.AppendLine("Укажите название договора");
            if (string.IsNullOrEmpty(NameSingerTB.Text))
                errors.AppendLine("Укажите имя исполнителя");
            if (string.IsNullOrEmpty(FamiliaSingerTB.Text))
                errors.AppendLine("Укажите фамилию исполнителя");
            if (string.IsNullOrEmpty(PatronymicSingerTB.Text))
                errors.AppendLine("Укажите отчество исполнителя");
            if (string.IsNullOrEmpty(MusicStudioTB.Text))
                errors.AppendLine("Укажите студии звукозаписи");
            if (string.IsNullOrEmpty(NameProducerTB.Text))
                errors.AppendLine("Укажите имя продюсера");
            if (string.IsNullOrEmpty(DayBirthdayTB.Text))
                errors.AppendLine("Укажите дату рождения исполнителя");
            if (string.IsNullOrEmpty(CountCompositionsTB.Text))
                errors.AppendLine("Укажите количество композиций");
            if (string.IsNullOrEmpty(ExperienceTB.Text))
                errors.AppendLine("Укажите достоверность опыта работы исполнителя");
            if (string.IsNullOrEmpty(DateStartTB.Text))
                errors.AppendLine("Укажите начало сотрдничества");
            if (string.IsNullOrEmpty(DateEndTB.Text))
                errors.AppendLine("Укажите конец сотрудничества");
            if (string.IsNullOrEmpty(ProfitTB.Text))
                errors.AppendLine("Укажите итоговый гонорар");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (MusicStudioBaseEntities.GetContext().Contracts.Count(y => y.NameContract == NameContractTB.Text) > 0)
            {
                await Task.Delay(500);
                MessageBox.Show("Заказ уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                Contracts UserContract = new Contracts()
                {
                    NameContract = NameContractTB.Text,
                    NameSinger = NameSingerTB.Text,
                    FamiliaSinger = FamiliaSingerTB.Text,
                    PatronymicSinger = PatronymicSingerTB.Text,
                    MusicStudio = MusicStudioTB.Text,
                    NameProducer = NameProducerTB.Text,
                    YearBirthday = DateTime.Parse(DayBirthdayTB.Text),
                    CountCompositons = int.Parse(CountCompositionsTB.Text),
                    Experience = ExperienceTB.Text,
                    DataStart = DateTime.Parse(DateStartTB.Text),
                    DataEnd = DateTime.Parse(DateEndTB.Text),
                    Profit = int.Parse(ProfitTB.Text)
                };
                await Task.Delay(500);
                MusicStudioBaseEntities.GetContext().Contracts.Add(UserContract);
                MusicStudioBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные добавлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NameContractTB.Text = "";
                NameSingerTB.Text = "";
                FamiliaSingerTB.Text = "";
                PatronymicSingerTB.Text = "";
                MusicStudioTB.Text = "";
                NameProducerTB.Text = "";
                DayBirthdayTB.Text = "";
                CountCompositionsTB.Text = "";
                ExperienceTB.Text = "";
                DateStartTB.Text = "";
                DateEndTB.Text = "";
                ProfitTB.Text = "";
                NameContractTB.IsEnabled = true;
                NameSingerTB.IsEnabled = true;
                FamiliaSingerTB.IsEnabled = true;
                PatronymicSingerTB.IsEnabled = true;
                MusicStudioTB.IsEnabled = true;
                NameProducerTB.IsEnabled = true;
                CountCompositionsTB.IsEnabled = true;
                ExperienceTB.IsEnabled = true;
                ProfitTB.IsEnabled = true;
                AddContractBT.Visibility = Visibility.Hidden;
                RedContractBT.Visibility = Visibility.Hidden;
                PrintContractBT.Visibility = Visibility.Hidden;
                SaveContract.Visibility = Visibility.Visible;
                BackContract.Visibility = Visibility.Visible;
                ListContract.Visibility = Visibility.Visible;
                PickerDateStart.Visibility = Visibility.Visible;
                PickerDayBirthday.Visibility = Visibility.Visible;
                PickerDateEnd.Visibility = Visibility.Visible;
            }
            catch
            {
                await Task.Delay(500);
                MessageBox.Show("Ошибка при добавлении данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private async void RedContractBT_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            NameContractTB.IsEnabled = true;
            NameSingerTB.IsEnabled = true;
            FamiliaSingerTB.IsEnabled = true;
            PatronymicSingerTB.IsEnabled = true;
            MusicStudioTB.IsEnabled = true;
            NameProducerTB.IsEnabled = true;
            CountCompositionsTB.IsEnabled = true;
            ExperienceTB.IsEnabled = true;
            ProfitTB.IsEnabled = true;
            AddContractBT.Visibility = Visibility.Hidden;
            RedContractBT.Visibility = Visibility.Hidden;
            PrintContractBT.Visibility = Visibility.Hidden;
            SaveContract.Visibility = Visibility.Visible;
            BackContract.Visibility = Visibility.Visible;
            ListContract.Visibility = Visibility.Visible;
            PickerDateStart.Visibility = Visibility.Visible;
            PickerDayBirthday.Visibility = Visibility.Visible;
            PickerDateEnd.Visibility = Visibility.Visible;
        }

        private async void PrintContractBT_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            List<string> textList = new List<string>();
            textList.Add(NameContractTB.Text);
            textList.Add(NameSingerTB.Text);
            textList.Add(FamiliaSingerTB.Text);
            textList.Add(PatronymicSingerTB.Text);
            textList.Add(MusicStudioTB.Text);
            textList.Add(NameProducerTB.Text);
            textList.Add(DayBirthdayTB.Text);
            textList.Add(CountCompositionsTB.Text);
            textList.Add(ExperienceTB.Text);
            textList.Add(DateStartTB.Text);
            textList.Add(DateEndTB.Text);
            textList.Add(ProfitTB.Text);
            Manager.MFrame.Navigate(new Pages.InformationContract(textList));
        }

        private async void DateEndTB_GotFocus(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            PickerDateEnd.Visibility = Visibility.Visible;
        }

        private async void DateStartTB_GotFocus(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            PickerDateStart.Visibility = Visibility.Visible;
        }

        private async void DayBirthdayTB_GotFocus(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            PickerDayBirthday.Visibility = Visibility.Visible;
        }
    }
}
