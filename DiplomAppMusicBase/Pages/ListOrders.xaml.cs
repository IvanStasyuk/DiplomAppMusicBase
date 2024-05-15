using System;
using System.Collections.Generic;
using System.Globalization;
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
using static DiplomAppMusicBase.Pages.ListOrders;

namespace DiplomAppMusicBase.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListOrders.xaml
    /// </summary>
    public partial class ListOrders : Page
    {
        public ListOrders()
        {
            InitializeComponent();
            var uriMainFon = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMainFon);
            ListOrdersFon.Background = new ImageBrush(bitmapMain);
            ListOrdersGrid.ItemsSource = MusicStudioBaseEntities.GetContext().Orders.ToList();
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
            var OrderRemoving = ListOrdersGrid.SelectedItems.Cast<Orders>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {OrderRemoving.Count()} элементов",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MusicStudioBaseEntities.GetContext().Orders.RemoveRange(OrderRemoving);
                    MusicStudioBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    ListOrdersGrid.ItemsSource = MusicStudioBaseEntities.GetContext().Orders.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
