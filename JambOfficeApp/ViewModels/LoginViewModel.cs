using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JambOfficeApp.Models;
using JambOfficeApp.Services;

namespace JambOfficeApp.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly AuthService _authService = new AuthService();
    
    [ObservableProperty] private string _username;
    [ObservableProperty] private string _password;
    [ObservableProperty] private string _errorMessage;
    
    public User LoggedInUser { get; set; }
    
    public bool LoginSucceeded { get; set; }
    
    public event Action RequestClose;
    public event Action RequestGoToRegister;

    [RelayCommand]
    private void Login()
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Enter both username and password";
            return;
        }

        var user = _authService.Login(Username, Password);
        if (user == null)
        {
            ErrorMessage = "Invalid username or password";
            return;
        }
        
        LoggedInUser = user;
        LoginSucceeded = true;
        RequestClose?.Invoke();
    }

    [RelayCommand]
    private void GoToRegister()
    {
        RequestGoToRegister?.Invoke();
    }
}