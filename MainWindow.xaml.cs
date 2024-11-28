using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using WPFUIKitProfessional.Themes;
using WPFUIKitProfessional.Pages;

namespace WPFUIKitProfessional
{

    public partial class MainWindow : Window
    {



        public MainWindow()
        {
            InitializeComponent();


            this.Loaded += MainWindow_Loaded; // Подписываемся на событие Loaded
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Создаем анимацию для изменения непрозрачности
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,    // Начальное значение
                To = 1,      // Конечное значение
                Duration = TimeSpan.FromSeconds(1) // Длительность анимации
            };

            // Применяем анимацию к свойству непрозрачности окна
            this.BeginAnimation(Window.OpacityProperty, fadeInAnimation);
        }




        private void Themes_Click(object sender, RoutedEventArgs e)
        {
            if (Themes.IsChecked == true)
                ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
            else
                ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdHome_Click(object sender, RoutedEventArgs e)
        {
            AnimateFrameContent(new Home());
        }

        private void rdAnalytics_Click(object sender, RoutedEventArgs e)
        {
            AnimateFrameContent(new Analytics());
        }

        private void rdMessages_Click(object sender, RoutedEventArgs e)
        {
            AnimateFrameContent(new Messages());
        }

        private void rdCollections_Click(object sender, RoutedEventArgs e)
        {
            AnimateFrameContent(new Collections());
        }

    

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            AnimateFrameContent(new Home()); // Здесь анимация будет запущена

            rdHome_Click(sender, e);


        }


        private void AnimateFrameContent(Page page)
        {
            // Анимация для плавного исчезновения текущего содержимого
            DoubleAnimation fadeOutAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2));
            fadeOutAnimation.Completed += (s, a) =>
            {
                // Меняем содержимое после завершения анимации исчезновения
                frameContent.Content = page;

                // Анимация для плавного появления нового содержимого
                DoubleAnimation fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2));
                frameContent.BeginAnimation(OpacityProperty, fadeInAnimation);
            };

            // Запускаем анимацию исчезновения
            frameContent.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private void FrameContent_Navigated(object sender, NavigationEventArgs e)
        {
            // Если во Frame есть содержимое, скрываем кнопку и лейбл
            if (frameContent.Content != null)
            {
                actionButton.Visibility = Visibility.Collapsed;
                welcomeLabel.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Если содержимого нет, показываем их
                actionButton.Visibility = Visibility.Visible;
                welcomeLabel.Visibility = Visibility.Visible;
            }
        }









        //bibon



    }
}
