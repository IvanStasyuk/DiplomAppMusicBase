using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
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
using static DiplomAppMusicBase.Manager;

namespace DiplomAppMusicBase.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListPrices.xaml
    /// </summary>
    public partial class ListPrices : Page
    {
        static ObservableCollection<Manager.InstrumentsList> ItemsInstrument { get; set; }
        static ObservableCollection<Manager.JanrsList> ItemsJanr { get; set; }
        static ObservableCollection<Manager.EffectsList> ItemsEffect { get; set; }
        public ListPrices()
        {
            InitializeComponent();
            var uriMainFon = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMainFon);
            PricesListFon.Background = new ImageBrush(bitmapMain);
            ItemsInstrument = new ObservableCollection<InstrumentsList>
            {
                new InstrumentsList { Instrument = "Гитара", PriceInstrument = 1700 },
                new InstrumentsList { Instrument = "Фортепиано", PriceInstrument = 2000 },
                new InstrumentsList { Instrument = "Скрипка", PriceInstrument = 3400 },
                new InstrumentsList { Instrument = "Саксофон", PriceInstrument = 700 },
                new InstrumentsList { Instrument = "Флейта", PriceInstrument = 1500 },
                new InstrumentsList { Instrument = "Барабаны", PriceInstrument = 2100 },
                new InstrumentsList { Instrument = "Труба", PriceInstrument = 3100 },
                new InstrumentsList { Instrument = "Клавишные", PriceInstrument = 3000 },
                new InstrumentsList { Instrument = "Голос", PriceInstrument = 4000 },
                new InstrumentsList { Instrument = "Бас-гитара", PriceInstrument = 2500 }
            };
            ItemsJanr = new ObservableCollection<JanrsList>
            {
                new JanrsList { Janr = "Поп", PriceJanr = 5000 },
                new JanrsList { Janr = "Рок", PriceJanr = 4500 },
                new JanrsList { Janr = "Металл", PriceJanr = 3200 },
                new JanrsList { Janr = "Кантри", PriceJanr = 1700 },
                new JanrsList { Janr = "Блюз", PriceJanr = 900 },
                new JanrsList { Janr = "Электронная", PriceJanr = 2600 },
                new JanrsList { Janr = "Классическая", PriceJanr = 1000 },
                new JanrsList { Janr = "Фанк", PriceJanr = 2100 },
                new JanrsList { Janr = "Хаус", PriceJanr = 3400 },
                new JanrsList { Janr = "Соул", PriceJanr = 1900 },
            };
            ItemsEffect = new ObservableCollection<EffectsList>
            {
                new EffectsList { Effect = "Дилей", PriceEffect = 2500 },
                new EffectsList { Effect = "Реверберация", PriceEffect = 3000 },
                new EffectsList { Effect = "Фленжер", PriceEffect = 1700 },
                new EffectsList { Effect = "Эхо", PriceEffect = 2300 },
                new EffectsList { Effect = "Хорус", PriceEffect = 4100 },
                new EffectsList { Effect = "Компрессия", PriceEffect = 1800 },
                new EffectsList { Effect = "Дисторшн", PriceEffect = 800 },
                new EffectsList { Effect = "Эквализация", PriceEffect = 2900 },
                new EffectsList { Effect = "Фазовращение", PriceEffect = 3500 },
                new EffectsList { Effect = "Вокодер", PriceEffect = 1500 }
            };
            InstrumentsGrid.ItemsSource = ItemsInstrument;
            JanrsGrid.ItemsSource = ItemsJanr;
            EffectsGrid.ItemsSource = ItemsEffect;
        }

        private async void BackOrder_Click(object sender, RoutedEventArgs e)
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
    }
}
