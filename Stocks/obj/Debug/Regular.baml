   M S B A M L   `   `   `  ����A  =Stocks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:StocksStocks  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5          Stocks.Regular   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:Stocks     5      X TXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .s&clr-namespace:System;assembly=mscorlib  5      .��5      � .+�5   
   ]� .+�5      ]� $	��Top=�6   .+�5      �� $
��Left=�6     �� Orientation$  
Horizontal=�63   .+�5      �� $��Center=�6:   ��6   �� *ChkCmd+6:   �� 5      
���  ��$��Lagging Span��6-   $��14(�6D   �� 5      $��Center=�6:   ��6   �� *ChkCmd+6:   �� 5      
���� ��$��Conversion Line��60   $��14(�6J   �� 5      $��Center=�6:   ��6   �� *ChkCmd+6:   �� 5      
����  �$��	Base Line��6.   $��14(�6B   �� 5      $��Center=�6:   ��6   �� *ChkCmd+6:   �� 5      
��� ���$��Leading Span A��60   $��14(�6I   �� 5      $��Center=�6:   ��6   �� *ChkCmd+6:   �� 5      
���   �$��Leading Span B��6/   $��14(�6H   5      �� 5      $��Right=�6   $  
Horizontal=�64   $��Right=�6M   $��
2, 2, 2, 2q�6i   .+�5      �� $��Begin Date:��6   �� 5      $	��100��6k   $��20��6w   ��6   �� *	BeginDate+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.��6�   ��5      1&clr-namespace:System;assembly=mscorlibSystem 5   9   �� 6   0sys&clr-namespace:System;assembly=mscorlib  69    �� 
TargetType" ��6$    �� 	Resources 5   "      CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%�   &	        !� 5    &   $
 Left=�6I   $ Center=�6[   $
 None=�6o    5!   *   �� 5"   .   $��
yyyy-mm-dd��64   
�������6I   5$   '   5%   #   	 �� Triggers	 5&   "   P� 5'   &   
 P� Property
 � �� �� Empty P� Value# Z 6>   . �5(   *   ��  �� Property � �� �� Value# [  6G   5)   '   P� 5*   &   
 � �� 6>   Z� . �5+   *   ��  � ��# [  6G   5,   '   5.      .��5/   )   50      �� 51      $��	End Date:��6   �� 52      $	��100��6i   $��20��6u   ��6   �� *	EndDate+$ TwoWay=�$ PropertyChanged=�.��6�   ��53      1&clr-namespace:System;assembly=mscorlibSystem 54   9   �� 6   0sys&clr-namespace:System;assembly=mscorlib  69   " ��6$    55   "   ��%�   &	        !� 56   &   $
 Left=�6I   $ Center=�6[   $
 None=�6o    57   *   �� 58   .   $��
yyyy-mm-dd��64   
�������6I   5:   '   5;   #   	 5<   "   P� 5=   &   
 � ��# Z 6>   . �5>   *   ��  � ��# [  6G   5?   '   P� 5@   &   
 � �� 6>   Z� . �5A   *   ��  � ��# [  6G   5B   '   5D      .��5E   )   5F      �� 5G      $��Search: ��6   �� 5H      $	��100��6l   $��20��6x   ��6   �� *
SearchText+$ TwoWay=�$ PropertyChanged=�.��6�   ��5I      1&clr-namespace:System;assembly=mscorlibSystem 5J   9   �� 6   0sys&clr-namespace:System;assembly=mscorlib  69   " ��6$    5K   "   ��%�   &	        !� 5L   &   $
 Left=�6I   $ Center=�6[   $
 None=�6o    5M   *   �� 5N   .   $��Search��64   
�������6E   5P   '   5Q   #   	 5R   "   P� 5S   &   
 � ��# Z 6>   . �5T   *   ��  � ��# [  6G   5U   '   P� 5V   &   
 � �� 6>   Z� . �5W   *   ��  � ��# [  6G   5X   '   P� 5Y   &   
 �@J�IsKeyboardFocused . . �5Z   *   ��  � ��
 �����6G   5[   '   5]      .��5^   )   5_      �� 5`      $��Advanced Search��6   ��67   �� *DataContext.ChangeToAdvanced+ �� RelativeSource ��  �� AncestorType   Stocks.MainWindow"  5a      5b         Stocks.Chart  5c      $��Bottom=�6   $
��1200��6O   $	��600��6\      Data	 62   �� *ChartPoints+
6\   5d      5e      5f      