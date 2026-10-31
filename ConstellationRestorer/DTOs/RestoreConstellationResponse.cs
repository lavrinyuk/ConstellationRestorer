namespace ConstellationRestorer.DTOs;

public class RestoreConstellationResponse
{
    public string MissingSymbols { get; set; } = string.Empty;

    public bool IsComplete { get; set; }
}
