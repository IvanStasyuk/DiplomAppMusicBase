﻿using System;
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
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        public Account()
        {
            InitializeComponent();
            var uriMain = new Uri("pack://application:,,,/Resources/greyfonpeople.png");
            var bitmapMain = new BitmapImage(uriMain);
            PeopleFon.Background = new ImageBrush(bitmapMain);
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

        private async void ExitToGo_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.Autorisation());
        }

        private async void AddNewProducer_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.MakeProducer());
        }

        private async void AddNewMusicStudio_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.MakeMusicStudio());
        }
    }
}
