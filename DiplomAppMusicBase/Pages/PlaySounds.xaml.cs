using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для PlaySounds.xaml
    /// </summary>
    public partial class PlaySounds : Page
    {
        private MediaPlayer mediaPlayer;
        private MediaPlayer mediaPlayer2;
        private MediaPlayer mediaPlayer3;
        private MediaPlayer mediaPlayer4;
        private MediaPlayer mediaPlayer5;
        private MediaPlayer mediaPlayer6;
        private MediaPlayer mediaPlayer7;
        private MediaPlayer mediaPlayer8;
        private MediaPlayer mediaPlayer9;
        private MediaPlayer mediaPlayer10;
        public object FileContent { get; private set; }
        public PlaySounds()
        {
            InitializeComponent();
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri("music_guitar.wav", UriKind.Relative));
            mediaPlayer2 = new MediaPlayer();
            mediaPlayer2.Open(new Uri("music_fortepiano.wav", UriKind.Relative));
            mediaPlayer3 = new MediaPlayer();
            mediaPlayer3.Open(new Uri("music_violin.wav", UriKind.Relative));
            mediaPlayer4 = new MediaPlayer();
            mediaPlayer4.Open(new Uri("music_saxophone.wav", UriKind.Relative));
            mediaPlayer5 = new MediaPlayer();
            mediaPlayer5.Open(new Uri("music_flute.wav", UriKind.Relative));
            mediaPlayer6 = new MediaPlayer();
            mediaPlayer6.Open(new Uri("music_drums.wav", UriKind.Relative));
            mediaPlayer7 = new MediaPlayer();
            mediaPlayer7.Open(new Uri("music_tube.wav", UriKind.Relative));
            mediaPlayer8 = new MediaPlayer();
            mediaPlayer8.Open(new Uri("music_keyboards.wav", UriKind.Relative));
            mediaPlayer9 = new MediaPlayer();
            mediaPlayer9.Open(new Uri("music_voice.wav", UriKind.Relative));
            mediaPlayer10 = new MediaPlayer();
            mediaPlayer10.Open(new Uri("music_bassguitar.wav", UriKind.Relative));
        }

        private void SoundGuitar_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
        }

        private void SoundFortepiano_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer2.Play();
        }

        private void SoundViolin_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer3.Play();
        }

        private void SoundSaxophone_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer4.Play();
        }

        private void SoundFlute_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer5.Play();
        }

        private void SoundDrums_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer6.Play();
        }

        private void SoundTube_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer7.Play();
        }

        private void SoundKeyboards_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer8.Play();
        }

        private void SoundVoice_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer9.Play();
        }

        private void SoundBassGuitar_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer10.Play();
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

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                string content = System.IO.File.ReadAllText(filename);
                FileContent = content;
            }
        }

        private async void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt) |*.txt|All files (*.*) |*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filename = saveFileDialog.FileName;
            }
        }

        private async void FileInternet_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите перейти на сайт приложения?", "Подтвердите", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Process.Start("https://audiomaster.su/");
            }
            else
            {
                return;
            }
        }
    }
}
