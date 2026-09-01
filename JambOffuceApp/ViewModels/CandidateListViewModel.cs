using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JambOffuceApp.Data;
using JambOffuceApp.Models;
using JambOffuceApp.Views;

namespace JambOffuceApp.ViewModels;

public partial class CandidateListViewModel : ObservableObject
{
    private readonly AppDbContext _db = new ();
    
    [ObservableProperty] 
    private ObservableCollection<Candidate> _candidates = new ();

    [ObservableProperty] private string _searchText;

    public CandidateListViewModel()
    {
        LoadCandidates();
    }

    private void LoadCandidates()
    {
        Candidates = new ObservableCollection<Candidate>(_db.Candidates.ToList());
    }

    [RelayCommand]
    private void Search()
    {
        var query = _db.Candidates.AsQueryable();

        if (!string.IsNullOrWhiteSpace(_searchText))
        {
            query = query.Where(c =>
                c.RegNumber.Contains(_searchText) ||
                c.FullName.Contains(_searchText));
        }

        Candidates = new ObservableCollection<Candidate>(query.ToList());
    }

    [RelayCommand]
    private void DeleteCandidate(object parameter)
    {
        if (parameter is not Candidate candidate) return;

        _db.Candidates.Remove(candidate);
        _db.SaveChanges();
        Candidates.Remove(candidate);
    }

    [RelayCommand]
    private void OpenAddCandidate()
    {
        var dialog = new AddCandidateView
        {
            Owner = Application.Current.MainWindow
        };
        
        dialog.ShowDialog();

        if (dialog.WasSaved)
        {
            LoadCandidates();
        }
    }
}