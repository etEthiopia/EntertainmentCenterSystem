   M S B A M L   `   `   `  ����@  <MyEnt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DagiCaliburn.ViewsDagiCaliburn.Views  5      "    DagiCaliburn.Views.StatsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:DagiCaliburn.Views     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Stretch=�6   $��0��63    �� Orientation$ 
Horizontal=�5      $��5,15,5,5q�5      .+�5      	� -   5         	�Name$ DailyBtn��5          MaterialDesignTabRadioButton#��[  5      $��4q�5      $��12(�6    d� 	IsChecked$ FalseY�5      $��
DAILY DATA��5      	� 5      -   5        $ 	WeeklyBtn��5      #��[  5      $��4q�5      $��12(�6   $ FalseY�5      $��WEEKLY DATA��5      	� 5      -   5        $ 
MonthlyBtn��5      #��[  5      $��4q�5      $��12(�6   $ FalseY�5      $��MONTHLY DATA��5      5%      �� 5&   
   -   5         ��Name$ 
ActiveItem��5(      $��1��5'      5(      5*      5+      