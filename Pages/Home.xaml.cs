using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Management;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Principal;

namespace WPFUIKitProfessional.Pages
{

    public static class Wallpaper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public static void SetWallpaper(string path)
        {
            SystemParametersInfo(0x0014, 0, path, 0x0001);
        }
    }


    public partial class Home : Page
    {
        private BackgroundWorker worker;


        public Home()
        {
            InitializeComponent();

            
            // Начинаем асинхронную загрузку информации после загрузки окна
            this.Loaded += MainWindow_Loaded;
            // Инициализация счетчика процессора

        }


      
        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            var yellowBrush = (Brush)new BrushConverter().ConvertFromString("#FFD60A");
            var greenBrush = (Brush)new BrushConverter().ConvertFromString("#32D74B");
            var redBrush = (Brush)new BrushConverter().ConvertFromString("#FF453A");

            worker = new BackgroundWorker();
            worker.DoWork += (s, args) =>
            {
                try
                {
                    ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c powercfg /s SCHEME_MIN")
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                    using (Process process = Process.Start(processInfo))
                    {
                        process.WaitForExit();
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();

                        if (!string.IsNullOrEmpty(error))
                        {
                            args.Result = false;
                        }
                        else
                        {
                            args.Result = true;
                        }
                    }
                }
                catch (Exception)
                {
                    args.Result = false;
                }
            };

            worker.RunWorkerCompleted += (s, args) =>
            {
                if (args.Error != null)
                {
                    // Ошибка выполнения команды
                    btnExecute.Background = redBrush;
                    progressBar.Foreground = redBrush;
                }
                else if ((bool)args.Result)
                {
                    // Команда выполнена успешно
                    btnExecute.Background = greenBrush;
                    progressBar.Value = 100;
                    progressBar.Foreground = greenBrush;
                }
                else
                {
                    // Ошибка при выполнении команды
                    btnExecute.Background = redBrush;
                    progressBar.Foreground = redBrush;
                }
            };

            if (!worker.IsBusy)
            {
                // Запуск BackgroundWorker и изменения состояния UI
                btnExecute.Background = yellowBrush;
                progressBar.Value = 0;
                progressBar.Foreground = greenBrush; // Желтый при старте
                worker.RunWorkerAsync();
            }
        }

        private void btnDisableBackgroundApps_Click(object sender, RoutedEventArgs e)
        {
            var yellowBrush = (Brush)new BrushConverter().ConvertFromString("#FFD60A");
            var greenBrush = (Brush)new BrushConverter().ConvertFromString("#32D74B");
            var redBrush = (Brush)new BrushConverter().ConvertFromString("#FF453A");

            worker = new BackgroundWorker();
            worker.DoWork += (s, args) =>
            {
                try
                {
                    // Команда PowerShell для отключения фоновых приложений
                    ProcessStartInfo processInfo = new ProcessStartInfo("powershell",
                        "-Command \"Set-ItemProperty -Path 'HKCU:\\Software\\Microsoft\\Windows\\CurrentVersion\\BackgroundAccessApplications' -Name 'GlobalUserDisabled' -Value 1\"")
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                    using (Process process = Process.Start(processInfo))
                    {
                        process.WaitForExit();
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();

                        if (!string.IsNullOrEmpty(error))
                        {
                            args.Result = false;
                        }
                        else
                        {
                            args.Result = true;
                        }
                    }
                }
                catch (Exception)
                {
                    args.Result = false;
                }
            };

            worker.RunWorkerCompleted += (s, args) =>
            {
                if (args.Error != null)
                {
                    // Ошибка выполнения команды
                    btnDisableBackgroundApps.Background = redBrush;
                    progressBar1.Foreground = redBrush;
                    progressBar1.Value = 100;
                }
                else if ((bool)args.Result)
                {
                    // Команда выполнена успешно
                    btnDisableBackgroundApps.Background = greenBrush;
                    progressBar1.Value = 100;
                    progressBar1.Foreground = greenBrush;
                }
                else
                {
                    // Ошибка при выполнении команды
                    btnDisableBackgroundApps.Background = redBrush;
                    progressBar1.Foreground = redBrush;
                    progressBar1.Value = 100;
                }
            };

            if (!worker.IsBusy)
            {
                // Запуск BackgroundWorker и изменения состояния UI
                btnDisableBackgroundApps.Background = yellowBrush; // Желтый при старте
                progressBar1.Value = 0;
                progressBar1.Foreground = greenBrush; // Прогресс бар становится желтым при старте
                worker.RunWorkerAsync();
            }
        }

        private void ExecutePowerShell_Click(object sender, RoutedEventArgs e)
        {
            // Определяем цвета для изменения интерфейса
            var yellowBrush = (Brush)new BrushConverter().ConvertFromString("#FFD60A");
            var greenBrush = (Brush)new BrushConverter().ConvertFromString("#32D74B");
            var redBrush = (Brush)new BrushConverter().ConvertFromString("#FF453A");

            worker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };

            worker.DoWork += (s, args) =>
            {
                try
                {
                    // Команда PowerShell для загрузки и выполнения скрипта
                    ProcessStartInfo processInfo = new ProcessStartInfo("powershell",
                        "-Command \"irm https://get.activated.win | iex\"")
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                    using (Process process = Process.Start(processInfo))
                    {
                        // Здесь мы симулируем прогресс, поскольку выполнение PowerShell не предоставляет прямых данных о ходе выполнения
                        for (int i = 0; i <= 100; i += 20)
                        {
                            Thread.Sleep(500); // Имитация задержки для прогресса
                            worker.ReportProgress(i); // Отчет о прогрессе
                        }

                        process.WaitForExit();
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();

                        if (!string.IsNullOrEmpty(error))
                        {
                            args.Result = false; // Если есть ошибки
                        }
                        else
                        {
                            args.Result = true; // Если команда выполнена успешно
                        }
                    }
                }
                catch (Exception)
                {
                    args.Result = false; // Обрабатываем ошибки выполнения
                }
            };

            worker.ProgressChanged += (s, args) =>
            {
                // Обновляем прогресс-бар
                progressBar3.Value = args.ProgressPercentage;
            };

            worker.RunWorkerCompleted += (s, args) =>
            {
                if (args.Error != null)
                {
                    // Ошибка выполнения команды
                    btnExecutePowerShell.Background = redBrush;
                    progressBar3.Foreground = redBrush;
                    progressBar3.Value = 100;
                }
                else if ((bool)args.Result)
                {
                    // Команда выполнена успешно
                    btnExecutePowerShell.Background = greenBrush;
                    progressBar3.Value = 100;
                    progressBar3.Foreground = greenBrush;
                }
                else
                {
                    // Ошибка при выполнении команды
                    btnExecutePowerShell.Background = redBrush;
                    progressBar3.Foreground = redBrush;
                    progressBar3.Value = 100;
                }
            };

            if (!worker.IsBusy)
            {
                // Запуск BackgroundWorker и обновление интерфейса UI
                btnExecutePowerShell.Background = yellowBrush; // Желтый при старте
                progressBar3.Value = 0;
                progressBar3.Foreground = greenBrush; // Прогресс бар становится желтым при старте
                worker.RunWorkerAsync();
            }
        }

        private void btnoffice_Click(object sender, RoutedEventArgs e)
        {
            // Определяем цвета для изменения интерфейса
            var yellowBrush = (Brush)new BrushConverter().ConvertFromString("#FFD60A");
            var greenBrush = (Brush)new BrushConverter().ConvertFromString("#32D74B");
            var redBrush = (Brush)new BrushConverter().ConvertFromString("#FF453A");

            worker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };

            worker.DoWork += (s, args) =>
            {
                try
                {
                    // Команда PowerShell для загрузки и выполнения скрипта
                    ProcessStartInfo processInfo = new ProcessStartInfo("powershell",
                        "-Command \"irm https://get.activated.win | iex\"")
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                    using (Process process = Process.Start(processInfo))
                    {
                        // Здесь мы симулируем прогресс, поскольку выполнение PowerShell не предоставляет прямых данных о ходе выполнения
                        for (int i = 0; i <= 100; i += 20)
                        {
                            Thread.Sleep(500); // Имитация задержки для прогресса
                            worker.ReportProgress(i); // Отчет о прогрессе
                        }

                        process.WaitForExit();
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();

                        if (!string.IsNullOrEmpty(error))
                        {
                            args.Result = false; // Если есть ошибки
                        }
                        else
                        {
                            args.Result = true; // Если команда выполнена успешно
                        }
                    }
                }
                catch (Exception)
                {
                    args.Result = false; // Обрабатываем ошибки выполнения
                }
            };

            worker.ProgressChanged += (s, args) =>
            {
                // Обновляем прогресс-бар
                progressBar5.Value = args.ProgressPercentage;
            };

            worker.RunWorkerCompleted += (s, args) =>
            {
                if (args.Error != null)
                {
                    // Ошибка выполнения команды
                    btnoffice.Background = redBrush;
                    progressBar5.Foreground = redBrush;
                    progressBar5.Value = 100;
                }
                else if ((bool)args.Result)
                {
                    // Команда выполнена успешно
                    btnoffice.Background = greenBrush;
                    progressBar5.Value = 100;
                    progressBar5.Foreground = greenBrush;
                }
                else
                {
                    // Ошибка при выполнении команды
                    btnoffice.Background = redBrush;
                    progressBar5.Foreground = redBrush;
                    progressBar5.Value = 100;
                }
            };

            if (!worker.IsBusy)
            {
                // Запуск BackgroundWorker и обновление интерфейса UI
                btnoffice.Background = yellowBrush; // Желтый при старте
                progressBar5.Value = 0;
                progressBar5.Foreground = greenBrush; // Прогресс бар становится желтым при старте
                worker.RunWorkerAsync();
            }
        }

        private void btnOpenWebsite_Click(object sender, RoutedEventArgs e)
        {
            // Определяем цвета для изменения интерфейса
            var yellowBrush = (Brush)new BrushConverter().ConvertFromString("#FFD60A");
            var greenBrush = (Brush)new BrushConverter().ConvertFromString("#32D74B");
            var redBrush = (Brush)new BrushConverter().ConvertFromString("#FF453A");

            worker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };

            worker.DoWork += (s, args) =>
            {
                try
                {
                    // Здесь мы симулируем прогресс открытия сайта
                    for (int i = 0; i <= 100; i += 20)
                    {
                        Thread.Sleep(500); // Имитация задержки для прогресса
                        worker.ReportProgress(i); // Отчет о прогрессе
                    }

                    // Открываем сайт в браузере
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://gist.github.com/PurpleVibe32/1e9b30754ff18d69ad48155ed29d83de",
                        UseShellExecute = true
                    });

                    args.Result = true; // Успешное завершение задачи
                }
                catch (Exception)
                {
                    args.Result = false; // Если произошла ошибка
                }
            };

            worker.ProgressChanged += (s, args) =>
            {
                // Обновляем прогресс-бар
                progressBar6.Value = args.ProgressPercentage;
            };

            worker.RunWorkerCompleted += (s, args) =>
            {
                if (args.Error != null)
                {
                    // Ошибка выполнения команды
                    btnOpenWebsite.Background = redBrush;
                    progressBar6.Foreground = redBrush;
                    progressBar6.Value = 100;
                }
                else if ((bool)args.Result)
                {
                    // Команда выполнена успешно
                    btnOpenWebsite.Background = greenBrush;
                    progressBar6.Value = 100;
                    progressBar6.Foreground = greenBrush;
                }
                else
                {
                    // Ошибка при выполнении команды
                    btnOpenWebsite.Background = redBrush;
                    progressBar6.Foreground = redBrush;
                    progressBar6.Value = 100;
                }
            };

            if (!worker.IsBusy)
            {
                // Запуск BackgroundWorker и обновление интерфейса UI
                btnOpenWebsite.Background = yellowBrush; // Желтый при старте
                progressBar6.Value = 0;
                progressBar6.Foreground = greenBrush; // Прогресс бар становится желтым при старте
                worker.RunWorkerAsync();
            }
        }









        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Wallpaper", "13.jpg");
            Wallpaper.SetWallpaper(imagePath);
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Wallpaper", "17.jpg");
            Wallpaper.SetWallpaper(imagePath);
        }
        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Wallpaper", "18.jpg");
            Wallpaper.SetWallpaper(imagePath);
        }



        private void btnDisableBackgroundApps2_Click(object sender, RoutedEventArgs e)
        {
            // Создаем кисти для изменения цвета кнопки и прогресс бара
            var yellowBrush = (Brush)new BrushConverter().ConvertFromString("#FFD60A");
            var greenBrush = (Brush)new BrushConverter().ConvertFromString("#32D74B");
            var redBrush = (Brush)new BrushConverter().ConvertFromString("#FF453A");

            worker = new BackgroundWorker();

            // Основная работа в BackgroundWorker
            worker.DoWork += (s, args) =>
            {
                try
                {
                    // Запуск диспетчера задач
                    ProcessStartInfo processInfo = new ProcessStartInfo("taskmgr")
                    {
                        UseShellExecute = true
                    };

                    using (Process process = Process.Start(processInfo))
                    {
                        // Ожидание завершения процесса
                        process.WaitForExit();
                        args.Result = true; // Успешное выполнение
                    }
                }
                catch (Exception)
                {
                    args.Result = false; // Ошибка при выполнении
                }
            };

            // Действия после завершения задачи
            worker.RunWorkerCompleted += (s, args) =>
            {
                if (args.Error != null)
                {
                    // Ошибка выполнения команды
                    btnDisableBackgroundApps2.Background = redBrush;
                    progressBar12.Foreground = redBrush;
                    progressBar12.Value = 100;
                }
                else if ((bool)args.Result)
                {
                    // Процесс успешно выполнен
                    btnDisableBackgroundApps2.Background = greenBrush;
                    progressBar12.Value = 100;
                    progressBar12.Foreground = greenBrush;
                }
                else
                {
                    // Ошибка при выполнении команды
                    btnDisableBackgroundApps2.Background = redBrush;
                    progressBar12.Foreground = redBrush;
                    progressBar12.Value = 100;
                }
            };

            if (!worker.IsBusy)
            {
                // Стартуем BackgroundWorker и меняем состояние UI
                btnDisableBackgroundApps2.Background = yellowBrush; // Желтый цвет при запуске
                progressBar12.Value = 0;
                progressBar12.Foreground = yellowBrush; // Прогресс бар тоже желтый при старте
                worker.RunWorkerAsync();
            }
        }












        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await LoadSystemInformationAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке информации: " + ex.Message);
                Application.Current.Shutdown(); // Закрываем программу при возникновении критической ошибки
            }
        }

        private async Task LoadSystemInformationAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    var systemInfo = new List<string>
            {
                "PC Name: " + Environment.MachineName,
                "User Name: " + Environment.UserName,
                "Windows Version: " + GetWindowsVersion(),
                "Processor Architecture: " + (Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit"),
                "Manufacturer: " + GetHardwareInfo("Win32_ComputerSystem", "Manufacturer"),
                "Model: " + GetHardwareInfo("Win32_ComputerSystem", "Model"),
                "BIOS Version: " + GetHardwareInfo("Win32_BIOS", "Version"),
                "RAM: " + FormatBytes(Convert.ToInt64(GetHardwareInfo("Win32_ComputerSystem", "TotalPhysicalMemory"))),
                "GPU: " + GetHardwareInfo("Win32_VideoController", "Name"),
                "Local IP: " + GetLocalIPAddress(),
                "Public IP: " + GetPublicIPAddress()
            };

                    // Добавляем информацию о дисках
                    DriveInfo[] drives = DriveInfo.GetDrives();
                    foreach (DriveInfo drive in drives)
                    {
                        if (drive.IsReady)
                        {
                            string driveType = drive.DriveType == DriveType.Fixed ? "SSD/HDD" : "Other";
                            systemInfo.Add($"{drive.Name} {driveType}, Free space: {FormatBytes(drive.AvailableFreeSpace)}");
                        }
                    }

                    // Обновляем UI один раз
                    Dispatcher.Invoke(() =>
                    {
                        InfoStackPanel.Children.Clear(); // Очищаем перед добавлением новой информации
                        foreach (var info in systemInfo)
                        {
                            InfoStackPanel.Children.Add(new TextBlock
                            {
                                Text = info,
                                TextWrapping = TextWrapping.Wrap,
                                Foreground = (Brush)Application.Current.Resources["PrimaryTextColor1"] // Устанавливаем цвет текста
                            });
                        }
                    });
                }
                catch (Exception ex)
                {
                    // Логирование ошибки
                    Console.WriteLine("Ошибка при сборе информации: " + ex.Message);
                }
            });
        }

        private string GetWindowsVersion()
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        string version = obj["Version"].ToString();
                        string productType = obj["ProductType"].ToString();
                        int majorVersion = int.Parse(version.Split('.')[0]);
                        int minorVersion = int.Parse(version.Split('.')[1]);

                        if (majorVersion == 10 && minorVersion == 0)
                        {
                            return productType == "1" ? "Windows 10" : "Windows Server";
                        }
                        if (majorVersion == 10 && minorVersion == 22000)
                        {
                            return "Windows 11";
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "Unknown Windows Version";
            }

            return "Unknown Windows Version";
        }

        private string GetHardwareInfo(string query, string property)
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher($"select {property} from {query}"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        return obj[property]?.ToString() ?? "Not available";
                    }
                }
            }
            catch (Exception)
            {
                // Обработка исключения, если произошла ошибка
                return "Not available";
            }

            // Возвращаем значение по умолчанию, если ничего не найдено
            return "Not available";
        }


        private string GetLocalIPAddress()
        {
            try
            {
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                return ip.Address.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "Not available";
            }

            return "Not available";
        }
        private string GetPublicIPAddress()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadString("http://checkip.amazonaws.com").Trim();
                }
            }
            catch (Exception)
            {
                return "Not available";
            }
        }

        private string FormatBytes(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }





        //кнопка блок сайта start


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Создаем кисти для изменения цвета кнопки и прогресс бара
            var yellowBrush = (Brush)new BrushConverter().ConvertFromString("#FFD60A");
            var greenBrush = (Brush)new BrushConverter().ConvertFromString("#32D74B");
            var redBrush = (Brush)new BrushConverter().ConvertFromString("#FF453A");

            try
            {
                if (!IsAdministrator())
                {
                    RunAsAdmin();
                }

                // Путь к файлу hosts
                string filePath = @"C:\Windows\system32\drivers\etc\hosts";

                // Удаление файла, если он существует
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                // Загрузка данных с GitHub
                string url = "https://raw.githubusercontent.com/bibonuwu/Bibon/main/blocked_sites.txt"; // Укажите ваш URL к файлу на GitHub
                string content = await DownloadBlockedSitesAsync(url);

                // Создание нового файла и запись в него
                File.WriteAllText(filePath, content + Environment.NewLine);

                // Очистка DNS-кэша
                FlushDNS();

                btnOpenWeb1site.Background = greenBrush;
            }
            catch (Exception ex)
            {
                btnOpenWeb1site.Background = redBrush;
            }
        }
        private async Task<string> DownloadBlockedSitesAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
                    return string.Empty;
                }
            }
        }
        private void FlushDNS()
        { 
            // Создаем кисти для изменения цвета кнопки и прогресс бара
            var yellowBrush = (Brush)new BrushConverter().ConvertFromString("#FFD60A");
            var greenBrush = (Brush)new BrushConverter().ConvertFromString("#32D74B");
            var redBrush = (Brush)new BrushConverter().ConvertFromString("#FF453A");

            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = "ipconfig",
                    Arguments = "/flushdns",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var process = Process.Start(processInfo);
                process.WaitForExit();

                btnOpenWeb1site.Background = greenBrush;
            }
            catch (Exception ex)
            {
                btnOpenWeb1site.Background = redBrush;
            }
        }

        private bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void RunAsAdmin()
        {
            var processInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = Process.GetCurrentProcess().MainModule.FileName,
                Verb = "runas"
            };

            try
            {
                Process.Start(processInfo);
                Application.Current.Shutdown();
            }
            catch (Exception)
            {
                MessageBox.Show("Запуск от имени администратора был отменен.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteHostsFile(object sender, RoutedEventArgs e)
        {
            // Создаем кисти для изменения цвета кнопки и прогресс бара
            var yellowBrush = (Brush)new BrushConverter().ConvertFromString("#FFD60A");
            var greenBrush = (Brush)new BrushConverter().ConvertFromString("#32D74B");
            var redBrush = (Brush)new BrushConverter().ConvertFromString("#FF453A");

            string hostsPath = @"C:\Windows\system32\drivers\etc\hosts";

            if (!IsAdministrator())
            {
                // Перезапуск приложения с правами администратора
                var processInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = Process.GetCurrentProcess().MainModule.FileName,
                    Verb = "runas"
                };
                try
                {
                    Process.Start(processInfo);
                }
                catch (Exception ex)
                {
                    btnOpen1Web1site.Background = redBrush;
                }
                Application.Current.Shutdown();
                return;
            }

            try
            {
                if (File.Exists(hostsPath))
                {
                    File.Delete(hostsPath);
                    btnOpen1Web1site.Background = greenBrush;
                }
                else
                {
                    btnOpen1Web1site.Background = redBrush;
                }
            }
            catch (Exception ex)
            {
                btnOpen1Web1site.Background = redBrush;
            }
        }

        //кнопка блок сайта end

    }

}
