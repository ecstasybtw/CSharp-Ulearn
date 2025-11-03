using System;
using System.Reflection;

namespace Names;

internal static class HistogramTask
{
    public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
    {
        var days = new string[31];
        for (int i = 0; i < days.Length; i++)
            days[i] = (i + 1).ToString(); 

        var birthsTemp = new double[31];

        foreach (var element in names)
        {
            if (element.Name == name && element.BirthDate.Day != 1)
            {
                birthsTemp[element.BirthDate.Day - 1]++;
            }
        }

        return new HistogramData(
            $"Рождаемость людей с именем '{name}'", days, birthsTemp);
    }
}