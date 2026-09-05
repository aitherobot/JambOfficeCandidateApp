using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JambOfficeApp.Data;
using JambOfficeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JambOfficeApp.ViewModels;

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

    public async Task LoadForCandidate(Candidate candidate)
    {
        CandidateName = candidate.FullName;

        var existing = await _db.DocumentStatuses
            .Where(d => d.CandidateId == candidate.Id)
            .ToListAsync();

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

            await _db.SaveChangesAsync();
        }

        Documents = new ObservableCollection<DocumentStatus>(existing);
    }

    [RelayCommand]
    private async Task SaveAndClose()
    {
        await _db.SaveChangesAsync();
        RequestClose?.Invoke();
    }
}