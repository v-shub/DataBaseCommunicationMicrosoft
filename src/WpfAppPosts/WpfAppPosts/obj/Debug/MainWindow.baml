   M S B A M L   `   `   `  ����F  BWpfAppPosts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfAppPostsWpfAppPosts  5   	       WpfAppPosts.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:WpfAppPosts     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5	       �� 
TargetType �� Property �� Value��%�   '��       �� 5
   
   " ��6   .�5      ��  � ��$ 	20 8 20 8q�6'   �� 5       � ��$	 100��6&   �� 5       � ��$ 30��6'   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 	postsGrid��5   /      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 
Creator_ID��6D   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 125 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *
Creator_ID+6X     5      $	 
Created_at��6D   $
 80 6X    6%   �� *
Created_at+6X     5      $	 Header��6@   $	
 125 6P    6%   �� *Header+6P     5      $	 Post_content��6F   $	
 125 6\    6%   �� *Post_content+6\   5      �� 5   
   $��Center=�6   $��1��63    �� Orientation$ 
Horizontal=�6@   .+�5      �� -   5         ��Name$ updateButton��5      $��Update��6+   6<   �� 5       -   5        $ deleteButton��5       $��Delete��6+   6<   5!      5"      5#      