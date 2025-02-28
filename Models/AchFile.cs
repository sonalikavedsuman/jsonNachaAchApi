using System;
using System.Collections.Generic;

public class AchFile
{
    public List<IATBatch> IATBatches { get; set; }
    public object? NotificationOfChange { get; set; } = null;
    public object? ReturnEntries { get; set; } = null;
    public List<Batch> Batches { get; set; }
    public FileADVControl FileADVControl { get; set; }
    public FileControl FileControl { get; set; }
    public FileHeader FileHeader { get; set; }
    public string? Id { get; set; } = string.Empty;
} 