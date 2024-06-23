using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
            if (Manager.IsRole == 3)
            {
                Loaded += Window_Loaded;
            }
            else
            {
                return;
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int columnIndex = 5;
            Manager.PasswordConverter converter = new Manager.PasswordConverter();
            DataGridTextColumn column = (DataGridTextColumn)ListUsersGrid.Columns[columnIndex];
            column.Binding = new Binding(column.SortMemberPath) { Converter = converter };
            int columnIndex2 = 4;
            Manager.PasswordConverter converter2 = new Manager.PasswordConverter();
            DataGridTextColumn column2 = (DataGridTextColumn)ListUsersGrid.Columns[columnIndex2];
            column2.Binding = new Binding(column2.SortMemberPath) { Converter = converter2 };
        }
    }
}
