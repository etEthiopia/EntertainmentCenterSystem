   M S B A M L   `   `   `  ����@  <MyEnt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DagiCaliburn.ViewsDagiCaliburn.Views  5      ` \De.TorstenMandelkow.MetroChart, Version=0.9.5818.20693, Culture=neutral, PublicKeyToken=nullwTclr-namespace:De.TorstenMandelkow.MetroChart;assembly=De.TorstenMandelkow.MetroChartDe.TorstenMandelkow.MetroChart 5	      !    DagiCaliburn.Views.SaleView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:DagiCaliburn.Views     5      ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullY UMaterialDesignThemes.MahApps, Version=0.1.0.325, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5      g
MetroChartTclr-namespace:De.TorstenMandelkow.MetroChart;assembly=De.TorstenMandelkow.MetroChart   5	      
�������.��5      �   � RowDefinitions  5   
   �� 5      $��6* �6   �� 5      $��3* �6   .+�5   
   � -   5         �Name$ MainGridIsVisible��5      $��0��6     5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��10q�6   $��0��6!    �� Orientation$ 
Horizontal=�6.   .+�5      �� -   5         ��Name$ SimpleSells��5      $��0,0,10,0q�6.    #  MaterialDesignRaisedLightButton#��[  5      $	��100��5       � ToolTip$4 .Resource name: MaterialDesignRaisedLightButton��5      .��6>   
Simplified5      �� 5       -   5        $ AllSells��5       $��0,0,10,0q�6(     MaterialDesignRaisedButton#��[ 5!      $	��100��5"      $/ )Resource name: MaterialDesignRaisedButton��5#      .��69   Listed5%      �� 5&      -   5        $ RecordBooks��5&       " MaterialDesignRaisedDarkButton#��[ 5'      $	��100��5(      $3 -Resource name: MaterialDesignRaisedDarkButton��5)      .��6=   Total5+      5,      � 5-      -   5        $ FileOnProgressIsVisible��5-   !   $��1��6   .+�5.      #  MaterialDesignThemes.Wpf.Chip  $��5,0,5,0q�6"   
���������5/      ��  �� Path$ 
OnProgress� �� Mode$ OneWay=�51      &   System.Windows.Controls.DataGrid  52      $��2��6   $��1��6$   $��0 8 0 8q�53         CanUserReorderColumns .  $
��Auto��57         CanUserResizeColumns . 	   AutoGenerateColumns	 .  
   
IsReadOnly
 .    CanUserDeleteRows .     CanUserSortColumns .   Caliburn.Micro.Message   Attach$6 0[Event MouseDoubleClick] = [Action SellDClick()]��5?         CanUserAddRows .  ��55      �� *Sells+$ OneWay=� �� SelectedIndex 58      �� *SelectedIndex+ �� SelectedItem 5>      �� *SelectedSell+$ TwoWay=�   Columns 5B      0  *System.Windows.Controls.DataGridTextColumn  5C      ,  &System.Windows.Controls.DataGridColumn   Header$  ��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 60   �� $ initial�5E        5F      $
 Name��6%      MaxWidth$	 300Q�63    6B   �� $
 Name�5G        5H      $ Status��6%    65   �� $ Status� �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�5I        5K      $ Price��6%    64   �� $ 
TotalPrice�5L        5M      $
 Size��6%    63   �� $
 size�5N      5S      5U      � 5V   
   -   5        $ BlueIsVisible��5V   6   $��0��6   
�������.+�5Y      #  MaterialDesignThemes.Wpf.Card  $��1��6"   $��20q�6/   $
��Auto��5Z      $
��Auto��5[      $��8q�5\      .��5]      �   5^      �� 5_      $
��Auto �6(   �� 5`      $
��Auto �6(   �� 5a      $
��Auto �6(   .+�5c      �� -   5        $ BackSelected��5c   <   $��5,5,0,0q�6   $��0��6/   $��25��6R   $��25��6^   $
��Left=�6j   $	��Top=�6�    / +MaterialDesignFloatingActionMiniLightButton#��[ 5d      $1 +MaterialDesignFloatingActionMiniLightButton��5e      .��5f      '  !MaterialDesignThemes.Wpf.PackIcon  
��������	  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]] 	  Kind$ KeyboardBackspace=�5h      $��14��5i      $��14��5j      5k      �� 5m      -   5        $ CloseSelected��5m   ;   $��0,5,5,0q�6   $��0��6.   $��25��6R   $��25��6^   
���  ��$��Right=�6z   $��Right=�6�   $	��Top=�6�   #��[ 5n      $1 +MaterialDesignFloatingActionMiniLightButton��5o      .��5p        
�������$ Close=�5r      $��14��5s      $��14��5t      5u      � 5v      $��1��6   $��20q�6(    � ColumnDefinitions 5w      �� 5x      $��* �6/   �� 5y      $��* �6/     5{      �� 5|      $
��Auto �6,   �� 5}      $
��Auto �6,   �� 5~      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   .+�5�      �� $ 
Horizontal=�6%   $
��auto��6>   $��0��6L   $��2��6Y   .+�5�      d� $��0��6+   {�.  $��50��5�      $��50��6+   $��10q�5�   #    $  MaterialDesignActionToggleButton#��[ 5�   %   $&  MaterialDesignActionToggleButton��5�   %   +
  %MaterialDesignThemes.Wpf.ShadowAssist 
  ShadowDepth$ Depth3=�5�   %   ��5�   %   �� $ SelectedInitials�$ OneWay=�5�   %   �� 5�      $��5q�6M   $
��Auto��6X   $
��Wrap=�6f    # MaterialDesignHeadlineTextBlock#��[ 5�   %   ��5�   (   �� $ SelectedName�5�      5�      �� 5�      $ 
Horizontal=�6%   $��1��6>   $��0��6L   $��10q�6\   .+�5�      ��  % !MaterialDesignSubheadingTextBlock#��[ 5�   %   ��5�   (   �� $ SelectedPrice�5�      �� 5�      $��  BIRR��6)   #��[ 5�   %   5�      5�      �� 5�      $��1��6I   $��1��6W   $��10q�6g   #��[ 5�   %   ��5�   $   �� $ SelectedSize�5�      �� 5�      $��2��6$   $��2��62   $��10,5,0,0q�6F   �� 5�         MaterialDesignBody1Hyperlink#��[ 5�   !   .�6W   Dir5�      5�      �� 5�      $��3��6$   $��WrapWithOverflow=�6U   $��2��6v   $��
10,0,10,10q�6�   #��[ 5�   !   ��5�   1   �� $ SelectedDir�5�      �� 5�      $��4��6$   $��Center=�6Z   $
��Wrap=�6u   $��0��6�   $��10q�6�      MaterialDesignTitleTextBlock#��[ 5�      ��5�   1   �� $ SelectedDateTime�5�      �� 5�      $��4��6%   $ Vertical=�62   $��1��6I   $��10q�6Y   .+�5�      �� �� 5�      #��[ 5�      .�6=   
Reciever5�      5�      �� 5�      #��[ 5�      ��5�   (   �� $ SelectedReciever�5�      5�      �� 5�      $��5��6%   $ Vertical=�62   $��0��6I   $��10q�6Y   .+�5�      �� $��Center=�6(   #��[ 5�      Today's Sells5�      �� 5�      
����  �$��Center=�6e    #	 MaterialDesignDisplay1TextBlock#��[	 5�      ��5�   (   �� $ SelectedTodaySells�5�      5�      �� 5�      $��5��6%   $ Vertical=�62   $��1��6I   $��10q�6Y   .+�5�      �� $��Center=�6(   #��[ 5�      Total Sells5�      �� 5�      
����  �$��Center=�6e   #��[	 5�      ��5�   (   �� $ SelectedTotalSells�5�      5�      5�      5�      5�      5�      � 5�   
   -	   5        $ GreenIsVisible��5�   5   $��0��6   
�������.+�5�        $��1��6"   $��20q�6/   $
��Auto=�6;   ��. $
��Auto��5�      $
��Auto��5�      $��8q�5�      .��5�      �   5�      �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   .+�5�      �� -
   5        $ FCloseSelected��5�   *   $��0��6   $��25��6B   $��25��6N   
���  ��$��Right=�6j   $	��Top=�6�   #��[ 5�      $1 +MaterialDesignFloatingActionMiniLightButton��5�      .��5�        
�������$ Close=�5�   !   $��14��5�   !   $��14��5�   !   5�      � 5�      $��1��6   $��20q�6(    5�      �� 5�      $��* �6/   �� 5�      $��* �6/     5�      �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5     $��* �6,   .+�5     �� $ 
Horizontal=�6%   $��0��6>   $��2��6K   .+�5     d� $��0��6+   {�.  $��50��5     $��50��6+   $��10q�5	  #   #��[ 5  %   $&  MaterialDesignActionToggleButton��5  %   $ Depth3=�5  %   ��5
  %   �� $ SelectedInitials�$ OneWay=�5  %   �� 5     $ Vertical=�6)   .+�5  "   �� $��10q�6R   $��WrapWithOverflow=�6^   #��[ 5     ��5  ,   �� $ SelectedName�5  #   �� 5  "   $��3��6,   $��WrapWithOverflow=�6]   $��2��6~   $��10q�6�   #��[ 5  !   ��5  9   �� $ SelectedDir�5  #   5     5     �� 5      $ 
Horizontal=�6%   $��1��6>   $��0��6L   $��10q�6\   .+�5!     �� #��[ 5"     ��5!  (   �� $ SelectedPrice�5$     �� 5%     $��  BIRR��6)   #��[ 5&     5(     5)     �� 5+     $��1��6I   $��1��6W   $��10q�6g   #��[ 5,     ��5+  $   �� $ SelectedSize�5.     �� 50     $��2��6$   $��Center=�6Z   $
��Wrap=�6u   $��0��6�   $��10q�6�   #��[ 51     ��50  1   �� $ SelectedDateTime�2019-09-05 09:08:2753     �� 55     $��2��6%   $ Vertical=�62   $��1��6I   $��10q�6Y   .+�56     �� �� 57     #��[ 59     .�6=   
Reciever5;     5<     �� 5=     #��[ 5>     ��5=  (   �� $ SelectedReciever�E:/5@     5A       5C     $��3��6#   $��2��60   $��Visible=�6G   ��. $��0 8 0 8q�5D     $
��Auto��5F      . 	 .  
 .  .   . $6 0[Event MouseDoubleClick] = [Action SellDClick()]��5N      .  ��5E     �� *FolderFiles+$ OneWay=� 5G     �� *SelectedIndex+ 5M     �� *SelectedSell+$ TwoWay=� 5Q       5R  "   $  ��65    6@   �� $ initial�5T  #     5U  "   $
 Name��65   $	 330Q�6C    6R   �� $
 Name�5V  #     5X  "   $ Price��65    6D   �� $ 
TotalPrice�5Y  #     5Z  "   $
 Size��65    6C   �� $
 size�5[  #     5\  "      MinWidth$ 10Q�65   $  ��6C   5]  #   5a     5c     5j     5k     5l     � 5m  
   $��1��6    5n     �� 5o     $��* �6#   �� 5p     $��* �6#   .+�5t     2  ,De.TorstenMandelkow.MetroChart.DoughnutChart  $��0��5u  R   $��0q�6b   .  (De.TorstenMandelkow.MetroChart.ChartBase   ChartSubTitle$ Todays Earnings��6n      
ChartTitle 6   �� $ NumberTodaysEarnings�$ OneWay=�   Series 5w     0  *De.TorstenMandelkow.MetroChart.ChartSeries     DisplayMember$ Element��5x        SeriesTitle$ Doughnut��5y         ValueMember$  Percent��5{     ��5w  1   �� $ TodaysEarnings�5{     5~     �� 5�     $ vertical=�6   5�     5�     5�     5�     