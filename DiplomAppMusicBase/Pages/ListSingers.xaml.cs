using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для ListSingers.xaml
    /// </summary>
    public partial class ListSingers : Page
    {
        public ListSingers()
        {
            InitializeComponent();
            var uriMainFon = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMainFon);
            ListSingersFon.Background = new ImageBrush(bitmapMain);
            ListSingersGrid.ItemsSource = MusicStudioBaseEntities.GetContext().Singers.ToList();
            if (Manager.IsRole == 3)
            {
                RegButtonSinger.IsEnabled = false;
                ListDelete.IsEnabled = false;
            }
            if (Manager.IsRole == 2)
            {
                ListDelete.IsEnabled = false;
            }
        }

        private async void ListBack_Click(object sender, RoutedEventArgs e)
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

        private async void ListDelete_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            var SingerRemoving = ListSingersGrid.SelectedItems.Cast<Singers>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {SingerRemoving.Count()} элементов",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MusicStudioBaseEntities.GetContext().Singers.RemoveRange(SingerRemoving);
                    MusicStudioBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    ListSingersGrid.ItemsSource = MusicStudioBaseEntities.GetContext().Singers.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private async void RegButtonSinger_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.RegistrationSinger());
        }
    }
}
