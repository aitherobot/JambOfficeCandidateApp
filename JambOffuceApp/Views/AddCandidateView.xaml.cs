using System.Windows;
using JambOffuceApp.ViewModels;

namespace JambOffuceApp.Views;

public partial class AddCandidateView : Window
{
    public AddCandidateView()
    {
        InitializeComponent();

        if (DataContext is AddCandidateViewModel vm)
        {
            vm.RequestClose += () => Close();
        }
    }
    
    public bool WasSaved => (DataContext as AddCandidateViewModel)?.WasSaved ?? false;
}