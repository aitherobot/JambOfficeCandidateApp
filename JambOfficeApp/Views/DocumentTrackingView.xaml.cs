using System.Windows;
using System.Windows.Interop;
using JambOfficeApp.Models;
using JambOfficeApp.Services;
using JambOfficeApp.ViewModels;

namespace JambOfficeApp.Views;

public partial class DocumentTrackingView : Window
{
    public DocumentTrackingView()
    {
        InitializeComponent();

        if (DataContext is DocumentTrackingViewModel vm)
        {
            vm.RequestClose += () => Close();
        }
        
        SourceInitialized += (s, e) =>
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            ThemeService.SetTitleBarTheme(hwnd, ThemeService.IsWindowsInDarkMode());
        };
    }

    public async Task SetCandidateAsync(Candidate candidate)
    {
        if (DataContext is DocumentTrackingViewModel vm)
        {
            await vm.LoadForCandidate(candidate);
        }
    }
}