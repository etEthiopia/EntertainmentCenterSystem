   M S B A M L   `   `   `  ����@  <MyEnt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DagiCaliburn.ViewsDagiCaliburn.Views  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5	          DagiCaliburn.Views.GBView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:DagiCaliburn.Views     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullY UMaterialDesignThemes.MahApps, Version=0.1.0.325, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5      ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5      XiPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity  5	      $
��Auto��5      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��1.5* �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $
��Auto��6   $��20q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      �� -   5         ��Name$ 
AudioTypes��5   B   $��1��6   $��0��6'   $��20(�64    �� SelectedItem 6V   �� *CurrentAudio+��5      �� 5       .R�5!      �� ��6$   �� *Name+5"      5$      �� 5%      -   5         ��Name$ CloseGBSelected��5%   C   $��2��6   $��0,5,5,0q�6%   $��0��66   $��25��6\   $��25��6h   
���  ��$��Right=�6�   $	��Top=�6�    /  +MaterialDesignFloatingActionMiniLightButton#��[  5&       � ToolTip$1 +MaterialDesignFloatingActionMiniLightButton��5'      .��5(      '  !MaterialDesignThemes.Wpf.PackIcon  
��������  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ Close=�5*      $��14��5+      $��14��5,      5-      5.      &   System.Windows.Controls.DataGrid  5/   
   $��1��6   $��1��6    $��0 8 0 8q�50         CanUserReorderColumns .  $
��Auto��53      $
��Left=�54         CanUserResizeColumns . 	   AutoGenerateColumns	 .  $
   HorizontalScrollBarVisibility$

 Auto=�59         
IsReadOnly .    CanUserDeleteRows .     CanUserSortColumns .    CanUserAddRows .  ��52      �� *GBMusicSells+ �� Mode$ OneWay=� �� SelectedIndex 56      �� *SelectedIndex+ 5=      �� *SelectedSell+$ TwoWay=�   Columns 5@      0  *System.Windows.Controls.DataGridTextColumn  5B      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.5* 63   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   ��  �� Path$
 Name�5C        5D      $
 Size��6%    64   �� $
 Size�5E        5F      $	 Dir��6%   $
 0.4* 62    6@   �� $	 Dir�5G      5M      #  MaterialDesignThemes.Wpf.Card  5N   
   $��2��6   $��Center=�6+   $��Center=�6H     PrimaryHueLightBrush#���  5O      +	  %MaterialDesignThemes.Wpf.ShadowAssist 	  ShadowDepth$ Depth5=�5P       " PrimaryHueLightForegroundBrush#���  5Q      $	��300��5R      $��20q�5S      .��5U      �� $��Stretch=�6   $��Center=�67    �� Orientation$ Vertical=�6R   .+�5W      �� $��Stretch=�6   $��Center=�6;   $ Vertical=�5X      .+�5Y      �� -   5         ��Name$ TotalSum��5Y       $��0,0,0,0q�62   $��Center=�6D    # MaterialDesignDisplay1TextBlock#��[ 5Z      5\      � 5]      $��Stretch=�6   .+�5_      �� -   5         ��Name$ GBmoney��5_   S   $��Center=�6#    �� 
IsReadOnly . $��25(�6d   $��0,0,0,0q�6r    % !MaterialDesignFloatingHintTextBox#��[ 5a      �� 5b      -   5        $��20,0,0,0q�5b   !   $��Right=�64   $��20��6P   $��20��6\   #��[  5c      $2 ,MaterialDesignFloatingActionMiniAccentButton��5d      .��5e        $
 Edit=�5f      $��15��5g      $��15��5h      5i      5l      5o      5q      5r      �� 5s   
   $��3��6   $��	20,10,0,0q�6!    # MaterialDesignHeadlineTextBlock#��[ 5t      
Copy To:5v      �� 5x   
   $��Stretch=�6   $��4��65   $
��Auto=�6B   $��0 0 0 0q�6e   .��5y      �� -   5         ��Name$ Drives��5y      $	��Top=�6+   $��Stretch=�6C   ��5{      �� 5|      .R�5}      A�  A� Columns$ 2��6&   5~      ��5�      �� 5�      .R�5�        $��Center=�6.    � Cursor$
 Hand��6I   $��Stretch=�6W   $ Depth3=�5�      
  Caliburn.Micro.Message 
  Attach$: 4[Event MouseDoubleClick] = [Action GBClick(Address)]��5�        PrimaryHueDarkBrush#���  5�       ! PrimaryHueDarkForegroundBrush#���  5�      $��10q�5�      $��10q�5�         UniformCornerRadius$ 6Q�5�      .��5�      �  5�   "   �� 5�   &   $��* �67   �� 5�   &   $��3* �67   .+�5�   "   � $��0��6'   $��0,0,10,0q�67     5�   &   �� 5�   *   $��* �68   �� 5�   *   $��* �68   .+�5�   &     $��Center=�6>   $
��Left=�6[   $	��Top=�6}   $��20��6�   $
��Auto��6�   $��0��6�   $��0��6�   #���  6�   $ Harddisk=�6  �� 5�   &   $ Address��60   $��1��6A   $��Center=�6N   $��Center=�6k   $��0��6�   $��25(�6�    % !MaterialDesignSubheadingTextBlock#��[ 5�      ��5�   �   �� *Port+5�   '   5�   #   � 5�   "   $��1��6'     5�   &   �� 5�   *   $��* �68   �� 5�   *   $
��Auto �68   �� 5�   *   $��* �68   .+�5�   &   �� $��0��60   $��Center=�6=   $��10(�6Z   $��Light"�6   #��[ 5�      ��5�   h   �� *Props+5�   '   �� 5�   &   $��1��60   $��Center=�6=   #��[ 5�      ��5�   Z   �� *Cost+5�   '   �� 5�   &   $��Center=�60   $��2��6M   $��10(�6Z   #��[ 5�      ��5�   h   �� *Items+5�   '   5�   #   5�      5�      5�      5�      5�      5�      5�      