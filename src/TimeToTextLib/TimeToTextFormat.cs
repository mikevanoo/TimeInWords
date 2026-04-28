namespace TimeToTextLib;

public class TimeToTextFormat
{
    public string TimeAsText { get; set; } = string.Empty;

    public int AdditionalMinutes { get; set; }

    public override string ToString() => $"{TimeAsText} +{AdditionalMinutes}";
}
