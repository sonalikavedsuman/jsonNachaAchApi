public class IATBatch
{
    public IATBatchHeader IATBatchHeader { get; set; }
    public List<IATEntryDetail> IATEntryDetails { get; set; }
    public BatchControl BatchControl { get; set; }
    public string? Id { get; set; } = string.Empty;
} 