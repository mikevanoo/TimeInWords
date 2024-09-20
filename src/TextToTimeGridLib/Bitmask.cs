using System.Text;

namespace TextToTimeGridLib;

public class Bitmask(bool[][] mask)
{
    public bool[][] Mask { get; } = mask;

    public override string ToString()
    {
        var sb = new StringBuilder();

        foreach (var line in Mask)
        {
            foreach (var cell in line)
            {
                sb.Append(cell ? "1" : "0");
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
