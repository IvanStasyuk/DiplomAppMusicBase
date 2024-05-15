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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiplomAppMusicBase.Pages
{
    /// <summary>
    /// Логика взаимодействия для InformationContract.xaml
    /// </summary>
    public partial class InformationContract : Page
    {
        public InformationContract(List<string> textList)
        {
            InitializeComponent();
            NameInfoContract.Text = textList[0];
            NameSingerInfo.Text = textList[1];
            FamiliaSingerInfo.Text = textList[2];
            PatronymicSingerInfo.Text = textList[3];
            MusicStudioInfo.Text = textList[4];
            NameProducerInfo.Text = textList[5];
            DayBirthdayInfo.Text = textList[6];
            CountCompositionsInfo.Text = textList[7];
            ExperienceInfo.Text = textList[8];
            DateStartInfo.Text = textList[9];
            DateEndInfo.Text = textList[10];
            ProfitInfo.Text = textList[11];
        }

        private async void BackContract_Click(object sender, RoutedEventArgs e)
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

        private void PrintSave_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FixedDocument document = new FixedDocument();
                document.DocumentPaginator.PageSize = new Size(1920, 1080);

                Visual visual = (Visual)this.Content;
                printDialog.PrintVisual(visual, "Печать страницы");
            }
        }
    }
}
