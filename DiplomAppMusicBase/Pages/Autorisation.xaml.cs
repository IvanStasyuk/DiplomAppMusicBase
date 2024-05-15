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
    /// Логика взаимодействия для Autorisation.xaml
    /// </summary>
    public partial class Autorisation : Page
    {
        public Autorisation()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            RandomCaptcha.Text = "";
            for (int i = 0; i < 5; i++)
            {
                int charType = random.Next(0, 2);
                char randomChar;
                if (charType == 0)
                {
                    randomChar = (char)random.Next(65, 91);
                }
                else
                {
                    randomChar = (char)random.Next(48, 58);
                }
                RandomCaptcha.Text += randomChar;
            }
        }

        private async void RegButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.Registration());
        }

        private async void GhostButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.IsGuest = true;
            Manager.MFrame.Navigate(new Pages.Tabs());
        }

        private async void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            var LoginUser = MusicStudioBaseEntities.GetContext().Users.FirstOrDefault(y => y.Login == TBlogin.Text);
            var PasswordUser = MusicStudioBaseEntities.GetContext().Users.FirstOrDefault(z => z.Password == TBPassword.Text);

            try
            {
                StringBuilder errors = new StringBuilder();

                if (string.IsNullOrEmpty(TBlogin.Text))
                    errors.AppendLine("Укажите логин");
                if (string.IsNullOrEmpty(TBPassword.Text))
                    errors.AppendLine("Укажите пароль");
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                var UserVhod = MusicStudioBaseEntities.GetContext().Users.FirstOrDefault(x => x.Login == TBlogin.Text && x.Password == TBPassword.Text);
                if (LoginUser == null)
                {
                    MessageBox.Show("Логин введен с ошибками", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (PasswordUser == null)
                {
                    MessageBox.Show("Пароль введен с ошибками", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (CaptchaInput.Text == "")
                {
                    MessageBox.Show("Введите капчу", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (CaptchaInput.Text != RandomCaptcha.Text)
                {
                    MessageBox.Show("Капча введена неверно", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (CaptchaInput.Text == RandomCaptcha.Text)
                {
                    MessageBox.Show("Капча верна", "Авторизация успешна", MessageBoxButton.OK, MessageBoxImage.Information);
                    switch (UserVhod.idRole)
                    {
                        case 1:
                            MessageBox.Show("Здравствуй, Администратор " + UserVhod.NameUser + "!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                            Manager.MFrame.Navigate(new Pages.Tabs());
                            Manager.IsGuest = false;
                            break;
                        case 2:
                            MessageBox.Show("Здравствуй, Менеджер " + UserVhod.NameUser + "!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                            Manager.MFrame.Navigate(new Pages.Tabs());
                            Manager.IsGuest = false;
                            break;
                        case 3:
                            MessageBox.Show("Здравствуй, Клиент " + UserVhod.NameUser + "!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                            Manager.MFrame.Navigate(new Pages.Tabs());
                            Manager.IsGuest = false;
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены " + UserVhod.NameUser + "!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                            Manager.MFrame.Navigate(new Pages.Tabs());
                            Manager.IsGuest = false;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString() + "Критическая ошибка приложения", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void RepeatCaptcha_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            MainWindow_Loaded(sender, e);
        }
    }
}
