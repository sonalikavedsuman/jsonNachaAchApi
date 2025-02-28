public class BatchControl
{
    public string ODFIIdentification { get; set; }
    public int BatchNumber { get; set; }
    public string CompanyIdentification { get; set; }
    public int EntryAddendaCount { get; set; }
    public int EntryHash { get; set; }
    public string? Id { get; set; } = string.Empty;
    public int ServiceClassCode { get; set; }
    public int TotalCredit { get; set; }
    public int TotalDebit { get; set; }
} 