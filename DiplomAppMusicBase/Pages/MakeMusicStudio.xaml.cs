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
    /// Логика взаимодействия для MakeMusicStudio.xaml
    /// </summary>
    public partial class MakeMusicStudio : Page
    {
        private MusicStudios _currentMusicStudio = new MusicStudios();
        public MakeMusicStudio(MusicStudios selectedMusicStudio)
        {
            InitializeComponent();
            var uriMain = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMain);
            AddMusicStudioFon.Background = new ImageBrush(bitmapMain);
            DataContext = MusicStudioBaseEntities.GetContext().MusicStudios.ToList();
            if (selectedMusicStudio != null)
                _currentMusicStudio = selectedMusicStudio;
            DataContext = _currentMusicStudio;
        }

        private async void SaveMusicStudio_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(AddMSFull.Text))
                errors.AppendLine("Укажите полное название студии");
            if (string.IsNullOrEmpty(AddMSLit.Text))
                errors.AppendLine("Укажите краткое название студии");
            if (string.IsNullOrEmpty(NameAdministratorMS.Text))
                errors.AppendLine("Укажите имя администратора");
            if (string.IsNullOrEmpty(NameAccountantMS.Text))
                errors.AppendLine("Укажите имя бухгалтера");
            if (string.IsNullOrEmpty(CityMS.Text))
                errors.AppendLine("Укажите город");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            var ReditingMusicStudio = MusicStudioBaseEntities.GetContext().MusicStudios.FirstOrDefault(y => y.NameStudio == AddMSFull.Text);
            if (ReditingMusicStudio != null)
            {
                ReditingMusicStudio.NameStudio = AddMSFull.Text;
                ReditingMusicStudio.MinName = AddMSLit.Text;
                ReditingMusicStudio.NameAdministrator = NameAdministratorMS.Text;
                ReditingMusicStudio.NameAccountant = NameAccountantMS.Text;
                ReditingMusicStudio.City = CityMS.Text;

                MusicStudioBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Студия звукозаписи обновлена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            { 
                try
                {
                    MusicStudios NewMusicStudio = new MusicStudios()
                    {
                        NameStudio = AddMSFull.Text,
                        MinName = AddMSLit.Text,
                        NameAdministrator = NameAdministratorMS.Text,
                        NameAccountant = NameAccountantMS.Text,
                        City = CityMS.Text
                    };
                    await Task.Delay(500);
                    MusicStudioBaseEntities.GetContext().MusicStudios.Add(NewMusicStudio);
                    MusicStudioBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Студия звукозаписи создана!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AddMSFull.Text = "";
                    AddMSLit.Text = "";
                    NameAdministratorMS.Text = "";
                    NameAccountantMS.Text = "";
                    CityMS.Text = "";
                    Manager.MFrame.Navigate(new Pages.ListMusicStudios());
                }
                catch
                {
                    await Task.Delay(500);
                    MessageBox.Show("Ошибка при добавлении данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private async void BackMusicStudio_Click(object sender, RoutedEventArgs e)
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

        private async void ListMusicStudio_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.ListMusicStudios());
        }
    }
}
