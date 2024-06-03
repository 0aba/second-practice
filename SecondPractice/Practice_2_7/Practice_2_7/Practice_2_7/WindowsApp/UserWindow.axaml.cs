using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Practice_2_7.DB;

namespace Practice_2_7.WindowsApp;

public partial class UserWindow : Window
{
    private DataGrid usersDGrid;
    private TextBox searchTBox;
    public UserWindow()
    {
        InitializeComponent();

        usersDGrid = this.FindControl<DataGrid>("UsersDGrid");
        searchTBox = this.FindControl<TextBox>("SearchTBox");
        
        usersDGrid.ItemsSource = DatabaseService.GetApplicationContext().Users.Include(q=>q.IdRoleNavigation).ToList();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void LogOutBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }
    
    private void SearchBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(searchTBox.Text))
        {
            usersDGrid.ItemsSource = DatabaseService.GetApplicationContext().Users.Include(q=>q.IdRoleNavigation).ToList();
        }
        else
        {
            usersDGrid.ItemsSource = DatabaseService.GetApplicationContext().Users
                .Where(q => q.Login.ToLower().Contains(searchTBox.Text.ToLower()) 
                            || q.Name.ToLower().Contains(searchTBox.Text.ToLower()) 
                            || q.Surname.ToLower().Contains(searchTBox.Text.ToLower())).ToList();   
        }
    }
}