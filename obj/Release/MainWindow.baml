   M S B A M L   `   `   `  ���� @  <BIBON, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WPFUIKitProfessionalWPFUIKitProfessional  %    WPFUIKitProfessional.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:WPFUIKitProfessional     -     �Name$
  home�� � Title$ BIBON��$	��720��$
��1100�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=� � WindowState$ Normal=�$��Poppins)� � 	Resources  �� 
TargetType	 �� Property
 �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property P� Value N� EnterActions	 W� To �� TargetProperty g� Duration	 �� To N� ExitActions��%  '��       �� " ��.��� 	 � ��
 �� " ��.R��� $��3q�#��z��#��z��#��z��$ 6�����#FFFFFF._��� $��Center=�$��Center=� P�  �@J�IsMouseOver .  �� .���� .��W� $	 210X�$ Width�$ 0:0:0.3D�W� $	 230X�$ Height�$ 0:0:0.3D��� $ CoralX�$( "Background.(SolidColorBrush.Color)�$ 0:0:0.3D� �� .���� .��W� $	 200X�$ Width�$ 0:0:0.3D�W� $	 220X�$ Height�$ 0:0:0.3D��� $ WhiteX�$( "Background.(SolidColorBrush.Color)�$ 0:0:0.3D�.���� $ 10��   PrimaryBackgroundColor#���   ._��  � RowDefinitions �� �� $
��179* �.+��� $ 10 10 10 10�� �� Padding$ 10q�  SecundaryBackgroundColor#���  $��	10,6,10,0q�$��1��$	��Top=�._�� .+��� -    ��Name$ btnClose��  IconButtonsStyle#��[  	 close#���  
 � Tag$ IsCloseButton��$
��Left=�� -    �Name$ gridMenu��$��Center=�$��Center=�.+��� $��1�� �� Orientation$ 
Horizontal=�.+�	� -    	�Name$ rdHome��$
��Home��  MenuRadioButtonStyle#���  $��0,0,10,0q�$��Center=�$��Center=�	� -   $ rdAnalytics��$��Store��#���  $��0,0,10,0q�$��Center=�$��Center=��� $��3��$ 18��$	��110��$��35��  TertiaryBackgroundColor#���  $��0,0,10,0q�._��� $ 
Horizontal=�$��Center=�.+�F�   sun#���  $��16��$��16�� �� Stretch$ UniformToFill=�  PrimaryYellowColor#���  $��Center=�d� -    d�Name$ Themes�� d� 	IsChecked$ FalseY�$��Right=�  ToggleButtonStyle#���  $��	10 0 10 0q�F�  	 moon#��� 	 $��16��$��16��$
 Fill=� 
 PrimaryWhiteColor#��� 
 $��Center=�	� -   $ 
rdMessages��$��Course��#���    messages# �  $��Center=�$��Center=�$��0,0,10,0q�	� -   $ rdCollections��$��About��#���    collections# �  $��Center=�$��Center=��� -	   #��[ #���  $ IsCloseButton��$��Right=�� $��2�� �� $��83 ��� .+��� $��1��$ 0 0 5 0��
������ $��0,10,0,0q�._�� $��1��.+��� $��Center=�$��Center=�.+��� -
     ��Name$  welcomeLabel��$��36(�$��Segoe Script)�
�������$��Welcome to BIBON��$��Center=�$��0,0,0,20q��� -   $ actionButton��$	��220��$	��200��$
��Bold"�
����p������ ! �� ImageSource$! /Assets/ic_near.png��" j� Stretch$" UniformToFill=��� ���	#FF898989$��Center=�$��0,20,0,0q�.��	BIBON v.4 � -   #  �Name$# frameContent��$  � NavigationUIVisibility$$ Hidden=�$��
10,0,10,10q�