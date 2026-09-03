using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JambOffuceApp.Services;

namespace JambOffuceApp.ViewModels;

public partial class RegisterViewModel : ObservableObject
{
    private readonly AuthService _authService = new AuthService();
    
    [ObservableProperty] private string _username;
    [ObservableProperty] private string _password;
    [ObservableProperty] private string _confirmPassword;
    [ObservableProperty] private string _errorMessage;
    
    public bool RegisterSucceeded { get; private set; }
    public event Action RequestClose;

    [RelayCommand]
    private void Register()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Enter a username and password";
            return;
        }

        if (Password != ConfirmPassword)
        {
            ErrorMessage = "Passwords do not match";
            return;
        }

        var user = _authService.Register(Username, Password, "Staff");
        if (user == null)
        {
            ErrorMessage = "That username is already taken";
            return;
        }
        
        RegisterSucceeded = true;
        RequestClose?.Invoke();
    }
}