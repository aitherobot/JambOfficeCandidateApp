using System.Windows;
using JambOffuceApp.Models;
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

    public void SetEditCandidate(Candidate candidate)
    {
        if (DataContext is AddCandidateViewModel vm)
        {
            vm.LoadForEdit(candidate);
            Title = "Edit Candidate";
            HeaderText.Text="Edit Candidate";
        }
    }
    
    public bool WasSaved => (DataContext as AddCandidateViewModel)?.WasSaved ?? false;
}