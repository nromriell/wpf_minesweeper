   M S B A M L   `   `   `  ���� L  HMinesweeperVisual, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MinesweeperVisualMinesweeperVisual  "    MinesweeperVisual.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:MinesweeperVisual       � Title$!  Nathan Romriell Minesweeper��$	��500��$	��525��.���� -   $��mainArea��$��Stretch=�$	��Top=�.+��� 
���   �.+�A�  A� Rows$ 1��$��Stretch=�$��Center=�
���   �.+��� -   $��
menubutton��$
��Left=�$��Center=�$��30��$��60��.��Menu�� -   $��difficultylabel��
�������$��Center=�$��Center=�$��15(�
Difficulty�� -   $��highscorelabel��
�������$��Center=�$��Center=�$��15(�High Score: None�� -   $��gamemenu�� K� ZIndex$ 999999��$y�Hidden=�.+�A�  A� Columns$ 1��$ 3��$	��150��$	��100��
�������$
��Left=�.+��� -   $��newEGameButton��
�������.��New Easy Game�� -   $��newMGameButton��
�������.��New Medium Game�� -   $��newHGameButton��
�������.��New Hard Game�� -	   $��topbar��$��Stretch=�$��50��.+�A� $ 1��$��Stretch=�$��Center=�
���   �.+��� -
   $��minesleftlabel��$��Stretch=�$��Center=�$��40(� �� TextAlignment$ Center=�
���   �
���  ��10�� -   $��
gamebutton��$��Center=�$��Center=�$��50��.��Game Button�� -   $��	timelabel��$��Stretch=�$��Center=�$��40(�$ Center=�
���   �
���  ��0