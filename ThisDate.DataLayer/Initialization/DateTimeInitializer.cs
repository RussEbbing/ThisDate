using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using JetBrains.Annotations;
using ThisDate.DateTimeDataLayer.Context;
using ThisDate.DateTimeDataLayer.InstanceConstructors;
using ThisDate.DateTimeDataLayer.Models;

namespace ThisDate.DateTimeDataLayer.Initialization
{
	/// <summary>	Data date/time dimension database initializer methods. </summary>
	[PublicAPI]
	public static class DateTimePopulation
	{
		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	Populates full year of dates missing between January 1 start-year to December 31 end-
		/// 	year. If the year is found, the year is skipped. Method assumes full year, if a year
		/// 	exists this year is skipped.
		/// </summary>
		///
		/// <exception cref="ArgumentOutOfRangeException">	Years are out of range. </exception>
		/// <exception cref="ArgumentException">		  	if startYear &gt; endYear. </exception>
		///
		/// <param name="startYear">	The start year. </param>
		/// <param name="endYear">  	The end year. </param>
		///
		/// <returns>	An asynchronous result. </returns>
		///-------------------------------------------------------------------------------------------------
		public static async Task PopulateDateDimensionAsync(int startYear, int endYear)
		{
			if (startYear < DateTime.MinValue.Year || startYear > DateTime.MaxValue.Year)
			{
				throw new ArgumentOutOfRangeException(nameof(startYear));
			}

			if (endYear < DateTime.MinValue.Year || endYear > DateTime.MaxValue.Year)
			{
				throw new ArgumentOutOfRangeException(nameof(endYear));
			}

			if (startYear > endYear)
			{
				throw new ArgumentException(nameof(startYear) + " is greater than " + nameof(endYear));
			}

			using (var db = new DateTimeDimensionContext())
			{
				var containYears = db.DateDimension.Select(s => s.Year).Distinct();
				var missingYears = Enumerable.Range(startYear, endYear - startYear + 1).Except(containYears).ToList();
				if (!missingYears.Any())
				{
					return;
				}

				//Calendars.NewYorkStockExchange();
				var dateInstances = new List<DateDimension>();
				foreach (var year in missingYears)
				{
					var startDate = new DateTime(year, 1, 1);
					var endDate = new DateTime(year, 12, 31);
					for (var currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
					{
						var dateInstance = DateTimeDimension.DateDimensionInstance(currentDate);
						dateInstances.Add(dateInstance);
					}
				}

				using (var transaction = db.Database.BeginTransaction())
				{
					await db.DateDimension.AddRangeAsync(dateInstances).ConfigureAwait(false);
					await db.BulkInsertAsync(dateInstances).ConfigureAwait(false);
					transaction.Commit();
				}
			}
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	Populate the time dimension at given time interval if the data has not yet been populated.
		/// </summary>
		///
		/// <exception cref="ArgumentException">		  	Time span cannot be zero. </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// 	The parameters specify a <see cref="T:System.TimeSpan"/> value less than
		/// 	<see cref="F:System.TimeSpan.MinValue"/> or greater than
		/// 	<see cref="F:System.TimeSpan.MaxValue"/>.
		/// </exception>
		///
		/// <param name="incrementTimeSpan">	The increment time span. </param>
		///
		/// <returns>	An asynchronous result. </returns>
		///-------------------------------------------------------------------------------------------------
		public static async Task PopulateTimeDimensionAsync(TimeSpan incrementTimeSpan)
		{
			if (incrementTimeSpan <= TimeSpan.Zero)
			{
				throw new ArgumentException("Time span, " + nameof(incrementTimeSpan) + " must be greater than zero.");
			}

			using (var db = new DateTimeDimensionContext())
			{
				if (db.TimeDimension.Any())
				{
					return;
				}

				var timeInstances = new List<TimeDimension>();
				var start = new DateTime();
				var end = start.AddDays(1);
				for (var currentTime = start; currentTime < end; currentTime += incrementTimeSpan)
				{
					var timeInstance = DateTimeDimension.TimeDimensionInstance(currentTime);
					timeInstances.Add(timeInstance);
				}

				using (var transaction = db.Database.BeginTransaction())
				{
					db.TimeDimension.AddRange(timeInstances);
					await db.BulkInsertAsync(timeInstances).ConfigureAwait(false);
					transaction.Commit();
				}
			}
		}
	}
}