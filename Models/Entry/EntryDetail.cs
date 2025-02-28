public class EntryDetail
{
    public string DFIAccountNumber { get; set; }
    public string RDFIIdentification { get; set; }
    public int Amount { get; set; }
    public string Category { get; set; }
    public string CheckDigit { get; set; }
    public string? DiscretionaryData { get; set; } = string.Empty;
    public string? Id { get; set; } = string.Empty;
    public string IdentificationNumber { get; set; }
    public string IndividualName { get; set; }
    public string TraceNumber { get; set; }
    public int TransactionCode { get; set; }
} 