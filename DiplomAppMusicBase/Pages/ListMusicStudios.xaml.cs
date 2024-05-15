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
    /// Логика взаимодействия для ListMusicStudios.xaml
    /// </summary>
    public partial class ListMusicStudios : Page
    {
        public ListMusicStudios()
        {
            InitializeComponent();
            var uriMainFon = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMainFon);
            ListMusicStudioFon.Background = new ImageBrush(bitmapMain);
            //ListOrdersGrid.ItemsSource = MusicStudioBaseEntities.GetContext().Orders.ToList();
        }
    }
}
