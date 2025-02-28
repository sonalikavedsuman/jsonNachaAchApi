using System.ComponentModel.DataAnnotations;

public class BatchHeader
{
    public string ODFIIdentification { get; set; }
    public int BatchNumber { get; set; }
    public string CompanyDescriptiveDate { get; set; }
    public string CompanyEntryDescription { get; set; }
    public string CompanyIdentification { get; set; }
    public string CompanyName { get; set; }
    public string EffectiveEntryDate { get; set; }
    public string? Id { get; set; } = string.Empty;
    public int OriginatorStatusCode { get; set; }
    public int ServiceClassCode { get; set; }
    public string? SettlementDate { get; set; } = string.Empty;
    public string StandardEntryClassCode { get; set; }    
} 