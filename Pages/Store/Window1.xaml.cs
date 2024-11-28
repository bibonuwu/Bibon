using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bibon.Pages.Store
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Page
    {
        public Window1()
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
            OpenWebsite("https://icons8.ru/app/windows-pichon", OpenWebsiteButton12);
        }

        private void OpenWebsite13_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.wireshark.org/#downloadLink", OpenWebsiteButton13);
        }

        private void OpenWebsite24_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://anydesk.com/ru", OpenWebsiteButton24);
        }

        private void OpenWebsite11_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://github.com/0x7c13/Notepads/releases", OpenWebsiteButton11);
        }

        private void OpenWebsite14_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.aida64.com/downloads", OpenWebsiteButton14);
        }

        private void OpenWebsite23_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://drive.google.com/file/d/1yEiwapt2XWfvBqHvjljfY_g5NLVe_Zmh/view?usp=sharing", OpenWebsiteButton23);
        }

        private void OpenWebsite10_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://lizardsystems.com/wi-fi-scanner/", OpenWebsiteButton10);
        }

        private void OpenWebsite15_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.nvidia.com/ru-ru/software/nvidia-app/", OpenWebsiteButton15);
        }

        private void OpenWebsite22_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://ncl.pki.gov.kz/", OpenWebsiteButton22);
        }

        private void OpenWebsite9_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://lizardsystems.com/network-scanner/", OpenWebsiteButton9);
        }

        private void OpenWebsite16_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.malwarebytes.com/", OpenWebsiteButton16);
        }

        private void OpenWebsite21_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://mechvibes.com/", OpenWebsiteButton21);
        }

        private void OpenWebsite8_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.torproject.org/download/", OpenWebsiteButton8);
        }

        private void OpenWebsite17_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://anvi-folder-locker.ru.uptodown.com/windows", OpenWebsiteButton17);
        }

        private void OpenWebsite20_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://kamilszymborski.github.io/", OpenWebsiteButton20);
        }

        private void OpenWebsite19_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist?view=msvc-170", OpenWebsiteButton19);
        }

        private void OpenWebsite18_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.teamviewer.com/ru-cis/download/windows/", OpenWebsiteButton18);
        }

        private void OpenWebsite7_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://nmap.org/download", OpenWebsiteButton7);
        }

    }
}
