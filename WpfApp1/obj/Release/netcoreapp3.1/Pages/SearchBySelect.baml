   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp1.PagesWpfApp1.Pages  "    WpfApp1.Pages.SearchBySelect   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:WpfApp1.Pages     b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     N� Title$  SearchBySelect�� N� WindowTitle$# CarGallery - search by select��   MaterialDesignBody#���   $��Medium"�$��14(� N� 
FontFamily :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .���  � ColumnDefinitions �� $��20 ��� $
��auto ��� $��* ��� $��20 � � RowDefinitions �� $
��auto ��� $��* �.+��� $
��auto��$��1,1,1,1q� �� CornerRadius$ 8,8,8,8�����	#FF535151$��1��$��2��$��0,10,0,0q�._��  �� $��10 ��� $��* ��� $��10 � �� $��10 ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* ��� $��10 �.+��� -    ��Name$ 
txtBoxFind�� $  MaterialDesignHeadline5TextBlock#���  
�������$��1��$��1���� -   $��	cmbBrands��$��1��$��2�� �� SelectedIndex$ 0��$��Center=�
�������$
��10,0q�$��5q�$
��auto������ .R��� ���� *	BrandName+�� -   $��cmbModel��$��1��$��3��$ 0��$��Center=�
�������$
��10,0q�$��5q�$
��auto������ .R��� ���� *Model+�� -   $��cmbYear��$��1��$��4��$ 0��$��Center=�
�������$
��10,0q�$��5q�$
��auto������ .R��� ���� *Year+� $��5��$��1��$��Right=� �� $
��auto ��� $
��auto �.+��� -   $��0��$	��Go!��$
��25,0q�$	��150���� -   $��1��$
��Back��$	��150���� $
��auto��$��1,1,1,1q�$ 8,8,8,8�����	#FF535151$��2��$��1��$��2��$��	0,10,0,10q�._�� $��Center=� �� $��10 ��� $��* ��� $��10 � �� $��10 ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* ��� $��10 �.+��� -   $��carInfoMainText�� $  MaterialDesignHeadline6TextBlock#���  
�������$��1��$��1���� $��1��$��2��$��1��
�������$ 8,8,8,8��$��0,5,0,5q�� $��1��$��3��$��5,0,0,0q� �� $
��auto ��� $
��auto �.+�� $��1�� �� $
��auto ��� $
��auto � �� $
��auto ��� $
��auto �.+��� -	   $��image1��$��2��$	��334��$��40,0,5,5q�$
��Left=�$
��Fill=�$	��Top=�$	��446���� -
   $��btnSave��$��1��$��0��$y�	Collapsed=�$
��Save��$��5q�$	��150���� -   $��btnFile��$��1��$��1��$y�	Collapsed=�$��Choose file��$��5q�$	��150���� .+�� -   $��generalInfoGrid�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto � �� $��* ��� $��* �.+��� $
��auto�����#00A5BA$
��10,0q�$ 10,10,10,10��$��2��._���   MaterialDesignTextBlock#���  
�������$��General��$��Center=���    MaterialDesignBody1TextBlock#���  
�������$��1��$��0��$��Price:��$��0,0,10,0q��� -   $��carInfoPrice��#���  
�������$��1��$��1���� #���  $��2��$��0��
�������$��	Car body:��$��0,0,10,0q��� -   $��carInfoBodyType��#���  
�������$��2��$��1���� #���  $��3��$��0��
�������$��Number of seats:��$��0,0,10,0q��� -   $��carInfoSeats��#���  
�������$��3��$��1���� #���  $��4��$��0��
�������$��First year of production:��$��0,0,10,0q��� -   $��carInfoFirstYear��#���  
�������$��4��$��1��� -   $��engineInfoGrid�� �� $
��auto ��� $
��auto ��� $
��auto � �� $��* ��� $��* �.+��� $
��auto�����#00A5BA$
��10,0q�$ 10,10,10,10��$��2��._��� #���  
�������$��Drive��$��Center=��� #���  $��1��$��0��
�������$��Drive:��$��0,0,10,0q��� -   $��carInfoDrive��#���  
�������$��1��$��1���� #���  $��2��$��0��
�������$��Engine type:��$��0,0,10,0q��� -   $��carInfoEngine��#���  
�������$��2��$��1��� -   $��performanceInfoGrid�� �� $
��auto ��� $
��auto ��� $
��auto � �� $��* ��� $��* �.+��� $
��auto�����#00A5BA$
��10,0q�$ 10,10,10,10��$��2��._��� #���  
�������$��Performance��$��Center=��� #���  $��1��$��0��
�������$��
Top speed:��$��0,0,10,0q��� -   $��carInfoSpeed��#���  
�������$��1��$��1���� #���  $��2��$��0��
�������$��Acceleration 0-100 km/h:��$��0,0,10,0q��� -   $��
carInfoAcc��#���  
�������$��2��$��1��� -   $��brandInfoGrid�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto � �� $��* ��� $��* �.+��� $
��auto�����#00A5BA$
��10,0q�$ 10,10,10,10��$��2��._��� #���  
�������$��Brand��$��Center=��� #���  $��1��$��0��
�������$��Brand name:��$��0,0,10,0q��� -   $��brandInfoName��#���  
�������$��1��$��1���� #���  $��2��$��0��
�������$��Country:��$��0,0,10,0q��� -   $��brandInfoCounty��#���  
�������$��2��$��1���� #���  $��3��$��0��
�������$��Foundation year:��$��0,0,10,0q��� -   $��brandInfoFoundationYear��#���  
�������$��3��$��1���� #���  $��4��$��0��
�������$��Company value��$��0,0,10,0q��� -   $��brandInfoValue��#���  
�������$��4��$��1��