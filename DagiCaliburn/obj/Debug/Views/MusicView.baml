   M S B A M L   `   `   `  ����@  <MyEnt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DagiCaliburn.ViewsDagiCaliburn.Views  5      "    DagiCaliburn.Views.MusicView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:DagiCaliburn.Views     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullY UMaterialDesignThemes.MahApps, Version=0.1.0.325, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5      ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5      
�������.��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   � $
��Auto��6   $��10q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      �� -   5         ��Name$ CurrentAudio��5      $��0��6.    #  MaterialDesignDisplay1TextBlock#��[  5      $��0,0,20,0q�6J   5      �� 5      -   5         ��Name$ 
AudioTypes��5   4   $��1��6   $��0��6'    �� SelectedIndex 6H   �� *
AudioIndex+ �� SelectedItem 6m   �� *CurrentAudio+��5       �� 5!      .R�5"      �� ��6$   �� *Name+5#      5%      �� 5&      -   5         ��Name$ CloseGBSelected��5&   C   $��2��6   $��0,5,5,0q�6%   $��0��66   $��25��6\   $��25��6h   
���  ��$��Right=�6�   $	��Top=�6�    / +MaterialDesignFloatingActionMiniLightButton#��[ 5'       � ToolTip$1 +MaterialDesignFloatingActionMiniLightButton��5(      .��5)      '  !MaterialDesignThemes.Wpf.PackIcon  
��������  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ Close=�5+      $��14��5,      $��14��5-      5.      5/      &   System.Windows.Controls.DataGrid  52   
   $��1��6   $��1��6    $��0 8 0 8q�53      	   CanUserReorderColumns	 .  $
��Auto��56      
   CanUserResizeColumns
 .    AutoGenerateColumns .     
IsReadOnly .    CanUserDeleteRows .     CanUserSortColumns .    CanUserAddRows .  ��55      �� *GBMusicSells+ �� Mode$ OneWay=� 57      �� *SelectedIndex+ 5=      �� *SelectedSell+$ TwoWay=�   Columns 5@      0  *System.Windows.Controls.DataGridTextColumn  5B      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%      MaxWidth$	 300Q�63   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   ��  �� Path$
 Name�5C        5D      $	 Dir��6%   $	 300Q�62    6A   �� $	 Dir�5E        5F      $
 Size��6%    63   �� $
 size�5G      5L      #  MaterialDesignThemes.Wpf.Card  5M   
   $��2��6   $��Center=�6+   $��Center=�6H     PrimaryHueLightBrush#���  5N      +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=�5O       " PrimaryHueLightForegroundBrush#���  5P      $	��300��5Q      $��30q�5R      .��5T      �� $��Stretch=�6   $��Center=�67    �� Orientation$ Vertical=�6R   .+�5V      �� $��Center=�6   $��Center=�6:   $ Vertical=�6V   .+�5W      �� -   5        $ TotalSum��5W       $��0,0,0,40q�62   $��Center=�6D   #��[  5X      5Z      �� 5[      $��Center=�6     # MaterialDesignHeadlineTextBlock#��[ 5\      ��5[   =   �� $ 
TotalPrice�5^      5`      5a      5b      � 5d   
   -   5         �Name$ DrivesIsVisible��5d      $��3��6   $��Bottom=�66   .+�5f      �� -   5         ��Name$ DrivesToCopyTo��5f   (   $��1��6   ��5g      �� 5h      .R�5i      � 5j      ��5l      �� 5m      .R�5n        -   5        $��8q�5n   .   $��20q�6;   	  Caliburn.Micro.Message 	  Attach$; 5[Event MouseDoubleClick] = [Action HardDClick(DName)]��5o         UniformCornerRadius$ 6Q�5q      .��5r      � $
��Auto��6#   .+�5t   "   � $��5q�6'    5u   &   �� 5v   *   $
��Auto �6;   �� 5w   *   $��* �6;     5y   &   �� 5z   *   $��3* �68   �� 5{   *   $��* �68   .+�5}   &   �� $ DName��60   $��Center=�6?   #��[ 6Z   $
��Left=�6�   $��5,0,0,0q�6�   $��30(�6�   $��0��6�   $��1��6�   ��6�   �� *DName+  5~   &   $��0��6>   $��50��6K   $��50��6V   $��0��6b    6r   �� *Icon+�� 5   &   $��Center=�61   $��2��6N   $��5q�6b   $ 
Horizontal=�6m   $��1��6�   .+�5�   *   �� ��64   �� *	FreeSpace+�� 5�   *   $��  /  ��64   �� 5�   *   ��64   �� *
TotalSpace+5�   '   5�   #   5�      5�      5�      5�      5�      � 5�   
   -   5        $ RegisterIsVisible��5�      $��3��6   $��Bottom=�68   $��Right=�6S   .+�5�      �� $��10q�6   $��0��6%   $ 
Horizontal=�62   .+�5�      �� -	   5        $ Cancel��5�      $��0,0,30,0q�6)    # MaterialDesignRaisedLightButton#��[ 5�      $	��150��5�      $��50��5�      $��25(�5�      $4 .Resource name: MaterialDesignRaisedLightButton��5�      .��6>   Cancel5�      �� 5�      -
   5        $ Register��5�       " MaterialDesignRaisedDarkButton#��[ 5�      $	��150��5�      $��50��5�      $��25(�5�      $3 -Resource name: MaterialDesignRaisedDarkButton��5�      .��6=   
Register5�      5�      5�      5�      5�      