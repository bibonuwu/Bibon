using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFUIKitProfessional.Pages
{
    public partial class Collections : Page
    {
        private int passwordLength = 8;

        public Collections()
        {
            InitializeComponent();
            InitializeInfoText();
            ShowWifiProfiles_Click(this, new RoutedEventArgs());
        }

        private void InitializeInfoText()
        {
            InfoTextBox.Text =
                "Сайтқа өту арқылы bibon кейіпкері жасаған Windows 10 және 11 арналған сборкасын жүктеп алсаңыз болады.\n\n" +
                "Бұл сброканың артықшылығы:\n" +
                "- Windows жүйесіне жоғары деңгейдегі оптимизация.\n" +
                "- Таза және мінсіз жүйе орнатылған.\n" +
                "- Барлық қажетсіз системалық бағдарламалар мен телеметрия өшірілген.\n" +
                "- Жылдам жұмыс істеу қабілеті мен ресурстарды үнемдеу мүмкіндігі.\n" +
                "- Қолдануға ыңғайлы әрі түсінікті интерфейс.";

            InfoTextBox1.Text =
                "Генерация жүзеге асуы\n" +
                "- Қазіргі уақыт милисекунды + жыл + рандом + батарея проценті";
        }

        private async void ShowWifiProfiles_Click(object sender, RoutedEventArgs e)
        {
            StatusText.Text = "Wifi тізімі шығарудамын...";

            var psi = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = "-Command \"(netsh wlan show profiles) | Select-String '\\:(.+)$' | %{$name=$_.Matches.Groups[1].Value.Trim(); $_} | %{(netsh wlan show profile name=\\\"$name\\\" key=clear)} | Select-String 'Содержимое ключа\\W+\\:(.+)$' | %{$pass=$_.Matches.Groups[1].Value.Trim(); $_} | %{[PSCustomObject]@{ ProfileName=$name; Password=$pass }} | ConvertTo-Json -Compress\"",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            try
            {
                using (var process = new Process { StartInfo = psi })
                {
                    process.Start();

                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    process.WaitForExit();

                    if (!string.IsNullOrWhiteSpace(error))
                    {
                        StatusText.Text = "Ошибка PowerShell: " + error;
                        MessageBox.Show("Ошибка PowerShell:\n" + error);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(output))
                    {
                        StatusText.Text = "Нет данных от PowerShell";
                        MessageBox.Show("PowerShell не вернул данные. Возможно, нет Wi-Fi профилей или нет прав.");
                        return;
                    }

                    List<WifiProfile> wifiProfiles = null;
                    try
                    {
                        wifiProfiles = JsonConvert.DeserializeObject<List<WifiProfile>>(output);
                    }
                    catch (JsonSerializationException)
                    {
                        try
                        {
                            var singleProfile = JsonConvert.DeserializeObject<WifiProfile>(output);
                            if (singleProfile != null)
                                wifiProfiles = new List<WifiProfile> { singleProfile };
                        }
                        catch (Exception ex2)
                        {
                            StatusText.Text = "Ошибка десериализации: " + ex2.Message;
                            MessageBox.Show("Ошибка десериализации:\n" + ex2.Message + "\n\nRaw output:\n" + output);
                            return;
                        }
                    }

                    if (wifiProfiles == null || wifiProfiles.Count == 0)
                    {
                        StatusText.Text = "Wi-Fi профили не найдены";
                        MessageBox.Show("Wi-Fi профили не найдены.\n\nRaw output:\n" + output);
                        wifiDataGrid.ItemsSource = null;
                    }
                    else
                    {
                        wifiDataGrid.ItemsSource = wifiProfiles;
                        StatusText.Text = "Wifi тізімі";
                    }
                }
            }
            catch (Exception ex)
            {
                StatusText.Text = "Ошибка: " + ex.Message;
                MessageBox.Show("Ошибка:\n" + ex.Message);
            }
        }

        public class WifiProfile
        {
            public string ProfileName { get; set; }
            public string Password { get; set; }
        }

        private void OpenWebsite(string url, Button button)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
            button.Background = (Brush)new BrushConverter().ConvertFromString("#32D74B");
        }

        private void OpenWebsiteButton1_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://sites.google.com/view/sbros-pc-by-bibon", OpenWebsiteButton1);
        }

        private void DecreaseLength(object sender, RoutedEventArgs e)
        {
            if (passwordLength > 4)
            {
                passwordLength--;
                PasswordLength.Text = passwordLength.ToString();
            }
        }

        private void IncreaseLength(object sender, RoutedEventArgs e)
        {
            if (passwordLength < 32)
            {
                passwordLength++;
                PasswordLength.Text = passwordLength.ToString();
            }
        }

        private void GeneratePassword(object sender, RoutedEventArgs e)
        {
            bool includeUppercase = IncludeUppercase.IsChecked == true;
            bool includeNumbers = IncludeNumbers.IsChecked == true;
            bool includeLowercase = IncludeLowercase.IsChecked == true;
            bool includeSymbols = IncludeSymbols.IsChecked == true;

            if (!includeUppercase && !includeNumbers && !includeLowercase && !includeSymbols)
            {
                MessageBox.Show("Please select at least one option!");
                return;
            }

            PasswordBox.Text = GenerateRandomPassword(passwordLength, includeUppercase, includeNumbers, includeLowercase, includeSymbols);
        }

        private static string GenerateRandomPassword(int length, bool includeUppercase, bool includeNumbers, bool includeLowercase, bool includeSymbols)
        {
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string symbols = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            var characterPool = new StringBuilder();
            if (includeUppercase) characterPool.Append(uppercase);
            if (includeLowercase) characterPool.Append(lowercase);
            if (includeNumbers) characterPool.Append(numbers);
            if (includeSymbols) characterPool.Append(symbols);

            if (characterPool.Length == 0) return string.Empty;

            DateTime now = DateTime.Now;
            int seed = now.Millisecond + now.Second + now.Minute + now.Hour + now.Year + GetMemoryUsagePercentage();
            var random = new Random(seed);

            var password = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(characterPool.Length);
                password.Append(characterPool[index]);
            }
            return password.ToString();
        }

        private static int GetMemoryUsagePercentage()
        {
            try
            {
                var info = new Microsoft.VisualBasic.Devices.ComputerInfo();
                double usedMemory = info.TotalPhysicalMemory - info.AvailablePhysicalMemory;
                return (int)((usedMemory / info.TotalPhysicalMemory) * 100);
            }
            catch
            {
                return 50;
            }
        }
    }
}