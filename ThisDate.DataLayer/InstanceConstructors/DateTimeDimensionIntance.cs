using JetBrains.Annotations;
using System;
using System.Globalization;
using ThisDate.DateTimeDataLayer.Models;

namespace ThisDate.DateTimeDataLayer.InstanceConstructors
{
	/// <summary>	Date Time data warehouse dimension class. </summary>
	public static class DateTimeDimension
	{
		///-------------------------------------------------------------------------------------------------
		/// <summary>	Creates new instance of the DateDimension for given date. </summary>
		///
		/// <param name="date">	The current date. </param>
		///
		/// <returns>	A DateDimension. </returns>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		public static DateDimension DateDimensionInstance(DateTime date)
		{
			return new DateDimension
			{
				DateEuro = date.ToString("yyyy/M/d"),
				DateId = date.DateId(),
				DateTime = date,
				DateUsa = date.ToString("M/d/yyyy"),
				DayOfMonth = date.Day,
				DayOfMonthLeadingZero = date.ToString("dd"),
				DayOfWeekFullName = Enum.GetName(typeof(DayOfWeek), date.DayOfWeek),
				DayOfWeek2LetterName = Enum.GetName(typeof(DayOfWeek), date.DayOfWeek).Substring(0, 2),
				DayOfWeek3LetterName = Enum.GetName(typeof(DayOfWeek), date.DayOfWeek).Substring(0, 3),
				DayOfWeekCountInMonth = date.DayOfWeekCountInMonth(),
				DayOfWeekNumber = (int)date.DayOfWeek,
				DayOfYear = date.DayOfYear,
				DayOfYearLeadingZeros = date.DayOfYear.ToString("000"),
				EventsDayOff = string.Join("|", date.EventsOnDate(false, true)),
				EventsToday = string.Join("|", date.EventsOnDate(true, true)),
				EventsWorkday = string.Join("|", date.EventsOnDate(true, false)),
				IsDayOff = date.IsDayOff(),
				IsFirstDayOfMonth = date.IsFirstDayOfMonth(),
				IsFirstWeekOfMonth = date.IsFirstWeekOfMonth(),
				IsLastDayOfMonth = date.LastDateOfMonth() == date,
				IsLastWeekOfMonth = date.IsLastWeekOfMonth(),
				IsLeapYear = date.IsLeapYear(),
				IsWeekDay = date.IsWeekDay(),
				IsWeekend = date.IsWeekend(),
				IsWorkDay = date.IsWorkDay(),
				MonthFull = date.ToString("MMMM"),
				MonthShort = date.ToString("MMM"),
				MonthNumber = date.Month,
				MonthNumberLeadZero = date.Month.ToString("00"),
				Quarter = date.Quarter(),
				QuarterLong = date.QuarterLong(),
				QuarterShort = date.QuarterShort(),
				WeekNumber = date.WeekOfYear(),
				WeekNumberLeadingZero = date.WeekOfYear().ToString("00"),
				Year = date.Year,
				YearShort = date.Year.ToString().Substring(2, 2)
			};
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Creates a new time dimension for given date. </summary>
		///
		/// <param name="time">	The current time. </param>
		///
		/// <returns>	A TimeDimension. This will never be null. </returns>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		public static TimeDimension TimeDimensionInstance(DateTime time)
		{
			var instance = new TimeDimension
			{
				AmPm = time.ToString("tt", CultureInfo.InvariantCulture),
				// ReSharper disable once ExceptionNotDocumented
				Hour12 = Convert.ToInt16(time.ToString("hh")),
				Hour12LeadingZero = time.ToString("hh"),
				Hour24 = time.Hour,
				Hour24LeadingZero = time.ToString("HH"),
				Minute = time.Minute,
				MinuteLeadingZero = time.ToString("mm"),
				RoundToHour = time.RoundToHour(),
				RoundToMinute = time.RoundToMinute(),
				RoundToSecond = time.RoundToSecond(),
				Second = time.Second,
				SecondLeadingZero = time.ToString("ss"),
				Time = time,
				Time12HourMin = time.ToString("h:mm"),
				Time12HourMinAmPm = time.ToString("h:mm tt"),
				Time12HourMinSecAmPm = time.ToString("h:mm:ss tt"),
				Time12HourMinSecMiliAmPm = time.ToString("h:mm:ss.fff tt"),
				Time24HourMinCivilian = time.ToString("HH:mm"),
				Time24HourMinSecCivilian = time.ToString("HH:mm:ss"),
				Time24HourMinSecMiliCivilian = time.ToString("HH:mm:ss.fff"),
				Time24HourMinMilitary = time.ToString("HHmm"),
				Time24HourMinSecMilitary = time.ToString("HHmmss"),
				Time24HourMinSecMiliMilitary = time.ToString("HHmmss.fff"),
				TimeId = time.TimeId(),
			};
			return instance;
		}
	}
}