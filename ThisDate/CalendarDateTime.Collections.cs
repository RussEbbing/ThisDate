using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ThisDate
{
	/// <summary>	A calendar date time. </summary>
	public static partial class CalendarDateTime
	{
		/// <summary>	Dictionary of date events. </summary>
		[NotNull] private static readonly Dictionary<string, IDateEvent> DateEventsDictionary = new Dictionary<string, IDateEvent>(StringComparer.InvariantCultureIgnoreCase);

		/// <summary>	Dictionary of monthly events. </summary>
		[NotNull] private static readonly Dictionary<string, IMonthlyEvent> MonthlyEventsDictionary = new Dictionary<string, IMonthlyEvent>(StringComparer.InvariantCultureIgnoreCase);

		/// <summary>	Dictionary of weekly events. </summary>
		[NotNull] private static readonly Dictionary<string, IWeeklyEvent> WeeklyEventsDictionary = new Dictionary<string, IWeeklyEvent>(StringComparer.InvariantCultureIgnoreCase);

		/// <summary>	Dictionary of yearly events. </summary>
		[NotNull] private static readonly Dictionary<string, IYearlyEvent> YearlyEventsDictionary = new Dictionary<string, IYearlyEvent>(StringComparer.InvariantCultureIgnoreCase);

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets the total number of date events. </summary>
		///
		/// <value>	The total number of date events. </value>
		///-------------------------------------------------------------------------------------------------
		public static int CountDateEvents => DateEventsDictionary.Count;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets the total number of events. </summary>
		///
		/// <value>	The total number of events. </value>
		///-------------------------------------------------------------------------------------------------
		public static int CountEvents => YearlyEventsDictionary.Count + MonthlyEventsDictionary.Count + WeeklyEventsDictionary.Count + DateEventsDictionary.Count;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets the total number of monthly events. </summary>
		///
		/// <value>	The total number of monthly events. </value>
		///-------------------------------------------------------------------------------------------------
		public static int CountMonthlyEvents => MonthlyEventsDictionary.Count;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets the total number of weekly events. </summary>
		///
		/// <value>	The total number of weekly events. </value>
		///-------------------------------------------------------------------------------------------------
		public static int CountWeeklyEvents => WeeklyEventsDictionary.Count;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets the total number of yearly events. </summary>
		///
		/// <value>	The total number of yearly events. </value>
		///-------------------------------------------------------------------------------------------------
		public static int CountYearlyEvents => YearlyEventsDictionary.Count;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets the keys date events. </summary>
		///
		/// <value>	The keys date events. </value>
		///-------------------------------------------------------------------------------------------------
		public static ImmutableArray<string> KeysDateEvents => DateEventsDictionary.Keys.ToImmutableArray();

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets the keys events. </summary>
		///
		/// <value>	The keys events. </value>
		///-------------------------------------------------------------------------------------------------
		public static ImmutableArray<string> KeysEvents
		{
			get
			{
				var keys = new List<string>();
				keys.AddRange(YearlyEventsDictionary.Keys);
				keys.AddRange(MonthlyEventsDictionary.Keys);
				keys.AddRange(WeeklyEventsDictionary.Keys);
				keys.AddRange(DateEventsDictionary.Keys);
				return keys.ToImmutableArray();
			}
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets the keys monthly events. </summary>
		///
		/// <value>	The keys monthly events. </value>
		///-------------------------------------------------------------------------------------------------
		public static ImmutableArray<string> KeysMonthlyEvents => MonthlyEventsDictionary.Keys.ToImmutableArray();

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets the keys weekly events. </summary>
		///
		/// <value>	The keys weekly events. </value>
		///-------------------------------------------------------------------------------------------------
		public static ImmutableArray<string> KeysWeeklyEvents => WeeklyEventsDictionary.Keys.ToImmutableArray();

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets the keys yearly events. </summary>
		///
		/// <value>	The keys yearly events. </value>
		///-------------------------------------------------------------------------------------------------
		public static ImmutableArray<string> KeysYearlyEvents => YearlyEventsDictionary.Keys.ToImmutableArray();

		/// <summary>	Clears the event calendar. </summary>
		public static void ClearCalendar()
		{
			YearlyEventsDictionary.Clear();
			MonthlyEventsDictionary.Clear();
			WeeklyEventsDictionary.Clear();
			DateEventsDictionary.Clear();
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Query if 'eventName' contains event key. </summary>
		///
		/// <param name="eventName">	Name of the event. This may be null. </param>
		///
		/// <returns>	True if it succeeds, false if it fails. </returns>
		///-------------------------------------------------------------------------------------------------
		public static bool ContainsEventKey([CanBeNull] string eventName)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				return false;
			}

			return YearlyEventsDictionary.ContainsKey(eventName)
				   || MonthlyEventsDictionary.ContainsKey(eventName)
				   || WeeklyEventsDictionary.ContainsKey(eventName)
				   || DateEventsDictionary.ContainsKey(eventName);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Extension method that gets event dates between date ranges. </summary>
		///
		/// <param name="eventName">	Name of the event. </param>
		/// <param name="date1">		(Optional) The first date, DateTime.MinValue if null. </param>
		/// <param name="date2">		(Optional) The second date, DateTime.MaxValue if null. </param>
		///
		/// <returns>	An ImmutableArray&lt;DateTime&gt; </returns>
		///-------------------------------------------------------------------------------------------------
		public static ImmutableArray<DateTime> EventDatesBetween([CanBeNull] this string eventName, DateTime? date1 = null, DateTime? date2 = null)
		{
			var result = new List<DateTime>();
			var dateRange = new MinMaxSwapDate(date1, date2);

			if (string.IsNullOrEmpty(eventName))
			{
			}
			else if (YearlyEventsDictionary.ContainsKey(eventName))
			{
				result = YearlyEventsDictionary[eventName].EventDatesBetween(dateRange.Min, dateRange.Max);
			}
			else if (MonthlyEventsDictionary.ContainsKey(eventName))
			{
				result = MonthlyEventsDictionary[eventName].EventDatesBetween(dateRange.Min, dateRange.Max);
			}
			else if (WeeklyEventsDictionary.ContainsKey(eventName))
			{
				result = WeeklyEventsDictionary[eventName].EventDatesBetween(dateRange.Min, dateRange.Max);
			}
			else if (DateEventsDictionary.ContainsKey(eventName))
			{
				// Although this path will only return an empty set, or one date. It is provided
				// for consistency with all the other code paths.
				var date = DateEventsDictionary[eventName].Date;
				if (date.IsBetweenEqual(dateRange.Min, dateRange.Max))
				{
					result = new List<DateTime> { date };
				}
			}

			return result.ToImmutableArray();
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Extension method that gets event dates between date ranges. </summary>
		///
		/// <exception cref="ArgumentOutOfRangeException">
		/// 	Thrown if either year1 or year2 are out of DateTime year min or max.
		/// </exception>
		///
		/// <param name="eventName">	Name of the event. This may be null. </param>
		/// <param name="year1">		(optional) The first year, DateTime.MinValue.Year if null. </param>
		/// <param name="year2">		(optional) The second year, DateTime.MaxValue.Year if null. </param>
		///
		/// <returns>	An ImmutableArray&lt;DateTime&gt; </returns>
		///-------------------------------------------------------------------------------------------------
		public static ImmutableArray<DateTime> EventDatesBetween([CanBeNull] this string eventName, int? year1, int? year2)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				return new ImmutableArray<DateTime>();
			}

			if (DateTime.MinValue.Year > year1 || year1 > DateTime.MaxValue.Year)
			{
				var message = ErrorMessageMinMaxOutOfRange((int)year1, nameof(year1), DateTime.MinValue.Year, DateTime.MaxValue.Year);
				throw new ArgumentOutOfRangeException(nameof(year1), year1, message);
			}

			if (DateTime.MinValue.Year > year2 || year2 > DateTime.MaxValue.Year)
			{
				var message = ErrorMessageMinMaxOutOfRange((int)year2, nameof(year2), DateTime.MinValue.Year, DateTime.MaxValue.Year);
				throw new ArgumentOutOfRangeException(nameof(year2), year2, message);
			}

			if (year1 > year2)
			{
				var t = year1;
				year1 = year2;
				year2 = t;
			}

			DateTime? date1 = null;
			if (year1 != null)
			{
				date1 = new DateTime((int)year1, 1, 1);
			}

			DateTime? date2 = null;
			if (year2 != null)
			{
				date2 = new DateTime((int)year2, 12, 31);
			}

			return eventName.EventDatesBetween(date1, date2);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Removes the event described by eventName. </summary>
		///
		/// <param name="eventName">	Name of the event. This may be null. </param>
		///
		/// <returns>	True if it succeeds, false if it fails. </returns>
		///-------------------------------------------------------------------------------------------------
		public static bool RemoveEvent([CanBeNull] string eventName)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				return false;
			}

			return YearlyEventsDictionary.Remove(eventName)
				   || MonthlyEventsDictionary.Remove(eventName)
				   || WeeklyEventsDictionary.Remove(eventName)
				   || DateEventsDictionary.Remove(eventName);
		}
	}
}