namespace TimeToTextLib;

public class TimeToTextFormat
{
    public string TimeAsText { get; set; }

    public int AdditionalMinutes { get; set; }

    public override string ToString() => $"{TimeAsText} +{AdditionalMinutes}";
}
