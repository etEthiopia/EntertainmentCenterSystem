   M S B A M L   `   `   `  ����@  <MyEnt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DagiCaliburn.ViewsDagiCaliburn.Views  5      !    DagiCaliburn.Views.SellView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:DagiCaliburn.Views     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullY UMaterialDesignThemes.MahApps, Version=0.1.0.325, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5      .��5      � .+�5   
   � -   5          �Name$  TFormIsVisible��5      .+�5      �� $
��Auto=�6   .��5      #  MaterialDesignThemes.Wpf.Card  $��1��6&   $��20q�63   $
��Auto��5      $��8q�5      .��5      �  � RowDefinitions 5      �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   .+�5      d� $��0��6'   $��10,30,10,10q�65   $��75��5      $��75��6+   $��Light"�66   $��30(�5   P    $   MaterialDesignActionToggleButton#��[  5   %    � ToolTip$&  MaterialDesignActionToggleButton��5   %   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth0=�5   %   ��5   %   ��  �� Path$
 Icon� �� Mode$ OneWay=�5   %   �� 5       -   5         ��Name$ 	CloseType��5    O   $��2��6!   $��0,5,5,0q�61   $��0��6B   $��20��6b   $��20��6n   
���  ��$��Right=�6�   $	��Top=�6�    / +MaterialDesignFloatingActionMiniLightButton#��[ 5!      $1 +MaterialDesignFloatingActionMiniLightButton��5"      .��5#      '  !MaterialDesignThemes.Wpf.PackIcon  
��������  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ Close=�5%      $��14��5&      $��14��5'      5(      � 5)      $��1��6   $��10q�6,    � ColumnDefinitions 5*      �� 5+   "   $��6* �63   �� 5,   "   $
��Auto �63   �� 5-   "   $��2* �63   .+�50      �� -   5        	 ��Name$
	 Name��52      $��0��50   &   $��5q�66   $��25(�6A   )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$

 Name��51      $��Light"�6>      PrimaryHueMidForegroundBrush   
Foreground# �  53   &    % !MaterialDesignFloatingHintTextBox#��[ 54      �� 56      $��1��6'   $��Bottom=�68   $��0,5,5,5q�6T   $��15(�57   ,   $
 
 File Type��58   ,   $��Light"�6X   # �  59   ,    & "MaterialDesignFloatingHintComboBox#��[ 5:   ,   ��57   :   �� *	FileTypes+ �� SelectedItem 5;   ,   �� *
FileType+5=      �� 5?      $��2��6'   $��Light"�68   $��Bottom=�6K   $��0,5,5,5q�6g   $��15(�5@   ,   $

 Type��5A   ,   # �  5B   ,   #��[ 5C   ,   ��5@   :   �� *Types+ 5D   ,   �� *Type+��5E   "   �� 5F   &   .R�5G   *   �� ��64   �� *Name+5H   '   5J      5K      � 5L      $��2��6   $��10q�6,    5M      �� 5N   "   $��3* �63   �� 5O   "   $��* �63   �� 5P   "   $��* �63   .+�5R      �� -   5        $		 Dir��5U      $��Light"�5R   '   # �  6:   $��0��5S   &   $��	5,10,5,10q�66   $��25(�6I   $	
 Dir��5T      #��[ 5V      �� 5W      -   5        $	 Price��5Z      $��Light"�5W   '   $��1��6:   $��	5,10,5,10q�6J   $��25(�6]   $
 Price��5X      # �  5Y   +   #��[ 5[      �� 5\      -   5        $
	 Size��5_      $��Light"�5\   '   $��2��6:   $��	5,10,5,10q�6J   $��25(�6]   $

 Size��5]      # �  5^   +   #��[ 5`      5c      �� 5d       �� Orientation$ 
Horizontal=�6%   $��3��6>   $
��Left=�6K   .+�5e      �� $��Light"�6(   $��3��6;   $��10q�6H   $
��Left=�6T   $
��Auto��6o   $
 Drive��5f      $��15(�6F   #��[ 5g       �� SelectedValue 5f   T   �� *Drrive+��6u   �� *	Drivees+5g      �� 5h      -   5        $ Refresh��5h   R   $��Light"�6&   $��10q�69   $��0��6E   $��20��6c   $��20��6o   $��Right=�6{    . *MaterialDesignFloatingActionMiniDarkButton#��[ 5i      $0 *MaterialDesignFloatingActionMiniDarkButton��5j      .��5k        $ Refresh=�5l      $��14��5m      $��14��5n      5o      5p      �� 5q      -   5        $
 Save��5q   .   $��4��6!   $��Right=�6<   $��10q�6X    " MaterialDesignRaisedDarkButton#��[ 5r      $8 2Resource name: MaterialDesignRaisedLightDarkButton��5s      .��6B   SAVE5u      �� 5v      -	   5         ��Name$ SellViewMessage��5v   D   $��20q�6$   $��Light"�61   $��5��6]   $��Center=�6j   $��20(�6�    # MaterialDesignHeadlineTextBlock#��[ 5w      5y      5}      5~      5      5�      5�      5�      