   M S B A M L   `   `   `  ����@  <BIBON, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WPFUIKitProfessionalWPFUIKitProfessional  5   	   %    WPFUIKitProfessional.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:WPFUIKitProfessional     5   	   -   5          �Name$
  home��5       � Title$ BIBON��5   	   $	��720��6   $
��1100��6$    � WindowStartupLocation$ CenterScreen=�5
   	    � WindowStyle$
 None=�6.   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5   	    � WindowState$ Normal=�6   $��Poppins)�64    � 	Resources 5       �� 
TargetType	 �� Property
 �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property P� Value N� EnterActions	 W� To �� TargetProperty g� Duration	 �� To N� ExitActions��%�  '��       �� 5   
   " ��6   .�5      �� 	 � ��
 5      �� 5      " ��6&   .R�5      �� $��3q�6!   #��z��65   #��z��6U   #��z��6w   $ 6��6�   ���#FFFFFF._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver .  5    "   �� 5!   &   .��5"   *   �� .��5#   .   W� $	 210X�6>   $ Width�6H   $ 0:0:0.3D�6j   W� 5$   .   $	 230X�6>   $ Height�6H   $ 0:0:0.3D�6k   �� 5%   .   $ CoralX�6=   $( "Background.(SolidColorBrush.Color)�6H   $ 0:0:0.3D�6�   5&   +   5'   '    5*   "   �� 5+   &   .��5,   *   �� .��5-   .   W� $	 200X�6>   $ Width�6H   $ 0:0:0.3D�6j   W� 5.   .   $	 220X�6>   $ Height�6H   $ 0:0:0.3D�6k   �� 5/   .   $ WhiteX�6=   $( "Background.(SolidColorBrush.Color)�6H   $ 0:0:0.3D�6�   50   +   51   '   53      55      57      5:      5;      .��5=      �� $ 10��6      PrimaryBackgroundColor#���   6   ._�5>   
   �  � RowDefinitions 5?      �� 5@      �� 5A      $
��179* �6    .+�5F      �� $ 10 10 10 10��6    �� Padding$ 10q�5G        SecundaryBackgroundColor#���  5H      $��	10,6,10,0q�6M   $��1��6`   $	��Top=�6m   ._�5I      �  � ColumnDefinitions 5J      �� 5K      $��* �6+   �� 5L      $
��Auto �6+   �� 5M      $��* �6+   .+�5P      ��  �� Orientation$ 
Horizontal=�6!   $��20,0,0,0q�6:   .+�5Q      �� $��/Assets/ic_near.png��6    $��40��6=   �� 5R      $��BIBON V��6    $��10,0,0,0q�62   $��20(�6D   $
��Bold"�6R     PrimaryTextColor#���  6d   $��Center=�6�   5T      �� 5V      -   5         ��Name$ gridMenu��5V   %   $ 
Horizontal=�67   $��1��6P   .+�5W      	� -   5         	�Name$ rdHome��5W   *   $
��Home��6:     MenuRadioButtonStyle#���  6I   6y   �� 5Y        TertiaryBackgroundColor#���  6!   $��1��6X   $��1��6b   $��	10,5,10,5q�6r   	� 5\      -   5        $ rdAnalytics��5\   &   $��Store��6;   #���  6K   6z   5^      �� 5c      $ 
Horizontal=�6!   $��2��6:   $��Right=�6J   $��0,0,5,0q�6f   .+�5e      �� $ 18��6!   $	��110��63   $��35��6?   #���  6K   $��0,0,20,0q�6�   ._�5f      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5g   "   F�   sun#���  6'   $��16��6D   $��16��6P    �� Stretch$ UniformToFill=�6[     PrimaryYellowColor#���  6s   $��Center=�6�   d� 5h   "   -   5         d�Name$ Themes��5h   /    d� 	IsChecked$ FalseY�5i      $��Right=�5k        ToggleButtonStyle#���  5l      $��	10 0 10 0q�5m      F� 5n   "     moon#���  6'   $��16��6E   $��16��6Q   $
 Fill=�6\    	 PrimaryWhiteColor#��� 	 6k   $��Center=�6�   5o      5p      �� 5q      -   5         ��Name$ btnMinimize��5q   !    
 IconButtonsStyle#��� 
 66     minimize#���  6a   6�   �� 5r      -   5        $ 
btnRestore��5r   !   $	��5 0q�65   ��5s      �� 5t   "   " ��6(     �� BasedOn#  [
 6E   .�5u   &   �� 	 � ��  maximize#
 �  6@   ! �� Triggers! 5v   &   �� 5w   *   " �� Binding" 66   �� # �� ElementName$
# home��$ �� Path$$ WindowState�% �� Value% 	Maximized6m   .a�5x   .   �� 	 � ��  restore#
 �  6H   5y   +   5{   #   5}      �� 5~      -   5        #��[
 5~   "    	 close#���  6L   
& � Tag$& IsCloseButton��6�   5�      5�      5�      � 5�      $��2��6    5�      �� 5�      $��83 �6$   �� 5�      .+�5�      �� $��1��6   $ 0 0 5 0��6&   
������ $��0,10,0,0q�6V   ._�5�      � $��1��6   .+�5�      �� $��Center=�6%   $��Center=�6B   .+�5�      �� -	   5        ' ��Name$' welcomeLabel��5�       $��36(�66   $��Segoe Script)�6D   
�������$��Welcome to BIBON��6q   $��Center=�6�   $��0,0,0,20q�6�   �� 5�      -
   5        $ actionButton��5�   %   $	��220��6;   $	��200��6H   $
��Bold"�6T   
����p����5�      �� 5�   "   ( �� ImageSource$( /Assets/ic_near.png��6-   ) j� Stretch$) UniformToFill=�6O   5�      �� 5�      ���	#FF898989$��Center=�67   $��0,20,0,0q�6T   .��6f   	BIBON V6o   5�       � 5�      -   5        *  �Name$* frameContent��5�       +  � NavigationUIVisibility$+ Hidden=�66   $��
10,0,10,10q�6V   6j   5�      5�      5�      5�      5�      5�      