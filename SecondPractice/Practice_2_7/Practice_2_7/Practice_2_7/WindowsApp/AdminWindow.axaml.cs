using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Practice_2_7.DB.Models;
using Microsoft.EntityFrameworkCore;
using Practice_2_7.DB;
 

namespace Practice_2_7.WindowsApp;

public partial class AdminWindow : Window
{
    private DataGrid usersDGrid;
    private TextBox searchTBox;
    
    public AdminWindow()
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
    
    private void DeleteBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        User? selectedUser = (User) usersDGrid.SelectedItem;
        
        if (selectedUser != null)
        {
            DatabaseService.GetApplicationContext().Users.Remove(selectedUser);
            DatabaseService.GetApplicationContext().SaveChanges();
            usersDGrid.ItemsSource = DatabaseService.GetApplicationContext().Users.Include(q=>q.IdRoleNavigation).ToList();
        }
    }
    
    private async void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        User? selectedUser = (User) usersDGrid.SelectedItem;

        if (selectedUser != null)
        {
            await new EditWindow(selectedUser).ShowDialog(this);
            usersDGrid.ItemsSource = DatabaseService.GetApplicationContext().Users.Include(q=>q.IdRoleNavigation).ToList();
        }
    }
}