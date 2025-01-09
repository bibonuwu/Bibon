using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace WPFUIKitProfessional.Pages
{
    /// <summary>
    /// Lógica de interacción para Messages.xaml
    /// </summary>
    public partial class Messages : Page
    {

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, uint dwFlags);

        private long totalCleanedBytes = 0;
        public Messages()
        {
            InitializeComponent();
            CheckAdministratorRights();
        }

        private void CheckAdministratorRights()
        {
            if (!IsAdministrator1())
            {
                RunAsAdmin1();
                return; // Перезапустим приложение с правами администратора
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
        private async void CleanButton_Click(object sender, RoutedEventArgs e)
        {
            totalCleanedBytes = 0;
            progressBar.Value = 0;

            int totalTasks = 7;
            int completedTasks = 0;

            try
            {
                if (chkTempFiles.IsChecked == true)
                {
                    CleanTempFiles();
                    UpdateProgress(++completedTasks, totalTasks);
                }

                if (chkWinSxS.IsChecked == true)
                {
                    CleanWinSxS();
                    UpdateProgress(++completedTasks, totalTasks);
                }

                if (chkWindowsLog.IsChecked == true)
                {
                    CleanWindowsLog();
                    UpdateProgress(++completedTasks, totalTasks);
                }

                if (chkDriverCache.IsChecked == true)
                {
                    CleanDriverCache();
                    UpdateProgress(++completedTasks, totalTasks);
                }

                if (chkRecycleBin.IsChecked == true)
                {
                    CleanRecycleBin();
                    UpdateProgress(++completedTasks, totalTasks);
                }

                if (chkReports.IsChecked == true)
                {
                    CleanReports();
                    UpdateProgress(++completedTasks, totalTasks);
                }

                if (chkEvents.IsChecked == true)
                {
                    CleanEvents();
                    UpdateProgress(++completedTasks, totalTasks);
                }

                double cleanedGB = totalCleanedBytes / (1024.0 * 1024 * 1024);
                MessageBox.Show($"Очистка завершена!\nУдалено: {cleanedGB:F2} ГБ", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Снимаем все галочки после очистки
                UncheckAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void UpdateProgress(int completedTasks, int totalTasks)
        {
            progressBar.Value = (completedTasks / (double)totalTasks) * 100;
        }

        private void CleanTempFiles()
        {
            string tempPath = System.IO.Path.GetTempPath();
            totalCleanedBytes += DeleteFilesAndDirectories(tempPath);
        }

        private void CleanWinSxS()
        {
            string winSxSPath = @"C:\Windows\WinSxS\Temp";
            totalCleanedBytes += DeleteFilesAndDirectories(winSxSPath);
        }

        private void CleanWindowsLog()
        {
            string logsPath = @"C:\Windows\Logs";
            totalCleanedBytes += DeleteFilesAndDirectories(logsPath);
        }

        private void CleanDriverCache()
        {
            string driverCachePath = @"C:\Windows\System32\DriverStore\FileRepository";
            totalCleanedBytes += DeleteFilesAndDirectories(driverCachePath);
        }

        private void CleanRecycleBin()
        {
            SHEmptyRecycleBin(IntPtr.Zero, null, 0);
        }

        private void CleanReports()
        {
            string reportsPath = @"C:\ProgramData\Microsoft\Windows\WER\ReportQueue";
            totalCleanedBytes += DeleteFilesAndDirectories(reportsPath);
        }

        private void CleanEvents()
        {
            string eventsPath = @"C:\Windows\System32\winevt\Logs";
            totalCleanedBytes += DeleteFilesAndDirectories(eventsPath);
        }

        private long DeleteFilesAndDirectories(string path)
        {
            long cleanedBytes = 0;

            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    try
                    {
                        var fileInfo = new FileInfo(file);
                        cleanedBytes += fileInfo.Length;
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Не удалось удалить файл {file}: {ex.Message}");
                    }
                }

                foreach (var dir in Directory.GetDirectories(path))
                {
                    try
                    {
                        var dirInfo = new DirectoryInfo(dir);
                        cleanedBytes += GetDirectorySize(dirInfo);
                        Directory.Delete(dir, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Не удалось удалить папку {dir}: {ex.Message}");
                    }
                }
            }

            return cleanedBytes;
        }

        private long GetDirectorySize(DirectoryInfo directoryInfo)
        {
            long size = 0;

            try
            {
                foreach (var file in directoryInfo.GetFiles("*", SearchOption.AllDirectories))
                {
                    size += file.Length;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось получить размер папки {directoryInfo.FullName}: {ex.Message}");
            }

            return size;
        }

        private void UncheckAll()
        {
            chkTempFiles.IsChecked = false;
            chkWinSxS.IsChecked = false;
            chkWindowsLog.IsChecked = false;
            chkDriverCache.IsChecked = false;
            chkRecycleBin.IsChecked = false;
            chkReports.IsChecked = false;
            chkEvents.IsChecked = false;
        }

        private void chkTempFiles_Checked(object sender, RoutedEventArgs e)
        {
            // Автоматически выделяем зависимые галочки
            chkWinSxS.IsChecked = true;
            chkWindowsLog.IsChecked = true;
            chkDriverCache.IsChecked = true;
        }

        private void chkTempFiles_Unchecked(object sender, RoutedEventArgs e)
        {
            // Автоматически снимаем зависимые галочки
            chkWinSxS.IsChecked = false;
            chkWindowsLog.IsChecked = false;
            chkDriverCache.IsChecked = false;
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
            string url = "https://sites.google.com/view/antiha";

            // Открыть сайт в браузере по умолчанию
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true // Это важно для открытия в браузере по умолчанию
            });
        }





    }
}
