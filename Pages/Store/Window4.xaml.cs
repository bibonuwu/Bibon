using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bibon.Pages.Store
{
    /// <summary>
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : Page
    {
        public Window4()
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
            OpenWebsite("https://vk.com/topic-187744169_40406807", OpenWebsiteButton12);
        }

        private void OpenWebsite13_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.sublimetext.com/", OpenWebsiteButton13);
        }

        private void OpenWebsite24_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.image-line.com/", OpenWebsiteButton24);
        }

        private void OpenWebsite11_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://vk.com/topic-187744169_49271176", OpenWebsiteButton11);
        }

        private void OpenWebsite14_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://workspace.google.com/intl/en-GB_ALL/products/drive/#download", OpenWebsiteButton14);
        }

        private void OpenWebsite23_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.unrealengine.com/en-US/download", OpenWebsiteButton23);
        }

        private void OpenWebsite10_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://t.me/filescp/5739", OpenWebsiteButton10);
        }

        private void OpenWebsite15_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.google.ru/intl/en_in/chrome/", OpenWebsiteButton15);
        }

        private void OpenWebsite22_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://drive.google.com/file/d/19fIYUcW8nXwfkqdcvPe3pp77JLAie4IV/view?usp=sharing", OpenWebsiteButton22);
        }

        private void OpenWebsite9_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://t.me/filescp/13116", OpenWebsiteButton9);
        }

        private void OpenWebsite16_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://desktop.github.com/download/", OpenWebsiteButton16);
        }

        private void OpenWebsite21_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://github.com/AutoDarkMode/Windows-Auto-Night-Mode/releases", OpenWebsiteButton21);
        }

        private void OpenWebsite8_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://code.visualstudio.com/Download", OpenWebsiteButton8);
        }

        private void OpenWebsite17_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://unity.com/ru/download", OpenWebsiteButton17);
        }

        private void OpenWebsite20_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://store.steampowered.com/app/1815630/Fences_4/", OpenWebsiteButton20);
        }

        private void OpenWebsite19_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://t.me/officialsklad/36", OpenWebsiteButton19);
        }

        private void OpenWebsite18_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.blender.org/download/", OpenWebsiteButton18);
        }

        private void OpenWebsite7_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.jetbrains.com/pycharm/download/?section=windows", OpenWebsiteButton7);
        }

    }
}
