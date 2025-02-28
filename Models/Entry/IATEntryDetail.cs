public class IATEntryDetail
{
    public string DFIAccountNumber { get; set; }
    public string? OFACScreeningIndicator { get; set; } = string.Empty;
    public string RDFIIdentification { get; set; }
    public Addenda10 Addenda10 { get; set; }
    public Addenda11 Addenda11 { get; set; }
    public Addenda12 Addenda12 { get; set; }
    public Addenda13 Addenda13 { get; set; }
    public Addenda14 Addenda14 { get; set; }
    public Addenda15 Addenda15 { get; set; }
    public Addenda16 Addenda16 { get; set; }
    public int AddendaRecordIndicator { get; set; }
    public int AddendaRecords { get; set; }
    public int Amount { get; set; }
    public string Category { get; set; }
    public string CheckDigit { get; set; }
    public string? Id { get; set; } = string.Empty;
    public string? SecondaryOFACScreeningIndicator { get; set; } = string.Empty;
    public string TraceNumber { get; set; }
    public int TransactionCode { get; set; }    
} 