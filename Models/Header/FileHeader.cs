public class FileHeader
{
    public string FileCreationDate { get; set; }
    public string FileCreationTime { get; set; }
    public string FileIDModifier { get; set; }
    public string? Id { get; set; } = string.Empty;
    public string ImmediateDestination { get; set; }
    public string ImmediateDestinationName { get; set; }
    public string ImmediateOrigin { get; set; }
    public string ImmediateOriginName { get; set; }   
} 