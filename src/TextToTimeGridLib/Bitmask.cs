using System.Text;

namespace TextToTimeGridLib;

public record Bitmask(bool[][] Mask)
{
    public override string ToString()
    {
        var sb = new StringBuilder();

        foreach (var line in Mask)
        {
            foreach (var cell in line)
            {
                sb.Append(cell ? "1" : "0");
            }
            sb.Append('\n');
        }

        return sb.ToString();
    }
}
