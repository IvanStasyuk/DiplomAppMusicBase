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
    /// Логика взаимодействия для ListUsers.xaml
    /// </summary>
    public partial class ListUsers : Page
    {
        public ListUsers()
        {
            InitializeComponent();
            var uriMainFon = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMainFon);
            ListUsersFon.Background = new ImageBrush(bitmapMain);
            ListUsersGrid.ItemsSource = MusicStudioBaseEntities.GetContext().Users.ToList();
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
            var UserRemoving = ListUsersGrid.SelectedItems.Cast<Users>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {UserRemoving.Count()} элементов",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MusicStudioBaseEntities.GetContext().Users.RemoveRange(UserRemoving);
                    MusicStudioBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    ListUsersGrid.ItemsSource = MusicStudioBaseEntities.GetContext().Users.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
