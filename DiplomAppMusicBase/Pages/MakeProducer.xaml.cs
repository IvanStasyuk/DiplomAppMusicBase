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
    /// Логика взаимодействия для MakeProducer.xaml
    /// </summary>
    public partial class MakeProducer : Page
    {
        public MakeProducer()
        {
            InitializeComponent();
        }

        private async void PickerBirthday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(500);
            YearBirthdayProducer.Text = PickerBirthday.SelectedDate.ToString();
            PickerBirthday.Visibility = Visibility.Collapsed;
        }

        private async void SaveProducer_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(NameProducer.Text))
                errors.AppendLine("Укажите имя продюсера");
            if (string.IsNullOrEmpty(FamiliaProducer.Text))
                errors.AppendLine("Укажите фамилию продюсера");
            if (string.IsNullOrEmpty(PatronymicProducer.Text))
                errors.AppendLine("Укажите отчество исполнителя");
            if (string.IsNullOrEmpty(NicknameProducer.Text))
                errors.AppendLine("Укажите никнейм продюсера");
            if (string.IsNullOrEmpty(YearBirthdayProducer.Text))
                errors.AppendLine("Укажите дату рождения продюсера");
            if (string.IsNullOrEmpty(NameMusicStudio.Text))
                errors.AppendLine("Укажите студию звукозаписи");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (MusicStudioBaseEntities.GetContext().Producers.Count(y => y.Nickname == NicknameProducer.Text) > 0)
            {
                await Task.Delay(500);
                MessageBox.Show("Продюсер уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                Producers newProducer = new Producers()
                {
                    NameProducer = NameProducer.Text,
                    FamiliaProducer = FamiliaProducer.Text,
                    PatronymicProducer = PatronymicProducer.Text,
                    Nickname = NicknameProducer.Text,
                    YearBirthday = DateTime.Parse(YearBirthdayProducer.Text),
                    NameStudio = NameMusicStudio.Text
                };
                await Task.Delay(500);
                MusicStudioBaseEntities.GetContext().Producers.Add(newProducer);
                MusicStudioBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Продюсер добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NameProducer.Text = "";
                FamiliaProducer.Text = "";
                PatronymicProducer.Text = "";
                NicknameProducer.Text = "";
                YearBirthdayProducer.Text = "";
                NameMusicStudio.Text = "";
            }
            catch
            {
                await Task.Delay(500);
                MessageBox.Show("Ошибка при добавлении данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private async void BackProducer_Click(object sender, RoutedEventArgs e)
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

        private async void ListProducers_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            Manager.MFrame.Navigate(new Pages.ListProducers());
        }
    }
}
