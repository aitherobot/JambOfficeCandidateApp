using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JambOfficeApp.Data;
using JambOfficeApp.Models;

namespace JambOfficeApp.ViewModels;

public partial class AddCandidateViewModel : ObservableObject
{
    private readonly AppDbContext _db =  new AppDbContext();

    private Candidate _editingCandidate;

    [ObservableProperty] private bool isEditMode;
    [ObservableProperty] private string regNumber;
    [ObservableProperty] private string fullName;
    [ObservableProperty] private DateTime? dateOfBirth;
    [ObservableProperty] private string gender;
    [ObservableProperty] private string phoneNumber;
    [ObservableProperty] private string email;
    [ObservableProperty] private string departmentApplied;
    [ObservableProperty] private string institutionApplied;
    [ObservableProperty] private int jambScore;
    
    [ObservableProperty] private string errorMessage;
    
    public bool WasSaved { get; set; }

    public event Action RequestClose;

    public async Task LoadForEdit(Candidate candidate)
    {
        _editingCandidate = await _db.Candidates.FindAsync(candidate.Id);
        IsEditMode = true;

        RegNumber = _editingCandidate.RegNumber;
        FullName = _editingCandidate.FullName;
        DateOfBirth = _editingCandidate.DateOfBirth == DateTime.MinValue ? null : _editingCandidate.DateOfBirth;
        Gender = _editingCandidate.Gender;
        PhoneNumber = _editingCandidate.PhoneNumber;
        Email = _editingCandidate.Email;
        DepartmentApplied = _editingCandidate.DepartmentApplied;
        InstitutionApplied = _editingCandidate.InstitutionApplied;
        JambScore = _editingCandidate.JambScore;
    }

    [RelayCommand]
    private async Task Save()
    {
        if (!ValidateFields()) return;

        try
        {
            if (IsEditMode && _editingCandidate != null)
            {
                _editingCandidate.RegNumber = RegNumber;
                _editingCandidate.FullName = FullName;
                _editingCandidate.DateOfBirth = DateOfBirth ?? DateTime.MinValue;
                _editingCandidate.Gender = Gender;
                _editingCandidate.PhoneNumber = PhoneNumber;
                _editingCandidate.Email = Email;
                _editingCandidate.DepartmentApplied = DepartmentApplied;
                _editingCandidate.InstitutionApplied = InstitutionApplied;
                _editingCandidate.JambScore = JambScore;

                await _db.SaveChangesAsync();
            }
            else
            {
                var candidate = new Candidate
                {
                    RegNumber = RegNumber,
                    FullName = FullName,
                    DateOfBirth = DateOfBirth ?? DateTime.MinValue,
                    Gender = Gender,
                    PhoneNumber = PhoneNumber,
                    Email = Email,
                    DepartmentApplied = DepartmentApplied,
                    InstitutionApplied = InstitutionApplied,
                    JambScore = JambScore
                };

                _db.Candidates.Add(candidate);
                await _db.SaveChangesAsync();
            }
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException)
        {
            ErrorMessage = "Could not save this candidate. Check that the Reg Number is not already in use.";
            return;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"An unexpected error occured while saving: {ex.Message}";
            return;
        }
        
        WasSaved = true;
        RequestClose?.Invoke();
    }

    private bool ValidateFields()
    {
        if (string.IsNullOrWhiteSpace(RegNumber))
        {
            ErrorMessage = "Reg Number is required";
            return false;
        }
        
        if (string.IsNullOrWhiteSpace(FullName))
        {
            ErrorMessage = "Full Name is required";
            return false;
        }

        if (!string.IsNullOrWhiteSpace(Email) && !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            ErrorMessage = "Enter a valid email address, or leave it blank.";
            return false;
        }

        if (!string.IsNullOrWhiteSpace(PhoneNumber) && !Regex.IsMatch(PhoneNumber, @"^\d{7,15}$"))
        {
            ErrorMessage = "Phone number should be 7-15 digits only, or leave it blank.";
            return false;
        }

        if (JambScore < 0 || JambScore > 400)
        {
            ErrorMessage = "JAMB score must be between 0 and 400";
            return false;
        }

        ErrorMessage = null;
        return true;
    }

    [RelayCommand]
    private void Cancel()
    {
        WasSaved = false;
        RequestClose?.Invoke();
    }
}