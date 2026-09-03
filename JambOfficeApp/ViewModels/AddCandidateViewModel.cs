using System.Runtime.InteropServices.JavaScript;
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
    [ObservableProperty] private int jambScore;
    
    public bool WasSaved { get; set; }

    public event Action RequestClose;

    public void LoadForEdit(Candidate candidate)
    {
        _editingCandidate = _db.Candidates.Find(candidate.Id);
        IsEditMode = true;

        RegNumber = _editingCandidate.RegNumber;
        FullName = _editingCandidate.FullName;
        DateOfBirth = _editingCandidate.DateOfBirth == DateTime.MinValue ? null : _editingCandidate.DateOfBirth;
        Gender = _editingCandidate.Gender;
        PhoneNumber = _editingCandidate.PhoneNumber;
        Email = _editingCandidate.Email;
        DepartmentApplied = _editingCandidate.DepartmentApplied;
        JambScore = _editingCandidate.JambScore;
    }

    [RelayCommand]
    private void Save()
    {
        if (string.IsNullOrWhiteSpace(regNumber) || string.IsNullOrWhiteSpace(fullName)) return;

        if (IsEditMode && _editingCandidate != null)
        {
            _editingCandidate.RegNumber = RegNumber;
            _editingCandidate.FullName = FullName;
            _editingCandidate.DateOfBirth = DateOfBirth ??  DateTime.MinValue;
            _editingCandidate.Gender = Gender;
            _editingCandidate.PhoneNumber = PhoneNumber;
            _editingCandidate.Email = Email;
            _editingCandidate.DepartmentApplied = DepartmentApplied;
            _editingCandidate.JambScore = JambScore;

            _db.SaveChanges();
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
                JambScore = JambScore
            };

            _db.Candidates.Add(candidate);
            _db.SaveChanges();
        }
        
        WasSaved = true;
        RequestClose?.Invoke();
    }

    [RelayCommand]
    private void Cancel()
    {
        WasSaved = false;
        RequestClose?.Invoke();
    }
}