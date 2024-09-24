using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;

namespace WPFUIKitProfessional.Pages
{
    public partial class Collections : Page
    {
        private bool isWebViewInitialized = false; // Флаг, проверяющий инициализацию WebView2

        public Collections()
        {
            InitializeComponent();
            // Проверка наличия WebView2 Runtime перед инициализацией
            string browserVersion = CoreWebView2Environment.GetAvailableBrowserVersionString();
            if (string.IsNullOrEmpty(browserVersion))
            {
                MessageBox.Show("WebView2 Runtime не установлен. Пожалуйста, установите его для работы программы.");
            }
            else
            {
                InitializeAsync(); // Инициализация WebView2
            }
        }

        // Асинхронная инициализация WebView2
        private async void InitializeAsync()
        {
            if (!isWebViewInitialized)
            {
                try
                {
                    // Создание окружения для WebView2 с указанием временной папки
                    var environment = await CoreWebView2Environment.CreateAsync(
                        null, "C:\\Temp\\WebView2Cache"); // Убедитесь, что приложение имеет права на запись в эту папку

                    await webView.EnsureCoreWebView2Async(environment);

                    // Подключение обработчиков событий для навигации
                    webView.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;
                    webView.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
                    webView.CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;

                    // Переход по URL
                    webView.Source = new Uri("https://sites.google.com/view/abekenaik/app");

                    isWebViewInitialized = true; // WebView2 успешно инициализирован
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка инициализации WebView2: {ex.Message}");
                }
            }
        }


        // Обработчик начала навигации
        private void CoreWebView2_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            // Отображаем ProgressBar при начале навигации
            progressBar.Visibility = Visibility.Visible;
            progressBar.IsIndeterminate = true;
        }

        // Обработчик завершения навигации
        private void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            // Скрываем ProgressBar после завершения навигации
            progressBar.Visibility = Visibility.Hidden;
        }

        // Обработчик ошибок процесса WebView2
        private void CoreWebView2_ProcessFailed(object sender, CoreWebView2ProcessFailedEventArgs e)
        {
            // Логирование ошибок процесса WebView2
            MessageBox.Show($"Ошибка процесса WebView2: {e.ProcessFailedKind}");
        }

        // Запуск инициализации при загрузке страницы
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isWebViewInitialized)
            {
                InitializeAsync(); // Повторная попытка инициализации WebView2 при загрузке страницы
            }
        }
    }
}
