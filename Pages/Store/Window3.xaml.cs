using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bibon.Pages.Store
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Page
    {
        public Window3()
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
            OpenWebsite("https://store.steampowered.com/app/227300/Euro_Truck_Simulator_2/", OpenWebsiteButton12);
        }

        private void OpenWebsite13_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.python.org/", OpenWebsiteButton13);
        }

        private void OpenWebsite24_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://github.com/t1m0thyj/WinDynamicDesktop", OpenWebsiteButton24);
        }

        private void OpenWebsite11_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://obsproject.com/", OpenWebsiteButton11);
        }

        private void OpenWebsite14_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://github.com/zhongyang219/TrafficMonitor/releases/tag/V1.84.1", OpenWebsiteButton14);
        }

        private void OpenWebsite23_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://vk.com/topic-187744169_46180679", OpenWebsiteButton23);
        }

        private void OpenWebsite10_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.utorrent.com/downloads/complete/track/stable/os/win/", OpenWebsiteButton10);
        }

        private void OpenWebsite15_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.softportal.com/software-4539-unlocker.html", OpenWebsiteButton15);
        }

        private void OpenWebsite22_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://vk.com/topic-187744169_46180679", OpenWebsiteButton22);
        }

        private void OpenWebsite9_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://store.epicgames.com/ru/", OpenWebsiteButton9);
        }

        private void OpenWebsite16_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.smoothscroll.net/win/", OpenWebsiteButton16);
        }

        private void OpenWebsite21_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://vk.com/topic-187744169_46180679", OpenWebsiteButton21);
        }

        private void OpenWebsite8_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.artmoney.ru/r_download.htm", OpenWebsiteButton8);
        }

        private void OpenWebsite17_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://nmkd.itch.io/flowframes", OpenWebsiteButton17);
        }

        private void OpenWebsite20_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://vk.com/topic-187744169_40368472", OpenWebsiteButton20);
        }

        private void OpenWebsite19_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.microsoft.com/ru-ru/software-download/windows10", OpenWebsiteButton19);
        }

        private void OpenWebsite18_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://nmkd.itch.io/t2i-gui", OpenWebsiteButton18);
        }

        private void OpenWebsite7_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.ruby-lang.org/en/", OpenWebsiteButton7);
        }
    }
}
