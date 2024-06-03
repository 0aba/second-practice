using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Practice_2_7.DB;
using Practice_2_7.DB.Models;


namespace Practice_2_7.WindowsApp;

public partial class EditWindow : Window
{
    private TextBox loginTBox;
    private TextBox passwordTBox;
    private TextBox nameTBox;
    private TextBox surnameTBox;
    private TextBox phonenumberTBox;
    private ComboBox roleCBox;
    private User editUser;
    
    public EditWindow()
    {
        InitializeComponent();
    }
    
    public EditWindow(User editUser)
    {
        InitializeComponent();
        
        loginTBox = this.FindControl<TextBox>("LoginTBox");
        passwordTBox = this.FindControl<TextBox>("PasswordTBox");
        nameTBox = this.FindControl<TextBox>("NameTBox");
        surnameTBox = this.FindControl<TextBox>("SurnameTBox");
        phonenumberTBox = this.FindControl<TextBox>("PhonenumberTBox");
        roleCBox = this.FindControl<ComboBox>("RoleCBox");
        this.editUser = editUser;
        
        roleCBox.ItemsSource = DatabaseService.GetApplicationContext().Roles.ToList();

        loginTBox.Text = editUser.Login;
        passwordTBox.Text = editUser.Password;
        nameTBox.Text = editUser.Name;
        surnameTBox.Text = editUser.Surname;
        phonenumberTBox.Text = editUser.PhoneNumber;
        roleCBox.SelectedItem = editUser.IdRoleNavigation;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(loginTBox.Text) &&
            !string.IsNullOrWhiteSpace(passwordTBox.Text) &&
            !string.IsNullOrWhiteSpace(nameTBox.Text) &&
            !string.IsNullOrWhiteSpace(surnameTBox.Text) &&
            !string.IsNullOrWhiteSpace(phonenumberTBox.Text))
        {
            editUser.Name = nameTBox.Text;
            editUser.Surname = surnameTBox.Text;
            editUser.Login = loginTBox.Text;
            editUser.Password = passwordTBox.Text;
            editUser.PhoneNumber = phonenumberTBox.Text;
            editUser.IdRoleNavigation = roleCBox.SelectedItem as Role;
            
            DatabaseService.GetApplicationContext().SaveChanges();
            Close();
        }
    }
    
    private void BackBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }
}