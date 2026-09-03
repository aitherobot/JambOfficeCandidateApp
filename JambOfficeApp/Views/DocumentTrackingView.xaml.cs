using System.Windows;
using JambOfficeApp.Models;
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
    }

    public void SetCandidate(Candidate candidate)
    {
        if (DataContext is DocumentTrackingViewModel vm)
        {
            vm.LoadForCandidate(candidate);
        }
    }
}