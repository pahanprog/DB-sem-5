   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp1.PagesWpfApp1.Pages      WpfApp1.Pages.CarOnfoById   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:WpfApp1.Pages     -     N� Title$  CarOnfoById�� N� WindowTitle$ CarGallery - car info��.���  � ColumnDefinitions �� $��10 ��� $��* ��� $��10 � � RowDefinitions �� $��10 ��� $��* ��� $��10 �.+�� $��1��$��1��$��Center=�$��Center=� �� $
��auto ��� $
��auto � �� $
��auto ��� $
��auto �.+��� -    ��Name$ CarName�� $   MaterialDesignHeadline6TextBlock#���   
�������$��2��$
��Left=�$��Center=��� -   $��backBtn��$��0��$��1��$	��150��$��Right=�$��10q�$
��Back��� $��1��$��1�� �� $
��auto ��� $
��auto � �� $
��auto ��� $
��auto �.+��� -   $��image1��$��2��$	��334��$��40,0,5,5q�$
��Left=�$
��Fill=�$	��Top=�$	��446���� -   $��btnSave��$��1��$��0��$y�	Collapsed=�$
��Save��$��5q�$	��150���� -   $��btnFile��$��1��$��1��$y�	Collapsed=�$��Choose file��$��5q�$	��150���� $��1��.+�� -   $��generalInfoGrid�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto � �� $��* ��� $��* �.+��� $
��auto�����#00A5BA$
��10,0q� �� CornerRadius$ 10,10,10,10��$��2��._���   MaterialDesignTextBlock#���  
�������$��General��$��Center=���    MaterialDesignBody1TextBlock#���  
�������$��1��$��0��$��Price:��$��0,0,10,0q��� -   $��carInfoPrice��#���  
�������$��1��$��1���� #���  $��2��$��0��
�������$��	Car body:��$��0,0,10,0q��� -	   $��carInfoBodyType��#���  
�������$��2��$��1���� #���  $��3��$��0��
�������$��Number of seats:��$��0,0,10,0q��� -
   $��carInfoSeats��#���  
�������$��3��$��1���� #���  $��4��$��0��
�������$��First year of production:��$��0,0,10,0q��� -   $��carInfoFirstYear��#���  
�������$��4��$��1��� -   $��engineInfoGrid�� �� $
��auto ��� $
��auto ��� $
��auto � �� $��* ��� $��* �.+��� $
��auto�����#00A5BA$
��10,0q�$ 10,10,10,10��$��2��._��� #���  
�������$��Drive��$��Center=��� #���  $��1��$��0��
�������$��Drive:��$��0,0,10,0q��� -   $��carInfoDrive��#���  
�������$��1��$��1���� #���  $��2��$��0��
�������$��Engine type:��$��0,0,10,0q��� -   $��carInfoEngine��#���  
�������$��2��$��1��� -   $��performanceInfoGrid�� �� $
��auto ��� $
��auto ��� $
��auto � �� $��* ��� $��* �.+��� $
��auto�����#00A5BA$
��10,0q�$ 10,10,10,10��$��2��._��� #���  
�������$��Performance��$��Center=��� #���  $��1��$��0��
�������$��
Top speed:��$��0,0,10,0q��� -   $��carInfoSpeed��#���  
�������$��1��$��1���� #���  $��2��$��0��
�������$��Acceleration 0-100 km/h:��$��0,0,10,0q��� -   $��
carInfoAcc��#���  
�������$��2��$��1��� -   $��brandInfoGrid�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto � �� $��* ��� $��* �.+��� $
��auto�����#00A5BA$
��10,0q�$ 10,10,10,10��$��2��._��� #���  
�������$��Brand��$��Center=��� #���  $��1��$��0��
�������$��Brand name:��$��0,0,10,0q��� -   $��brandInfoName��#���  
�������$��1��$��1���� #���  $��2��$��0��
�������$��Country:��$��0,0,10,0q��� -   $��brandInfoCounty��#���  
�������$��2��$��1���� #���  $��3��$��0��
�������$��Foundation year:��$��0,0,10,0q��� -   $��brandInfoFoundationYear��#���  
�������$��3��$��1���� #���  $��4��$��0��
�������$��Company value��$��0,0,10,0q��� -   $��brandInfoValue��#���  
�������$��4��$��1��