   M S B A M L   `   `   `  ���� W  SSmite_PnB_Layout, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3abd7126bc9c2f293clr-namespace:Smite_PnB_LayoutSmite_PnB_Layout  $    Smite_PnB_Layout.InGameOverlay   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:Smite_PnB_Layout       � Title$  In Game Overlay��$	��900��$
��1600�� � SizeToContent$ WidthAndHeight=�$
��1600��$	��900�� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � Left$ 0��
 � Top$ 0����Z�  � 	Resources    
nameDesign �� 
TargetType �� Property	 �� Value��%p   &	        �� " ��.���  � ��$K	 EResources/Penumbra-HalfSerif-Std-Semibold.ttf #Penumbra HalfSerif Std)�.���� $	��900��$
��1600��
��� � �.+��� -   
 ��Name$
 scoreboardCover��$	��900��$
��1600��$
��Fill=�����  �� 	UriSource$$ Resources/Scoreboard_Cover.png?��� -   $
 leftTeamScoreboardLogo�� �� Left$	 425��
 �� Top$ 36��$��36��$��36��$
��Fill=����� $  ?��� -   $
 rightTeamScoreboardLogo��$
 1138��$ 36��$��36��$��36��$
��Fill=����� $  ?��� -    ��Name$ teamOneNameDisplay��$��Team One��$��14(�$��Center=�$��Center=�#��[  ���	#FFECECEC$	��225��$	 423��$ 35��$��40���� -   $ teamTwoNameDisplay��$��Team Two��$��14(�$��Center=�$��Center=�#��[  ���	#FFECECEC$	��225��$��40��$ 35��$	 953���� -   $ teamTwoScore��$��0��$��15(�$
��Left=�#��[  ���	#FFECECEC$��25��$	 865��$ 48���� -   $ teamOneScore��$��0��$��15(�$��Right=�#��[  ���	#FFECECEC$��25��$	 710��$ 48���� -   $ 
centerText��$��Best of Three��$��11(�$��Center=�#��[  ���	#FFECECEC$	��130��$	 735��$ 50��