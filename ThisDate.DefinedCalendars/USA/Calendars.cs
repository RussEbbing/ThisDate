using System;

namespace ThisDate.DefinedCalendars.USA
{
	///-------------------------------------------------------------------------------------------------
	/// <summary>	USA calendars. </summary>
	///
	/// <seealso cref="Holidays"/>
	///-------------------------------------------------------------------------------------------------
	public static class Calendars
	{
		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	New York Stock Exchange (NYSE) calendar. New Years, Martin Luther King, Presidents day,
		/// 	Good Friday, Easter, Memorial Day, Independence Day, Labor Day, Thanksgiving Day,
		/// 	Christmas, Saturdays and Sundays.
		/// </summary>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	Calendars.NewYorkStockExchange();
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void NewYorkStockExchange()
		{
			Holidays.NewYearsDay(true, false, true);
			Holidays.MartinLutherKingDay(true);
			Holidays.PresidentsDay(true);
			Holidays.GoodFriday(true);
			Holidays.MemorialDay(true);
			Holidays.IndependenceDay(true, true, true);
			Holidays.LaborDay(true);
			Holidays.ThanksgivingDay(true);
			Holidays.ChristmasDay(true, true, true);
			Holidays.WeeklyDayOff(DayOfWeek.Saturday);
			Holidays.WeeklyDayOff(DayOfWeek.Sunday);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	USA Federal Government event calendar. All non work days. New Years, Martin Luther King,
		/// 	presidents day, Memorial Day, Independence Day, Labor Day, Columbus Day, Veterans Day,
		/// 	Thanksgiving Day, Christmas, Saturdays, Sundays.
		/// </summary>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	Calendars.UsaFederal();
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void UsaFederal()
		{
			Holidays.NewYearsDay(true, false, true);
			Holidays.MartinLutherKingDay(true);
			Holidays.PresidentsDay(true);
			Holidays.MemorialDay(true);
			Holidays.IndependenceDay(true, true, true);
			Holidays.LaborDay(true);
			Holidays.ColumbusDay(true);
			Holidays.VeteransDay(true, true, true);
			Holidays.ThanksgivingDay(true);
			Holidays.ChristmasDay(true, true, true);
			Holidays.WeeklyDayOff(DayOfWeek.Saturday);
			Holidays.WeeklyDayOff(DayOfWeek.Sunday);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>
		/// 	USA full observance calendar with typical workday configurations. Includes USA Federal
		/// 	plus and Groundhog day, Mothers day, Fathers Day, Good Friday, Easter Sunday, Halloween,
		/// 	Saint Patrick's Day.
		/// </summary>
		///
		/// <example>
		/// 	<code>
		/// 	// using ThisDate.DefinedCalendars.USA;
		/// 	Calendars.UsaObservance();
		/// 	</code>
		/// </example>
		///-------------------------------------------------------------------------------------------------
		public static void UsaObservance()
		{
			Holidays.NewYearsDay(true, false, true);
			Holidays.MartinLutherKingDay(true);
			Holidays.PresidentsDay(true);
			Holidays.MemorialDay(true);
			Holidays.IndependenceDay(true, true, true);
			Holidays.LaborDay(true);
			Holidays.ColumbusDay(false);
			Holidays.VeteransDay(false, true, true);
			Holidays.ThanksgivingDay(true);
			Holidays.ChristmasDay(true, true, true);
			Holidays.WeeklyDayOff(DayOfWeek.Saturday);
			Holidays.WeeklyDayOff(DayOfWeek.Sunday);

			Holidays.ValentinesDay(false);
			Holidays.MothersDay(false);
			Holidays.FathersDay(false);
			Holidays.GoodFriday(false);
			Holidays.EasterSunday(false);
			Holidays.GroundhogDay(false);
			Holidays.Halloween(false);
			Holidays.SaintPatrickDay(false);
		}
	}
}