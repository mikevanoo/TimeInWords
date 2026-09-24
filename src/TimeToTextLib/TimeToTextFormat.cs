namespace TimeToTextLib;

public class TimeToTextFormat
{
    public required string TimeAsText { get; init; }

    public required int AdditionalMinutes { get; init; }

    public override string ToString() => $"{TimeAsText} +{AdditionalMinutes}";
}
