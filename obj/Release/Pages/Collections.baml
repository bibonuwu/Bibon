   M S B A M L   `   `   `  ���� @  <BIBON, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:WPFUIKitProfessional.PagesWPFUIKitProfessional.Pages  ,    &WPFUIKitProfessional.Pages.Collections   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:WPFUIKitProfessional.Pages       N� 
Background
  ����  N� 
FontFamily$ Poppins)�.����  �� CornerRadius$ 10��   SecundaryBackgroundColor#���   ._�� $��Center=�$��Center=�.+���  �� Orientation$ 
Horizontal=�.+���   SecundaryBackgroundColor1#���    CardBorderStyle1#���   �� Padding$ 	0,40,0,40q�$	��550��._�� .+��� $��Center=�$	��Top=�.+��� $��/Assets/Home/wifi.png��$��Uniform=�$��50��$��Center=�$	��Top=��� $��WiFi Networks��$��15(�$
��Bold"�  PrimaryTextColor#���  $��Center=�$��Center=�$��	0,20,0,20q�&   System.Windows.Controls.DataGrid  -     Name$ wifiDataGrid��$
��Auto=�   AutoGenerateColumns .  
����������	#FF496FFF$��1q�$
��Bold"�   HeadersVisibility$ Column=�   GridLinesVisibility$
 None=�	   CanUserAddRows	 .  
   
IsReadOnly
 .    ColumnHeaderHeight$ 40Q�   AlternatingRowBackground �#F9F9F9   RowBackground
 �����$��	20,0,20,0q� � 	Resources =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value)  #System.Windows.Controls.DataGridRow*  $System.Windows.Controls.DataGridCell��%T  '        '      '  �    �� "  .���  � �� �	#FF496FFF��  � ��
 �������  � ��$ Segoe UI)���  � ��$
 Bold"���  � ��$ Center=���  � ��$ Center=���  � ��$ 40����  � ��$ 	10,0,10,0q���  � �� �	#FF496FFF��  � ��$ 0,0,0,1q��� "  .���  � ��$ Normal"���  � ��$ 14(���  � ��
 �������  � ��$ 0,0,0,5q���  � ��$ Center=��� "  .���  � ��$ 	15,5,15,5q���  � ��$ 0,0,1,1q���  � �� �#E0E0E0��  � ��$ Center=���  � ��$ Center=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	WiFi Name��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ProfileName+  $ Password��$ *  �� *
Password+�� -    ��Name$ 
StatusText��$��15(�#���  $��Center=�$��Center=�$��0,20,0,0q��� #���  #���  $��20,0,0,0q�$ 	0,40,0,40q�$	��550��._��� $	��Top=�.+��� $��/Assets/78.png��$��Uniform=�$��50��$��Center=�$	��Top=��� $��Windows 10 by bibon��$��15(�$
��Bold"�#���  $��Center=�$��Center=�$��	0,20,0,20q��� -    ��Name$ OpenWebsiteButton1��  PrimaryButtonStyle1#���  $��Download��$��Center=�$��Bottom=�$��0,0,0,20q��� -   $��InfoTextBox��#���  $��Stretch=�$��Stretch=�$
��Wrap=�$��14(�$��	20,0,20,0q���Z� �� #���  #���  $��20,0,0,0q�$ 	0,40,0,10q�$	��550��._��� .+��� $!��/Assets/Home/generatkey.png��$��Uniform=�$��50��$��Center=��� $��Generate password��$��15(�$
��Bold"�#���  $��	0,20,0,20q�$��Center=��� -   $��PasswordBox�� �� 
IsReadOnly . $��20(����	#FF496FFF$��
20,0,20,20q�$��Center=��� -   #���  $��GENERATE��$��Center=�$��Bottom=�$��0,0,0,20q��� $ 
Horizontal=�$��Center=�$��0,0,0,20q�.+�� $��Center=�.+��� -    K� ZIndex$ 1�� J� Opacity$ 0Q�A� #���   $��50��$��50���� #���  $��Center=�$��Center=�$��36(�.��-�� -   $��PasswordLength��$��8��$��	10,0,10,0q�$��Center=�$��24(�#���  � $��Center=�.+��� -	   $ 1��$ 0Q�A� $��50��$��50��#���   �� #���  $��Center=�$��Center=�$��36(�.��+�� #���   $ 10,10,10,10q�$ 10,10,10,10��$��	20,0,20,0q�._��� .+��� -
   $��IncludeUppercase��#���  $��Capitalize letter�� d� 	IsChecked$
 TrueY�$	��0,5q����	#FF496FFF$��Stretch=�$��14(��� -   $��IncludeNumbers��#���  $��Number��$
 TrueY�$	��0,5q����	#FF496FFF$��14(��� -   $��IncludeLowercase��#���  $��Lowercase letters��$
 TrueY�$	��0,5q����	#FF496FFF$��14(��� -   $��IncludeSymbols��#���  $��	Symbolism��$
 TrueY�$	��0,5q����	#FF496FFF$��14(��� -   $��InfoTextBox1��#���  $��Stretch=�$��Stretch=�$
��Wrap=�$��14(�$��
20,10,20,0q���Z� 