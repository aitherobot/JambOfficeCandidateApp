using System.Runtime.InteropServices.JavaScript;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JambOffuceApp.Data;

namespace JambOffuceApp.ViewModels;

public partial class AddCandidateViewModel : ObservableObject
{
    private readonly AppDbContext _db =  new AppDbContext();

    [ObservableProperty] private string regNumber;
    [ObservableProperty] private string fullName;
    [ObservableProperty] private DateTime? dateOfBirth;
    [ObservableProperty] private string gender;
    [ObservableProperty] private string phoneNumber;
    [ObservableProperty] private string email;
    [ObservableProperty] private string departmentApplied;
    [ObservableProperty] private int jambScore;
    
    public bool WasSaved { get; set; }

    public event Action RequestClose;

    [RelayCommand]
    private void Save()
    {
        if (string.IsNullOrWhiteSpace(regNumber) || string.IsNullOrWhiteSpace(fullName)) return;
    }
}