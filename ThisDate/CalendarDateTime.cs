using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;

namespace ThisDate
{
	/// <summary>	Yearly, monthly, weekly Calendar, Date, and Time extensions. </summary>
	[PublicAPI]
	public static partial class CalendarDateTime
	{
		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	DateTime extension method that calculates (+/-) n-workdays from date. Workdays are
		/// 	defined by the calendar configuration.
		/// </summary>
		///
		/// <remarks>
		/// 	Workdays are defined by calendar 'Add-' configuration methods, isWorkday define what days
		/// 	are skipped.
		/// </remarks>
		///
		/// <param name="date">	The date. </param>
		/// <param name="days">	Workdays to +add / -subtract. </param>
		///
		/// <returns>	A DateTime. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	// 30 work days forward, skipping non-workdays in the calendar.
		/// 	var someDate = aDate.AddWorkdays(30);
		///
		/// 	// 30 work days back, skipping non-workdays in the calendar.
		/// 	var backDate = aDate.AddWorkdays(-30);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static DateTime AddWorkdays(this DateTime date, int days)
		{
			var sign = days < 0 ? -1 : 1;
			var unsignedDays = Math.Abs(days);
			var weekdaysAdded = 0;
			while (weekdaysAdded < unsignedDays)
			{
				date = date.AddDays(sign);
				if (IsWorkDay(date))
				{
					weekdaysAdded++;
				}
			}

			return date;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	date as "yyyyMMdd", typically used as dateId key on date dimension tables. </summary>
		///
		/// <remarks>	2018/1/1 (1/1/2018) => 20180101. </remarks>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	An int representing a date as yyyyMMdd. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var aDate = new DateTime(2018, 5, 8);
		/// 	var result = aDate.DateId();
		/// 	// result = 20180508
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static int DateId(this DateTime date) => Convert.ToInt32(date.ToString("yyyyMMdd"));

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	DateTime extension method returns the day-of-week count in month. Example: Feb 12, 2019
		/// 	is the second Tuesday of the month.
		/// </summary>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	An int. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var aDate = new DateTime(2018, 5, 8);
		/// 	var result = aDate.DayOfWeekCountInMonth();
		/// 	// result = 2
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static int DayOfWeekCountInMonth(this DateTime date)
		{
			var firstDayOfWeek = DayOfWeekMonthForward(date.Year, date.Month, date.DayOfWeek, 1);
			var result = (date.Day - firstDayOfWeek.Day) / 7 + 1;
			return result;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	DateTime extension method returns week count of the year. </summary>
		///
		/// <remarks>
		/// 	Example: Feb 4, 2019 is 5 Mondays into the year and Feb 5, 2019 is 6 Tuesdays into the
		/// 	year.
		/// </remarks>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	An int. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var aDate = new DateTime(2019, 2, 5);
		/// 	var result = aDate.DayOfWeekCountInYear();
		/// 	// result = 6
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static int DayOfWeekCountInYear(this DateTime date)
		{
			var firstDayOfWeek = DayOfWeekMonthForward(date.Year, 1, date.DayOfWeek, 1);
			var result = (date - firstDayOfWeek).Days / 7 + 1;
			return result;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Returns the n-th Day-of-week from the start of the month going forward. </summary>
		///
		/// <remarks>
		/// 	Use this method to return a day-of-week date count into the month. Example, 2th Tuesday
		/// 	of 2018 is February 13, 2018.
		/// </remarks>
		///
		/// <exception cref="ArgumentOutOfRangeException">
		/// 	Thrown when DateTime.Year is out of bounds and if Month is out of bounds.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// 	Thrown when weeksForwards is less than 1.
		/// </exception>
		///
		/// <param name="year">		   	The year. </param>
		/// <param name="month">	   	The month. </param>
		/// <param name="dayOfWeek">   	The day-of-week. </param>
		/// <param name="weeksForward">	The weeks forward. </param>
		///
		/// <returns>	A DateTime. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var result = CalendarDateTime.DayOfWeekMonthForward(2018, 2, DayOfWeek.Tuesday, 2);
		/// 	// result = {2/13/2018 12:00:00 AM}
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static DateTime DayOfWeekMonthForward(int year, int month, DayOfWeek dayOfWeek, int weeksForward)
		{
			if (year < DateTime.MinValue.Year || year > DateTime.MaxValue.Year)
			{
				throw new ArgumentOutOfRangeException(nameof(year), ErrorMessageMinMaxOutOfRange(year, nameof(year), DateTime.MinValue.Year, DateTime.MaxValue.Year));
			}

			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException(nameof(month), ErrorMessageMinMaxOutOfRange(month, nameof(month), 1, 12));
			}

			if (weeksForward < 1)
			{
				throw new ArgumentException(ErrorMessageZeroOrNegative(nameof(weeksForward), weeksForward));
			}

			var firstDayOfMonth = new DateTime(year, month, 1);
			var offset = firstDayOfMonth.DayOfWeek <= dayOfWeek
				? dayOfWeek - firstDayOfMonth.DayOfWeek
				: 7 - (firstDayOfMonth.DayOfWeek - dayOfWeek);
			var firstDayOfWeek = firstDayOfMonth.AddDays(offset);
			var nthDayOfWeekDate = firstDayOfWeek.AddDays(7 * (weeksForward - 1));
			return nthDayOfWeekDate;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Returns the nth Day-of-week from the end of the month going reverse. </summary>
		///
		/// <remarks>
		/// 	Use this method to return a day-of-week date count from the end of the month. Example,
		/// 	2th February Tuesday from the end of the month in 2018, is February 20, 2018.
		/// </remarks>
		///
		/// <exception cref="ArgumentOutOfRangeException">
		/// 	Thrown when DateTime.Year is out of bounds and if Month is out of bounds.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// 	Thrown when weeksForwards is less than 1.
		/// </exception>
		///
		/// <param name="year">		   	The year. </param>
		/// <param name="month">	   	The month. </param>
		/// <param name="dayOfWeek">   	The day-of-week. </param>
		/// <param name="weeksReverse">	The weeks forward. </param>
		///
		/// <returns>	A DateTime. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var result = CalendarDateTime.DayOfWeekMonthReverse(2018, 2, DayOfWeek.Tuesday, 2);
		/// 	// result = {2/20/2018 12:00:00 AM} </code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static DateTime DayOfWeekMonthReverse(int year, int month, DayOfWeek dayOfWeek, int weeksReverse)
		{
			if (year < DateTime.MinValue.Year || year > DateTime.MaxValue.Year)
			{
				throw new ArgumentOutOfRangeException(nameof(year), ErrorMessageMinMaxOutOfRange(year, nameof(year), DateTime.MinValue.Year, DateTime.MaxValue.Year));
			}

			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException(nameof(month), ErrorMessageMinMaxOutOfRange(month, nameof(month), 1, 12));
			}

			if (weeksReverse < 1)
			{
				throw new ArgumentException(ErrorMessageZeroOrNegative(nameof(weeksReverse), weeksReverse));
			}

			var lastDayOfMonth = LastDateOfMonth(year, month);
			var offset = lastDayOfMonth.DayOfWeek >= dayOfWeek
				? dayOfWeek - lastDayOfMonth.DayOfWeek
				: -7 + (dayOfWeek - lastDayOfMonth.DayOfWeek);
			var lastDayOfWeek = lastDayOfMonth.AddDays(offset);
			var nthDayOfWeekDate = lastDayOfWeek.AddDays(-7 * (weeksReverse - 1));
			return nthDayOfWeekDate;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Shift to target day-of-week from date within the week. </summary>
		///
		/// <remarks>	Target day-of-week is always in the same week of the date. </remarks>
		///
		/// <param name="date">  	The date. </param>
		/// <param name="target">	Target for day-of-week. </param>
		///
		/// <returns>	A DateTime. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var date = new DateTime(2018, 5, 2);                        // date.DayOfWeek = Wednesday
		///
		/// 	var resultSun = date.DayOfWeekShift(DayOfWeek.Sunday);
		/// 	// resultSun = {4/29/2018 12:00:00 AM}
		///
		/// 	var resultMon = date.DayOfWeekShift(DayOfWeek.Monday);
		/// 	// resultMon = {4/30/2018 12:00:00 AM}
		///
		/// 	var resultTue = date.DayOfWeekShift(DayOfWeek.Tuesday);
		/// 	// resultTue = {5/1/2018 12:00:00 AM}
		///
		/// 	var resultWed = date.DayOfWeekShift(DayOfWeek.Wednesday);
		/// 	// resultWed = {5/2/2018 12:00:00 AM}
		///
		/// 	var resultThu = date.DayOfWeekShift(DayOfWeek.Thursday);
		/// 	// resultThu = {5/3/2018 12:00:00 AM}
		///
		/// 	var resultFri = date.DayOfWeekShift(DayOfWeek.Friday);
		/// 	// resultFri = {4/4/2018 12:00:00 AM}
		///
		/// 	var resultSat = date.DayOfWeekShift(DayOfWeek.Saturday);
		/// 	// resultSat = {5/5/2018 12:00:00 AM}
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static DateTime DayOfWeekShift(this DateTime date, DayOfWeek target)
		{
			var shift = (int)target - (int)date.DayOfWeek;
			return date.AddDays(shift);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that returns list of events on date. </summary>
		///
		/// <param name="date">			  	The date. </param>
		/// <param name="includeWorkdays">	True to include, false to include workday events. </param>
		/// <param name="includeDaysOff"> 	True to disable, false to include days off events. </param>
		///
		/// <returns>	An ImmutableArray&lt;string&gt; </returns>
		///
		/// <example>
		/// 	<code>
		/// 	CalendarDateTime.AddYearlyDateEvent("myBirthday", true, 5, 20, false, false);
		/// 	var aDate = new DateTime(2018, 5, 20, 5, 30, 0);
		/// 	var events = aDate.EventsOnDate(true, true);
		/// 	// events = [0] = "myBirthday"
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static ImmutableArray<string> EventsOnDate(this DateTime date, bool includeWorkdays, bool includeDaysOff)
		{
			date = date.Date;
			var events = new List<string>();

			var yearlyEvents = YearlyEventsDictionary
				.Where(v => date.IsBetweenEqual(v.Value.DateStart, v.Value.DateEnd)
							&& (v.Value.DayOff && includeDaysOff || v.Value.WorkDay && includeWorkdays)
							&& v.Value.Date(date.Year) == date)
				.Select(k => k.Key);

			var monthlyEvents = MonthlyEventsDictionary
				.Where(v => date.IsBetweenEqual(v.Value.DateStart, v.Value.DateEnd)
						&& (v.Value.DayOff && includeDaysOff || v.Value.WorkDay && includeWorkdays)
						&& v.Value.Date(date.Year, date.Month) == date)
				.Select(k => k.Key);

			var weeklyEvents = WeeklyEventsDictionary
				.Where(v => date.IsBetweenEqual(v.Value.DateStart, v.Value.DateEnd)
							&& (v.Value.DayOff && includeDaysOff || v.Value.WorkDay && includeWorkdays)
							&& v.Value.IsEventDay(date))
				.Select(k => k.Key);

			var dateEvents = DateEventsDictionary
				.Where(v => (v.Value.DayOff && includeDaysOff || v.Value.WorkDay && includeWorkdays)
							&& v.Value.Date == date)
				.Select(k => k.Key);

			events.AddRange(yearlyEvents);
			events.AddRange(monthlyEvents);
			events.AddRange(weeklyEvents);
			events.AddRange(dateEvents);
			return events.ToImmutableArray();
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	A DateTime extension method returns true if date is between or equal date range.
		/// </summary>
		///
		/// <remarks>	Order of date1 and date2 is unimportant. </remarks>
		///
		/// <param name="date"> 	The date. </param>
		/// <param name="date1">	The date 1 Date/Time. </param>
		/// <param name="date2">	The date 2 Date/Time. </param>
		///
		/// <returns>	True if between equal, false if not. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var date1 = new DateTime(2012, 5, 4);
		/// 	var date2 = new DateTime(2020, 4, 1);
		/// 	var alpha = new DateTime(2018, 4, 4);
		/// 	var result = alpha.IsBetweenEqual(date1, date2);
		/// 	// true
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static bool IsBetweenEqual(this DateTime date, DateTime date1, DateTime date2)
		{
			if (date1 > date2)
			{
				var temp = date1;
				date1 = date2;
				date2 = temp;
			}

			return date1 <= date && date <= date2;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	A DateTime extension method that query if 'date' is day off. The days off is configured
		/// 	using the Add-calendar event methods.
		/// </summary>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	True if day off, false if not. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	// LINQ example.
		/// 	var starting = new DateTime(2018, 1, 1);
		/// 	var ending = new DateTime(2018, 12, 31);
		/// 	var allDates = Enumerable.Range(0, 1 + ending.Subtract(starting).Days).Select(i=&gt; starting.AddDays(i));
		///
		/// 	CalendarDateTime.AddYearlyDateEvent("theDate", true, 6, 10, false, false);
		/// 	var result = allDates.Where(d =&gt; d.IsDayOff());
		/// 	// result = [0] = {6/10/2018 12:00:00 AM}
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static bool IsDayOff(this DateTime date)
		{
			date = date.Date;
			var p = YearlyEventsDictionary.Any(v => v.Value.DayOff && v.Value.Date(date.Year) == date);
			var m = MonthlyEventsDictionary.Any(v => v.Value.DayOff && v.Value.Date(date.Year, date.Month) == date);
			var w = WeeklyEventsDictionary.Any(v => v.Value.DayOff && v.Value.IsEventDay(date));
			var a = DateEventsDictionary.Any(v => v.Value.DayOff && v.Value.Date == date);
			return p || m || w || a;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that query if 'date' is first day of month. </summary>
		///
		/// <remarks>
		/// 	Simple method, returns true if the date is the 1rst of the month. Provided for
		/// 	consistency.
		/// </remarks>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	True if first day of month, false if not. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var aDate = new DateTime(2018, 2, 1);
		/// 	var result = aDate.IsFirstDaDayOfMonth();
		/// 	// true
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static bool IsFirstDayOfMonth(this DateTime date)
		{
			return date.Day == 1;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that query if 'date' is first week of month. </summary>
		///
		/// <remarks>
		/// 	The first week is the first week that ends on Saturday, not always a full week.
		/// </remarks>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	True if first week of month, false if not. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var d = new DateTime(2018, 5, 29);
		/// 	var result = d.IsLastWeekOfMonth();
		/// 	// false
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static bool IsFirstWeekOfMonth(this DateTime date)
		{
			return WeekOfMonth(date) == 1;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that query if 'date' is last day of month. </summary>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	True if last day of month, false if not. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var d = new DateTime(2018, 5, 31);
		/// 	var result = d.IsLastDayOfMonth();
		/// 	// true
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static bool IsLastDayOfMonth(this DateTime date)
		{
			return date.Date == date.LastDateOfMonth().Date;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that query if 'date' is last week of month. </summary>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	True if last week of month, false if not. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var d = new DateTime(2018, 5, 29);
		/// 	var result = d.IsLastWeekOfMonth();
		/// 	// true
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static bool IsLastWeekOfMonth(this DateTime date)
		{
			return WeekOfYear(date) == WeekOfYear(LastDateOfMonth(date.Year, date.Month));
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	An int extension method that query if 'year' is leap year. </summary>
		///
		/// <param name="year">	The year. </param>
		///
		/// <returns>	True if leap year, false if not. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var result = CalendarDateTime.IsLeapYear(2018);
		/// 	// false
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static bool IsLeapYear(this int year)
		{
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	An int extension method that query if 'year' is leap year. </summary>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	True if leap year, false if not. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var result = CalendarDateTime.IsLeapYear(new DateTime(2018, 5, 5));
		/// 	// false
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static bool IsLeapYear(this DateTime date)
		{
			return IsLeapYear(date.Year);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that query if 'date' is nth day-of-week. </summary>
		///
		/// <remarks>	Example: true if the date is the 3rd Monday of the month. </remarks>
		///
		/// <param name="date">		 	The date. </param>
		/// <param name="weekNumber">	The week number. </param>
		/// <param name="dayOfWeek"> 	The day-of-week. </param>
		///
		/// <returns>	True if nth day of week, false if not. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var d = new DateTime(2018, 5, 21);
		/// 	var result = d.IsNthDayOfWeek(3, DayOfWeek.Monday);
		/// 	// true
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static bool IsNthDayOfWeek(this DateTime date, int weekNumber, DayOfWeek dayOfWeek)
		{
			if (date.DayOfWeek != dayOfWeek)
			{
				return false;
			}

			return DayOfWeekMonthForward(date.Year, date.Month, dayOfWeek, weekNumber) == date.Date;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	A DateTime extension method that query if 'date' is week day [Monday...Friday].
		/// </summary>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	True if week day, false if not. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var d = new DateTime(2018, 5, 21);
		/// 	var result = d.IsWeekDay();
		/// 	// true
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static bool IsWeekDay(this DateTime date)
		{
			return !date.IsWeekend();
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	A DateTime extension method that query if 'date' is weekend, [Saturday...Sunday].
		/// </summary>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	True if weekend, false if not. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var d = new DateTime(2018, 5, 21);
		/// 	var result = d.IsWeekDay();
		/// 	// false
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static bool IsWeekend(this DateTime date)
		{
			return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	A DateTime extension method that query if 'date' is work day. Workdays is configured by
		/// 	calendar Add- event methods.
		/// </summary>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	True if work day, false if not. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	// LINQ example.
		/// 	var starting = new DateTime(2018, 1, 1);
		/// 	var ending = new DateTime(2018, 12, 31);
		/// 	var allDates = Enumerable.Range(0, 1 + ending.Subtract(starting).Days).Select(i=&gt; starting.AddDays(i));
		///
		/// 	CalendarDateTime.AddYearlyDateEvent("theDate", true, 6, 10, false, false);
		/// 	var result = allDates.Where(d == d.IsWorkDay());
		/// 	// result = lots, except {6/10/2018 12:00:00 AM}
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static bool IsWorkDay(this DateTime date)
		{
			return !date.IsDayOff();
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Returns the last date of month. </summary>
		///
		/// <exception cref="ArgumentOutOfRangeException">
		/// 	Thrown when year is out of DateTime.MinValue/MaxValue year. Month is less than 1 or
		/// 	greater than 12.
		/// </exception>
		///
		/// <param name="year"> 	The year. </param>
		/// <param name="month">	The month. </param>
		///
		/// <returns>	A DateTime. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var result = CalendarDateTime.LastDateOfMonth(2018, 5);
		/// 	//	result = {5/31/2018 12:00:00 AM}
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static DateTime LastDateOfMonth(int year, int month)
		{
			if (DateTime.MinValue.Year > year || year > DateTime.MaxValue.Year)
			{
				throw new ArgumentOutOfRangeException(nameof(year), ErrorMessageMinMaxOutOfRange(year, nameof(year), DateTime.MinValue.Year, DateTime.MaxValue.Year));
			}

			if (1 > month || month > 12)
			{
				throw new ArgumentOutOfRangeException(nameof(month), ErrorMessageMinMaxOutOfRange(month, nameof(month), 1, 12));
			}

			return new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Returns the last date of month. </summary>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	A DateTime. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var aDate = new DateTime(2018, 5, 3);
		/// 	var result = aDate.LastDateOfMonth();
		/// 	//	result = {5/31/2018 12:00:00 AM}
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static DateTime LastDateOfMonth(this DateTime date)
		{
			return LastDateOfMonth(date.Year, date.Month);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	A DateTime extension method returns the date quarter count, i.e. [1, 2, 3, 4].
		/// </summary>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	An int. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var result = aDate.Quarter();
		/// 	//	result = 2
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static int Quarter(this DateTime date)
		{
			return (date.Month + 2) / 3;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	A DateTime extension method returns date quarter as 'Quarter n' where n is [1...4].
		/// </summary>
		///
		/// <remarks>	Returns as ["Quarter 1", "Quarter 2", "Quarter 3", "Quarter 4"]. </remarks>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	A string. This will never be null. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var result = aDate.Quarter();
		/// 	//	result = Quarter 2
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		public static string QuarterLong(this DateTime date)
		{
			return "Quarter " + date.Quarter();
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	A DateTime extension method returns date quarter as 'Qn' where n is [1...4].
		/// </summary>
		///
		/// <remarks>	Returns as ["Q1", "Q2", "Q3", "Q4"]. </remarks>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	A string. This will never be null. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var result = aDate.Quarter();
		/// 	//	result = Q2
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		public static string QuarterShort(this DateTime date)
		{
			return "Q" + date.Quarter();
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that rounds to hour. </summary>
		///
		/// <remarks>	Returns 4:36:45.1575 PM as 5:00:00.000 PM. </remarks>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	A DateTime. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var dateTime = new DateTime(2018, 5, 21, 13, 36, 45);
		/// 	var result = dateTime.RoundToHour();
		/// 	//	result = {5/21/2018 2:00:00 PM}
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static DateTime RoundToHour(this DateTime date)
		{
			return date.RoundToInterval(new TimeSpan(0, 1, 0, 0));
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that rounds to TimeSpan interval./. </summary>
		///
		/// <param name="date">	   	The date. </param>
		/// <param name="interval">	The interval. </param>
		///
		/// <returns>	A DateTime. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var dateTime = new DateTime(2018, 5, 21, 23, 36, 47, 854);
		///
		/// 	// Round to the day.
		/// 	var timeSpan = new TimeSpan(1, 0, 0, 0);
		///
		/// 	var result = dateTime.RoundToInterval(timeSpan);
		/// 	//	result = {5/22/2018 12:00:00 AM}
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static DateTime RoundToInterval(this DateTime date, TimeSpan interval)
		{
			var halfInterval = (interval.Ticks + 1) >> 1;
			return date.AddTicks(halfInterval - ((date.Ticks + halfInterval) % interval.Ticks));
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that rounds to minute. </summary>
		///
		/// <remarks>	Returns 4:36:45.6575 PM as 4:36:46.000 PM. </remarks>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	A DateTime. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var dateTime = new DateTime(2018, 5, 21, 13, 36, 47, 854);
		/// 	var result = dateTime.RoundToSecond();
		/// 	//	result = {5/21/2018 1:36:48 PM}
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static DateTime RoundToMinute(this DateTime date)
		{
			return date.RoundToInterval(new TimeSpan(0, 0, 1, 0));
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that rounds to second. </summary>
		///
		/// <remarks>	Returns 4:36:45.857 PM as 16:36:46.000 PM. </remarks>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	A DateTime. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var aDate = new DateTime(2018, 4, 12, 12, 30, 54, 825);
		/// 	var result = aDate.RoundToSecond();
		/// 	// result = {4/12/2018 12:30:55 PM}
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static DateTime RoundToSecond(this DateTime date)
		{
			return date.RoundToInterval(new TimeSpan(0, 0, 0, 1));
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	A DateTime extension method that time identifier in HHmmssfff format. Typically used as
		/// 	Time key on tables.
		/// </summary>
		///
		/// <remarks>	Returns 4:36:45.157 PM as 163645157. </remarks>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	A string. This will never be null. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var dateTime = new DateTime(2018, 5, 21, 23, 36, 47, 854);
		/// 	var result = dateTime.TimeId();
		/// 	//	result = "233647854"
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		public static string TimeId(this DateTime date)
		{
			return date.ToString("HHmmssfff");
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that rounds time identifier to hour. </summary>
		///
		/// <remarks>	Returns 4:36:45.1575 PM as 170000000. </remarks>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	A string. This will never be null. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var dateTime = new DateTime(2018, 5, 21, 10, 36, 47, 854);
		/// 	var result = dateTime.TimeIdToHour();
		/// 	//	result = "110000000"
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		public static string TimeIdToHour(this DateTime date)
		{
			var dateId = date.RoundToHour();
			return dateId.TimeId();
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	A DateTime extension method that rounds time identifier to TimeSpan interval.
		/// </summary>
		///
		/// <remarks>	Custom round off interval. </remarks>
		///
		/// <param name="date">	   	The date. </param>
		/// <param name="interval">	The interval. </param>
		///
		/// <returns>	A string. This will never be null. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var dateTime = new DateTime(2018, 5, 21, 10, 36, 47, 854);
		///
		/// 	// 10 millisecond round off.
		/// 	var timeSpan = new TimeSpan(0, 0, 0, 0, 10);
		///
		/// 	var result = dateTime.TimeIdToInterval(timeSpan);
		/// 	//	result = "103647850"
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		public static string TimeIdToInterval(this DateTime date, TimeSpan interval)
		{
			var dateId = date.RoundToInterval(interval);
			return dateId.TimeId();
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that rounds time identifier to minute. </summary>
		///
		/// <remarks>	Returns 4:36:45.6575 PM as 163700000. </remarks>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	A string. This will never be null. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var dateTime = new DateTime(2018, 5, 21, 10, 36, 47, 854);
		/// 	var result = dateTime.TimeIdToMinute();
		/// 	//	result = "103700000"
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		public static string TimeIdToMinute(this DateTime date)
		{
			var dateId = date.RoundToMinute();
			return dateId.TimeId();
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that rounds time identifier to second. </summary>
		///
		/// <remarks>	Returns 4:36:45.6575 PM as 163646000. </remarks>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	A string. This will never be null. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var dateTime = new DateTime(2018, 5, 21, 10, 36, 47, 854);
		/// 	var result = dateTime.TimeIdToSecond();
		/// 	//	result = "103648000"
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		public static string TimeIdToSecond(this DateTime date)
		{
			var dateId = date.RoundToSecond();
			return dateId.TimeId();
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	A DateTime extension method that adjusts the date back if on Saturday or forward if on
		/// 	Sunday.
		/// </summary>
		///
		/// <remarks>
		/// 	Some events, such as New Years day is celebrated on Friday if on Saturday, or on Monday
		/// 	if on Sunday for example.
		/// </remarks>
		///
		/// <param name="date">			 	The date. </param>
		/// <param name="saturdaysBack"> 	True to adjust Saturday dates back to Friday. </param>
		/// <param name="sundaysForward">	True to adjust Sundays dates forward to Monday. </param>
		///
		/// <returns>	A DateTime. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var aSaturday = new DateTime(2018, 5, 19);
		/// 	var aSunday = new DateTime(2018, 5, 20);
		///
		/// 	var back = aSaturday.WeekendAdjustDate(true, false);
		/// 	// back = {5/18/2018 12:00:00 AM}
		///
		/// 	var forward = aSunday.WeekendAdjustDate(true, true);
		/// 	// forward = {5/21/2018 12:00:00 AM}
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static DateTime WeekendAdjustDate(this DateTime date, bool saturdaysBack, bool sundaysForward)
		{
			if (saturdaysBack && date.DayOfWeek == DayOfWeek.Saturday)
				date = date.AddDays(-1);
			else if (sundaysForward && date.DayOfWeek == DayOfWeek.Sunday)
				date = date.AddDays(1);

			return date;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that returns the date week of the month. </summary>
		///
		/// <param name="dateTime">	The dateTime to act on. </param>
		///
		/// <returns>	An int. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var aDate = new DateTime(2018, 5, 8);
		/// 	var result = aDate.WeekOfMonth();
		/// 	// result = 2
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static int WeekOfMonth(this DateTime dateTime)
		{
			var thisYearWeek = WeekOfYear(dateTime);
			var yearWeek = WeekOfYear(new DateTime(dateTime.Year, dateTime.Month, 1));
			var weekOfMonth = thisYearWeek - yearWeek + 1;
			return weekOfMonth;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A DateTime extension method that returns the date week of year. </summary>
		///
		/// <param name="dateTime">	The dateTime to act on. </param>
		///
		/// <returns>	An int. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var aDate = new DateTime(2018, 5, 8);
		/// 	var result = aDate.WeekOfYear();
		/// 	// result = 19
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static int WeekOfYear(this DateTime dateTime)
		{
			var currentCulture = CultureInfo.CurrentCulture;
			var weekOfYear = currentCulture.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
			return weekOfYear;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Returns the number of weeks in month./. </summary>
		///
		/// <remarks>	From first of the month, to the last day of the month integer days / 7. </remarks>
		///
		/// <exception cref="ArgumentOutOfRangeException">
		/// 	Thrown when year exceeds DateTime.MinValue.Year/DateTime.MaxValue.Year, or Month is less
		/// 	than 1 or greater than 12. the required range.
		/// </exception>
		///
		/// <param name="year"> 	The year. </param>
		/// <param name="month">	The month. </param>
		///
		/// <returns>	An int. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var result = CalendarDateTime.WeeksInMonth(2018, 5);
		/// 	// result = result = 4
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static int WeeksInMonth(int year, int month)
		{
			if (DateTime.MinValue.Year > year || year > DateTime.MaxValue.Year)
			{
				throw new ArgumentOutOfRangeException(nameof(year), ErrorMessageMinMaxOutOfRange(year, nameof(year), DateTime.MinValue.Year, DateTime.MaxValue.Year));
			}

			if (1 > month || month > 12)
			{
				throw new ArgumentOutOfRangeException(nameof(month), ErrorMessageMinMaxOutOfRange(month, nameof(month), 1, 12));
			}

			return LastDateOfMonth(year, month).Day / 7;
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Returns the number of weeks in month./. </summary>
		///
		/// <remarks>	From first of the month, to the last day of the month integer days / 7. </remarks>
		///
		/// <param name="date">	The date. </param>
		///
		/// <returns>	An int. </returns>
		///
		/// <example>
		/// 	<code>
		/// 	var aDate = new DateTime(2018, 5, 8);
		/// 	var result = aDate.WeeksInMonth();
		/// 	// result = 4
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static int WeeksInMonth(this DateTime date)
		{
			return WeeksInMonth(date.Year, date.Month);
		}
	}
}