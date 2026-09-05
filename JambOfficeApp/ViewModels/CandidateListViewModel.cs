using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JambOfficeApp.Data;
using JambOfficeApp.Models;
using JambOfficeApp.Services;
using JambOfficeApp.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace JambOfficeApp.ViewModels;

public partial class CandidateListViewModel : ObservableObject
{
    private readonly AppDbContext _db = new ();
    
    [ObservableProperty] 
    private ObservableCollection<Candidate> _candidates = new ();

    [ObservableProperty] private string _searchText;

    public CandidateListViewModel()
    {
        _ = LoadCandidatesAsync();
    }

    private async Task LoadCandidatesAsync()
    {
        var results = await _db.Candidates.AsNoTracking().ToListAsync();
        Candidates = new ObservableCollection<Candidate>(results);
    }

    [RelayCommand]
    private async Task Search()
    {
        var query = _db.Candidates.AsQueryable();

        if (!string.IsNullOrWhiteSpace(_searchText))
        {
            query = query.Where(c =>
                c.RegNumber.Contains(_searchText) ||
                c.FullName.Contains(_searchText));
        }

        var results = await query.ToListAsync();
        Candidates = new ObservableCollection<Candidate>(results);
    }

    [RelayCommand]
    private async Task DeleteCandidate(object parameter)
    {
        if (parameter is not Candidate candidate) return;

        _db.Candidates.Remove(candidate);
        await _db.SaveChangesAsync();
        Candidates.Remove(candidate);
    }

    [RelayCommand]
    private async Task EditCandidate(object parameter)
    {
        if (parameter is not Candidate candidate) return;

        var dialog = new AddCandidateView()
        {
            Owner = Application.Current.MainWindow
        };
        
        await dialog.SetEditCandidateAsync(candidate);
        
        dialog.ShowDialog();

        if (dialog.WasSaved)
        {
            await LoadCandidatesAsync();
        }
    }

    [RelayCommand]
    private async Task OpenDocumentTracking(object parameter)
    {
        if (parameter is not Candidate candidate) return;

        var dialog = new DocumentTrackingView()
        {
            Owner = Application.Current.MainWindow
        };
        
        await dialog.SetCandidateAsync(candidate);
        dialog.ShowDialog();
    }

    [RelayCommand]
    private void ExportToExcel()
    {
        var saveDialog = new SaveFileDialog
        {
            Filter = "Excel Workbook (*.xlsx)|*.xlsx",
            FileName = "Candidates.xlsx"
        };
        
        if (saveDialog.ShowDialog() != true) return;

        var exportService = new ExportService();
        exportService.ExportCandidatesToExcel(_db.Candidates.AsNoTracking().ToList(), saveDialog.FileName);
        
        MessageBox.Show("Export completed successfully.", "Export",  MessageBoxButton.OK, MessageBoxImage.Information);
    }

    [RelayCommand]
    private async Task OpenAddCandidate()
    {
        var dialog = new AddCandidateView
        {
            Owner = Application.Current.MainWindow
        };
        
        dialog.ShowDialog();

        if (dialog.WasSaved)
        {
            await LoadCandidatesAsync();
        }
    }
}