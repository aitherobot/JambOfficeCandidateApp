namespace JambOfficeApp.Models;

public class Candidate
{
    public int Id { get; set; }
    public string RegNumber { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string DepartmentApplied { get; set; }
    public string InstitutionApplied { get; set; }
    public int JambScore { get; set; }

    public ICollection<DocumentStatus> Documents { get; set; } = new List<DocumentStatus>();
}