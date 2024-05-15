using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page, INotifyPropertyChanged
    {
        private List<string> colorsFon = new List<string> { "Красный", "Синий", "Зелёный", "Жёлтый", "Коричневый", "По умолчанию"};

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsPage()
        {
            InitializeComponent();
            var uriMainFon = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMainFon);
            SettingsFon.Background = new ImageBrush(bitmapMain);
            LaunchFon.ItemsSource = colorsFon;
            Volume = 50;
            DataContext = this;
        }

        private void LaunchFon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (LaunchFon.SelectedValue.ToString())
            {
                case "Красный":
                    var uri = new Uri("pack://application:,,,/Resources/redWallpaper.png");
                    var bitmap = new BitmapImage(uri);
                    Manager.GridBackground.Background = new ImageBrush(bitmap);
                    break;
                case "Синий":
                    var uri2 = new Uri("pack://application:,,,/Resources/blueWallpaper.png");
                    var bitmapTwo = new BitmapImage(uri2);
                    Manager.GridBackground.Background = new ImageBrush(bitmapTwo);
                    break;
                case "Зелёный":
                    var uri3 = new Uri("pack://application:,,,/Resources/greenWallpaper.png");
                    var bitmapThree = new BitmapImage(uri3);
                    Manager.GridBackground.Background = new ImageBrush(bitmapThree);
                    break;
                case "Жёлтый":
                    var uri4 = new Uri("pack://application:,,,/Resources/yellowWallpaper.png");
                    var bitmapFour = new BitmapImage(uri4);
                    Manager.GridBackground.Background = new ImageBrush(bitmapFour);
                    break;
                case "Коричневый":
                    var uri5 = new Uri("pack://application:,,,/Resources/grownWallpaper.png");
                    var bitmapFive = new BitmapImage(uri5);
                    Manager.GridBackground.Background = new ImageBrush(bitmapFive);
                    break;
                case "По умолчанию":
                    var uriMain = new Uri("pack://application:,,,/Resources/imagestudio.png");
                    var bitmapMain = new BitmapImage(uriMain);
                    Manager.GridBackground.Background = new ImageBrush(bitmapMain);
                    break;
            }
        }

        private void EnterSettingWindow_Click(object sender, RoutedEventArgs e)
        {
            SettingsPage settingsPage = Manager.MFrame.Content as SettingsPage;
            if (settingsPage != null)
            {
                int newHeight, newWidth;
                if (int.TryParse(SettingWidth.Text, out newWidth) && int.TryParse(SettingHeight.Text, out newHeight))
                {
                    int minHeight = 1000;
                    int minWidth = 1000;
                    int maxHeight = 1080;
                    int maxWidth = 1920;

                    if (newHeight < minHeight || newWidth < minWidth || newHeight > maxHeight || newWidth > maxWidth)
                    {
                        MessageBox.Show($"Размер окна должен быть в пределах от {minWidth}x{minHeight} до {maxWidth}x{maxHeight}", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        Application.Current.MainWindow.Height = newHeight;
                        Application.Current.MainWindow.Width = newWidth;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Некорректные значения", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private async void CanToGo_Click(object sender, RoutedEventArgs e)
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

        private float volume;

        public float Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                OnPropertyChanged("Volume");
            }
        }
        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Volume = (float)e.NewValue;
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
