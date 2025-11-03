using System.Globalization;

namespace Names;

internal static class HeatmapTask
{
    public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
    {
        var days = new string[30];
        for (int i = 0; i < days.Length; i++)
            days[i] = (i + 2).ToString();

        var month = new string[12];
        for (int i = 0; i < month.Length; i++)
            month[i] = (i + 1).ToString();

        var cardData = new double[30, 12];
        for (int x = 0; x < 30; x++)
        {
            for (int y = 0; y < 12; y++)
            {
                foreach (var name in names)
                {
                    if (name.BirthDate.Day > 1)
                    {
                        int dayIndex = name.BirthDate.Day - 2; 
                        int monthIndex = name.BirthDate.Month - 1; 
                        cardData[dayIndex, monthIndex]++;
                    }

                }
            }
        }


        return new HeatmapData(
            "Пример карты интенсивностей", cardData, month, days);
    }
}