using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace DiplomAppMusicBase.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationSinger.xaml
    /// </summary>
    public partial class RegistrationSinger : Page
    {
        private Singers _currentSinger = new Singers();
        public RegistrationSinger(Singers selectedSinger)
        {
            InitializeComponent();
            var uriMainFon = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMainFon);
            SingerRegFon.Background = new ImageBrush(bitmapMain);
            DataContext = MusicStudioBaseEntities.GetContext().Singers.ToList();
            if (selectedSinger != _currentSinger)
                _currentSinger = selectedSinger;
            DataContext = _currentSinger;
        }

        private async void ListBack_Click(object sender, RoutedEventArgs e)
        {
            if (Manager.MFrame.CanGoBack)
            {
                await Task.Delay(500);
                Manager.MFrame.GoBack();
            }
            else
            {
                Manager.MFrame = null;
            }
        }

        private async void RegButtonSingerBT_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(NameSingerReg.Text))
                errors.AppendLine("Укажите имя исполнителя");
            if (string.IsNullOrEmpty(FamiliaSingerReg.Text))
                errors.AppendLine("Укажите фамилию исполнителя");
            if (string.IsNullOrEmpty(PatronymicSingerReg.Text))
                errors.AppendLine("Укажите отчество исполнителя");
            if (string.IsNullOrEmpty(NicknameSinger.Text))
                errors.AppendLine("Укажите никнейм исполнителя");
            if (string.IsNullOrEmpty(YearBirthdaySingerReg.Text))
                errors.AppendLine("Укажите день рождения исполнителя");
            if (string.IsNullOrEmpty(IDContractReg.Text))
                errors.AppendLine("Укажите ID договора");
            if (string.IsNullOrEmpty(IDProducerReg.Text))
                errors.AppendLine("Укажите ID продюсера");
            if (string.IsNullOrEmpty(IDTirageReg.Text))
                errors.AppendLine("Укажите ID тиража");
            if (string.IsNullOrEmpty(IDOrderReg.Text))
                errors.AppendLine("Укажите ID заказа");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            var ReditingSinger = MusicStudioBaseEntities.GetContext().Singers.FirstOrDefault(y => y.Nickname == NicknameSinger.Text);
            if (ReditingSinger != null)
            {
                ReditingSinger.NameSinger = NameSingerReg.Text;
                ReditingSinger.FamiliaSinger = FamiliaSingerReg.Text;
                ReditingSinger.PatronymicSinger = PatronymicSingerReg.Text;
                ReditingSinger.Nickname = NicknameSinger.Text;
                ReditingSinger.YearBirthday = DateTime.Parse(YearBirthdaySingerReg.Text);
                ReditingSinger.idContract = int.Parse(IDContractReg.Text);
                ReditingSinger.idProducer = int.Parse(IDProducerReg.Text);
                ReditingSinger.idTirage = int.Parse(IDTirageReg.Text);
                ReditingSinger.idOrder = int.Parse(IDOrderReg.Text);

                MusicStudioBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные исполнителя обновлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            { 
                try
                {
                    Singers SingerReg = new Singers()
                    {
                        NameSinger = NameSingerReg.Text,
                        FamiliaSinger = FamiliaSingerReg.Text,
                        PatronymicSinger = PatronymicSingerReg.Text,
                        Nickname = NicknameSinger.Text,
                        YearBirthday = DateTime.Parse(YearBirthdaySingerReg.Text),
                        idContract = int.Parse(IDContractReg.Text),
                        idProducer = int.Parse(IDProducerReg.Text),
                        idTirage = int.Parse(IDTirageReg.Text),
                        idOrder = int.Parse(IDOrderReg.Text),
                    };
                    await Task.Delay(500);
                    MusicStudioBaseEntities.GetContext().Singers.Add(SingerReg);
                    MusicStudioBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Исполнитель добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NameSingerReg.Text = "";
                    FamiliaSingerReg.Text = "";
                    PatronymicSingerReg.Text = "";
                    NicknameSinger.Text = "";
                    YearBirthdaySingerReg.Text = "";
                    IDContractReg.Text = "";
                    IDProducerReg.Text = "";
                    IDTirageReg.Text = "";
                    IDOrderReg.Text = "";
                    Manager.MFrame.Navigate(new Pages.ListSingers());
                }
                catch
                {
                    await Task.Delay(500);
                    MessageBox.Show("Ошибка при добавлении данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private async void yearbirthdaysinger_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(500);
            YearBirthdaySingerReg.Text = yearbirthdaysinger.SelectedDate.ToString();
            yearbirthdaysinger.Visibility = Visibility.Collapsed;
        }

        private async void ContractInfo_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.ListContracts());
        }

        private async void ProducerInfo_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.ListProducers());
        }

        private async void TirageInfo_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.ListTirage());
        }

        private async void OrderInfo_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.ListOrders());
        }
    }
}
