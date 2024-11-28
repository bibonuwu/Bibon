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
            ShowWifiProfiles_Click(this, new RoutedEventArgs());

            // Текст для отображения в TextBox
            string infoText =
                "Сайтқа өту арқылы bibon кейіпкері жасаған Windows 10 және 11 арналған сборкасын жүктеп алсаңыз болады.\n\n" +
                "Бұл сброканың артықшылығы:\n" +
                "- Windows жүйесіне жоғары деңгейдегі оптимизация.\n" +
                "- Таза және мінсіз жүйе орнатылған.\n" +
                "- Барлық қажетсіз системалық бағдарламалар мен телеметрия өшірілген.\n" +
                "- Жылдам жұмыс істеу қабілеті мен ресурстарды үнемдеу мүмкіндігі.\n" +
                "- Қолдануға ыңғайлы әрі түсінікті интерфейс.";
                

            // Установка текста в TextBox
            InfoTextBox.Text = infoText;

            // Текст для отображения в TextBox
            string infoText1 =
                "Генерация жүзеге асуы\n" +
                "- Қазіргі уақыт милисекунды + жыл + рандом + батарея проценті";

            // Установка текста в TextBox
            InfoTextBox1.Text = infoText1;
        }
        //----------------------------------------------------------------------


        private async void ShowWifiProfiles_Click(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = "-Command \"(netsh wlan show profiles) | Select-String '\\:(.+)$' | %{$name=$_.Matches.Groups[1].Value.Trim(); $_} | %{(netsh wlan show profile name=\\\"$name\\\" key=clear)} | Select-String 'Содержимое ключа\\W+\\:(.+)$' | %{$pass=$_.Matches.Groups[1].Value.Trim(); $_} | %{[PSCustomObject]@{ ProfileName=$name; Password=$pass }} | ConvertTo-Json -Compress\"",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            var process = new Process { StartInfo = psi };
            try
            {
                var tcs = new TaskCompletionSource<bool>();

                process.Exited += (s, ea) => tcs.SetResult(true);
                process.EnableRaisingEvents = true;
                process.Start();

                var outputTask = process.StandardOutput.ReadToEndAsync();
                var errorTask = process.StandardError.ReadToEndAsync();

                await tcs.Task; // Ждем завершения процесса

                string output = await outputTask;
                string error = await errorTask;

                if (!string.IsNullOrEmpty(error))
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(output))
                {
                    return;
                }

                List<WifiProfile> wifiProfiles = null;
                try
                {
                    wifiProfiles = JsonConvert.DeserializeObject<List<WifiProfile>>(output);
                }
                catch (JsonSerializationException)
                {
                    var singleWifiProfile = JsonConvert.DeserializeObject<WifiProfile>(output);
                    if (singleWifiProfile != null)
                    {
                        wifiProfiles = new List<WifiProfile> { singleWifiProfile };
                    }
                }

                if (wifiProfiles == null || wifiProfiles.Count == 0)
                {
                }
                else
                {
                    wifiDataGrid.ItemsSource = wifiProfiles;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public class WifiProfile
        {
            public string ProfileName { get; set; }
            public string Password { get; set; }
        }
        //----------------------------------------------------------------------

        private void OpenWebsite(string url, Button button)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true // Необходим для открытия URL в браузере
            });

            // Изменение цвета кнопки на цвет с кодом #32D74B
            button.Background = (Brush)new BrushConverter().ConvertFromString("#32D74B");
        }

        private void OpenWebsiteButton1_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://sites.google.com/view/sbros-pc-by-bibon", OpenWebsiteButton1);

        }
        //----------------------------------------------------------------------


        // Уменьшить длину пароля
        // Уменьшить длину пароля
        private void DecreaseLength(object sender, RoutedEventArgs e)
        {
            if (passwordLength > 4)
            {
                passwordLength--;
                PasswordLength.Text = passwordLength.ToString();
            }
        }
        // Метод для получения процента заряда батареи
        private int GetMemoryUsagePercentage()
        {
            try
            {
                var totalMemory = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
                var availableMemory = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory;
                double usedMemory = totalMemory - availableMemory;
                return (int)((usedMemory / totalMemory) * 100);
            }
            catch
            {
                // Если информация недоступна, возвращаем 50% по умолчанию
                return 50;
            }
        }
        // Увеличить длину пароля
        private void IncreaseLength(object sender, RoutedEventArgs e)
        {
            if (passwordLength < 32)
            {
                passwordLength++;
                PasswordLength.Text = passwordLength.ToString();
            }
        }

        // Генерация пароля
        private void GeneratePassword(object sender, RoutedEventArgs e)
        {
            bool includeUppercase = IncludeUppercase.IsChecked ?? false;
            bool includeNumbers = IncludeNumbers.IsChecked ?? false;
            bool includeLowercase = IncludeLowercase.IsChecked ?? false;
            bool includeSymbols = IncludeSymbols.IsChecked ?? false;

            if (!includeUppercase && !includeNumbers && !includeLowercase && !includeSymbols)
            {
                MessageBox.Show("Please select at least one option!");
                return;
            }

            PasswordBox.Text = GenerateRandomPassword(passwordLength, includeUppercase, includeNumbers, includeLowercase, includeSymbols);
        }

        private string GenerateRandomPassword(int length, bool includeUppercase, bool includeNumbers, bool includeLowercase, bool includeSymbols)
        {
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string symbols = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            StringBuilder characterPool = new StringBuilder();
            if (includeUppercase) characterPool.Append(uppercase);
            if (includeLowercase) characterPool.Append(lowercase);
            if (includeNumbers) characterPool.Append(numbers);
            if (includeSymbols) characterPool.Append(symbols);

            if (characterPool.Length == 0) return string.Empty;

            // Используем текущее время, год и процент загруженной памяти
            DateTime now = DateTime.Now;
            int year = now.Year;
            int memoryUsage = GetMemoryUsagePercentage();
            int seed = now.Millisecond + now.Second + now.Minute + now.Hour + year + memoryUsage;
            Random random = new Random(seed);

            StringBuilder password = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(characterPool.Length);
                password.Append(characterPool[index]);
            }

            return password.ToString();
        }


        //----------------------------------------------------------------------
       
        
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------
        //----------------------------------------------------------------------




    }
}





