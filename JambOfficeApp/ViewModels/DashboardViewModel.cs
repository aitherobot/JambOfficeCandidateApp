using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JambOfficeApp.Data;
using Microsoft.EntityFrameworkCore;

namespace JambOfficeApp.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    private readonly AppDbContext _db = new AppDbContext();

    [ObservableProperty] private int totalCandidates;
    [ObservableProperty] private int documentsPendingVerification;
    [ObservableProperty] private int documentsFullyVerified;

    public DashboardViewModel()
    {
        _ = LoadStatsAsync();
    }

    [RelayCommand]
    private async Task Refresh()
    {
        await LoadStatsAsync();
    }

    private async Task LoadStatsAsync()
    {
        TotalCandidates = await _db.Candidates.AsNoTracking().CountAsync();

        var allDocs = await _db.DocumentStatuses.AsNoTracking().ToListAsync();
        DocumentsPendingVerification = allDocs.Count(d => d.Submitted && !d.Verified);
        DocumentsFullyVerified = allDocs.Count(d => d.Verified);
    }
}