using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JambOffuceApp.Data;
using JambOffuceApp.Models;

namespace JambOffuceApp.ViewModels;

public partial class DocumentTrackingViewModel : ObservableObject
{
    private readonly AppDbContext _db = new();

    private static readonly string[] RequiredDocumentTypes =
    {
        "O Level Result",
        "Birth Certificate",
        "LGA Certificate",
        "Passport Photo"
    };

    [ObservableProperty] private string _candidateName;
    [ObservableProperty] private ObservableCollection<DocumentStatus> _documents = new ();
    
    public event Action RequestClose;

    public void LoadForCandidate(Candidate candidate)
    {
        CandidateName = candidate.FullName;

        var existing = _db.DocumentStatuses
            .Where(d => d.CandidateId == candidate.Id)
            .ToList();

        if (existing.Count == 0)
        {
            foreach (var docType in RequiredDocumentTypes)
            {
                var doc = new DocumentStatus
                {
                    CandidateId = candidate.Id,
                    DocumentType = docType,
                    Submitted = false,
                    Verified = false
                };
                _db.DocumentStatuses.Add(doc);
                existing.Add(doc);
            }

            _db.SaveChanges();
        }

        Documents = new ObservableCollection<DocumentStatus>(existing);
    }

    [RelayCommand]
    private void SaveAndClose()
    {
        _db.SaveChanges();
        RequestClose?.Invoke();
    }
}