   M S B A M L   `   `   `  ���� W  SSmite_PnB_Layout, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3abd7126bc9c2f293clr-namespace:Smite_PnB_LayoutSmite_PnB_Layout  !    Smite_PnB_Layout.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:Smite_PnB_Layout     -     � Title$  P/B Display��$	��900��$
��1600�����	#FF1A1A1A � 
ResizeMode$ NoResize=� � Icon$ PnB_Layout_Logo.ico�� � SizeToContent$ WidthAndHeight=� J� SnapsToDevicePixels . $
��1600��$	��900�� � WindowStyle$
 None=� � 	Resources    
nameDesign �� 
TargetType �� Property	 �� Value��%\   &	        �� " ��.���  � ��$7	 1Resources/LEMONMILK-Medium.otf #LEMON MILK Medium)�.���� -   
 ��Name$
 backgroundCanvas��$	��900��$
��1600��
���   �.+��� -    ��Name$ mainBackground��$
��Left=�$	��Top=�$
��Fill=�$
��1600��$	��900���� -   $ leftTeamDisplay��$	��650��$	��180��$
��Fill=� �� Left$ 0��
 �� Top$ 0���� -   $ rightTeamDisplay��$	��650��$	��180��$
��Fill=�$	 950��$ 0���� -    ��Name$ leftTeamScore��$��Center=�$��Center=�$��0��$	��100(�#��[  ���	#FFE8E8E8$	��180��$	��180��$	 460���� -   $ rightTeamScore��$��Center=�$��Center=�$��0��$	��100(�#��[  ���	#FFE8E8E8$	��180��$	��180��$	 960���� -   $ leftTeamPicks��$	��335��$	��720��$
��Fill=�$ 0��$	 180���� -	   $ rightTeamPicks��$	��335��$	��720��$
��Fill=�$
 1265��$	 180��"   Smite_PnB_Layout.PickDisplay  -
     Name$ leftTeamPick1��$	��335��$	��110��$	 180��$ 0��  -   $ leftTeamPick2��$	��335��$	��110��$	 290��$ 0��  -   $ leftTeamPick3��$	��335��$	��110��$	 400��$ 0��  -   $ leftTeamPick4��$	��335��$	��110��$	 510��$ 0��  -   $ leftTeamPick5��$	��335��$	��110��$	 620��$ 0��  -   $ rightTeamPick1��$	��335��$	��110��$	 180��$
 1265��  -   $ rightTeamPick2��$	��335��$	��110��$	 290��$
 1265��  -   $ rightTeamPick3��$	��335��$	��110��$	 400��$
 1265��  -   $ rightTeamPick4��$	��335��$	��110��$	 510��$
 1265��  -   $ rightTeamPick5��$	��335��$	��110��$	 620��$
 1265��!   Smite_PnB_Layout.BanDisplay  -     Name$ leftTeamBan1��$	��165��$��67��$	 735��$ 0��  -   $ leftTeamBan2��$	��165��$��67��$	 735��$ 67��  -   $ leftTeamBan3��$	��165��$��67��$	 735��$	 134��  -   $ leftTeamBan4��$	��165��$��67��$	 735��$	 201��  -   $ leftTeamBan5��$	��165��$��67��$	 735��$	 268��  -   $ rightTeamBan5��$	��165��$��67��$	 735��$
 1265��  -   $ rightTeamBan4��$	��165��$��67��$	 735��$
 1332��  -   $ rightTeamBan3��$	��165��$��67��$	 735��$
 1399��  -   $ rightTeamBan2��$	��165��$��67��$	 735��$
 1466��  -   $ rightTeamBan1��$	��165��$��67��$	 735��$
 1533��r� -    r�Name$ soundplayer��$
��Left=� r� LoadedBehavior$ Manual=�$	��100��$��0,0,0,0q�$	��Top=��� -   $
 leftTeamTopBans��$	��300��$	��200��$	 335��$	 700��.+�$   Smite_PnB_Layout.TopBanDisplay  -      Name$ leftTopBan1��$��60��$��60��$ 10��$ 60��  -!   $ leftTopBan2��$��60��$��60��$ 90��$ 60��  -"   $ leftTopBan3��$��60��$��60��$	 170��$ 60��  -#   $ leftTopBan4��$��60��$��60��$ 10��$	 130��  -$   $ leftTopBan5��$��60��$��60��$ 90��$	 130��  -%   $ leftTopBan6��$��60��$��60��$	 170��$	 130���� -&    ��Name$ leftTeamTopBansLabel��$
��Wrap=�  textBlockStyle#��[ $��18(�$��	Left Team�����	#FFE8E8E0$ 10��$ 25��$	��290��$��25���� -'   $ leftTopBansLabel��$
��Wrap=�#��[ $	��10 (�$��Top Bans Against�����	#FFE8E8E0$ 10��$ 10��$	��290��$��25���� -(   $
 rightTeamTopBans��$	��300��$	��200��$	 965��$	 700��.+�  -)   $ rightTopBan1��$��60��$��60��$	 230��$ 60��  -*   $ rightTopBan2��$��60��$��60��$	 150��$ 60��  -+   $ rightTopBan3��$��60��$��60��$ 70��$ 60��  -,   $ rightTopBan4��$��60��$��60��$	 230��$	 130��  --   $ rightTopBan5��$��60��$��60��$	 150��$	 130��  -.   $ rightTopBan6��$��60��$��60��$ 70��$	 130���� -/   $ rightTeamTopBansLabel��$
��Wrap=�#��[ $��18(�$��
Right Team�����	#FFE8E8E0$ 25��$	��290��$��25�� �� TextAlignment$ Right=��� -0   $ rightTopBansLabel��$
��Wrap=�#��[ $��10(�$��Top Bans Against�����	#FFE8E8E0$	��290��$ 10��$��25��$ Right=��� -1   $
 playerNamesGrid��$	��450��$	��140��$	 575��$	 756��.+��� -2   $ soloDisplay��#��[  $��Center=�$��Center=�$
��Solo��$��12(����	#FFE8E8E8$	��125��$ 162.5�����	#FFE8E8E8$	��2,0q�$��25���� -3   $ jungleDisplay��#��[  $��Center=�$��Center=�$��Jungle��$��12(����	#FFE8E8E8$	��125��$ 162.5��$ 28�����	#FFE8E8E8$	��2,0q�$��25���� -4   $ 
midDisplay��#��[  $��Center=�$��Center=�$	��Mid��$	��12 (����	#FFE8E8E8$	��125��$ 162.5��$ 56�����	#FFE8E8E8$	��2,0q�$��25��$	��Top=��� -5   $ supportDisplay��#��[  $��Center=�$��Center=�$��Support��$	��12 (����	#FFE8E8E8$	��125��$ 162.5��$ 84�����	#FFE8E8E8$	��2,0q�$��25��$	��Top=��� -6   $ carryDisplay��#��[  $��Center=�$��Center=�$��Carry��$	��12 (����	#FFE8E8E8$	��125��$ 162.5��$	 112�����	#FFE8E8E8$	��2,0q�$��25��$	��Top=��� -7   $ leftTeamSolo��#��[  $��Right=�$��Center=�$��playerName1��$��12(����	#FFE8E8E8$��25��$	��Top=�$	��160���� -8   $ leftTeamJung��#��[  $��Right=�$��Center=�$��playerName1��$��12(����	#FFE8E8E8$��25��$	��Top=�$ 28��$	��160���� -9   $ leftTeamMid��#��[  $��Right=�$��Center=�$��playerName1��$��12(����	#FFE8E8E8$��25��$	��Top=�$ 56��$	��160���� -:   $ leftTeamSupp��#��[  $��Right=�$��Center=�$��playerName1��$��12(����	#FFE8E8E8$��25��$	��Top=�$ 84��$	��160���� -;   $ leftTeamCarry��#��[  $��Right=�$��Center=�$��playerName1��$��12(����	#FFE8E8E8$��25��$	��Top=�$	 112��$	��160���� -<   $ rightTeamSolo��#��[  $
��Left=�$��Center=�$��playerName1��$��12(����	#FFE8E8E8$��25��$	��Top=�$	 289��$	��160���� -=   $ rightTeamJung��#��[  $
��Left=�$��Center=�$��playerName1��$��12(����	#FFE8E8E8$��25��$	��Top=�$	 289��$ 28��$	��160���� ->   $ rightTeamMid��#��[  $
��Left=�$��Center=�$��playerName1��$��12(����	#FFE8E8E8$��25��$	��Top=�$	 289��$ 56��$	��160���� -?   $ rightTeamSupp��#��[  $
��Left=�$��Center=�$��playerName1��$��12(����	#FFE8E8E8$��25��$	��Top=�$	 289��$ 84��$	��160���� -@   $ rightTeamCarry��#��[  $
��Left=�$��Center=�$��playerName1��$��12(����	#FFE8E8E8$��25��$	��Top=�$	 289��$	 112��$	��160��