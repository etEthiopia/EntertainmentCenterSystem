   M S B A M L   `   `   `  ���� 	  MyEnt!    DagiCaliburn.Views.CopyView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullY UMaterialDesignThemes.MahApps, Version=0.1.0.325, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes     � Title$  CopyView��$	��800��$	��300��.���  � ColumnDefinitions �� $��20 ��� $��* ��� $��20 � � RowDefinitions �� $��30 ��� $
��auto ��� $
��0.5* ��� $��* ��� $
��auto ��� $
��auto ��� $
��auto ��� $��30 �.+�� $��1��$��1�� �� $��* ��� $��5* ��� $��5* ��� $��* �.+�� $��1�� �� $��* ��� $��* �.+��� $��50��$��C:\images\aau.png��$��0��$
��auto���� $��1�� �� Orientation$ 
Horizontal=�.+��� $��20��$��20��$��E�� $   MaterialDesignActionDarkCheckBox#��[  $��10(� � ToolTip$&  MaterialDesignActionDarkCheckBox���� $��Center=�$��10,10,10,10q�   MaterialDesignBody1TextBlock#��[ 	Special'  !MaterialDesignThemes.Wpf.PopupBox     	StaysOpen . {��� *	IsChecked+ �� ElementName$ PopupEnabled��   PopupContent. � $	��300��$��8,8,8,8q� � 	Resources 	 �� 
TargetType  MaterialDesignCheckBox
 �� BasedOn �� Property �� Value  MaterialDesignTextBox)  #MaterialDesignThemes.Wpf.HintAssist��%�   '��       7  '�� .     7  �� "	 ��8
   .���  � ��$ 8,8,10,8q��� "	 ��8
   .���  �@ 
IsFloating . ��  � ��$ 8,8,8,8q� �� $��* ��� $
��Auto � �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� $��0��$��0��   MaterialDesignTitleTextBlock#��[ $��8,8,8,16q�	OPTIONS�� $��0��$��1��   Hint$ 	Setting 1��$	��200���� $��0��$��2��$ 	Setting 2��$	��400���� $��0��$��3��$ 	Setting 3��$	��600���� $��1��$��1��$��Bottom=��� $��1��$��2�� d� 	IsChecked$
 TrueY�$��Bottom=��� $��1��$��3��$��Bottom=��� $��0��$��4��$ 	Setting 4��   
IsFloating .  �� MaxDropDownHeight$	 200��$��8 8 8 8q�.@��� .��25%�� .��50%�� .��75%�� .��100%�� .��150%�� .��200%�� .��250%�� .��501%�� $��5��$��2��$ 
Horizontal=�$��Right=�.+��� $��_Save���� $��_Cancel������ "	 ��.���  � ��
 �  ��� $��10,0,0,0q�$��2�� �� $��* ��� $��* ��� $��* �.+��� $��Center=�$ 
Horizontal=�$��0��.+��� $��0,0,10,0q�#��[ Size :�� -    ��Name$ 	TotalSize��   MaterialDesignBody2TextBlock#��[ �� $��Center=�$ 
Horizontal=�$��1��.+��� $��0,0,10,0q�#��[ 	Items :�� -   $ 
TotalItems��#��[ �� $��Center=�$ 
Horizontal=�$��2��.+��� $��0,0,10,0q�#��[ 	Price :�� -   $ 
TotalPrice��#��[ �� #��[ Birr&   System.Windows.Controls.DataGrid  -     Name$ Clipp��$��2��$��1��$��0 8 0 8q�$
��Auto��   CanUserDeleteRows .     CanUserSortColumns .    CanUserAddRows .    -   $ Drives��$��3��$��1��$��0 8 0 8q�$
��Auto��   AutoGenerateColumns .   .   .  .   �� SelectedIndex �� *SelectedIndex+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$
 Name�  $	 Dir�� �� $ RootDirectory�  $ Space�� �� $ AvailableSpace�  $
 Size�� �� $ 	TotalSize��� $��1��$��4��  MaterialDesignRaisedButton#��[ $��70��$	��100��$��Right=�$8 2Resource name: MaterialDesignRaisedLightDarkButton��$��2��$
��Auto��$��
0,0,19.6,0q�.��	_Browse�� $��0,20,0,0q�$��Right=�$��5��$��1��$ 
Horizontal=�.+��� $��0,0,10,0q� # MaterialDesignRaisedLightButton#��[ $��70��$	��100��$4 .Resource name: MaterialDesignRaisedLightButton��.��Cancel�� -    ��Name$
 Send�� " MaterialDesignRaisedDarkButton#��[ $��70��$	��100��$8 2Resource name: MaterialDesignRaisedLightDarkButton��.��Send� -    �Name$ CopyProgress��$��1��$��6��$��Stretch=� � Value$ 25Q�$��10q�$��20��