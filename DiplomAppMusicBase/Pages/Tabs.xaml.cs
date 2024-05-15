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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiplomAppMusicBase.Pages
{
    /// <summary>
    /// Логика взаимодействия для Tabs.xaml
    /// </summary>
    public partial class Tabs : Page
    {
        public Tabs()
        {
            InitializeComponent();
            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = new Duration(TimeSpan.FromSeconds(3));
            PeopleBox.BeginAnimation(OpacityProperty, fadeInAnimation);
            ListenInstruments.BeginAnimation(OpacityProperty, fadeInAnimation);
            AddOrder.BeginAnimation(OpacityProperty, fadeInAnimation);
            AddDogovor.BeginAnimation(OpacityProperty, fadeInAnimation);
            SettingsBtn.BeginAnimation(OpacityProperty, fadeInAnimation);
            GoToTirage.BeginAnimation(OpacityProperty, fadeInAnimation);
            GoToAlbom.BeginAnimation(OpacityProperty, fadeInAnimation);

            if (Manager.IsGuest == true)
            {
                PeopleBox.IsEnabled = false;
                AddOrder.IsEnabled = false;
                GoToTirage.IsEnabled = false;
                GoToAlbom.IsEnabled = false;
                ListSingers.IsEnabled = false;
            }
            else if (Manager.IsGuest == false)
            {
                PeopleBox.IsEnabled = true;
                AddOrder.IsEnabled = true;
                GoToTirage.IsEnabled = true;
                GoToAlbom.IsEnabled = true;
                ListSingers.IsEnabled = true;
            }
        }

        private async void PeopleBox_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите зайти в свою учетную запись?", "Подтвердите", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                goto captcha;
            }
            else
            {
                return;
            }
            captcha:;
            CaptchaWindow window = new CaptchaWindow();
            window.Show();
        }

        private async void ListenInstruments_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.PlaySounds());
        }

        private async void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите сделать заказ?", "Подтвердите", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Manager.MFrame.Navigate(new Pages.MakeOrder());
            }
            else
            {
                return;
            }
        }

        private async void AddDogovor_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите подписать договор?", "Подтвердите", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Manager.MFrame.Navigate(new Pages.MakeContract());
            }
            else
            {
                return;
            }
        }

        private async void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.SettingsPage());
        }

        private async void GoToTirage_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите выпустить тираж?", "Подтвердите", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Manager.MFrame.Navigate(new Pages.MakeTirage());
            }
            else
            {
                return;
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

        private async void GoToAlbom_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите добавить альбом?", "Подтвердите", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Manager.MFrame.Navigate(new Pages.MakeAlbom());
            }
            else
            {
                return;
            }
        }

        private async void OProgramm_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            ProgrammInfo window = new ProgrammInfo();
            window.Show();
        }

        private async void ListSingers_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.ListSingers());
        }
    }
}
