   M S B A M L   `   `   `  ����W  SSmite_PnB_Layout, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3abd7126bc9c2f293clr-namespace:Smite_PnB_LayoutSmite_PnB_Layout  5   	   $    Smite_PnB_Layout.InGameOverlay   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Smite_PnB_Layout     5   	     � Title$  In Game Overlay��5   	   $	��900��6!   $
��1600��6.    � SizeToContent$ WidthAndHeight=�6R   $
��1600��6q   $	��900��6�    � 
ResizeMode$ NoResize=�6�    � WindowStyle$
 None=�6�    � Left$ 0��6�   
 � Top$ 0��6�   ��6;   Z�  � 	Resources 5	         
nameDesign �� 
TargetType �� Property	 �� Value��%�   &	        �� 5
   
   " ��6#   .�5      ��  � ��$K	 EResources/Penumbra-HalfSerif-Std-Semibold.ttf #Penumbra HalfSerif Std)�6+   6{   5      5      .��5      �� $	��900��6   $
��1600��6   
��� � �.+�5   
   �� -   5        
 ��Name$
 scoreboardCover��5      $	��900��6)   $
��1600��66   $
��Fill=�6C   ��5      �� 5       �� 	UriSource$$ Resources/Scoreboard_Cover.png?�6   6K   5      �� 5   
   -   5        $
 leftTeamScoreboardLogo��5       �� Left$	 425��60   
 �� Top$ 36��6B   $��36��6R   $��36��6]   $
��Fill=�6i   ��5      �� 5      $  ?�6   6-   5      �� 5   
   -   5        $
 rightTeamScoreboardLogo��5      $
 1138��61   $ 36��6D   $��36��6T   $��36��6_   $
��Fill=�6k   ��5      �� 5      $  ?�6   6-   5      �� 5   
   -   5         ��Name$ teamOneNameDisplay��5      $��Team One��6,   $��14(�6?   $��Center=�6M   $��Center=�6o   #��[  6�   ���	#FFECECEC$	��225��6�   $	 423��6�   $ 35��6�   $��40��6�   6  �� 5   
   -   5        $ teamTwoNameDisplay��5      $��Team Two��6,   $��14(�6?   $��Center=�6M   $��Center=�6o   #��[  6�   ���	#FFECECEC$	��225��6�   $��40��6�   $ 35��6�   $	 953��6�   6
  �� 5    
   -   5        $ teamTwoScore��5       $��0��6&   $��15(�62   $
��Left=�6@   #��[  6b   ���	#FFECECEC$��25��6�   $	 865��6�   $ 48��6�   �� 5!   
   -   5        $ teamOneScore��5!      $��0��6&   $��15(�62   $��Right=�6@   #��[  6c   ���	#FFECECEC$��25��6�   $	 710��6�   $ 48��6�   �� 5"   
   -   5        $ 
centerText��5"      $��Best of Three��6$   $��11(�6=   $��Center=�6K   #��[  6o   ���	#FFECECEC$	��130��6�   $	 735��6�   $ 50��6�   5#      5$      