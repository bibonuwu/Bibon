   M S B A M L   `   `   `  ���� @  <BIBON, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:WPFUIKitProfessional.PagesWPFUIKitProfessional.Pages  *    $WPFUIKitProfessional.Pages.Analytics   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:WPFUIKitProfessional.Pages       N� 
FontFamily$  Poppins)� � 	Resources    TabRadioButtonTemplate �� 
TargetType ��Name �� CornerRadius �� Padding �� Color ��Name �� Triggers	 P� Property
 P� Value N� EnterActions �� 
TargetName �� TargetProperty	 �� To g� Duration N� ExitActions��%~  &	        �� " 	�.R��� $ Border��$ 15��$
 10,5q����	#FF496FFF$��1q����� $ #F0F8FF��._��� $ 	TextBlock��#��z��$��Center=�$��Center=�$��16(����� $ 	#FF496FFF�� P� 	 �@d�	IsChecked$

 TrueY� �� .���� .���� $ Border��$* $(Background).(SolidColorBrush.Color)�$ #496FFFX�$ 0:0:0.3D��� $ 	TextBlock��$* $(Foreground).(SolidColorBrush.Color)�$ WhiteX�$ 0:0:0.3D� �� .���� .���� $ Border��$* $(Background).(SolidColorBrush.Color)�$ #F0F8FFX�$ 0:0:0.3D��� $ 	TextBlock��$* $(Foreground).(SolidColorBrush.Color)�$ #496FFFX�$ 0:0:0.3D�.���� $ 8��  SecundaryBackgroundColor#���  ._�� .+���  �� Orientation$ 
Horizontal=�$��Center=�$��Center=�.+��� $��Center=�$��Center=�.+���   SecundaryBackgroundColor1#���    CardBorderStyle1#���  $ 0,0,0,0q�$	��600��._��  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��
Recomended��$��25(�$
��Bold"�  PrimaryTextColor#���  $��Center=�$��Center=�$��	0,20,0,46q�� $��1��.+��� $��/Assets/folder.png��$��Uniform=�$��45��$��45��$	��Top=�$��Center=� K� ZIndex$ 1��$��	0,-30,0,0q�� $��50��.+��� -    ��Name$ OpenWebsiteButton1��  PrimaryButtonStyle1#���  $��QTTabBar��$��Center=�$��Center=�� $��2��$��0,30,0,0q�.+��� $��/Assets/Store/2.png��$��Uniform=�$��35��$��35��$	��Top=�$��Center=�$ 1��$��	0,-24,0,0q�� $��50��.+��� -   $ OpenWebsiteButton2��#���  $��NVIDIA��$��Center=�$��Center=�� $��3��$��0,30,0,0q�.+��� $��/Assets/Store/3.png��$��Uniform=�$��45��$��45��$	��Top=�$��Center=�$ 1��$��	0,-24,0,0q�� $��50��.+��� -   $ OpenWebsiteButton3��#���  $��Visual Studio��$��Center=�$��Center=�� $��4��$��0,30,0,0q�.+��� $��/Assets/Store/4.png��$��Uniform=�$��35��$��35��$	��Top=�$��Center=�$ 1��$��	0,-24,0,0q�� $��50��.+��� -   $ OpenWebsiteButton4��#���  $��VM Workstation��$��Center=�$��Bottom=�$��0,0,0,5q�� $��5��$��0,30,0,0q�.+��� $��/Assets/Store/18.png��$��Uniform=�$��35��$��35��$	��Top=�$��Center=�$ 1��$��	0,-15,0,0q�� $��50��.+��� -   $ OpenWebsiteButton5��#���  $��WinRAR��$��Center=�$��Center=�� $��6��$��0,30,0,0q�.+��� $��/Assets/Store/5.png��$��Uniform=�$��35��$��35��$	��Top=�$��Center=�$ 1��$��	0,-24,0,0q�� $��50��.+��� -   $ OpenWebsiteButton6��#���  $��Steam��$��Center=�$��Center=��� $��Bottom=�.+��� $ 
Horizontal=�$��	0,0,10,10q�$��Right=�.+��� $
��Wrap=�$	��370��$��0,0,20,0q�#���  $��14(�$��Center=� �� TextAlignment$ Right=���Z� ��Өзіңізге қажетті бағдарламаларды оңай және жылдам жүктеп алуға мүмкіндік бар!�� ���	#FF496FFF$ 10,10,10,10��._��� $ 
Horizontal=�$��Center=�$��Center=�.+�	� -    	�Name$ MyButton��$��	    1    �� 	� 	GroupName$
 Tabs��$��16(�$��5q� � Cursor$
 Hand��#��[  	� -   $��	    2    ��$
 Tabs��$��16(�$��5q�$
 Hand��#��[  	� -	   $��	    3    ��$
 Tabs��$��16(�$��5q�$
 Hand��#��[  	� -
   $��	    4    ��$
 Tabs��$��16(�$��5q�$
 Hand��#��[  �� $ 20,20,20,20��#���  $��10,0,0,0q�._�� .+��� $��Bottom=�$ 
Horizontal=�$��
10,20,10,0q�$��Center=�$	��720��$	��480�� � -     �Name$ frameContent1�� J� Opacity$ 1Q�  � NavigationUIVisibility$ Hidden=�