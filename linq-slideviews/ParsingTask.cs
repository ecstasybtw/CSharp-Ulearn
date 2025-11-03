using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public class ParsingTask
{
	public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
	{
		var linesAsList = lines.ToList();
		var slideLines = linesAsList.Skip(1);

		var combinedSlidesDict = slideLines
			.Select(lines => lines.Split(';'))
			.Where(parts => parts.Length > 2)
			.Where(parts => (parts[1] == "theory" || parts[1] == "quiz" || parts[1] == "exercise")
			&& int.TryParse(parts[0], out int id) == true)
			.Select(parts => new SlideRecord(int.Parse(parts[0]), Enum.Parse<SlideType>(parts[1], true), parts[2]))
			.ToDictionary(slideRecord => slideRecord.SlideId, slideRecord => slideRecord);

		return combinedSlidesDict;
	}

	public static IEnumerable<VisitRecord> ParseVisitRecords(
		IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
	{
		var linesAsList  = lines.ToList();
		var valuableLines = lines.Skip(1);
		var listOfVisits = valuableLines
			.Select(line => {
				var parts = line.Split(';');
				if (parts.Length != 4)
					throw new FormatException($"Wrong line [{line}]");
				try {
					var userId = int.Parse(parts[0]);
					var slideId = int.Parse(parts[1]);
					var dateTime = DateTime.Parse($"{parts[2]} {parts[3]}");
					var slideType = slides[slideId].SlideType;
                	return new VisitRecord(userId, slideId, dateTime, slideType);
				}
				catch (FormatException ex) {
					throw new FormatException($"Wrong line [{line}]", ex); }

				catch (KeyNotFoundException ex) {
					throw new FormatException($"Wrong line [{line}]", ex); }
			});	
		return listOfVisits;
	} 
}
