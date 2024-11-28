using Bibon.Pages.Store;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace WPFUIKitProfessional.Pages
{
    public partial class Analytics : Page
    {
        public Analytics()
        {
            InitializeComponent();
            // Вызов метода Click для кнопки
            RadioButton_Click(MyButton, new RoutedEventArgs());
        }





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

        private void AnimateFrameContent(Page page)
        {
            // Анимация для плавного исчезновения текущего содержимого
            DoubleAnimation fadeOutAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2));
            fadeOutAnimation.Completed += (s, a) =>
            {
                // Меняем содержимое после завершения анимации исчезновения
                frameContent1.Content = page;

                // Анимация для плавного появления нового содержимого
                DoubleAnimation fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2));
                frameContent1.BeginAnimation(OpacityProperty, fadeInAnimation);
            };

            // Запускаем анимацию исчезновения
            frameContent1.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }
         




        private void OpenWebsite1_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://developer.android.com/studio?hl=ru", OpenWebsiteButton1);
        }

        private void OpenWebsite2_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://www.nvidia.com/ru-ru/software/nvidia-app/", OpenWebsiteButton2);
        }

        private void OpenWebsite3_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://visualstudio.microsoft.com/ru/", OpenWebsiteButton3);
        }

        private void OpenWebsite4_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://drive.google.com/file/d/1yEiwapt2XWfvBqHvjljfY_g5NLVe_Zmh/view?usp=sharing", OpenWebsiteButton4);
        }

        private void OpenWebsite5_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://store.steampowered.com/about/", OpenWebsiteButton5);
        }

        private void OpenWebsite6_Click(object sender, RoutedEventArgs e)
        {
            OpenWebsite("https://aida64.it/scarica", OpenWebsiteButton6);
        }

     
     

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {

            AnimateFrameContent(new Window4());


        }



        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {


            AnimateFrameContent(new Window1());

        }

        private void RadioButton_Click_2(object sender, RoutedEventArgs e)
        {


            AnimateFrameContent(new Window2());

        }

        private void RadioButton_Click_3(object sender, RoutedEventArgs e)
        {


            AnimateFrameContent(new Window3());

        }

        private void FrameContent_Navigated(object sender, NavigationEventArgs e)
        {
            // Если во Frame есть содержимое, скрываем кнопку и лейбл
            if (frameContent1.Content != null)
            {
               
            }
            else
            {
                // Если содержимого нет, показываем их
               
            }
        }


    }
}
