   M S B A M L   `   `   `  ����@  <MyEnt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DagiCaliburn.ViewsDagiCaliburn.Views  5      %    DagiCaliburn.Views.SpecialsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:DagiCaliburn.Views     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullY UMaterialDesignThemes.MahApps, Version=0.1.0.325, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5      ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5	      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   &   System.Windows.Controls.DataGrid  $��0��6   $��0 8 0 8q�5         CanUserReorderColumns .  $	��250��5      $
��Auto��5         CanUserResizeColumns .    AutoGenerateColumns .     
IsReadOnly . "   VerticalScrollBarVisibility$
 Auto=�5         CanUserDeleteRows .     CanUserSortColumns .   Caliburn.Micro.Message   Attach$6 0[Event MouseDoubleClick] = [Action SellDClick()]��5      	   CanUserAddRows	 .  ��5      �� *
Specials+
 �� Mode$
 OneWay=� �� SelectedIndex 5      �� *SelectedIndex+ �� SelectedItem 5!      �� *SelectedSpecial+$
 TwoWay=�   Columns 5$      0  *System.Windows.Controls.DataGridTextColumn  5&      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.4* 63      
FontWeight$ Light"�6`   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   ��  �� Path$
 Name�5'        5)      $ Price��6%   $ Light"�6V    65   �� $ Price�5*        5+      $ Expire Date��6%   $
 0.4* 6:   $ Light"�6h    6G   �� $ AcDate�5,      50      � 51   
   -   5         �Name$ SpecialIsVisible��51      $��1��6)   .+�52      #  MaterialDesignThemes.Wpf.Card  $��1��6"   $��20q�6/   $
��Auto��53      $��8q�54      .��55      �   56      �� 57      $
��Auto �6(   �� 58      $
��Auto �6(   �� 59      $
��Auto �6(   �� 5:      $
��Auto �6(   �� 5;      $
��Auto �6(   �� 5<      $
��Auto �6(   .+�5>      �� $��0��6    $��10q�6-    #  MaterialDesignHeadlineTextBlock#��[  5?      ��5>   9   �� *SpecialTitle+5A      � 5C      $��1��6   $��10q�6(    � ColumnDefinitions 5D      �� 5E      $��3* �6/   �� 5F      $��* �6/   .+�5I      �� -   5         ��Name$
 Name��5K      $��0��5I   "   $��5q�62   $��25(�6=      PrimaryHueMidForegroundBrush)  #MaterialDesignThemes.Wpf.HintAssist   
Foreground# �  6L      Hint$
 Name��5J      $��Light"�6=    % !MaterialDesignFloatingHintTextBox#��[ 5L      ��5K   (   �� *SpName+5L      �� 5N      -   5        $ Price��5P      $��1��5N   "   $��Light"�62   $��5q�6E   $��25(�6P   # �  6^   $ Price��5O      #��[ 5Q      ��5P   -   �� *	SpPrice+5Q      5R      � 5S      -   5        $ ExisitingExpiryIsVisible��5S      $��2��6=   .+�5T      �� $��1��6   $��Light"�6/   $
��15,5q�6B   $��20(�6P   # �  6^   $ Existing Expiry Date��5U      #��[ 5W      ��5V      �� *SpExp+5W      5X      �� 5Y      $��3��6   $
��15,5q�6,   $ Expire Date��5Z       & "MaterialDesignFloatingHintComboBox#��[ 5[      ��5Y   :   �� *ExpireDates+ �� SelectedValue 5Z   0   �� *SpExp+5]      �� 5^      $��4��6!    �� Orientation$ 
Horizontal=�6.   $��Right=�6G   .+�5_      � -   5        $ DeleteIsVisible��5_   ;   $��Right=�6   .+�5a      �� -   5         ��Name$ Delete��5d      
�������
���  ��
���  ��$
��Left=�5a   [   $��5q�6v    " MaterialDesignRaisedDarkButton#��[ 5b      $��80��5c       � ToolTip$8 2Resource name: MaterialDesignRaisedLightDarkButton��5e      .��6V   DELETE5g      5h      �� 5i      -   5        $ Cancel��5i   "    # MaterialDesignRaisedLightButton#��[ 5j      $4 .Resource name: MaterialDesignRaisedLightButton��5l      .��6>   CANCEL5n      �� 5o      -   5        $
 Save��5r      $��Right=�5o   !   $��5q�6=   #��[ 5p      $��80��5q      $8 2Resource name: MaterialDesignRaisedLightDarkButton��5s      .��6V   SAVE5u      5v      5w      5x      5y      � 5z   
   -	   5        $ SuccessIsVisible��5z   &   $y�	Collapsed=�6   $��1��6@   .+�5|        $��1��6"   $��20q�6/   $
��Auto��5}      $��8q�5      .��5�      � .+�5�      �� $��20(�6@   $��Light"�6N   $��WrapWithOverflow=�6a   $��30,100q�6�   $��Center=�6�   $��Center=�6�    # MaterialDesignDisplay1TextBlock#��[ 5�      ��5�       �� *SpecialMessage+5�      �� 5�      -
   5        $ CloseSuccess��5�      $��Bottom=�63   $��0,0,0,10q�6N    $  MaterialDesignRaisedAccentButton#��[ 5�      $	��100��5�      $5 /Resource name: MaterialDesignRaisedAccentButton��5�      .��6?   OK5�      5�      5�      5�      � 5�   
   -   5        $ DPromptIsVisible��5�      $��1��6*   .+�5�        $��1��6"   $��20q�6/   $
��Auto��5�      $��8q�5�      .��5�      � .+�5�      �� $��20(�6@   $��Light"�6N   $��WrapWithOverflow=�6a   $��30,30,30,50q�6�   $��Center=�6�   $��Center=�6�   #��[ 5�      ��5�       �� *SpecialMessage+5�      �� 5�      $��Center=�6!   $ 
Horizontal=�6>   $��Bottom=�6W   .+�5�      �� -   5        $ CancelDelete��5�   !   $��Bottom=�67   $��0,0,5,10q�6R     MaterialDesignRaisedButton#��[ 5�      $	��100��5�      $5 /Resource name: MaterialDesignRaisedAccentButton��5�      .��6?   NO5�      �� 5�      -   5        $ OkDelete��5�   !   $��Bottom=�63   $��5,0,0,10q�6N   #��[ 5�      $	��100��5�      $5 /Resource name: MaterialDesignRaisedAccentButton��5�      .��6?   YES5�      5�      5�      5�      5�      5�      5�      