using System;

namespace ThisDate.DefinedCalendars.USA
{
	/// <summary>	USA calendar configuration. </summary>
	public static class Holidays
	{
		/// -------------------------------------------------------------------------------------------------
		///  <summary>	Add Christmas Day. </summary>
		/// <param name="dayOff">			Day off (not a workday). </param>
		/// <param name="saturdayBack"> 	If the date is Saturday then observe on Friday. </param>
		/// <param name="sundayForward">	If the date is Sunday then observe on Monday. </param>
		/// <example>
		///  	<code>
		///  	// using ThisDate.DefinedCalendars.USA;
		///  	Holiday.ChristmasDay(false, true, true);
		///  	</code>
		///  </example>
		/// -------------------------------------------------------------------------------------------------
		public static void ChristmasDay(bool dayOff, bool saturdayBack, bool sundayForward)
		{
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.ChristmasDayText, dayOff, 12, 25, saturdayBack, sundayForward);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Add Columbus Day. </summary>
		///
		/// <param name="dayOff">	Day off (not a workday). </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.ColumbusDay(false);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void ColumbusDay(bool dayOff)
		{
			var start = new DateTime(1492, 1, 1);
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(HolidayNames.ColumbusDayText, dayOff, 10, 2, DayOfWeek.Monday, start);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Add Easter Sunday. </summary>
		///
		/// <param name="dayOff">	Day off (not a workday). </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.EasterSunday(true);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void EasterSunday(bool dayOff)
		{
			var start = new DateTime(30, 1, 1); // Seems many believe the year was either 30 or 33.
			CalendarDateTime.AddYearlyCalculatedEvent(HolidayNames.EasterSundayText, dayOff, start);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Add Father's Day. </summary>
		///
		/// <param name="dayOff">	Day off (not a workday). </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.FathersDay(false);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void FathersDay(bool dayOff)
		{
			var start = new DateTime(1910, 1, 1);
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(HolidayNames.FathersDayText, dayOff, 6, 3, DayOfWeek.Sunday, start);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Add Good Friday. </summary>
		///
		/// <param name="dayOff">	Day off (not a workday). </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.GoodFriday(false);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void GoodFriday(bool dayOff)
		{
			var start = new DateTime(30, 1, 1); // Seems many believe the year was either 30 or 33.
			CalendarDateTime.AddYearlyCalculatedEvent(HolidayNames.GoodFridayText, dayOff, start);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Groundhog day. </summary>
		///
		/// <param name="dayOff">	Day off (not a workday). </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.GroundhogDay(false);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void GroundhogDay(bool dayOff)
		{
			var start = new DateTime(1887, 1, 1);
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.GroundHogDayText, dayOff, 2, 2, false, false, start);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Add Halloween. </summary>
		///
		/// <param name="dayOff">	Day off (not a workday). </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.Halloween(false);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void Halloween(bool dayOff)
		{
			var start = new DateTime(1850, 1, 1);   // Seems no official start year
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.HalloweenText, dayOff, 10, 31, false, false, start);
		}

		/// -------------------------------------------------------------------------------------------------
		///  <summary>	Add Independence Day (4th of July). </summary>
		/// <param name="dayOff">			Day off (not a workday). </param>
		/// <param name="saturdayBack"> 	If the date is Saturday observe on Friday. </param>
		/// <param name="sundayForward">	if the date is on Sunday observe on Monday. </param>
		/// <example>
		///  	<code>
		///  	// using ThisDate.DefinedCalendars.USA;
		///  	CalendarDateTime.Halloween(true);
		///  	</code>
		///  </example>
		/// -------------------------------------------------------------------------------------------------
		public static void IndependenceDay(bool dayOff, bool saturdayBack, bool sundayForward)
		{
			var start = new DateTime(1776, 1, 1);   // Seems no official start year
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.IndependentsDayText, dayOff, 7, 4, saturdayBack, sundayForward, start);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Add Labor Day. </summary>
		///
		/// <param name="dayOff">	Day off (not a workday). </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.LaborDay(true);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void LaborDay(bool dayOff)
		{
			var start = new DateTime(1894, 1, 1);
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(HolidayNames.LaborDayText, dayOff, 9, 1, DayOfWeek.Monday, start);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Add Martin Luther King day. </summary>
		///
		/// <param name="dayOff">	Day off (not a workday). </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.MartinLutherKingDay(true);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void MartinLutherKingDay(bool dayOff)
		{
			var start = new DateTime(1986, 1, 1);
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(HolidayNames.MartinLutherKingText, dayOff, 1, 3, DayOfWeek.Monday, start);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Add Memorial Day. </summary>
		///
		/// <param name="dayOff">	Day off (not a workday). </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.MemorialDay(true);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void MemorialDay(bool dayOff)
		{
			var startDate = new DateTime(1868, 1, 1);
			CalendarDateTime.AddYearlyDayOfWeekReverseEvent(HolidayNames.MemorialDayText, dayOff, 5, 1, DayOfWeek.Monday, startDate);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Add Mothers Day. </summary>
		///
		/// <param name="dayOff">	Day off (not a workday). </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.MothersDay(false);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void MothersDay(bool dayOff)
		{
			var start = new DateTime(1914, 1, 1);
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(HolidayNames.MothersDayText, dayOff, 5, 2, DayOfWeek.Sunday, start);
		}

		/// -------------------------------------------------------------------------------------------------
		///  <summary>	Adds the new years day. </summary>
		/// <param name="dayOff">			Day off (not a workday). </param>
		/// <param name="saturdayBack"> 	If date falls on Saturday then observe Friday. </param>
		/// <param name="sundayForward">	If date falls on Sunday then observe on Monday. </param>
		/// <example>
		///  	<code>
		///  	// using ThisDate.DefinedCalendars.USA;
		///  	CalendarDateTime.NewYearsDay(true);
		///  	</code>
		///  </example>
		/// -------------------------------------------------------------------------------------------------
		public static void NewYearsDay(bool dayOff, bool saturdayBack, bool sundayForward)
		{
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.NewYearsDayText, dayOff, 1, 1, saturdayBack, sundayForward);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Add Presidents day (Washington's day). </summary>
		///
		/// <param name="dayOff">	Day off (not a workday). </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.PresidensDay(false);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void PresidentsDay(bool dayOff)
		{
			var start = new DateTime(1971, 1, 1);
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(HolidayNames.PresidentsDayText, dayOff, 2, 3, DayOfWeek.Monday, start);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Add Saint Patrick's Day. </summary>
		///
		/// <param name="dayOff">	Day off (not a workday). </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.SaintPatrickDay(false);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void SaintPatrickDay(bool dayOff)
		{
			var start = new DateTime(1762, 1, 1);
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.SaintPatrickDayText, dayOff, 3, 17, false, false, start);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Add Thanksgiving Day. </summary>
		///
		/// <param name="dayOff">	Day off (not a workday). </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.ThanksgivingDay(false);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void ThanksgivingDay(bool dayOff)
		{
			var start = new DateTime(1619, 1, 1);   // No exact year, 1621, 1619 comes up.
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(HolidayNames.ThanksgivingDayText, dayOff, 11, 4, DayOfWeek.Thursday, start);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Add Saint Patrick's Day. </summary>
		///
		/// <param name="dayOff">	Day off (not a workday). </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.ValentinesDay(false);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void ValentinesDay(bool dayOff)
		{
			var start = new DateTime(300, 1, 1);
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.ValentinesDayText, dayOff, 2, 14, false, false, start);
		}

		/// -------------------------------------------------------------------------------------------------
		///  <summary>	Add Veterans Day, November 11. </summary>
		/// <param name="dayOff">			Day off (not a workday). </param>
		/// <param name="saturdayBack"> 	Move back to Friday if on Saturday. </param>
		/// <param name="sundayForward">	Move forward to Monday if on Sunday. </param>
		/// <example>
		///  	<code>
		///  	// using ThisDate.DefinedCalendars.USA;
		///  	CalendarDateTime.VeteransDay(false);
		///  	</code>
		///  </example>
		/// -------------------------------------------------------------------------------------------------
		public static void VeteransDay(bool dayOff, bool saturdayBack, bool sundayForward)
		{
			var start = new DateTime(1954, 1, 1);
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.VeteransDayText, dayOff, 11, 11, saturdayBack, sundayForward, start);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Add weekly day-of-week day off. </summary>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.Weekends();
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void Weekends()
		{
			var daysOfWeek = new DayOfWeek[] { DayOfWeek.Sunday, DayOfWeek.Saturday };
			CalendarDateTime.AddWeeklyEvent(HolidayNames.WeekendText, true, daysOfWeek);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Weekly day off. </summary>
		///
		/// <param name="dayOfWeek">	The day of week. </param>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	CalendarDateTime.WeeklyDayOff(DayOfWeek.Friday);
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void WeeklyDayOff(DayOfWeek dayOfWeek)
		{
			CalendarDateTime.AddWeeklyInMonthEvent(dayOfWeek.ToString(), true, dayOfWeek);
		}
	}
}