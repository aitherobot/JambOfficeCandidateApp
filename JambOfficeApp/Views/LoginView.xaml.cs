using System.Windows;
using JambOfficeApp.Models;
using JambOfficeApp.ViewModels;

namespace JambOfficeApp.Views;

public partial class LoginView : Window
{
    public LoginView()
    {
        InitializeComponent();

        if (DataContext is LoginViewModel vm)
        {
            vm.RequestClose += () => Close();
            vm.RequestGoToRegister += OpenRegisterWindow;
        }
    }

    private void PassWordInput_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is LoginViewModel vm)
        {
            vm.Password = PassWordInput.Password;
        }
    }

    private void OpenRegisterWindow()
    {
        var registerView = new RegisterView { Owner = this };
        registerView.ShowDialog();
    }

    public User LoggedInUser => (DataContext as LoginViewModel)?.LoginSucceeded ==
        true ? (DataContext as LoginViewModel).LoggedInUser : null;
}