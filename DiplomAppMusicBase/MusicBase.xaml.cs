using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiplomAppMusicBase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            var uriMain = new Uri("pack://application:,,,/Resources/imagestudio.png");
            var bitmapMain = new BitmapImage(uriMain);
            GridBackground.Background = new ImageBrush(bitmapMain);
            Manager.MFrame = MFrame;
            Manager.MFrame.Navigate(new Pages.Autorisation());
            Manager.GridBackground = FindName("GridBackground") as Grid;
        }

        
    }
}
