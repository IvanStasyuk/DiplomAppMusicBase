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
    /// Логика взаимодействия для CodeWindow.xaml
    /// </summary>
    public partial class CodeWindow : Window
    {
        public CodeWindow()
        {
            InitializeComponent();
        }

        private async void codeInEnter_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            if (codeIn.Text == "")
            {
                MessageBox.Show("Поле пустое!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (codeIn.Text != "ABCD")
            {
                MessageBox.Show("Код не верен!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (codeIn.Text == "ABCD")
            {
                MessageBox.Show("Код верен!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
                Manager.IsRole = 1;
                Manager.MFrame.Navigate(new Pages.Registration());
                return;
            }
        }

        private async void codeOutEnter_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            this.Close();
            Manager.IsRole = 3;
            Manager.MFrame.Navigate(new Pages.Registration());
        }
    }
}
