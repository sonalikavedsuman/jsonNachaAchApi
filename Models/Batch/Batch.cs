public class Batch
{
    public BatchControl BatchControl { get; set; }
    public BatchHeader BatchHeader { get; set; }
    public List<EntryDetail> EntryDetails { get; set; }
    public object? Offset { get; set; } = null;
} 