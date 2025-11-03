using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public class StatisticsTask
{
	public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
	{
		var timeIntervals = visits
		.GroupBy(user => user.UserId)	
		.SelectMany(group => 
		{
			var orderedVisits = group.OrderBy(visit => visit.DateTime).ToList();
			var timeIntervals = new List<double>();
			for (int i = 0; i < orderedVisits.Count - 1; i++)
			{
				if (orderedVisits[i].SlideType == slideType)
				{
					var currentVisit = orderedVisits[i];
					var nextVisit = orderedVisits[i  + 1];
					if (currentVisit != nextVisit)
					{
						var interval = (nextVisit.DateTime - currentVisit.DateTime).TotalMinutes;
						if (interval >= 1 && interval <= 120)
							timeIntervals.Add(interval);
					}
				}
			}
			return timeIntervals;
		});
		return timeIntervals.Count() > 0 ? ExtensionsTask.Median(timeIntervals) : 0;
	}
}