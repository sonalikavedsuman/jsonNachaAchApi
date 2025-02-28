public class IATBatchHeader
{
    public string IATIndicator { get; set; }
    public string ISODestinationCountryCode { get; set; }
    public string ISODestinationCurrencyCode { get; set; }
    public string ISOOriginatingCurrencyCode { get; set; }
    public string ODFIIdentification { get; set; }
    public int BatchNumber { get; set; }
    public string CompanyEntryDescription { get; set; }
    public string EffectiveEntryDate { get; set; }
    public string ForeignExchangeIndicator { get; set; }
    public string? ForeignExchangeReference { get; set; } = string.Empty;
    public int ForeignExchangeReferenceIndicator { get; set; }
    public string? Id { get; set; } = string.Empty;
    public string OriginatorIdentification { get; set; }
    public int OriginatorStatusCode { get; set; }
    public int ServiceClassCode { get; set; }
    public string? SettlementDate { get; set; } = string.Empty;
    public string StandardEntryClassCode { get; set; }
    
} 