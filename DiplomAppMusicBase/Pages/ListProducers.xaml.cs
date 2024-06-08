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
    /// Логика взаимодействия для ListProducers.xaml
    /// </summary>
    public partial class ListProducers : Page
    {
        public ListProducers()
        {
            InitializeComponent();
            var uriMainFon = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMainFon);
            ListProducersFon.Background = new ImageBrush(bitmapMain);
            ListProducersGrid.ItemsSource = MusicStudioBaseEntities.GetContext().Producers.ToList();
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
            var ProducerRemoving = ListProducersGrid.SelectedItems.Cast<Producers>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {ProducerRemoving.Count()} элементов",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MusicStudioBaseEntities.GetContext().Producers.RemoveRange(ProducerRemoving);
                    MusicStudioBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    ListProducersGrid.ItemsSource = MusicStudioBaseEntities.GetContext().Producers.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void RedBut_Click(object sender, RoutedEventArgs e)
        {
            Manager.MFrame.Navigate(new Pages.MakeProducer((sender as Button).DataContext as Producers));
        }
    }
}
