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
using System.Windows.Shapes;

namespace DiplomAppMusicBase
{
    /// <summary>
    /// Логика взаимодействия для CaptchaWindow.xaml
    /// </summary>
    public partial class CaptchaWindow : Window
    {
        public CaptchaWindow()
        {
            InitializeComponent();
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

        private void captchaInEnter_Click(object sender, RoutedEventArgs e)
        {
            if (captchaIn.Text != RandomCaptcha.Text)
            {
                MessageBox.Show("Неправильная капча!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (captchaIn.Text == RandomCaptcha.Text)
            {
                MessageBox.Show("Капча верна!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
                Manager.MFrame.Navigate(new Pages.Account());
            }
        }

        private async void RepeatCaptcha_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            CaptchaWindow window = new CaptchaWindow();
            window.Show();
            this.Close();
        }
    }
}
