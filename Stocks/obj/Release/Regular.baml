   M S B A M L   `   `   `  ���� A  =Stocks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:StocksStocks  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem     Stocks.Regular   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     &localclr-namespace:Stocks     X TXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .s&clr-namespace:System;assembly=mscorlib  .��� .+�]� .+�]� $	��Top=�.+��� $
��Left=�  �� Orientation$  
Horizontal=�.+��� 
���  ��$��Graph��$��14(��� 
���� ��$��SMA20��$��14(��� 
����  �$��SMA50��$��14(��� 
��� ���$��SMA100��$��14(��� 
���   �$��SMA200��$��14(��� $��Right=�$  
Horizontal=�$��Right=�$��
2, 2, 2, 2q�.+��� $��Begin Date:���� $	��100��$��20������ *	BeginDate+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.����1&clr-namespace:System;assembly=mscorlibSystem �� 0sys&clr-namespace:System;assembly=mscorlib   �� 
TargetType" �� �� 	Resources    CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%W   &	        !� $
 Left=�$ Center=�$
 None=� �� $��
yyyy-mm-dd��
�������	 �� Triggers	 P� 
 P� Property
 � �� �� Empty P� Value# Z . ���  �� Property � �� �� Value# [  P� 
 � �� Z� . ���  � ��# [  .���� $��	End Date:���� $	��100��$��20������ *	EndDate+$ TwoWay=�$ PropertyChanged=�.����1&clr-namespace:System;assembly=mscorlibSystem �� 0sys&clr-namespace:System;assembly=mscorlib  " �� ��%W   &	        !� $
 Left=�$ Center=�$
 None=� �� $��
yyyy-mm-dd��
�������	 P� 
 � ��# Z . ���  � ��# [  P� 
 � �� Z� . ���  � ��# [  .���� $��Search: ���� $	��100��$��20������ *
SearchText+$ TwoWay=�$ PropertyChanged=�.����1&clr-namespace:System;assembly=mscorlibSystem �� 0sys&clr-namespace:System;assembly=mscorlib  " �� ��%S   &	        !� $
 Left=�$ Center=�$
 None=� �� $��Search��
�������	 P� 
 � ��# Z . ���  � ��# [  P� 
 � �� Z� . ���  � ��# [  P� 
 �@J�IsKeyboardFocused . . ���  � ��
 �����.���� $��Advanced Search������ *DataContext.ChangeToAdvanced+ �� RelativeSource ��  �� AncestorType   Stocks.MainWindow"     Stocks.Chart  $��Bottom=�$
��1200��$	��600��   Data	 �� *ChartPoints+
