using System.Text;

namespace TextToTimeGridLib
{
    public class Bitmask
    {
        public bool[][] Mask { get; }

        public Bitmask(bool[][] mask)
        {
            Mask = mask;
        }

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
}
