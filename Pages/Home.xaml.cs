using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Bibon.Pages;

namespace WPFUIKitProfessional.Pages
{
    public partial class Home : Page
    {
        private readonly Brush yellowBrush = new BrushConverter().ConvertFromString("#FFD60A") as Brush;
        private readonly Brush greenBrush = new BrushConverter().ConvertFromString("#32D74B") as Brush;
        private readonly Brush redBrush = new BrushConverter().ConvertFromString("#FF453A") as Brush;
        private static readonly HttpClient httpClient = new HttpClient();

        public Home()
        {
            InitializeComponent();
        }

        // Проверка администратора
        private static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        // Запуск от имени администратора
        private static void EnsureRunAsAdmin()
        {
            if (!IsAdministrator())
            {
                var psi = new ProcessStartInfo
                {
                    FileName = Process.GetCurrentProcess().MainModule.FileName,
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(psi);
                Application.Current.Shutdown();
            }
        }

        // Проверка наличия интернета
        private static async Task<bool> IsInternetAvailableAsync()
        {
            try
            {
                var response = await httpClient.GetAsync("https://www.google.com");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // Скачивание пароля
        private static async Task<string> DownloadPasswordAsync(string url)
        {
            return (await httpClient.GetStringAsync(url)).Trim();
        }

        // Универсальный запуск команд
        private async void ExecuteCommandAsync(Button button, string fileName, string arguments, bool useShellExecute = false)
        {
            SetButtonState(button, false, yellowBrush, "Processing...");

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

            SetButtonState(button, true, success ? greenBrush : redBrush, success ? "Completed" : "Error");
        }

        private void SetButtonState(Button button, bool enabled, Brush background, string content)
        {
            button.IsEnabled = enabled;
            button.Background = background;
            button.Content = content;
        }

        // Запуск .bat файла с правами администратора
        private void btnDisableBack3groundApps2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string batPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "all_scripts.bat");
                if (!File.Exists(batPath))
                {
                    MessageBox.Show("Файл all_scripts.bat не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = batPath,
                    UseShellExecute = true,
                    Verb = "runas"
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при запуске скрипта:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Запуск PowerShell-скрипта
        private void ExecutePowerShell_Click(object sender, RoutedEventArgs e)
        {
            ExecuteCommandAsync(btnExecutePowerShell, "powershell", "-Command \"irm https://get.activated.win | iex\"");
        }

        // Установка обоев
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
                string sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Wallpaper", fileName);
                if (!File.Exists(sourcePath))
                {
                    MessageBox.Show($"Файл не найден: {sourcePath}");
                    return;
                }

                string bmpPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
                using (var img = System.Drawing.Image.FromFile(sourcePath))
                {
                    img.Save(bmpPath, System.Drawing.Imaging.ImageFormat.Bmp);
                }

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true))
                {
                    key.SetValue(@"WallpaperStyle", "2");
                    key.SetValue(@"TileWallpaper", "0");
                }

                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, bmpPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка установки обоев");
            }
        }

        // Кнопка блокировки сайтов
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            SetButtonState(btnOpenWeb1site, false, yellowBrush, "Processing...");

            bool success = await Task.Run(() =>
            {
                try
                {
                    if (!IsAdministrator())
                    {
                        EnsureRunAsAdmin();
                        return false;
                    }

                    string filePath = @"C:\Windows\System32\drivers\etc\hosts";
                    string url = "https://raw.githubusercontent.com/bibonuwu/Bibon/main/blocked_sites.txt";
                    string content = httpClient.GetStringAsync(url).Result;

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

            SetButtonState(btnOpenWeb1site, true, success ? greenBrush : redBrush, success ? "Completed" : "Error");
        }

        // Кнопка удаления hosts
        private async void DeleteHostsFile(object sender, RoutedEventArgs e)
        {
            SetButtonState(btnOpen1Web1site, false, yellowBrush, "Processing...");

            bool success = await Task.Run(() =>
            {
                try
                {
                    if (!IsAdministrator())
                    {
                        EnsureRunAsAdmin();
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

            SetButtonState(btnOpen1Web1site, true, success ? greenBrush : redBrush, success ? "Completed" : "Error");
        }

        // Защита файла от удаления
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

        // Сброс прав файла
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

        // Выполнение команды cmd
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

        // Отключение Microsoft Store
        private void DisableMicrosoftStore()
        {
            ExecuteCmdCommand("/c powershell Get-AppxPackage -AllUsers *WindowsStore* | Remove-AppxPackage");
        }

        // Включение Microsoft Store
        private void EnableMicrosoftStore()
        {
            ExecuteCmdCommand("/c powershell Get-AppXPackage *WindowsStore* -AllUsers | Foreach {Add-AppxPackage -DisableDevelopmentMode -Register \"$($_.InstallLocation)\\AppXManifest.xml\"}");
        }

        // Кнопка для ввода пароля и показа Cookie
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Проверка прав администратора
            if (!IsAdministrator())
            {
                EnsureRunAsAdmin();
                return;
            }

            // Проверка интернета
            if (!await IsInternetAvailableAsync())
            {
                MessageBox.Show("Отсутствует интернет-соединение. Проверьте подключение и попробуйте снова.", "Ошибка подключения", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string passwordUrl = "https://raw.githubusercontent.com/bibonuwu/Bibon/main/passwordcookie.txt";
                string correctPassword = await DownloadPasswordAsync(passwordUrl);

                var passwordWindow = new PasswordCookie();
                passwordWindow.ShowDialog();

                string inputPassword = passwordWindow.EnteredPassword;

                if (string.IsNullOrEmpty(inputPassword) || inputPassword != correctPassword)
                {
                    MessageBox.Show("Құпия сөз еңгізілмеді немесе құпия сөз қате", "Қате", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var cookieWindow = new Bibon.Pages.Cookie();
                cookieWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}