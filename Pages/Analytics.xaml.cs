using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFUIKitProfessional.Pages
{
    public partial class Analytics : Page
    {
        public Analytics()
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

        private void OpenWebsite1_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://developer.android.com/studio?hl=ru", OpenWebsiteButton1);
        }

        private void OpenWebsite2_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.nvidia.com/ru-ru/software/nvidia-app/", OpenWebsiteButton2);
        }

        private void OpenWebsite3_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://visualstudio.microsoft.com/ru/", OpenWebsiteButton3);
        }

        private void OpenWebsite4_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://support.broadcom.com/group/ecx/productdownloads?subfamily=VMware%20Workstation%20Pro", OpenWebsiteButton4);
        }

        private void OpenWebsite5_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://store.steampowered.com/about/", OpenWebsiteButton5);
        }

        private void OpenWebsite6_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://aida64.it/scarica", OpenWebsiteButton6);
        }

        private void OpenWebsite12_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.kali.org/get-kali/#kali-virtual-machines", OpenWebsiteButton12);
        }

        private void OpenWebsite13_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.rarlab.com/", OpenWebsiteButton13);
        }

        private void OpenWebsite24_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.microsoft.com/store/productId/9NKSQGP7F2NH?ocid=pdpshare", OpenWebsiteButton24);
        }

        private void OpenWebsite11_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://store.epicgames.com/ru/", OpenWebsiteButton11);
        }

        private void OpenWebsite14_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://central.github.com/deployments/desktop/desktop/latest/win32", OpenWebsiteButton14);
        }

        private void OpenWebsite23_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.microsoft.com/store/productId/9NZTWSQNTD0S?ocid=pdpshare", OpenWebsiteButton23);
        }

        private void OpenWebsite10_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://drive.google.com/file/d/19fIYUcW8nXwfkqdcvPe3pp77JLAie4IV/view?usp=sharing", OpenWebsiteButton10);
        }

        private void OpenWebsite15_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.utorrent.com/downloads/complete/track/stable/os/win/", OpenWebsiteButton15);
        }

        private void OpenWebsite22_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://sts.kz/eshdi/", OpenWebsiteButton22);
        }

        private void OpenWebsite9_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://ncl.pki.gov.kz/", OpenWebsiteButton9);
        }

        private void OpenWebsite16_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.malwarebytes.com/", OpenWebsiteButton16);
        }

        private void OpenWebsite21_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://github.com/bibonuwu/Takbir-Widget/releases", OpenWebsiteButton21);
        }

        private void OpenWebsite8_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://github.com/hainguyents13/mechvibes/releases", OpenWebsiteButton8);
        }

        private void OpenWebsite17_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://tlauncher.org/installer", OpenWebsiteButton17);
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
            OpenWebsite("https://anydesk.com/ru", OpenWebsiteButton18);
        }

        private void OpenWebsite7_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://obsproject.com/welcome", OpenWebsiteButton7);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
