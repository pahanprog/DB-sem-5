   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp1.PagesWpfApp1.Pages  /clr-namespace:WpfApp1.ModelsWpfApp1.Models  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem     WpfApp1.Pages.AdminMenu   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:WpfApp1.Pages     /local1clr-namespace:WpfApp1.Models     8codeg&clr-namespace:System;assembly=mscorlib     -     N� Title$  	AdminMenu�� N� WindowTitle$ CarGallery - admin menu�� � 	Resources  S� 
MethodName S� 
ObjectTypeU QSystem.Runtime, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a  System.Enum   UserEnumProvider S� MethodParameters M� Type   WpfApp1.Models.Status   WpfApp1.Models.CarData  BrandProvider  DriveProvider   WpfApp1.Models.BrandData  CountyProvider��%�   &	        &	 (     &	 B     &	 \     S� $ 	GetValues��"   M� "  S� $ 	GetBrands��"  S� $ 	GetDrives��"  S� $ GetCountries��"  .���  � ColumnDefinitions �� $��10 ��� $��* ��� $��10 � � RowDefinitions �� $��10 ��� $
��auto ��� $��* ��� $��10 �   CarActionsTemplate  UserActionsTemplate  BrandActionsTemplate  CountyActionsTemplate��%  &	       &	 :     &	 u     &	 �     �� .R��� $��CarDeleteBtn��-   $��1��$��Delete���� .R��� $��UserDeleteBtn��-   $��1��$��Delete���� .R��� $��BrandDeleteBtn��-   $��1��$��Delete���� .R��� $��CountryDeleteBtn��-   $��1��$��Delete��.+��� -   $��BackBtn��$
��Back��$��1��$��1��$��Right=�$	��150��$	��150��$��0,0,0,10q��� $��2��$��1��$��Visible=�$��0,0,10,0q�.���� .+�� $��10,0,0,0q� �� $��* ��� $
��auto �.+��� $��
Car models��
������� $  MaterialDesignHeadline5TextBlock#���  �� 	 �� Padding$	 5q�$��2��._��  �� $
��auto ��� $
��auto �.+��� -   
 ��Name$
 
PlusCarBtn��$��Center=�$��0��$��5q�$
��10,0q�.���� $��15��$��15��$��../img/plus.png���� -   $
 MinusCarBtn��$��Center=�$��1��$��5q�.���� $��15��$��15��$��../img/minus.png��&   System.Windows.Controls.DataGrid  -	     Name$ CarDataGrid��   CanUserResizeColumns$ False��$
��Auto=�$y�	Collapsed=�   AutoGenerateColumns$ False��   CanUserReorderColumns$ False��   CanUserDeleteRows$ False��   CanUserAddRows$ False��   CanUserSortColumns$ False��   CanUserResizeRows$ False�����	#FF535151   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly$
 True��   Header$ Id��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+4	  .System.Windows.Controls.DataGridTemplateColumn	  $ 
Brand name�� 	  CellTemplate �� .R���  �� SelectedValuePath$	 Key�� �� DisplayMemberPath$ Value������  �� Source# [  �� SelectedValue �� *	BrandId+ �� UpdateSourceTrigger$ PropertyChanged=�  $
 Name�� �� *Name+  $
 Body�� �� *	CarBody+  $
 Year�� �� *Year+  $ Seats�� �� *Seats+	  $ Drive�� �� .R��� $	 Key��$ Value������ # [  �� *	DriveId+$ PropertyChanged=�  $ Engine�� �� *
EngineType+  $ 	Top speed�� �� *
TopSpeed+  $ 0-100�� �� *Acceleration+  $ Price�� �� *Price+	  $ Image�� �� .R��� $��carImage��$��2��$��60��$��80��$
��Fill=����� *Image+ 	  CellEditingTemplate �� .R��  �Name$ popImage�� � 	StaysOpen$
 True��$��Bottom=�$
��True��$	��400��$	��300��  � AllowsTransparency$
  True��.���� $��Center=�$��Center=����	#FF535151.+��� $��imagePop��$	��200��$	��280��$
��Fill=����� *Image+�� $��fileBtn��-
   $��10(�$��3q�$��Choose file��	  $
 True��$ Actions��# [ �� -   $��	AddCarBtn��$y�	Collapsed=�$	��Add��$	��150��$��Right=�$��0,10,5,0q��� -   $��
CarEditBtn��$y�	Collapsed=�$��Save changes��$	��150��$��Right=�$��0,10,5,0q�� $��	10,10,0,0q� �� $��* ��� $
��auto �.+��� $��Users��
�������#���  �� $	 5q�$��2��._��  �� $
��auto ��� $
��auto �.+��� -   $
 PlusUserBtn��$��Center=�$��0��$��5q�$
��10,0q�.���� $��15��$��15��$��../img/plus.png���� -   $
 MinusUserBtn��$��Center=�$��1��$��5q�.���� $��15��$��15��$��../img/minus.png��  -   $ UserDataGrid��$y�	Collapsed=�$ False��$ False��$ False��$ False��$ False��$ False�����	#FF535151����    $
 True��$ Id�� �� *Id+  $
 True��$ Username�� �� *
Username+	  $ Status�� �� .R��� ���� # [  ! �� SelectedIndex! �� *
StatusId+$ PropertyChanged=�4
  .System.Windows.Controls.DataGridCheckBoxColumn
  $ Blocked�� �� *	Blocked+	  $
 True��$ Actions��# [ �� -   $��UserEditBtn��$y�	Collapsed=�$��Save changes��$	��150��$��Right=�$��0,10,5,0q�� $��	10,10,0,0q� �� $��* ��� $
��auto �.+��� $��Brands��
�������#���  �� $	 5q�$��2��._��  �� $
��auto ��� $
��auto �.+��� -   $
 BrandPlusBtn��$��Center=�$��0��$��5q�$
��10,0q�.���� $��15��$��15��$��../img/plus.png���� -   $
 BrandMinusBtn��$��Center=�$��1��$��5q�.���� $��15��$��15��$��../img/minus.png��  -   $ BrandDataGrid��$y�	Collapsed=�$ False��$ False��$ False��$ False��$ False��$ False�����	#FF535151   $
 True��$ Id�� �� *Id+  $ 
Brand name�� �� *	BrandName+	  $ Country�� �� .R��� $	 Key��$ Value������ # [  �� *	CountryId+$ PropertyChanged=�  $ Foundation year�� �� *FoundationYear+  $ Company value�� �� *CompanyValue+	  $
 True��$ Actions��# [ �� -   $��AddBrandBtn��$y�	Collapsed=�$	��Add��$	��150��$��Right=�$��0,10,5,0q��� -   $��BrandEditBtn��$y�	Collapsed=�$��Save changes��$��Right=�$��0,10,5,0q�� $��	10,10,0,0q� �� $��* ��� $
��auto �.+��� $��	Countries��
�������#���  �� $	 5q�$��2��._��  �� $
��auto ��� $
��auto �.+��� -   $
 PlusCountryBtn��$��Center=�$��0��$��5q�$
��10,0q�.���� $��15��$��15��$��../img/plus.png���� -   $
 MinusCountryBtn��$��Center=�$��1��$��5q�.���� $��15��$��15��$��../img/minus.png��  -   $ CountryDataGrid��$y�	Collapsed=�$ False��$ False��$ False��$ False��$ False��$ False�����	#FF535151   $
 True��$ Id�� �� *Id+  $
 Name�� �� *Name+	  $
 True��$ Actions��# [ �� -   $��AddCountryBtn��$y�	Collapsed=�$	��Add��$	��150��$��Right=�$��0,10,5,0q��� -   $��CountryEditBtn��$y�	Collapsed=�$��Save changes��$��Right=�$��0,10,5,0q�