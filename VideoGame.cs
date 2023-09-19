using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class VideoGame : IComparable<VideoGame>
    {
        public string Name { get; set; }
        public string Platform { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public double NA_Sales { get; set; }
        public double EU_Sales { get; set; }
        public double JP_Sales { get; set; }
        public double Other_Sales { get; set; }
        public double Global_Sales { get; set; }

        public int CompareTo(VideoGame other)
        {
            return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }
        public override string ToString()
        {
            return $"{Name}, {Platform}, {Year}, {Genre}, {Publisher}, " +
              $"{NA_Sales}, {EU_Sales}, {JP_Sales}, {Other_Sales}, {Global_Sales}";
        }

    }
}
