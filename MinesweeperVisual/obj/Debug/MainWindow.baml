   M S B A M L   `   `   `  ����L  HMinesweeperVisual, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:MinesweeperVisualGitMinesweeperVisualGit  5   	   %    MinesweeperVisualGit.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:MinesweeperVisualGit     5   	     � Title$!  Nathan Romriell Minesweeper��5   	   $	��500��6-   $	��525��6:   .��5	      �� .+�5
   
   �� -   5        $��resultsCanvas��5
       K� ZIndex$ 99999��6&   $��Center=�6;   $��Center=�6X   .+�5      �� -   5        $��resultsLabel��5   7   $��24(�6   
���  ��
You Lose6U   5      �� 5   
   -   5        $��mainArea��5      $��Stretch=�6%   $	��Top=�6C   .+�5      �� 
���   �.+�5      A�  A� Rows$ 1��6   $��Stretch=�6#   $��Center=�6A   
���   �.+�5      �� -   5        $��
menubutton��5   O   $
��Left=�6   $��Center=�64   $��30��6a   $��60��6m   .��6x   Menu6~   �� 5      -   5        $��difficultylabel��5   u   
�������$��Center=�6/   $��Center=�6J   $��15(�6g   
Difficulty6�   5      5      �� 5   
   -   5        $��gamemenu��5   ;   $ 999999��6   $y�Hidden=�6'   .+�5      A�  A� Columns$ 1��6   $ 3��6&   $	��150��6/   $	��133��6;   
�������$
��Left=�6Z   .+�5      �� -   5        $��newEGameButton��5      
�������.��6F   New Easy Game6U   �� 5      -   5        $��newMGameButton��5      
�������.��6F   New Medium Game6W   �� 5      -	   5        $��newHGameButton��5      
�������.��6F   New Hard Game6U   5      5      �� 5   
   -
   5        $��topbar��5      $��Stretch=�6#   $��50��6A   .+�5      A� $ 1��6   $��Stretch=�6#   $��Center=�6A   
���   �.+�5      �� -   5        $��minesleftlabel��5   U   $��Stretch=�6   $��Center=�6:   $��40(�6k    �� TextAlignment$ Center=�6y   
���   �
���  ��106�   �� 5      -   5        $��
gamebutton��5   Q   $��Center=�6   $��Center=�66   $��50��6c   .��6o   Game Button6|   �� 5      -   5        $��	timelabel��5   Y   $��Stretch=�6    $��Center=�6>   $��40(�6j   $ Center=�6x   
���   �
���  ��06�   5       5!      5"      5#      5$      