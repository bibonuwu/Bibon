   M S B A M L   `   `   `  ����@  <BIBON, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WPFUIKitProfessionalWPFUIKitProfessional  5   	   %    WPFUIKitProfessional.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:WPFUIKitProfessional     5   	   -   5          �Name$
  home��5       � Title$ BIBON��5   	   $	��720��6   $
��1100��6$    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowStyle$
 None=�6.   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5   	    � WindowState$ Normal=�6   $��Poppins)�64    � 	Resources 5       �� 
TargetType	 �� Property
 �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property P� Value N� EnterActions	 W� To �� TargetProperty g� Duration	 �� To N� ExitActions��%�  '��       �� 5   
   " ��6   .�5      �� 	 � ��
 5      �� 5      " ��6&   .R�5      �� $��3q�6!   #��z��65   #��z��6U   #��z��6w   $ 6��6�   ���#FFFFFF._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver .  5   "   �� 5    &   .��5!   *   �� .��5"   .   W� $	 210X�6>   $ Width�6H   $ 0:0:0.3D�6j   W� 5#   .   $	 230X�6>   $ Height�6H   $ 0:0:0.3D�6k   �� 5$   .   $ CoralX�6=   $( "Background.(SolidColorBrush.Color)�6H   $ 0:0:0.3D�6�   5%   +   5&   '    5)   "   �� 5*   &   .��5+   *   �� .��5,   .   W� $	 200X�6>   $ Width�6H   $ 0:0:0.3D�6j   W� 5-   .   $	 220X�6>   $ Height�6H   $ 0:0:0.3D�6k   �� 5.   .   $ WhiteX�6=   $( "Background.(SolidColorBrush.Color)�6H   $ 0:0:0.3D�6�   5/   +   50   '   52      54      56      59      5:      .��5;      �� $ 10��6      PrimaryBackgroundColor#���   6   ._�5<   
   �  � RowDefinitions 5=      �� 5>      �� 5?      $
��179* �6    .+�5D      �� $ 10 10 10 10��6    �� Padding$ 10q�5E        SecundaryBackgroundColor#���  5F      $��10,6,10,638q�6M   $��1��6b   ._�5G      � -   5         �Name$ gridMenu��5G      $��Center=�6)   $��Center=�6F   .+�5L      �� $��1��6!    �� Orientation$ 
Horizontal=�6.   .+�5M      	� -   5         	�Name$ rdHome��5M   &   $
��Home��66     MenuRadioButtonStyle#���  6E   $��0,0,10,0q�6�   $��Center=�6�   $��Center=�6�   	� 5N      -   5        $ rdAnalytics��5N   &   $��Store��6;   #���  6K   $��0,0,10,0q�6�   $��Center=�6�   $��Center=�6�   �� 5O      $��3��6!   $ 18��6.   $	��110��6@   $��35��6L     TertiaryBackgroundColor#���  6X   $��0,0,10,0q�6�   ._�5P      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5Q   "   F�   sun#���  6'   $��16��6D   $��16��6P    �� Stretch$ UniformToFill=�6[     PrimaryYellowColor#���  6s   $��Center=�6�   d� 5R   "   -   5         d�Name$ Themes��5R   /    d� 	IsChecked$ FalseY�5S   #   $��Right=�5U   #     ToggleButtonStyle#���  5V   #   $��	10 0 10 0q�5W   #   F� 5X   "     moon#���  6'   $��16��6E   $��16��6Q   $
 Fill=�6\     PrimaryWhiteColor#���  6k   $��Center=�6�   5Y      5a      	� 5c      -   5        $ 
rdMessages��5c   &   $��Course��6:   #���  6K    	 messages
 � Tag# � 	 6z   $��Center=�6�   $��Center=�6�   $��0,0,10,0q�6�   	� 5d      -   5        $ rdCollections��5d   &   $��About��6=   #���  6M    
 collections# � 
 6|   $��Center=�6�   $��Center=�6�   5h      5m      5n      � 5q      $��2��6    5r      �� 5s      $��83 �6$   �� 5t      .+�5|      �� $��1��6   $ 0 0 5 0��6&   
������ $��0,10,0,0q�6V   ._�5}      � $��1��6   .+�5�      �� -   5         ��Name$ welcomeLabel��5�       $��Center=�66   $	��Top=�6S   $��36(�6k   $��	0,100,0,0q�5�      $��Segoe Script)�6   
�������$��Welcome!��6B   �� 5�      -	   5          ��Name$  actionButton��5�   !   $��3q�67   $	��220��6B   $	��200��6O   $
��Bold"�6[   
����p����5�      �� 5�   "   ! �� ImageSource$! /Assets/ic_near.png��6-   " j� Stretch$" UniformToFill=�6O   5�       � 5�      -
   5        #  �Name$# frameContent��5�       $  � NavigationUIVisibility$$ Hidden=�66   $��
10,0,10,10q�6V   6j   5�      5�      5�      5�      5�      5�      