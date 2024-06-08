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
    /// Логика взаимодействия для MakeAlbom.xaml
    /// </summary>
    public partial class MakeAlbom : Page
    {
        private Alboms _currentAlbom = new Alboms();
        public MakeAlbom(Alboms selectedAlbom)
        {
            InitializeComponent();
            var uriMain = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMain);
            AlbomFon.Background = new ImageBrush(bitmapMain);
            DataContext = MusicStudioBaseEntities.GetContext().Alboms.ToList();
            if (selectedAlbom != null)
                _currentAlbom = selectedAlbom;
            DataContext = _currentAlbom;
        }

        private async void DateReleasePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(500);
            YearRelease.Text = DateReleasePicker.SelectedDate.ToString();
            DateReleasePicker.Visibility = Visibility.Collapsed;
        }

        private async void SaveAlbom_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(NameAlbomAdd.Text))
                errors.AppendLine("Укажите название альбома");
            if (string.IsNullOrEmpty(NameSingerAlbom.Text))
                errors.AppendLine("Укажите имя исполнителя");
            if (string.IsNullOrEmpty(FamiliaSingerAlbom.Text))
                errors.AppendLine("Укажите фамилию исполнителя");
            if (string.IsNullOrEmpty(PatronymicSingerAlbom.Text))
                errors.AppendLine("Укажите отчество исполнителя");
            if (string.IsNullOrEmpty(CountCompositionsAlbom.Text))
                errors.AppendLine("Укажите количество композиций");
            if (string.IsNullOrEmpty(Janr.Text))
                errors.AppendLine("Укажите жанр");
            if (string.IsNullOrEmpty(YearRelease.Text))
                errors.AppendLine("Укажите год выхода альбома");
            if (string.IsNullOrEmpty(IDSingerAdd.Text))
                errors.AppendLine("Укажите ID исполнителя");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            var ReditingAlbom = MusicStudioBaseEntities.GetContext().Alboms.FirstOrDefault(y => y.NameAlbom == NameAlbomAdd.Text);
            if (ReditingAlbom != null)
            {
                ReditingAlbom.NameAlbom = NameAlbomAdd.Text;
                ReditingAlbom.NameSinger = NameSingerAlbom.Text;
                ReditingAlbom.FamiliaSinger = FamiliaSingerAlbom.Text;
                ReditingAlbom.PatronymicSinger = PatronymicSingerAlbom.Text;
                ReditingAlbom.CountCompositions = int.Parse(CountCompositionsAlbom.Text);
                ReditingAlbom.Janr = Janr.Text;
                ReditingAlbom.YearRelease = DateTime.Parse(YearRelease.Text);
                ReditingAlbom.idSinger = int.Parse(IDSingerAdd.Text);

                MusicStudioBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Альбом обновлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.MFrame.Navigate(new Pages.ListAlboms());
            }
            else
            { 
                try
                {
                    Alboms NewAlbom = new Alboms()
                    {
                        NameAlbom = NameAlbomAdd.Text,
                        NameSinger = NameSingerAlbom.Text,
                        FamiliaSinger = FamiliaSingerAlbom.Text,
                        PatronymicSinger = PatronymicSingerAlbom.Text,
                        CountCompositions = int.Parse(CountCompositionsAlbom.Text),
                        Janr = Janr.Text,
                        YearRelease = DateTime.Parse(YearRelease.Text),
                        idSinger = int.Parse(IDSingerAdd.Text),
                    };
                    await Task.Delay(500);
                    MusicStudioBaseEntities.GetContext().Alboms.Add(NewAlbom);
                    MusicStudioBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Альбом добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NameAlbomAdd.Text = "";
                    NameSingerAlbom.Text = "";
                    FamiliaSingerAlbom.Text = "";
                    PatronymicSingerAlbom.Text = "";
                    CountCompositionsAlbom.Text = "";
                    Janr.Text = "";
                    YearRelease.Text = "";
                    IDSingerAdd.Text = "";
                    Manager.MFrame.Navigate(new Pages.ListAlboms());
                }
                catch
                {
                    await Task.Delay(500);
                    MessageBox.Show("Ошибка при добавлении данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private async void BackAlbom_Click(object sender, RoutedEventArgs e)
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

        private async void ListAlbom_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.ListAlboms());
        }
    }
}
