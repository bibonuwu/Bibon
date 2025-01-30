using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Bibon.Pages
{
    /// <summary>
    /// Логика взаимодействия для PasswordCookie.xaml
    /// </summary>
    public partial class PasswordCookie : Window
    {

        public string EnteredPassword { get; private set; }

        public PasswordCookie()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            EnteredPassword = PasswordBox.Password; // Получаем введенный пароль
            this.DialogResult = true;
            this.Close(); // Закрываем окно
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OkButton_Click(OkButton, null); // Симулируем нажатие кнопки
            }
        }

        private void TextBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OkButton_Click(OkButton, null); // Симулируем нажатие кнопки
            }
        }

        private void TogglePasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
            {
                // Показать пароль
                PasswordBox.Visibility = Visibility.Collapsed;
                TextBoxPassword.Visibility = Visibility.Visible;
                TextBoxPassword.Text = PasswordBox.Password;
                ToggleImage.Source = new BitmapImage(new Uri("/Assets/Home/show.png", UriKind.Relative));
            }
            else
            {
                // Скрыть пароль
                PasswordBox.Visibility = Visibility.Visible;
                TextBoxPassword.Visibility = Visibility.Collapsed;
                PasswordBox.Password = TextBoxPassword.Text;
                ToggleImage.Source = new BitmapImage(new Uri("/Assets/Home/hide.png", UriKind.Relative));
            }
        }
        private void OpenLink(object sender, MouseButtonEventArgs e)
        {
            // Открытие ссылки в браузере
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://t.me/bibonuwu",
                UseShellExecute = true
            });
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрывает текущее окно
        }
    }
}
