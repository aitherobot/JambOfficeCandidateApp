using System.Windows;
using JambOffuceApp.Models;
using JambOffuceApp.ViewModels;

namespace JambOffuceApp.Views;

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