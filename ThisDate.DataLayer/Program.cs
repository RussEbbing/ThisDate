using System;
using ThisDate.DateTimeDataLayer.Initialization;
using ThisDate.DefinedCalendars.USA;

namespace ThisDate.DateTimeDataLayer
{
	internal class Program
	{
		private static void InitializeDatabaseAsync()
		{
			// Define the calendar to use.
			Calendars.NewYorkStockExchange();

			// Time interval.
			var everyMinute = new TimeSpan(0, 0, 0, 1);
			DateTimePopulation.PopulateTimeDimensionAsync(everyMinute).Wait();

			var startYear = DateTime.Now.Year;
			var endYear = DateTime.Now.AddMonths(48).Year;
			DateTimePopulation.PopulateDateDimensionAsync(startYear, endYear).Wait();
		}

		private static void Main()
		{
			InitializeDatabaseAsync();
		}
	}
}