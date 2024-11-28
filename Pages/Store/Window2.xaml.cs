using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bibon.Pages.Store
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Page
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void OpenWebsite(string url, Button button)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true // Необходим для открытия URL в браузере
            });

            // Изменение цвета кнопки на цвет с кодом #32D74B
            button.Background = (Brush)new BrushConverter().ConvertFromString("#32D74B");
        }


        private void OpenWebsite12_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.kali.org/get-kali/#kali-virtual-machines", OpenWebsiteButton12);
        }

        private void OpenWebsite13_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://tlauncher.org/", OpenWebsiteButton13);
        }

        private void OpenWebsite24_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.whatsapp.com/download", OpenWebsiteButton24);
        }

        private void OpenWebsite11_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://github.com/GitSquared/edex-ui", OpenWebsiteButton11);
        }

        private void OpenWebsite14_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://store.steampowered.com/app/444200/World_of_Tanks_Blitz/", OpenWebsiteButton14);
        }

        private void OpenWebsite23_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://desktop.telegram.org/", OpenWebsiteButton23);
        }

        private void OpenWebsite10_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://apps.microsoft.com/detail/9ncbcszsjrsb?hl=en-US&gl=US", OpenWebsiteButton10);
        }

        private void OpenWebsite15_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://store.steampowered.com/app/1815780/Asphalt_Legends_Unite/", OpenWebsiteButton15);
        }

        private void OpenWebsite22_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://sts.kz/eshdi/", OpenWebsiteButton22);
        }

        private void OpenWebsite9_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://discord.com/download", OpenWebsiteButton9);
        }

        private void OpenWebsite16_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://tankionline.com/en/", OpenWebsiteButton16);
        }

        private void OpenWebsite21_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://github.com/bibonuwu/Takbir-Widget/releases", OpenWebsiteButton21);
        }

        private void OpenWebsite8_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://zoom.us/ru/download", OpenWebsiteButton8);
        }

        private void OpenWebsite17_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://store.steampowered.com/app/397950/Clustertruck/", OpenWebsiteButton17);
        }

        private void OpenWebsite20_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://vk.com/topic-187744169_46000270", OpenWebsiteButton20);
        }

        private void OpenWebsite19_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://github.com/bibonuwu/ToDoWin/releases", OpenWebsiteButton19);
        }

        private void OpenWebsite18_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://store.steampowered.com/app/289070/Sid_Meiers_Civilization_VI/", OpenWebsiteButton18);
        }

        private void OpenWebsite7_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.roblox.com/download", OpenWebsiteButton7);
        }
    }
}
