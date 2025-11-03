using System;
using System.Linq;

class gr4
{
    public class GoldBar
    {
        public double Weight { get; set; }
        public double Value { get; set; }

        public GoldBar(double weight, double value)
        {
            Weight = weight;
            Value = value;
        }

        public double ValuePerUnit => Value / Weight;
    }

    public static double GetMaxValue(double capacity, GoldBar[] bars)
    {
        var sortedBars = bars.OrderByDescending(bar => bar.ValuePerUnit).ToArray();

        double totalValue = 0;
        double remainingCapacity = capacity;

        foreach (var bar in sortedBars)
        {
            if (remainingCapacity <= 0) break;
            if (bar.Weight <= remainingCapacity)
            {
                totalValue += bar.Value;
                remainingCapacity -= bar.Weight;
            }
            else
            {
                totalValue += bar.ValuePerUnit * remainingCapacity;
                remainingCapacity = 0;
            }
        }

        return totalValue;
    }

    static void Main()
    {
        GoldBar[] bars = {
            new GoldBar(10, 60),
            new GoldBar(20, 100),
            new GoldBar(30, 120)
        };

        double capacity = 50;
        double maxValue = GetMaxValue(capacity, bars);
        Console.WriteLine($"Максимальная стоимость: {maxValue}");
    }
}
