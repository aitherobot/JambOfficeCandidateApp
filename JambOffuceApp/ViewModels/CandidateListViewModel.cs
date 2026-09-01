using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JambOffuceApp.Data;
using JambOffuceApp.Models;

namespace JambOffuceApp.ViewModels;

public partial class CandidateListViewModel : ObservableObject
{
    private readonly AppDbContext _db = new AppDbContext();
    
    [ObservableProperty] 
    private ObservableCollection<Candidate> _candidates = new ObservableCollection<Candidate>();

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

        Candidates.Remove(candidate);
    }
}