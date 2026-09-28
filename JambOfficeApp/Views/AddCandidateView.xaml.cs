using System.Windows;
using System.Windows.Interop;
using JambOfficeApp.Models;
using JambOfficeApp.Services;
using JambOfficeApp.ViewModels;

namespace JambOfficeApp.Views;

public partial class AddCandidateView : Window
{
    public AddCandidateView()
    {
        InitializeComponent();

        if (DataContext is AddCandidateViewModel vm)
        {
            vm.RequestClose += () => Close();
        }
        
        // SourceInitialized += (s, e) =>
        // {
        //     var hwnd = new WindowInteropHelper(this).Handle;
        //     ThemeService.SetTitleBarTheme(hwnd, ThemeService.IsWindowsInDarkMode());
        // };
    }

    public async Task SetEditCandidateAsync(Candidate candidate)
    {
        if (DataContext is AddCandidateViewModel vm)
        {
            await vm.LoadForEdit(candidate);
            Title = "Edit Candidate";
            HeaderText.Text="EDIT CANDIDATE";
        }
    }
    
    public bool WasSaved => (DataContext as AddCandidateViewModel)?.WasSaved ?? false;
}