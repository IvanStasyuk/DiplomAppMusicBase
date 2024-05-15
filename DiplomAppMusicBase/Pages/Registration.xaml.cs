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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiplomAppMusicBase.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
            var uriMainFon = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMainFon);
            RegFon.Background = new ImageBrush(bitmapMain);
            DataContext = MusicStudioBaseEntities.GetContext().Users.ToList();
        }

        private async void CanToGo_Click(object sender, RoutedEventArgs e)
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

        private async void RegButtonPage_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(TBNamePage.Text))
                errors.AppendLine("Укажите имя пользователя");
            if (string.IsNullOrEmpty(TBFamiliaPage.Text))
                errors.AppendLine("Укажите фамилию пользователя");
            if (string.IsNullOrEmpty(TBPatronymicPage.Text))
                errors.AppendLine("Укажите отчество пользователя");
            if (string.IsNullOrEmpty(TBLoginPage.Text))
                errors.AppendLine("Укажите логин пользователя");
            if (string.IsNullOrEmpty(TBPasswordPage.Text))
                errors.AppendLine("Укажите пароль пользователя");
            if (string.IsNullOrEmpty(TBRolePage.Text))
                errors.AppendLine("Укажите роль для пользователя");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (MusicStudioBaseEntities.GetContext().Users.Count(y => y.Login == TBLoginPage.Text) > 0)
            {
                await Task.Delay(500);
                MessageBox.Show("Пользователь уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                Users newReg = new Users()
                {
                    NameUser = TBNamePage.Text,
                    FamiliaUser = TBFamiliaPage.Text,
                    PatronymicUser = TBPatronymicPage.Text,
                    Login = TBLoginPage.Text,
                    Password = TBPasswordPage.Text,
                    idRole = int.Parse(TBRolePage.Text)
                };
                await Task.Delay(500);
                MusicStudioBaseEntities.GetContext().Users.Add(newReg);
                MusicStudioBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Пользователь добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                TBNamePage.Text = "";
                TBFamiliaPage.Text = "";
                TBPatronymicPage.Text = "";
                TBLoginPage.Text = "";
                TBPasswordPage.Text = "";
                TBRolePage.Text = "";
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private async void ListPeople_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.ListUsers());
        }
    }
}
