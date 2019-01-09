using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ThisDate;
using ThisDate.DefinedCalendars.USA;

namespace ConsoleApp1
{
	internal class Program
	{
		private static void Main()
		{
			// Configure the calendar, this a predefined holiday set for the NYSE.
			// See documentation on more configuration options.
			// There are a couple predefined calendars, US holidays, and
			// configurations to set yearly, monthly, weekly, day of week, and others.
			ThisDate.DefinedCalendars.USA.Calendars.NewYorkStockExchange(); ;

			var aDate = new DateTime(2018, 5, 30, 21, 44, 50);  // some random test date/time

			var isLastday = aDate.IsLastWeekOfMonth();          // True
			var isWorkday = aDate.IsWorkDay();                  // True
			var isDayOff = aDate.IsDayOff();                    // False
			var weekOfYear = aDate.WeekOfYear();                // 22
			var weekOfMonth = aDate.WeekOfMonth();              // 5
			var quarterNo = aDate.Quarter();                    // 2
			var quarterShort = aDate.QuarterShort();            // "Q2"
			var quarterLong = aDate.QuarterLong();              // "Quarter 2"
			var isWeekend = aDate.IsWeekend();                  // False
			var roundToMinute = aDate.RoundToMinute();          // 2018/5/1 21:44:50.0 => 2018/5/1 21:45:00.0
			var roundToHour = aDate.RoundToHour();              // 2018/5/1 21:44:50.0 => 2018/5/1 22:00:00.0
			var isLastWeek = aDate.IsLastWeekOfMonth();         // True
			var thirtyWorkDaysForward = aDate.AddWorkdays(30);  // Date 30 working days from aDate, skips holidays, weekends, etc.
			var thirtyWorkdaysBack = aDate.AddWorkdays(-30);    // Date 30 work days back from aDate, skips holidays, weekends, etc.
			var events = aDate.EventsOnDate(true, true);        // {}, for this example if aDate = April 4, 2018 then { "Easter Sunday", "Sunday" }
			var weeks = CalendarDateTime.WeeksInMonth(2018, 5); // 5
			var nyseHolidayNames = CalendarDateTime.KeysEvents; // All the NYSE holidays, "New Years Day", "Martin Luther King Day"... 10 in total.
			var isLastDayOfMonth = aDate.IsLastDayOfMonth();    // False
			var h = HolidayNames.MartinLutherKingText.EventDatesBetween(2010, 2020);    // All Martin Luther King dates from between 2010 and 2020.
			var ForthThursday2018Date = CalendarDateTime.DayOfWeekMonthForward(2018, 11, DayOfWeek.Thursday, 4); // Thursday, November 22 (2018/11/22)
			var lastDayOfMonth = CalendarDateTime.LastDateOfMonth(2018, 5);             // 31
			var isLastWeekOfMonth = aDate.IsLastWeekOfMonth();                          // True
			var aIsFithWendnesdayOfMonth = aDate.IsNthDayOfWeek(5, DayOfWeek.Wednesday);// True

			var anotherDate = new DateTime(2018, 11, 22);
			var eventsOnThisDay = CalendarDateTime.EventsOnDate(anotherDate, true, true);   // {"Thanksgiving Day"}

			// A LINQ example (IsWorkDay, IsDayOff). All but the comfiguration methods are LINQ friendly.
			var allDaysInMay = Enumerable.Range(1, DateTime.DaysInMonth(2018, 5)).Select(day => new DateTime(2018, 5, day));
			var workdays = allDaysInMay.Where(d => d.IsWorkDay());
			var daysOff = allDaysInMay.Where(d => d.IsDayOff());

			// Add days off for Golfing on Wednesday and Fridays in May.
			var fromDate = new DateTime(2018, 5, 1);
			var toDate = new DateTime(2018, 5, 31);
			var golfDays = new List<DayOfWeek> { DayOfWeek.Wednesday, DayOfWeek.Friday };
			CalendarDateTime.AddWeeklyEvent("Golf days off in May!", true, golfDays, fromDate, 1, fromDate, toDate);  // Add golf days off Wednesdays and Fridays in May.

			// Add Easter Sunday to the calendar, events starts in the year 30 and goes forever into the future (null).
			CalendarDateTime.AddYearlyCalculatedEvent(HolidayNames.EasterSundayText, true, new DateTime(30, 1, 1), null); // Add easter,

			// The calendar is static so once defined it's global.
			var someClass = new SomeClass();
			var allHolidays = someClass.GetHolidayList();
			var easterDateList = someClass.EasterDateRange(1983, 2020);

			// Remove Easter Sunday from the calendar.
			CalendarDateTime.RemoveEvent(HolidayNames.EasterSundayText);

			var countOfCalendarEvents = CalendarDateTime.CountEvents;
			var countOfWeeklyEvents = CalendarDateTime.CountWeeklyEvents; // 2, Wednesday & Friday golfdays.

			// And more...
		}
	}
	public class SomeClass
	{
		public ImmutableArray<DateTime> EasterDateRange(int year1, int year2)
		{
			return HolidayNames.EasterSundayText.EventDatesBetween(year1, year2);
		}

		public ImmutableArray<string> GetHolidayList()
		{
			// The defined calendar above works here too.
			return CalendarDateTime.KeysEvents;
		}
	}
}