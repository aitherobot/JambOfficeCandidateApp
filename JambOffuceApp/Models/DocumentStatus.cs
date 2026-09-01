namespace JambOffuceApp.Models;

public class DocumentStatus
{
    public int Id { get; set; }
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; }
    
    public string DocumentType { get; set; }
    public bool Submitted { get; set; }
    public bool Verified { get; set; }
}