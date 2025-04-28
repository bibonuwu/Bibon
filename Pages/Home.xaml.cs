using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace WPFUIKitProfessional.Pages
{
    public partial class Home : Page
    {
        private readonly Brush yellowBrush = new BrushConverter().ConvertFromString("#FFD60A") as Brush;
        private readonly Brush greenBrush = new BrushConverter().ConvertFromString("#32D74B") as Brush;
        private readonly Brush redBrush = new BrushConverter().ConvertFromString("#FF453A") as Brush;

        public Home()
        {
            InitializeComponent();
        }

        private async void ExecuteCommandAsync(Button button, string fileName, string arguments, bool useShellExecute = false)
        {
            button.IsEnabled = false;
            button.Background = yellowBrush;
            button.Content = "Processing...";

            bool success = await Task.Run(() =>
            {
                try
                {
                    var processInfo = new ProcessStartInfo(fileName, arguments)
                    {
                        CreateNoWindow = true,
                        UseShellExecute = useShellExecute,
                        RedirectStandardOutput = !useShellExecute,
                        RedirectStandardError = !useShellExecute
                    };

                    using (var process = Process.Start(processInfo))
                    {
                        process.WaitForExit();
                        return process.ExitCode == 0;
                    }
                }
                catch
                {
                    return false;
                }
            });

            button.Background = success ? greenBrush : redBrush;
            button.Content = success ? "Completed" : "Error";
            button.IsEnabled = true;
        }

     

        private void btnDisableBack3groundApps2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем путь к .bat файлу внутри проекта (например, рядом с .exe)
                string batPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "all_scripts.bat");

                // Проверяем, существует ли файл
                if (!File.Exists(batPath))
                {
                    MessageBox.Show("Файл all_scripts.bat не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Запускаем .bat файл
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = batPath,
                    UseShellExecute = true,
                    Verb = "runas" // Запуск от имени администратора
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при запуске скрипта:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ExecutePowerShell_Click(object sender, RoutedEventArgs e)
        {
            ExecuteCommandAsync(btnExecutePowerShell, "powershell", "-Command \"irm https://get.activated.win | iex\"");
        }

     

        private void btnOpenWebsite_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://gist.github.com/PurpleVibe32/1e9b30754ff18d69ad48155ed29d83de") { UseShellExecute = true });
        }

        private void btnOpenWebsite_Click1(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.cybermania.ws/") { UseShellExecute = true });
        }





        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;

        private void Button_Click_13(object sender, RoutedEventArgs e) => SetWallpaper("13.jpg");
        private void Button_Click_16(object sender, RoutedEventArgs e) => SetWallpaper("18.jpg");
        private void Button_Click_17(object sender, RoutedEventArgs e) => SetWallpaper("17.jpg");
        private void SetWallpaper(string fileName)
        {
            try
            {
                string sourcePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Wallpaper", fileName);
                if (!File.Exists(sourcePath))
                {
                    MessageBox.Show($"Файл не найден: {sourcePath}");
                    return;
                }

                string bmpPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "wallpaper.bmp");
                using (var img = System.Drawing.Image.FromFile(sourcePath))
                {
                    img.Save(bmpPath, System.Drawing.Imaging.ImageFormat.Bmp);
                }

                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
                key.SetValue(@"WallpaperStyle", "2");
                key.SetValue(@"TileWallpaper", "0");

                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, bmpPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка установки обоев");
            }
        }

        private bool IsAdministrator() => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        private void RunAsAdmin()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = Process.GetCurrentProcess().MainModule.FileName,
                UseShellExecute = true,
                Verb = "runas"
            });
            Application.Current.Shutdown();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            btnOpenWeb1site.IsEnabled = false;
            btnOpenWeb1site.Content = "Processing...";
            btnOpenWeb1site.Background = yellowBrush;

            bool success = await Task.Run(() =>
            {
                try
                {
                    if (!IsAdministrator())
                    {
                        RunAsAdmin();
                        return false;
                    }

                    string filePath = @"C:\Windows\System32\drivers\etc\hosts";
                    string url = "https://raw.githubusercontent.com/bibonuwu/Bibon/main/blocked_sites.txt";
                    string content = new HttpClient().GetStringAsync(url).Result;

                    File.WriteAllText(filePath, content + Environment.NewLine);

                    Process.Start(new ProcessStartInfo("ipconfig", "/flushdns")
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false
                    }).WaitForExit();

                    DisableMicrosoftStore();

                    DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Windows\System32\drivers\etc");
                    dirInfo.Attributes |= FileAttributes.Hidden;

                    ProtectFileFromDeletion(filePath);

                    return true;
                }
                catch
                {
                    return false;
                }
            });

            btnOpenWeb1site.Background = success ? greenBrush : redBrush;
            btnOpenWeb1site.Content = success ? "Completed" : "Error";
            btnOpenWeb1site.IsEnabled = true;
        }

        private async void DeleteHostsFile(object sender, RoutedEventArgs e)
        {
            btnOpen1Web1site.IsEnabled = false;
            btnOpen1Web1site.Content = "Processing...";
            btnOpen1Web1site.Background = yellowBrush;

            bool success = await Task.Run(() =>
            {
                try
                {
                    if (!IsAdministrator())
                    {
                        RunAsAdmin();
                        return false;
                    }

                    string hostsPath = @"C:\Windows\System32\drivers\etc\hosts";
                    string folderPath = @"C:\Windows\System32\drivers\etc";

                    if (File.Exists(hostsPath))
                    {
                        ResetFilePermissions(hostsPath);
                        File.Delete(hostsPath);
                    }

                    EnableMicrosoftStore();

                    DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
                    dirInfo.Attributes &= ~FileAttributes.Hidden;

                    foreach (var file in dirInfo.GetFiles())
                        file.Attributes &= ~FileAttributes.Hidden;

                    foreach (var dir in dirInfo.GetDirectories())
                        dir.Attributes &= ~FileAttributes.Hidden;

                    return true;
                }
                catch
                {
                    return false;
                }
            });

            btnOpen1Web1site.Background = success ? greenBrush : redBrush;
            btnOpen1Web1site.Content = success ? "Completed" : "Error";
            btnOpen1Web1site.IsEnabled = true;
        }
        private void ProtectFileFromDeletion(string filePath)
        {
            string[] commands = {
        $"/c takeown /f \"{filePath}\"",
        $"/c icacls \"{filePath}\" /inheritance:r /grant:r *S-1-5-18:(F)",
        $"/c icacls \"{filePath}\" /grant *S-1-1-0:(RX)",
        $"/c icacls \"{filePath}\" /deny *S-1-1-0:(DE,DC,WDAC,WO)"
    };

            foreach (var cmd in commands)
                ExecuteCmdCommand(cmd);
        }

        private void ResetFilePermissions(string filePath)
        {
            string[] commands = {
        $"/c takeown /f \"{filePath}\"",
        $"/c icacls \"{filePath}\" /reset",
        $"/c icacls \"{filePath}\" /grant %USERNAME%:F"
    };

            foreach (var cmd in commands)
                ExecuteCmdCommand(cmd);
        }

        private void ExecuteCmdCommand(string cmd)
        {
            var psi = new ProcessStartInfo("cmd.exe", cmd)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(psi)?.WaitForExit();
        }

        private void DisableMicrosoftStore()
        {
            ExecuteCmdCommand("/c powershell Get-AppxPackage -AllUsers *WindowsStore* | Remove-AppxPackage");
        }

        private void EnableMicrosoftStore()
        {
            ExecuteCmdCommand("/c powershell Get-AppXPackage *WindowsStore* -AllUsers | Foreach {Add-AppxPackage -DisableDevelopmentMode -Register \"$($_.InstallLocation)\\AppXManifest.xml\"}");
        }

    }

}
