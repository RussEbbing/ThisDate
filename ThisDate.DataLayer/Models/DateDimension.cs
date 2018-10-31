using System;
using JetBrains.Annotations;

namespace ThisDate.DateTimeDataLayer.Models
{
	/// <summary>	Date Dimension. </summary>
	[PublicAPI]
	public class DateDimension
	{
		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the date EURO format (YYYY/MM/DD). </summary>
		///
		/// <value>	The date European format. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string DateEuro { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the date ID (YYYYMMDD). </summary>
		///
		/// <value>	The date ID. </value>
		///-------------------------------------------------------------------------------------------------
		public int DateId { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the DateTime date. </summary>
		///
		/// <value>	The date time. </value>
		///-------------------------------------------------------------------------------------------------
		public DateTime DateTime { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the date USA format (MM/DD/YYYY). </summary>
		///
		/// <value>	The date us. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string DateUsa { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the day of month. </summary>
		///
		/// <value>	The day of month. </value>
		///-------------------------------------------------------------------------------------------------
		public int DayOfMonth { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the day of month with leading zero. </summary>
		///
		/// <value>	The day of month leading zero. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string DayOfMonthLeadingZero { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the name of the day of week2 letter. </summary>
		///
		/// <value>	The name of the day of week2 letter. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string DayOfWeek2LetterName { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the name of the day of week3 letter. </summary>
		///
		/// <value>	The name of the day of week3 letter. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		public string DayOfWeek3LetterName { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	Day-of-week count in month. Example: Feb 12, 2019 is the second Tuesday of the month.
		/// </summary>
		///
		/// <value>	The day of week count in month. </value>
		///-------------------------------------------------------------------------------------------------
		public int DayOfWeekCountInMonth { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the full name of the day of week. </summary>
		///
		/// <value>	The full name of the day of week. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		public string DayOfWeekFullName { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the day of week number, [0=Sunday, ... 6=Saturday]. </summary>
		///
		/// <value>	The day of week number. </value>
		///-------------------------------------------------------------------------------------------------
		public int DayOfWeekNumber { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the day of year. </summary>
		///
		/// <value>	The day of year. </value>
		///-------------------------------------------------------------------------------------------------
		public int DayOfYear { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the day of year leading zeros. </summary>
		///
		/// <value>	The day of year leading zeros. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string DayOfYearLeadingZeros { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the events day off. </summary>
		///
		/// <value>	The events day off. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string EventsDayOff { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the events today. </summary>
		///
		/// <value>	The events today. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string EventsToday { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the events workday. </summary>
		///
		/// <value>	The events workday. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string EventsWorkday { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets a value indicating whether this instance a day off (not a workday). </summary>
		///
		/// <value>	<c>true</c> if this instance is holiday; otherwise, <c>false</c>. </value>
		///-------------------------------------------------------------------------------------------------
		public bool IsDayOff { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	Gets or sets a value indicating whether this instance is first day of month.
		/// </summary>
		///
		/// <value>	<c>true</c> if this instance is first day of month; otherwise, <c>false</c>. </value>
		///-------------------------------------------------------------------------------------------------
		public bool IsFirstDayOfMonth { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	Gets or sets a value indicating whether this instance is first week of month.
		/// </summary>
		///
		/// <value>	<c>true</c> if this instance is first week of month; otherwise, <c>false</c>. </value>
		///-------------------------------------------------------------------------------------------------
		public bool IsFirstWeekOfMonth { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	Gets or sets a value indicating whether this instance is last day of month.
		/// </summary>
		///
		/// <value>	<c>true</c> if this instance is last day of month; otherwise, <c>false</c>. </value>
		///-------------------------------------------------------------------------------------------------
		public bool IsLastDayOfMonth { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	Gets or sets a value indicating whether this instance is last week of month.
		/// </summary>
		///
		/// <value>	<c>true</c> if this instance is last week of month; otherwise, <c>false</c>. </value>
		///-------------------------------------------------------------------------------------------------
		public bool IsLastWeekOfMonth { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets a value indicating whether this instance is leap year. </summary>
		///
		/// <value>	<c>true</c> if this instance is leap year; otherwise, <c>false</c>. </value>
		///-------------------------------------------------------------------------------------------------
		public bool IsLeapYear { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets a value indicating whether this instance is week day. </summary>
		///
		/// <value>	<c>true</c> if this instance is week day; otherwise, <c>false</c>. </value>
		///-------------------------------------------------------------------------------------------------
		public bool IsWeekDay { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets a value indicating whether this instance is weekend. </summary>
		///
		/// <value>	<c>true</c> if this instance is weekend; otherwise, <c>false</c>. </value>
		///-------------------------------------------------------------------------------------------------
		public bool IsWeekend { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets a value indicating whether this instance is workday (not a day off). </summary>
		///
		/// <value>	<c>true</c> if this instance is business day; otherwise, <c>false</c>. </value>
		///-------------------------------------------------------------------------------------------------
		public bool IsWorkDay { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the full name of the month. </summary>
		///
		/// <value>	The full name of the month. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		public string MonthFull { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the month number. </summary>
		///
		/// <value>	The month number. </value>
		///-------------------------------------------------------------------------------------------------
		public int MonthNumber { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the month number with leading zero. </summary>
		///
		/// <value>	The month number lead zero. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string MonthNumberLeadZero { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the short name of the month. </summary>
		///
		/// <value>	The short name of the month. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string MonthShort { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the quarter. </summary>
		///
		/// <value>	The quarter. </value>
		///-------------------------------------------------------------------------------------------------
		public int Quarter { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the long name of the quarter [First, Second, Third, Fourth]. </summary>
		///
		/// <value>	The long name of the quarter. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string QuarterLong { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the short name of the quarter. </summary>
		///
		/// <value>	The short name of the quarter. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string QuarterShort { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the week number. </summary>
		///
		/// <value>	The week number. </value>
		///-------------------------------------------------------------------------------------------------
		public int WeekNumber { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the week number with leading zero. </summary>
		///
		/// <value>	The week number leading zero. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		public string WeekNumberLeadingZero { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the year. </summary>
		///
		/// <value>	The year. </value>
		///-------------------------------------------------------------------------------------------------
		public int Year { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the year short {2016 -&gt; 16...}. </summary>
		///
		/// <value>	The year short. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string YearShort { get; set; } = string.Empty;
	}
}