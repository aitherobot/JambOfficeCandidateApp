using System.Windows;
using JambOfficeApp.ViewModels;

namespace JambOfficeApp.Views;

public partial class RegisterView : Window
{
    public RegisterView()
    {
        InitializeComponent();

        if (DataContext is RegisterViewModel vm)
        {
            vm.RequestClose += () => Close();
        }
    }

    private void PasswordInput_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is RegisterViewModel vm)
        {
            vm.Password = PasswordInput.Password;
        }
    }

    private void ConfirmPasswordInput_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is RegisterViewModel vm)
        {
            vm.ConfirmPassword = ConfirmPasswordInput.Password;
        }
    }
}