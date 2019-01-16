using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ThisDate
{
	/// <summary>	A calendar date time. </summary>
	public static partial class CalendarDateTime
	{
		///-------------------------------------------------------------------------------------------------
		/// <summary>	Adds an explicit single date event. </summary>
		///
		/// <remarks>	Adds an explicit single date such as July 18th, 2018 to the calendar. </remarks>
		///
		/// <exception cref="ArgumentNullException">	Thrown when eventName is null or empty. </exception>
		/// <exception cref="ArgumentException">		Thrown when eventName is not unique. </exception>
		///
		/// <param name="eventName">	Name of the event, must be unique. </param>
		/// <param name="dayOff">   	True if is a day off (is a workday). </param>
		/// <param name="date">			The date Date. </param>
		///
		/// <example>
		/// 	<code>
		/// 	CalendarDateTime.AddDateEvent("Party Day", true, new DateTime(2018, 18, 5);
		/// 	// Sets July 18, 2018 as a day off.
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void AddDateEvent([CanBeNull] string eventName, bool dayOff, DateTime date)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentNullException(nameof(eventName), ErrorMessageCannotBeNullOrEmpty(nameof(eventName)));
			}

			if (ContainsEventKey(eventName))
			{
				throw new ArgumentException(ErrorMessageKeyAlreadyAdded(nameof(eventName), eventName), nameof(eventName));
			}

			DateEventsDictionary.Add(eventName, new DateEvent(date, dayOff));
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Adds a monthly date event. </summary>
		///
		/// <remarks>
		/// 	Adds a recurring monthly day of each month. Example: The 8th of each month.
		/// </remarks>
		///
		/// <exception cref="ArgumentNullException">
		/// 	Thrown when eventName is null or empty.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// 	Thrown when eventName is not unique or startDate > endDate.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// 	Thrown when month day is out of range, assumes [1...31] range for unknown month.
		/// </exception>
		///
		/// <param name="eventName">	Name of the event, must be unique. </param>
		/// <param name="dayOff">   	True if is a day off (is a workday). </param>
		/// <param name="monthDay">
		/// 	The month day, assumes [1...31] range. Caution, 29, 30, and 31 days may be problematic.
		/// </param>
		/// <param name="startDate">	(Optional) The date start, DateTime.MinValue if null. </param>
		/// <param name="endDate">  	(Optional) The date end, DateTime.MaxValue if null. </param>
		///
		/// <example>
		/// 	<code>
		/// 	var start = new DateTime(2018, 2, 1);
		/// 	// Electric bill due, starts Feb 1, 2018 and never ends.
		/// 	CalendarDateTime.AddMonthlyDateEvent("Electric Bill Due", false, 8, start);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void AddMonthlyDateEvent([CanBeNull] string eventName, bool dayOff, int monthDay, DateTime? startDate = null, DateTime? endDate = null)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentNullException(nameof(eventName), ErrorMessageCannotBeNullOrEmpty(nameof(eventName)));
			}

			if (ContainsEventKey(eventName))
			{
				throw new ArgumentException(ErrorMessageKeyAlreadyAdded(nameof(eventName), eventName), eventName);
			}

			if (1 > monthDay || monthDay > 31)
			{
				// Although this depends of the month and leap year, this is the best we can do.
				throw new ArgumentOutOfRangeException(ErrorMessageMinMaxOutOfRange(monthDay, nameof(monthDay), 1, 31));
			}

			var dateRange = new MinMaxSwapDate(startDate, endDate);
			MonthlyEventsDictionary.Add(eventName, new MonthlyDayEvent(dayOff, monthDay, dateRange.Min, dateRange.Max));
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Adds a monthly day of week forward (from start of month) event. </summary>
		///
		/// <remarks>
		/// 	Add a monthly repeating n-week, day-of-week event, counting from the start of the month.
		/// 	Example: A meeting that occurs on the second Tuesday of every month.
		/// </remarks>
		///
		/// <exception cref="ArgumentNullException">
		/// 	Thrown when eventName is null or empty.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// 	Thrown when eventName is not unique or startDate > endDate.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// 	Thrown when weeksForward is less han 1.
		/// </exception>
		///
		/// <param name="eventName">   	Name of the event, must be unique. </param>
		/// <param name="dayOff">	   	True if is a day off (is a workday). </param>
		/// <param name="dayOfWeek">   	The day of week. </param>
		/// <param name="weeksForward">	Weeks forward from the start of the month. </param>
		/// <param name="startDate">   	(Optional) The date start, DateTime.MinValue if null. </param>
		/// <param name="endDate">	   	(Optional) The date end, DateTime.MaxValue if null. </param>
		///
		/// <example>
		/// 	<code>
		/// 	CalendarDateTime.AddMonthlyDayOfWeekForwardEvent("Monthly Second Tuesday", false, DayOfWeek.Tuesday, 2);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void AddMonthlyDayOfWeekForwardEvent([CanBeNull] string eventName, bool dayOff, DayOfWeek dayOfWeek, int weeksForward, DateTime? startDate = null, DateTime? endDate = null)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentNullException(nameof(eventName), ErrorMessageCannotBeNullOrEmpty(nameof(eventName)));
			}

			if (ContainsEventKey(eventName))
			{
				throw new ArgumentException(ErrorMessageKeyAlreadyAdded(nameof(eventName), eventName), eventName);
			}

			if (weeksForward < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(weeksForward), ErrorMessageZeroOrNegative(nameof(weeksForward), weeksForward));
			}

			var dateRange = new MinMaxSwapDate(startDate, endDate);
			MonthlyEventsDictionary.Add(eventName, new MonthlyDayOfWeekForwardEvent(dayOff, dayOfWeek, weeksForward, dateRange.Min, dateRange.Max));
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Adds a monthly day of week reverse (from end of month) event. </summary>
		///
		/// <remarks>
		/// 	Add a monthly Day-of-Week, n-weeks reverse from the end of the month. Example, The last
		/// 	Monday of each month. Memorial day (US) follows this pattern (it's not always the 4th
		/// 	Monday of May).
		/// </remarks>
		///
		/// <exception cref="ArgumentNullException">
		/// 	Thrown when eventName is null or empty.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// 	Thrown when eventName is not unique or startDate > endDate.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// 	Thrown when weeksForward is less han 1.
		/// </exception>
		///
		/// <param name="eventName">   	Name of the event, must be unique. </param>
		/// <param name="dayOff">	   	True if is a day off (is a workday). </param>
		/// <param name="dayOfWeek">   	The day of week. </param>
		/// <param name="weeksReverse">	Weeks reverse from the end of the month. </param>
		/// <param name="startDate">   	(Optional) The date start, DateTime.MinValue if null. </param>
		/// <param name="endDate">	   	(Optional) The date end, DateTime.MaxValue if null. </param>
		///
		/// <example>
		/// 	<code>
		/// 	CalendarDateTime.AddMonthlyDayOfWeekReverseEvent("Last Monday Every Month", false, DateTime.Tuesday, 1)
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void AddMonthlyDayOfWeekReverseEvent([CanBeNull] string eventName, bool dayOff, DayOfWeek dayOfWeek, int weeksReverse, DateTime? startDate = null, DateTime? endDate = null)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentNullException(nameof(eventName), ErrorMessageCannotBeNullOrEmpty(nameof(eventName)));
			}

			if (ContainsEventKey(eventName))
			{
				throw new ArgumentException(ErrorMessageKeyAlreadyAdded(nameof(eventName), eventName), eventName);
			}

			if (weeksReverse < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(weeksReverse), ErrorMessageZeroOrNegative(nameof(weeksReverse), weeksReverse));
			}

			var dateRange = new MinMaxSwapDate(startDate, endDate);
			MonthlyEventsDictionary.Add(eventName, new MonthlyDayOfWeekReverseEvent(dayOff, dayOfWeek, weeksReverse, dateRange.Min, dateRange.Max));
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Adds a monthly last day event. </summary>
		///
		/// <remarks>
		/// 	Add a monthly last day of the month. Typically the 30, or 31 of the month 28 or 29 in
		/// 	February.
		/// </remarks>
		///
		/// <exception cref="ArgumentNullException">	Thrown when eventName is null or empty. </exception>
		/// <exception cref="ArgumentException">
		/// 	Thrown when eventName is not unique or startDate >
		/// 	endDate.
		/// </exception>
		///
		/// <param name="eventName">	Name of the event, must be unique. </param>
		/// <param name="dayOff">   	True if is a day off (is a workday). </param>
		/// <param name="startDate">	(Optional) The date start, DateTime.MinValue if null. </param>
		/// <param name="endDate">  	(Optional) The date end, DateTime.MaxValue if null. </param>
		///
		/// <example>
		/// 	<code>
		/// 	CalendarDateTime.AddMonthlyLastDayEvent("Last date of every month", false);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void AddMonthlyLastDayEvent([CanBeNull] string eventName, bool dayOff, DateTime? startDate = null, DateTime? endDate = null)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentNullException(nameof(eventName));
			}

			if (ContainsEventKey(eventName))
			{
				throw new ArgumentException(ErrorMessageKeyAlreadyAdded(nameof(eventName), eventName), eventName);
			}

			var dateRange = new MinMaxSwapDate(startDate, endDate);
			MonthlyEventsDictionary.Add(eventName, new MonthlyLastDayEvent(dayOff, dateRange.Min, dateRange.Max));
		}

		/// -------------------------------------------------------------------------------------------------
		///  <summary>	Adds a weekly event. </summary>
		///
		///  <remarks>
		///  	Add a weekly day-of-week events, can occur on multiple days of the week and at some skip
		///  	interval. The baseDate is the date of the interval. Paydays typically follow this
		///  	pattern. The number of occurrences may vary from month to month because some months are 4
		///  	weeks, some are 5 weeks. Another example; a meeting every two weeks on Tuesdays and
		///  	Thursday within a date range.
		///  </remarks>
		///
		///  <exception cref="ArgumentNullException">
		///  	Thrown when eventName is null or empty.
		///  </exception>
		///  <exception cref="ArgumentException">
		///  	Thrown when eventName is not unique or startDate > endDate.
		///  </exception>
		///  <exception cref="ArgumentOutOfRangeException">
		///  	Thrown when interval is less than one.
		///  </exception>
		/// <param name="eventName"> 	Name of the event, must be unique. </param>
		/// <param name="dayOff">	 	True if is a day off (is a workday). </param>
		/// <param name="daysOfWeek">	The days of week. This may be null. </param>
		/// <param name="seedWeek">
		///     (Optional) (Optional if interval = 1) Seed date in the week of an occurrence.
		/// </param>
		/// <param name="interval">
		///     (Optional) The week skip interval, 1=every week, 2 = every 2 weeks, 3 = 3 weeks...
		/// </param>
		/// <param name="startDate"> 	(Optional) The date start, DateTime.MinValue if null. </param>
		/// <param name="endDate">   	(Optional) The date end, DateTime.MaxValue if null. </param>
		/// -------------------------------------------------------------------------------------------------
		public static void AddWeeklyEvent([CanBeNull] string eventName, bool dayOff,
			[CanBeNull] IEnumerable<DayOfWeek> daysOfWeek, DateTime? seedWeek = null, int interval = 1,
			DateTime? startDate = null, DateTime? endDate = null)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentNullException(nameof(eventName));
			}

			if (ContainsEventKey(eventName))
			{
				throw new ArgumentException(ErrorMessageKeyAlreadyAdded(nameof(eventName), eventName), nameof(eventName));
			}

			daysOfWeek = daysOfWeek?.Distinct().OrderBy(s => s).ToList();
			if (daysOfWeek == null || !daysOfWeek.Any())
			{
				throw new ArgumentNullException(nameof(daysOfWeek), ErrorMessageCannotBeNullOrEmpty(nameof(daysOfWeek)));
			}

			if (interval < 1)
			{
				throw new ArgumentOutOfRangeException(ErrorMessageZeroOrNegative(nameof(interval), interval));
			}

			if (interval > 1 && seedWeek == null)
			{
				throw new ArgumentNullException(nameof(seedWeek), "If interval > 1, then " + nameof(seedWeek) + " cannot be null.");
			}

			var seed = seedWeek ?? DateTime.MinValue;
			var dateRange = new MinMaxSwapDate(startDate, endDate);
			WeeklyEventsDictionary.Add(eventName, new WeeklyEvent(dayOff, interval, daysOfWeek, seed, dateRange.Min, dateRange.Max));
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Adds a weekly in month event where event names are DaysOfWeek.ToString(). </summary>
		///
		/// <remarks>
		/// 	Adds weekly day-of-week events, can occur on multiple days of the week and at some
		/// 	interval by week. If the interval is null, all weeks [1...5.] are added, the 5th week
		/// 	does not occur every month. Its possible to set an odd pattern such as 1rst, 2nd week of
		/// 	every month.
		/// </remarks>
		///
		/// <exception cref="ArgumentNullException">
		/// 	Thrown when eventName is null or empty.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// 	Thrown when eventName is not unique or startDate > endDate.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// 	Thrown when interval is less than 1 or greater than 5.
		/// </exception>
		///
		/// <param name="dayOff">   	True if is a day off (is a workday). </param>
		/// <param name="dayOfWeek">	The day of week. </param>
		/// <param name="weekIntervals">	(Optional) The weekly intervals, If null every week, 1: 1 first week, 2: second week, 3: third week... Not every month will have a fifth week. </param>
		/// <param name="startDate">	(Optional) The date start, DateTime.MinValue if null. </param>
		/// <param name="endDate">  	(Optional) The date end, DateTime.MaxValue if null. </param>
		///
		/// <example>
		/// 	<code>
		/// 	// Mondays, first and third week.
		/// 	var weekIntervals = new int[] {1, 3};
		/// 	CalendarDateTime.AddWeeklyInMonthEvent(false, DayOfWeek.Monday, weekIntervals);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------

		public static void AddWeeklyInMonthEvent(bool dayOff, DayOfWeek dayOfWeek, [CanBeNull] IEnumerable<int> weekIntervals = null, DateTime? startDate = null, DateTime? endDate = null)
		{
			AddWeeklyInMonthEvent(dayOff.ToString(), dayOff, dayOfWeek, weekIntervals, startDate, endDate);
		}
		
		///-------------------------------------------------------------------------------------------------
		/// <summary>	Adds a weekly in month event. </summary>
		///
		/// <remarks>
		/// 	Adds weekly day-of-week events, can occur on multiple days of the week and at some
		/// 	interval by week. If the interval is null, all weeks [1...5.] are added, the 5th week
		/// 	does not occur every month. Its possible to set an odd pattern such as 1rst, 2nd week of
		/// 	every month.
		/// </remarks>
		///
		/// <exception cref="ArgumentNullException">
		/// 	Thrown when eventName is null or empty.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// 	Thrown when eventName is not unique or startDate > endDate.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// 	Thrown when interval is less than 1 or greater than 5.
		/// </exception>
		///
		/// <param name="eventName">	Name of the event, must be unique. </param>
		/// <param name="dayOff">   	True if is a day off (is a workday). </param>
		/// <param name="dayOfWeek">	The day of week. </param>
		/// <param name="weekIntervals">	(Optional) The weekly intervals, If null every week, 1: 1 first week, 2: second week, 3: third week... Not every month will have a fifth week. </param>
		/// <param name="startDate">	(Optional) The date start, DateTime.MinValue if null. </param>
		/// <param name="endDate">  	(Optional) The date end, DateTime.MaxValue if null. </param>
		///
		/// <example>
		/// 	<code>
		/// 	// Mondays, first and third week.
		/// 	var weekIntervals = new int[] {1, 3};
		/// 	CalendarDateTime.AddWeeklyInMonthEvent("Backups", false, DayOfWeek.Monday, weekIntervals);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void AddWeeklyInMonthEvent([CanBeNull] string eventName, bool dayOff, DayOfWeek dayOfWeek, [CanBeNull] IEnumerable<int> weekIntervals = null, DateTime? startDate = null, DateTime? endDate = null)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentNullException(nameof(eventName));
			}

			if (ContainsEventKey(eventName))
			{
				throw new ArgumentException(ErrorMessageKeyAlreadyAdded(nameof(eventName), eventName), nameof(eventName));
			}

			const int minWeek = 1;
			const int maxWeek = 5;
			weekIntervals = weekIntervals?.Distinct().OrderBy(s => s).ToList() ?? Enumerable.Range(minWeek, maxWeek).ToList();
			if (minWeek > weekIntervals.Min())
			{
				throw new ArgumentOutOfRangeException(nameof(weekIntervals), ErrorMessageMinMaxOutOfRange(weekIntervals.Min(), nameof(weekIntervals), minWeek, maxWeek));
			}

			if (weekIntervals.Max() > maxWeek)
			{
				throw new ArgumentOutOfRangeException(nameof(weekIntervals), ErrorMessageMinMaxOutOfRange(weekIntervals.Max(), nameof(weekIntervals), minWeek, maxWeek));
			}

			var dateRange = new MinMaxSwapDate(startDate, endDate);
			WeeklyEventsDictionary.Add(eventName, new WeeklyInMonthEvent(dayOff, dayOfWeek, weekIntervals, dateRange.Min, dateRange.Max));
		}



		///-------------------------------------------------------------------------------------------------
		/// <summary>	Adds a yearly calculated event. </summary>
		///
		/// <remarks>
		/// 	Selects events what are mathematically derived, Easter Sunday and Good Friday.
		/// </remarks>
		///
		/// <exception cref="ArgumentNullException">	Thrown when eventName is null or empty. </exception>
		/// <exception cref="ArgumentException">
		/// 	Thrown when eventName is not either "EasterSunday" or "GoodFriday" or startDate >
		/// 	endDate.
		/// </exception>
		///
		/// <param name="eventName">	Name of the event, must be unique. </param>
		/// <param name="dayOff">   	True if is a day off (is a workday). </param>
		/// <param name="startDate">	(Optional) The date start, DateTime.MinValue if null. </param>
		/// <param name="endDate">  	(Optional) The date end, DateTime.MaxValue if null. </param>
		///
		/// <example>
		/// 	<code>
		/// 	// Note: Properties ending in "Text" (CalculatedEventsText.EasterSunday) are provided as convenience and to improve robustness.
		/// 	CalendarDateTime.AddYearlyCalculatedEvent(CalculatedEventsText.EasterSunday, true);
		/// 	CalendarDateTime.AddYearlyCalculatedEvent(CalculatedEventsText.GoodFriday, true);
		/// 	// Test:
		/// 	var easterDate2018 = new DateTime(2018, 4, 1);
		/// 	var result = easterDate2018.EventsOnDate(false, true);
		/// 	// [0] = "Easter Sunday"
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void AddYearlyCalculatedEvent([CanBeNull] string eventName, bool dayOff, DateTime? startDate = null, DateTime? endDate = null)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentNullException(nameof(eventName), ErrorMessageCannotBeNullOrEmpty(nameof(eventName)));
			}

			if (ContainsEventKey(eventName))
			{
				throw new ArgumentException(ErrorMessageKeyAlreadyAdded(nameof(eventName), eventName), nameof(eventName));
			}

			var dateRange = new MinMaxSwapDate(startDate, endDate);
			if (eventName == CalculatedEventsText.GoodFriday)
			{
				YearlyEventsDictionary.Add(CalculatedEventsText.GoodFriday, new YearlyCalculatedEvent(dayOff, GoodFridayDate, dateRange.Min, dateRange.Max));
			}
			else if (eventName == CalculatedEventsText.EasterSunday)
			{
				YearlyEventsDictionary.Add(CalculatedEventsText.EasterSunday, new YearlyCalculatedEvent(dayOff, EasterSundayDate, dateRange.Min, dateRange.Max));
			}
			else
			{
				throw new ArgumentException(ErrorMessageEasterSundayGoodFriday(nameof(eventName), eventName));
			}
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Adds a yearly date event. </summary>
		///
		/// <remarks>
		/// 	Selects yearly event on certain day of the month. If true, saturdayBack will shift
		/// 	celebration to Friday. If true, sundayForward will shift the celebration forward. New
		/// 	Years day is often shifted around the weekends.
		/// </remarks>
		///
		/// <exception cref="ArgumentNullException">
		/// 	Thrown when eventName is null or empty.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// 	Thrown when eventName is not unique or startDate > endDate.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// 	Thrown when month is out of range [1...12] or when day is out of range for the given
		/// 	month.
		/// </exception>
		///
		/// <param name="eventName">		Name of the event, must be unique. </param>
		/// <param name="dayOff">			True if is a day off (is a workday). </param>
		/// <param name="month">			The month. </param>
		/// <param name="day">				The day. </param>
		/// <param name="saturdayBack"> 	True to shift back to Friday if on Saturday. </param>
		/// <param name="sundayForward">	True to shift forward to Monday if on Sunday. </param>
		/// <param name="startDate">		(Optional) The start date. </param>
		/// <param name="endDate">			(Optional) The end date. </param>
		///
		/// <example>
		/// 	<code>
		/// 	// New Year's day, Jan 1, if on Saturday do not shift, if on Sunday then celebrate on Monday.
		/// 	CalendarDateTime("New Years Day", true, 1, 1, false, true);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void AddYearlyDateEvent([CanBeNull] string eventName, bool dayOff, int month, int day, bool saturdayBack, bool sundayForward, DateTime? startDate = null, DateTime? endDate = null)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentNullException(nameof(eventName), ErrorMessageCannotBeNullOrEmpty(nameof(eventName)));
			}

			if (ContainsEventKey(eventName))
			{
				throw new ArgumentException(ErrorMessageKeyAlreadyAdded(nameof(eventName), eventName), eventName);
			}

			if (month < 1 || 12 < month)
			{
				throw new ArgumentOutOfRangeException(nameof(month), ErrorMessageMinMaxOutOfRange(month, nameof(month), 1, 12));
			}

			if (day < 1 || day > MaxDaysInMonthEstimated(month))
			{
				throw new ArgumentOutOfRangeException(nameof(day), ErrorMessageMinMaxOutOfRange(day, nameof(day), 1, MaxDaysInMonthEstimated(month)));
			}

			var dateRange = new MinMaxSwapDate(startDate, endDate);
			YearlyEventsDictionary.Add(eventName, new YearlyMonthDatedEvent(dayOff, month, day, saturdayBack, sundayForward, dateRange.Min, dateRange.Max));
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Adds a yearly day of week forward event. </summary>
		///
		/// <remarks>
		/// 	Selects events yearly events that occur on a certain day of the week going forward from
		/// 	the start of the month. Example: Martin Luther King day occurs on the second Monday in
		/// 	January.
		/// </remarks>
		///
		/// <exception cref="ArgumentNullException">
		/// 	Thrown when eventName is null or empty.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// 	Thrown when eventName is not unique or startDate > endDate.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// 	Thrown when month is out of range [1...12] or when day is out of range for the given
		/// 	month.
		/// </exception>
		///
		/// <param name="eventName">   	Name of the event, must be unique. </param>
		/// <param name="dayOff">	   	True if is a day off (is a workday). </param>
		/// <param name="month">	   	The month. </param>
		/// <param name="weeksForward">	Weeks forward from the start of the month. </param>
		/// <param name="dayOfWeek">   	The day of week. </param>
		/// <param name="startDate">   	(Optional) The date start, DateTime.MinValue if null. </param>
		/// <param name="endDate">	   	(Optional) The date end, DateTime.MaxValue if null. </param>
		///
		/// <example>
		/// 	<code>
		/// 	// Thanksgiving is on the 4th Thursday (US) in November.
		/// 	CalendarDateTime.AddYearlyDayOfWeekForwardEvent("Thanksgiving", true, 11, 4);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void AddYearlyDayOfWeekForwardEvent([CanBeNull] string eventName, bool dayOff, int month, int weeksForward, DayOfWeek dayOfWeek, DateTime? startDate = null, DateTime? endDate = null)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentNullException(nameof(eventName), ErrorMessageCannotBeNullOrEmpty(nameof(eventName)));
			}

			if (ContainsEventKey(eventName))
			{
				throw new ArgumentException(ErrorMessageKeyAlreadyAdded(nameof(eventName), eventName), nameof(eventName));
			}

			if (1 > month || month > 12)
			{
				throw new ArgumentOutOfRangeException(nameof(month), ErrorMessageMinMaxOutOfRange(month, nameof(month), 1, 12));
			}

			if (weeksForward < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(weeksForward), ErrorMessageZeroOrNegative(nameof(weeksForward), weeksForward));
			}

			var dateRange = new MinMaxSwapDate(startDate, endDate);
			YearlyEventsDictionary.Add(eventName, new YearlyDayOfWeekForwardEvent(dayOff, month, dayOfWeek, weeksForward, dateRange.Min, dateRange.Max));
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Adds a yearly day of week reverse event. </summary>
		///
		/// <remarks>
		/// 	Selects events yearly events that occur on a certain day of the week, and week going
		/// 	reverse from the end of the month. Example: Memorial day is on the last Monday of May.
		/// </remarks>
		///
		/// <exception cref="ArgumentNullException">
		/// 	Thrown when eventName is null or empty.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// 	Thrown when eventName is not unique or startDate > endDate.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// 	Thrown when month is out of range [1...12] or when day is out of range for the given
		/// 	month.
		/// </exception>
		///
		/// <param name="eventName">   	Name of the event, must be unique. </param>
		/// <param name="dayOff">	   	True if is a day off (is a workday). </param>
		/// <param name="month">	   	The month. </param>
		/// <param name="weeksReverse">	Weeks reverse from the end of the month. </param>
		/// <param name="dayOfWeek">   	The day of week. </param>
		/// <param name="startDate">   	(Optional) The date start, DateTime.MinValue if null. </param>
		/// <param name="endDate">	   	(Optional) The date end, DateTime.MaxValue if null. </param>
		///
		/// <example>
		/// 	<code>
		/// 	// Labor Day is the last Monday in May (US), usually the 4 week but not always.
		/// 	CalendarDateTime.AddYearlyDayOfWeekReverseEvent("Labor Day", true, 1, DayOfWeek.Monday);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void AddYearlyDayOfWeekReverseEvent([CanBeNull] string eventName, bool dayOff, int month, int weeksReverse, DayOfWeek dayOfWeek, DateTime? startDate = null, DateTime? endDate = null)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentNullException(nameof(eventName), ErrorMessageCannotBeNullOrEmpty(nameof(eventName)));
			}

			if (ContainsEventKey(eventName))
			{
				throw new ArgumentException(ErrorMessageKeyAlreadyAdded(nameof(eventName), eventName), nameof(eventName));
			}

			if (1 > month || month > 12)
			{
				throw new ArgumentOutOfRangeException(nameof(month), ErrorMessageMinMaxOutOfRange(month, nameof(month), 1, 12));
			}

			if (weeksReverse < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(weeksReverse), ErrorMessageZeroOrNegative(nameof(weeksReverse), weeksReverse));
			}

			var dateRange = new MinMaxSwapDate(startDate, endDate);
			YearlyEventsDictionary.Add(eventName, new YearlyDayOfWeekReverseEvent(dayOff, month, dayOfWeek, weeksReverse, dateRange.Min, dateRange.Max));
		}
	}
}