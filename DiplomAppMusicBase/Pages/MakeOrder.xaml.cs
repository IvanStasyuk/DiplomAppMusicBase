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
    /// Логика взаимодействия для MakeOrder.xaml
    /// </summary>
    public partial class MakeOrder : Page
    {
        private List<string> instruments = new List<string> { "Гитара", "Фортепиано", "Скрипка", "Саксофон", "Флейта", "Барабаны", "Труба", "Клавишные", "Голос", "Бас-гитара" };
        private List<string> janrs = new List<string> { "Поп", "Рок", "Металл", "Кантри", "Блюз", "Электронная", "Классическая", "Фанк", "Хаус", "Соул" };
        private List<string> effects = new List<string> { "Дилей", "Реверберация", "Фленжер", "Эхо", "Хорус", "Компрессия", "Дисторшн", "Эквализация", "Фазовращение", "Вокодер" };
        private List<int> countmusic = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };
        public MakeOrder()
        {
            InitializeComponent();
            var uriMain = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMain);
            OrderFon.Background = new ImageBrush(bitmapMain);
            DataContext = MusicStudioBaseEntities.GetContext().Orders.ToList();

            InstrumentOrder.ItemsSource = instruments;
            JanrOrder.ItemsSource = janrs;
            EffectOrder.ItemsSource = effects;
            CountCompositionsOrder.ItemsSource = countmusic;
        }

        private async void DataStartOrder_GotFocus(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            startDatePicker.Visibility = Visibility.Visible;
        }

        private async void DataEndOrder_GotFocus(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            endDatePicker.Visibility = Visibility.Visible;
        }

        private async void startDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(500);
            if (endDatePicker.SelectedDate.HasValue && startDatePicker.SelectedDate > endDatePicker.SelectedDate)
            {
                endDatePicker.SelectedDate = startDatePicker.SelectedDate;
            }

            DataStartOrder.Text = startDatePicker.SelectedDate.ToString();
            startDatePicker.Visibility = Visibility.Collapsed;
        }

        private async void endDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(500);
            if (startDatePicker.SelectedDate.HasValue && endDatePicker.SelectedDate < startDatePicker.SelectedDate)
            {
                endDatePicker.SelectedDate = startDatePicker.SelectedDate;
            }

            DataEndOrder.Text = endDatePicker.SelectedDate.ToString();
            endDatePicker.Visibility = Visibility.Collapsed;
        }

        private async void SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(NameOrder.Text))
                errors.AppendLine("Укажите название заказа");
            if (string.IsNullOrEmpty(NameSingerOrder.Text))
                errors.AppendLine("Укажите имя исполнителя");
            if (string.IsNullOrEmpty(FamiliaSingerOrder.Text))
                errors.AppendLine("Укажите фамилию исполнителя");
            if (string.IsNullOrEmpty(PatronymicSingerOrder.Text))
                errors.AppendLine("Укажите отчество исполнителя");
            if (string.IsNullOrEmpty(InstrumentOrder.Text))
                errors.AppendLine("Укажите инструмент исполнителя");
            if (string.IsNullOrEmpty(JanrOrder.Text))
                errors.AppendLine("Укажите жанр композиции");
            if (string.IsNullOrEmpty(EffectOrder.Text))
                errors.AppendLine("Укажите эффект композиции");
            if (string.IsNullOrEmpty(ProfitOrder.Text))
                errors.AppendLine("Укажите цену услуги");
            if (string.IsNullOrEmpty(CountCompositionsOrder.Text))
                errors.AppendLine("Укажите количество композиций");
            if (string.IsNullOrEmpty(DataStartOrder.Text))
                errors.AppendLine("Укажите начало записи");
            if (string.IsNullOrEmpty(DataEndOrder.Text))
                errors.AppendLine("Укажите конец записи");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (MusicStudioBaseEntities.GetContext().Orders.Count(y => y.NameOrder == NameOrder.Text) > 0)
            {
                await Task.Delay(500);
                MessageBox.Show("Заказ уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                Orders UserOrder = new Orders()
                {
                    NameOrder = NameOrder.Text,
                    NameSinger = NameSingerOrder.Text,
                    FamiliaSinger = FamiliaSingerOrder.Text,
                    PatronymicSinger = PatronymicSingerOrder.Text,
                    Instrument = InstrumentOrder.Text,
                    Janr = JanrOrder.Text,
                    Effect = EffectOrder.Text,
                    Profit = (decimal?)SqlMoney.Parse(ProfitOrder.Text),
                    CountCompositions = int.Parse(CountCompositionsOrder.Text),
                    DateStart = DateTime.Parse(DataStartOrder.Text),
                    DateEnd = DateTime.Parse(DataEndOrder.Text)
                };
                await Task.Delay(500);
                MusicStudioBaseEntities.GetContext().Orders.Add(UserOrder);
                MusicStudioBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Заказ добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NameOrder.Text = "";
                NameSingerOrder.Text = "";
                FamiliaSingerOrder.Text = "";
                PatronymicSingerOrder.Text = "";
                InstrumentOrder.Text = "";
                JanrOrder.Text = "";
                EffectOrder.Text = "";
                ProfitOrder.Text = "";
                CountCompositionsOrder.Text = "";
                DataStartOrder.Text = "";
                DataEndOrder.Text = "";
            }
            catch
            {
                await Task.Delay(500);
                MessageBox.Show("Ошибка при добавлении данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private async void BackOrder_Click(object sender, RoutedEventArgs e)
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

        private async void ListOrders_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.ListOrders());
        }

        private void InstrumentOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTotalCost();
        }

        private void JanrOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTotalCost();
        }

        private void EffectOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTotalCost();
        }

        private void UpdateTotalCost()
        {
            decimal totalCost = GetPrice(InstrumentOrder.SelectedItem) + GetPrice(JanrOrder.SelectedItem) + GetPrice(EffectOrder.SelectedItem);
            if (decimal.TryParse(CountCompositionsOrder.Text, out decimal count) && count > 0)
            {
                totalCost *= count;
            }
            ProfitOrder.Text = totalCost.ToString("C");
        }
        private decimal GetPrice(object selectedItem)
        {
            if (selectedItem != null)
            {
                Dictionary<string, decimal> prices = new Dictionary<string, decimal>
                {
                    {"Поп", 5000m},
                    {"Рок", 4500m},
                    {"Металл", 3200m},
                    {"Кантри", 1700m},
                    {"Блюз", 900m},
                    {"Электронная", 2600m},
                    {"Классическая", 1000m},
                    {"Фанк", 2100m},
                    {"Хаус", 3400m},
                    {"Соул", 1900m},
                    {"Дилей", 2500m},
                    {"Реверберация", 3000m},
                    {"Фленжер", 1700m},
                    {"Эхо", 2300m},
                    {"Хорус", 4100m},
                    {"Компрессия", 1800m},
                    {"Дисторшн", 800m},
                    {"Эквализация", 2900m},
                    {"Фазовращение", 3500m},
                    {"Вокодер", 1500m},
                    {"Гитара", 1700m},
                    {"Фортепиано", 2000m},
                    {"Скрипка", 3400m},
                    {"Саксофон", 700m},
                    {"Флейта", 1500m},
                    {"Барабаны", 2100m},
                    {"Труба", 3100m},
                    {"Клавишные", 3000m},
                    {"Голос", 4000m},
                    {"Бас-гитара", 2500m},
                };
                string selectedValue = selectedItem.ToString();

                if (prices.ContainsKey(selectedValue))
                {
                    return prices[selectedValue];
                }
            }
            return 0m;
        }

        private async void ListPrices_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.ListPrices());
        }
    }
}
