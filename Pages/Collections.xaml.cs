using Bibon.Pages.WinX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Telegram.Bot.Types.Payments;

namespace WPFUIKitProfessional.Pages
{
    public partial class Collections : Page
    {

        public Collections()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            AnimateFrameContent(new Window1());


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }




        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
          
        }

        private void FrameContent_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            // Если во Frame есть содержимое, скрываем кнопку и лейбл
            if (frameContent1.Content != null)
            {
                q1.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Если содержимого нет, показываем их
                q1.Visibility = Visibility.Visible;
            }
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

     

        private void Window1(object sender, RoutedEventArgs e)
        {
            AnimateFrameContent(new Window1());

        }

        private void Window2(object sender, RoutedEventArgs e)
        {
            AnimateFrameContent(new Window2());

        }

        private void Window3(object sender, RoutedEventArgs e)
        {
            AnimateFrameContent(new Window3());

        }
    }
}
