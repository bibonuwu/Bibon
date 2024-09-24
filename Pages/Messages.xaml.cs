using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace WPFUIKitProfessional.Pages
{
    /// <summary>
    /// Lógica de interacción para Messages.xaml
    /// </summary>
    public partial class Messages : Page
    {
        public Messages()
        {
            InitializeComponent();
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Укажите URL вашего сайта
            string url = "https://t.me/bibonuwu";

            // Открыть сайт в браузере по умолчанию
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true // Это важно для открытия в браузере по умолчанию
            });

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Укажите URL вашего сайта
            string url = "https://t.me/bibonuwu";

            // Открыть сайт в браузере по умолчанию
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true // Это важно для открытия в браузере по умолчанию
            });
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Укажите URL вашего сайта
            string url = "https://t.me/bibonuwu";

            // Открыть сайт в браузере по умолчанию
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true // Это важно для открытия в браузере по умолчанию
            });
        }
    }
}
