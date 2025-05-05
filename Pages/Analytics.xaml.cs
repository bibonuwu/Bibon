using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Bibon;

namespace WPFUIKitProfessional.Pages
{
    public partial class Analytics : Page
    {
        public ObservableCollection<AppCard> RecommendedApps { get; set; }
        public ObservableCollection<AppCard> GamesApps { get; set; }
        public ObservableCollection<AppCard> SocialApps { get; set; }
        public ObservableCollection<AppCard> ToolsApps { get; set; }

        public Analytics()
        {
            InitializeComponent();

            RecommendedApps = new ObservableCollection<AppCard>
            {
                new AppCard { ImageSource="/Assets/Store/9.png",  ButtonContent="Office",         Url="https://gravesoft.dev/office_c2r_links#russian-ru-ru" },
                new AppCard { ImageSource="/Assets/Store/2.png",  ButtonContent="NVIDIA",         Url="https://www.nvidia.com/ru-ru/software/nvidia-app/" },
                new AppCard { ImageSource="/Assets/Store/3.png",  ButtonContent="Visual Studio",  Url="https://visualstudio.microsoft.com/ru/" },
                new AppCard { ImageSource="/Assets/Store/31.png", ButtonContent="GitHub Desktop", Url="https://desktop.github.com/download/" },
                new AppCard { ImageSource="/Assets/Store/18.png", ButtonContent="WinRAR",         Url="https://www.win-rar.com/download.html?&L=4" },
                new AppCard { ImageSource="/Assets/Store/15.png", ButtonContent="Malwarebytes",   Url="https://www.malwarebytes.com/mwb-download/thankyou" }
            };
            GamesApps = new ObservableCollection<AppCard>
            {
                new AppCard { ImageSource="/Assets/Store/5.png",  ButtonContent="Steam",          Url="https://store.steampowered.com/about" },
                new AppCard { ImageSource="/Assets/Store/8.png",  ButtonContent="Epic Games",     Url="https://store.epicgames.com/ru/" },
                new AppCard { ImageSource="/Assets/Store/28.png", ButtonContent="ArtMoney",       Url="https://www.artmoney.ru/r_download.htm" },
                new AppCard { ImageSource="/Assets/Store/69.png", ButtonContent="Flowframes",     Url="https://nmkd.itch.io/flowframes" },
                new AppCard { ImageSource="/Assets/Store/23.png", ButtonContent="Premiere Pro",   Url="https://rutracker.org/forum/tracker.php?nm=premiere" },
                new AppCard { ImageSource="/Assets/Store/26.png", ButtonContent="After Effects",  Url="https://rutracker.org/forum/tracker.php?nm=After%20Effects" }
            };
            SocialApps = new ObservableCollection<AppCard>
            {
                new AppCard { ImageSource="/Assets/Store/19.png",         ButtonContent="WhatsApp",      Url="https://www.whatsapp.com/download" },
                new AppCard { ImageSource="/Assets/Store/20.png",         ButtonContent="Telegram",      Url="https://desktop.telegram.org/" },
                new AppCard { ImageSource="/Assets/Store/bittorrent.png", ButtonContent="Bit Torrent",   Url="https://www.bittorrent.com/downloads/complete/" },
                new AppCard { ImageSource="/Assets/Store/34.png",         ButtonContent="Tor Browser",   Url="https://www.torproject.org/download/" },
                new AppCard { ImageSource="/Assets/Store/38.png",         ButtonContent="Wi-Fi Scanner", Url="https://lizardsystems.com/wi-fi-scanner/" },
                new AppCard { ImageSource="/Assets/Store/12.png",         ButtonContent="OBS",           Url="https://obsproject.com/" }
            };
            ToolsApps = new ObservableCollection<AppCard>
            {
                new AppCard { ImageSource="/Assets/Store/4.png",  ButtonContent="Virtual Machine", Url="https://drive.google.com/file/d/1yEiwapt2XWfvBqHvjljfY_g5NLVe_Zmh/view" },
                new AppCard { ImageSource="/Assets/Store/7.png",  ButtonContent="Kali Linux",      Url="https://www.kali.org/get-kali/#kali-virtual-machines" },
                new AppCard { ImageSource="/Assets/Store/22.png", ButtonContent="Takbir",          Url="https://github.com/bibonuwu/Takbir-Widget/releases" },
                new AppCard { ImageSource="/Assets/Store/41.png", ButtonContent="Visual C++ X64",  Url="https://aka.ms/vs/17/release/vc_redist.x64.exe" },
                new AppCard { ImageSource="/Assets/Store/10.png", ButtonContent="NCALayer",        Url="https://ncl.pki.gov.kz/" },
                new AppCard { ImageSource="/Assets/Store/24.png", ButtonContent="ToDoWin",         Url="https://github.com/bibonuwu/ToDoWin/releases" }
            };

            DataContext = this;
        }

        private void AppButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string url)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });

                btn.Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#32D74B");
            }
        }
    }
}