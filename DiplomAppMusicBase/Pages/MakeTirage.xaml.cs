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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiplomAppMusicBase.Pages
{
    /// <summary>
    /// Логика взаимодействия для MakeTirage.xaml
    /// </summary>
    public partial class MakeTirage : Page
    {
        private Tirage _currentTirage = new Tirage();
        public MakeTirage(Tirage selectedTirage)
        {
            InitializeComponent();
            var uriMain = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMain);
            TirageFon.Background = new ImageBrush(bitmapMain);
            DataContext = MusicStudioBaseEntities.GetContext().Tirage.ToList();
            if (selectedTirage != null)
                _currentTirage = selectedTirage;
            DataContext = _currentTirage;
        }

        private async void startDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(500);
            if (endDatePicker.SelectedDate.HasValue && startDatePicker.SelectedDate > endDatePicker.SelectedDate)
            {
                endDatePicker.SelectedDate = startDatePicker.SelectedDate;
            }

            DataStartTirage.Text = startDatePicker.SelectedDate.ToString();
            startDatePicker.Visibility = Visibility.Collapsed;
        }

        private async void endDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(500);
            if (startDatePicker.SelectedDate.HasValue && endDatePicker.SelectedDate < startDatePicker.SelectedDate)
            {
                endDatePicker.SelectedDate = startDatePicker.SelectedDate;
            }

            DataEndTirage.Text = endDatePicker.SelectedDate.ToString();
            endDatePicker.Visibility = Visibility.Collapsed;
        }

        private async void SaveTirage_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(NameTirage.Text))
                errors.AppendLine("Укажите размер тиража");
            if (string.IsNullOrEmpty(NameSingerTirage.Text))
                errors.AppendLine("Укажите имя исполнителя");
            if (string.IsNullOrEmpty(FamiliaSingerTirage.Text))
                errors.AppendLine("Укажите фамилию исполнителя");
            if (string.IsNullOrEmpty(PatronymicSingerTirage.Text))
                errors.AppendLine("Укажите отчество исполнителя");
            if (string.IsNullOrEmpty(NameAlbomTirage.Text))
                errors.AppendLine("Укажите имя альбома");
            if (string.IsNullOrEmpty(CountAlbomsTirage.Text))
                errors.AppendLine("Укажите количество альбомом");
            if (string.IsNullOrEmpty(PriceOneAlbom.Text))
                errors.AppendLine("Укажите цену 1 альбома");
            if (string.IsNullOrEmpty(CityTirage.Text))
                errors.AppendLine("Укажите город тиража");
            if (string.IsNullOrEmpty(DataStartTirage.Text))
                errors.AppendLine("Укажите начальную дату тиража");
            if (string.IsNullOrEmpty(DataEndTirage.Text))
                errors.AppendLine("Укажите конечную дату тиража");
            if (string.IsNullOrEmpty(ItogProfit.Text))
                errors.AppendLine("Укажите итоговую инкассацию");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            var ReditingTirage = MusicStudioBaseEntities.GetContext().Tirage.FirstOrDefault(y => y.NameAlbom == NameAlbomTirage.Text);
            if (ReditingTirage != null)
            {
                ReditingTirage.NameTirage = NameTirage.Text;
                ReditingTirage.NameSinger = NameSingerTirage.Text;
                ReditingTirage.FamiliaSinger = FamiliaSingerTirage.Text;
                ReditingTirage.PatronymicSinger = PatronymicSingerTirage.Text;
                ReditingTirage.NameAlbom = NameAlbomTirage.Text;
                ReditingTirage.CountAlboms = int.Parse(CountAlbomsTirage.Text);
                ReditingTirage.Price = (decimal?)SqlMoney.Parse(PriceOneAlbom.Text);
                ReditingTirage.City = CityTirage.Text;
                ReditingTirage.TimeStart = DateTime.Parse(DataStartTirage.Text);
                ReditingTirage.TimeEnd = DateTime.Parse(DataStartTirage.Text);
                ReditingTirage.Profit = (decimal?)SqlMoney.Parse(ItogProfit.Text);

                MusicStudioBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Тираж обновлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            { 
                try
                {
                    Tirage newTirage = new Tirage()
                    {
                        NameTirage = NameTirage.Text,
                        NameSinger = NameSingerTirage.Text,
                        FamiliaSinger = FamiliaSingerTirage.Text,
                        PatronymicSinger = PatronymicSingerTirage.Text,
                        NameAlbom = NameAlbomTirage.Text,
                        CountAlboms = int.Parse(CountAlbomsTirage.Text),
                        Price = (decimal?)SqlMoney.Parse(PriceOneAlbom.Text),
                        City = CityTirage.Text,
                        TimeStart = DateTime.Parse(DataStartTirage.Text),
                        TimeEnd = DateTime.Parse(DataStartTirage.Text),
                        Profit = (decimal?)SqlMoney.Parse(ItogProfit.Text)
                    };
                    await Task.Delay(500);
                    MusicStudioBaseEntities.GetContext().Tirage.Add(newTirage);
                    MusicStudioBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Тираж создан!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NameTirage.Text = "";
                    NameSingerTirage.Text = "";
                    FamiliaSingerTirage.Text = "";
                    PatronymicSingerTirage.Text = "";
                    NameAlbomTirage.Text = "";
                    CountAlbomsTirage.Text = "";
                    PriceOneAlbom.Text = "";
                    CityTirage.Text = "";
                    DataStartTirage.Text = "";
                    DataStartTirage.Text = "";
                    ItogProfit.Text = "";
                    Manager.MFrame.Navigate(new Pages.ListTirage());
                }
                catch
                {
                    await Task.Delay(500);
                    MessageBox.Show("Ошибка при добавлении данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private async void BackTirage_Click(object sender, RoutedEventArgs e)
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

        private async void ListTirage_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.ListTirage());
        }

        private void CountAlbomsTirage_TextChanged(object sender, TextChangedEventArgs e)
        {
            RecalculateProfit();
        }

        private void PriceOneAlbom_TextChanged(object sender, TextChangedEventArgs e)
        {
            RecalculateProfit();
        }

        private void RecalculateProfit()
        {
            if (int.TryParse(CountAlbomsTirage.Text, out int count) && decimal.TryParse(PriceOneAlbom.Text, out decimal price))
            {
                decimal totalProfit = count * price;

                ItogProfit.Text = totalProfit.ToString("C");
            }
            else
            {
                ItogProfit.Text = "Ошибка";
            }
        }
    }
}
