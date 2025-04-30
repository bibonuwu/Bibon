using System;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Bibon.Pages;

namespace WPFUIKitProfessional.Pages
{
    /// <summary>
    /// Логика взаимодействия для Messages.xaml
    /// </summary>
    public partial class Messages : Page
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public Messages()
        {
            InitializeComponent();
        }

    


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Открыть сайт в браузере по умолчанию
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://t.me/bibonuwu",
                UseShellExecute = true
            });
        }

   
    }
}