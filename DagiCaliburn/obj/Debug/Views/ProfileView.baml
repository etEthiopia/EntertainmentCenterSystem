   M S B A M L   `   `   `  ����@  <MyEnt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DagiCaliburn.ViewsDagiCaliburn.Views  5      $    DagiCaliburn.Views.ProfileView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:DagiCaliburn.Views     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullY UMaterialDesignThemes.MahApps, Version=0.1.0.325, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5      .��5      � .+�5   
   � -   5          �Name$  EditProfileIsVisible��5      .+�5   
   #  MaterialDesignThemes.Wpf.Card  +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth4=�5      $��32q�5      $��50q�6   .��5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5       A� $��	0,10,0,20q�6   $��50��6)   $��50��65   ��5!      �� 5"       �� ImageSource 5#      �� 5$      -   5        Picture�� �� DecodePixelHeight$	 150��5$   <    �� DecodePixelWidth$	 150��6T    �� 	UriSource$% ../DatasetSafariReadingList.ico?�6k   5&      5(      �� 5)      -   5         ��Name$
 Name��5+      $��1��5)      $��
50,10,50,0q�6#   $��Stretch=�67   $��1��6U   $��15(�6f   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Ent-center Name��5*          PrimaryHueMidForegroundBrush	   
Foreground#	 �   5,   &    % !MaterialDesignFloatingHintTextBox#��[ 5-      �� 5.      -   5        $ Email��50      $��2��5.      $��
50,10,50,0q�6#   $��Stretch=�67   $��1��6U   $��15(�6f   $ Email��5/      #	 �   51   &   #��[ 52      �� 53      -   5        $ Password��53   +   $��3��6   $��50,20,50,10q�6=   $��Stretch=�6R   $��1��6p   $��15(�6�   $ Password��54      �� 58      -   5        $ ConfirmPassword��58      $��4��68   $��
50,0,50,10q�6E   $��Stretch=�6Y   $��1��6w   $��15(�6�   $ Confrim Password��59      � 5<      -   5        $  ErrorIsVisible��5<      $��5��63   $��
50,0,50,10q�6@   .+�5>      �� $��Light"�6$   $��WrapWithOverflow=�67   $��15(�6W   
���  �� # MaterialDesignDisplay1TextBlock#��[ 5?      ��6>   �� *Error+5A      5C      �� 5D      $��6��6!   
 �� Orientation$
 
Horizontal=�6.   $��0,20,0,0q�6G   $��Right=�6Y   .+�5F      �� -   5         ��Name$ Cancel��5F      $
��Left=�6*    # MaterialDesignRaisedLightButton#��[ 5G       � ToolTip$4 .Resource name: MaterialDesignRaisedLightButton��5I      .��6>   CANCEL5K      �� 5L      -	   5        $
 Save��5O      $��Right=�5L      $��5q�65    " MaterialDesignRaisedDarkButton#��[ 5M      $��80��5N      $8 2Resource name: MaterialDesignRaisedLightDarkButton��5P      .��6V   SAVE5R      5S      5T      5U      5V      � 5W   
   -
   5        $  ProfileIsVisible��5W      .+�5X      �� $
��Auto=�6   .��5Y        $ Depth3=�5Z      $��32q�5[      $��25q�6   .��5^      �  5_      �� 5`      $
��Auto �6(   �� 5a      $
��Auto �6(   �� 5b      $
��Auto �6(   �� 5c      $
��Auto �6(   �� 5d      $
��Auto �6(   �� 5e      $
��Auto �6(   .+�5h      A� $��0,0,0,25q�6   $	��125��60   $	��125��6=   ��5i      �� 5j       5k   "   �� 5l   &   -   5        PictureS��$	 100��5l   E   $	 100��6]   $% ../DatasetSafariReadingList.ico?�6t   5n      5p      � 5q      -   5        $  ProfileCreatedIsVisible��5q   (   $��1��6    5r      �� 5s      $
��Auto �6,   �� 5t      $
��Auto �6,   �� 5u      $
��Auto �6,   �� 5v      $
��Auto �6,   �� 5w      $
��Auto �6,   �� 5x      $
��Auto �6,   .+�5z      �� $��	25,5,25,0q�6%   $��Center=�68   $
 Vertical=�6U   $��1��6l   .+�5{      �� $��Light"�6(   $��Center=�6;    % !MaterialDesignSubheadingTextBlock#��[ 5|      Name5~      �� 5   "   $��Light"�6,   $��WrapWithOverflow=�6?   $��25(�6_   $��Center=�6m   #��[ 5�      ��6>   �� *PName+5�      5�      �� 5�      $��
25,25,25,0q�6)   $��Center=�6=   $
 Vertical=�6Z   $��2��6q   .+�5�   "   �� $��Light"�6,   $��Center=�6?   #��[ 5�      Email5�      �� 5�   "   $��Light"�6,   $��WrapWithOverflow=�6?   $��20(�6_   $��Center=�6m   #��[ 5�      ��6>   �� *PEmail+5�      5�      �� 5�      $��5��6%   $
 
Horizontal=�62   $��25,50,10,25q�6K   $��Right=�6`   .+�5�   "   �� -   5        $ EditProfile��5�   V   $��Right=�6)   $��0,0,0,5q�6E    0 ,MaterialDesignFloatingActionMiniAccentButton#��[ 5�   %   $2 ,MaterialDesignFloatingActionMiniAccentButton��5�   %   .��5�   &   '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$
 Edit=�5�   )   $��24��5�   )   $��24��65   5�   #   5�      5�      � 5�      -   5        $  ProfileNewIsVisible��5�   (   $��1��6   .+�5�      � $	��250��5�      $��75��6   .+�5�      �� -   5        $ CreateProfile��5�      $��70��5�   %   $��20(�61     MaterialDesignRaisedButton#��[ 5�      3  -MaterialDesignThemes.Wpf.ButtonProgressAssist   Value$ -1Q�5�         IsIndicatorVisible .    IsIndeterminate . $��CREATE PROFILE��5�      $	��2,0q�5�      5�      5�      5�      5�      5�      5�      5�      5�      