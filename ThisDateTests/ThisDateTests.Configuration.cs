using System;
using System.Collections.Generic;
using ThisDate.DefinedCalendars.USA;
using Xunit;

// ReSharper disable ExceptionNotDocumented
namespace ThisDate.Tests
{
	public partial class ThisDateTests
	{
		[Fact]
		public static void AddMonthlyDayOfWeekReverseEventDuplicateThrows()
		{
			const string dupName = "dup";
			CalendarDateTime.AddMonthlyDayOfWeekReverseEvent(dupName, true, DayOfWeek.Monday, 2);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.AddMonthlyDayOfWeekReverseEvent(dupName, true, DayOfWeek.Monday, 2));
		}

		[Fact]
		public static void AddMonthlyDayOfWeekReverseEventNameThrows()
		{
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddMonthlyDayOfWeekReverseEvent(null, false, DayOfWeek.Friday, 2));
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddMonthlyDayOfWeekReverseEvent(string.Empty, false, DayOfWeek.Friday, 2));
		}

		[Fact]
		public static void AddMonthlyDayOfWeekReverseEventsInRange()
		{
			var eventName = "someName";
			var start = _startTestDate;
			var end = _endTestDate.LastDateOfMonth();
			CalendarDateTime.ClearCalendar();

			CalendarDateTime.AddMonthlyDayOfWeekReverseEvent(eventName, true, DayOfWeek.Monday, 1, start, end); // Last Monday of every month
			var foundLastMonday = false;

			// dateEnd need to seed in as equal or after the last Monday of the month to work (last day of the month works).
			for (var currentDate = end; currentDate >= start; currentDate = currentDate.AddDays(-1))
			{
				if (currentDate.IsLastDayOfMonth())
				{
					foundLastMonday = false;
				}

				if (!foundLastMonday && currentDate.DayOfWeek == DayOfWeek.Monday)
				{
					foundLastMonday = true;
					var actual = currentDate.EventsOnDate(true, true);
					Assert.Contains(eventName, actual);
				}
				else
				{
					Assert.Empty(currentDate.EventsOnDate(true, true));
				}
			}
		}

		[Fact]
		public static void AddMonthlyDayOfWeekReverseEventsOutsideRange()
		{
			var start = DateTime.Now.AddYears(-1);
			var end = DateTime.Now.AddYears(1);
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddMonthlyDayOfWeekReverseEvent("1", true, DayOfWeek.Monday, 1, start, end);
			for (var currentDate = start.AddYears(-2); currentDate <= end.AddYears(2); currentDate = currentDate.AddDays(1))
			{
				if (!currentDate.IsBetweenEqual(start, end))
				{
					Assert.Empty(currentDate.EventsOnDate(true, true));
				}
			}
		}

		[Fact]
		public static void AddMonthlyDayOfWeekReverseEventWeeksBackNegThrows()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddMonthlyDayOfWeekReverseEvent("event", false, DayOfWeek.Saturday, 0));
		}

		[Fact]
		public void AddDateEventThrowEventDuplicate1()
		{
			const string duplicate = "dup";
			CalendarDateTime.AddDateEvent(duplicate, false, DateTime.Now);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.AddDateEvent(duplicate, false, DateTime.Now));
		}

		[Fact]
		public void AddDateEventThrowEventNull1()
		{
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddDateEvent(string.Empty, false, DateTime.Now));
		}

		[Fact]
		public void AddMonthlyDayEventThrowsDuplicate()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddMonthlyDateEvent("1", true, 2);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.AddMonthlyDateEvent("1", false, 2));
		}

		[Fact]
		public void AddMonthlyDayEventThrowsEmptyNullEvent()
		{
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddMonthlyDateEvent(null, false, 1));
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddMonthlyDateEvent(string.Empty, false, 1));
		}

		[Fact]
		public void AddMonthlyDayEventThrowsInvalidDay()
		{
			CalendarDateTime.ClearCalendar();
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddMonthlyDateEvent("alpha", false, 0));
			CalendarDateTime.AddMonthlyDateEvent("beta", true, 1);
			CalendarDateTime.AddMonthlyDateEvent("delta", true, 31);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddMonthlyDateEvent("gamma", false, 32));
		}

		[Fact]
		public void AddMonthlyDayEventYearRange()
		{
			CalendarDateTime.ClearCalendar();
			var event1Name = "Workday1";
			const bool event1OffDay = false;
			var event1Day = 12;
			var firstValidDate = _startTestDate.AddYears(5);
			var lastValidDate = _endTestDate.AddYears(-5);

			Assert.Equal(0, CalendarDateTime.CountEvents);
			CalendarDateTime.AddMonthlyDateEvent(event1Name, event1OffDay, event1Day, firstValidDate, lastValidDate);
			Assert.Equal(1, CalendarDateTime.CountMonthlyEvents);
			Assert.Equal(1, CalendarDateTime.CountEvents);

			for (var currentDate = _startTestDate; currentDate <= _endTestDate; currentDate = currentDate.AddDays(1))
			{
				Assert.Equal(currentDate.IsDayOff(), event1OffDay);
				Assert.Equal(currentDate.IsWorkDay(), !event1OffDay);

				var expected = new List<string>();
				if (currentDate.IsBetweenEqual(firstValidDate, lastValidDate) && currentDate.Day == event1Day)
				{
					expected.Add(event1Name);
				}

				var actual = currentDate.EventsOnDate(true, true);
				Assert.Equal(expected, actual);
			}
		}

		[Fact]
		public void AddMonthlyDayOfWeekForwardEventThrowEmptyEvent()
		{
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddMonthlyDayOfWeekForwardEvent(null, true, DayOfWeek.Friday, 1));
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddMonthlyDayOfWeekForwardEvent(string.Empty, true, DayOfWeek.Friday, 1));
		}

		[Fact]
		public void AddMonthlyDayOfWeekForwardEventThrowsDuplicate()
		{
			const string dup = "duplicate";
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddMonthlyDayOfWeekForwardEvent(dup, false, DayOfWeek.Monday, 2);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.AddMonthlyDayOfWeekForwardEvent(dup, true, DayOfWeek.Friday, 1));
		}

		[Fact]
		public void AddMonthlyDayOfWeekForwardEventThrowsStartEndInbound()
		{
			const string eventName = "alpha";
			var start = new DateTime(DateTime.Now.AddYears(-2).Year, DateTime.Now.AddYears(-2).Month, 1);
			var end = DateTime.Now.AddYears(2);
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddMonthlyDayOfWeekForwardEvent(eventName, true, DayOfWeek.Friday, 1, start, end);
			bool foundFirstFriday = false;
			for (var currentDate = start; currentDate <= end; currentDate = currentDate.AddDays(1))
			{
				if (currentDate.IsFirstDayOfMonth())
				{
					foundFirstFriday = false;
				}

				if (!foundFirstFriday && currentDate.DayOfWeek == DayOfWeek.Friday)
				{
					foundFirstFriday = true;
					Assert.Contains(eventName, currentDate.EventsOnDate(true, true));
				}
				else
				{
					Assert.Empty(currentDate.EventsOnDate(true, true));
				}
			}
		}

		[Fact]
		public void AddMonthlyDayOfWeekForwardEventThrowsStartEndOutBound()
		{
			var start = DateTime.Now.AddYears(-2);
			var end = DateTime.Now.AddYears(2);
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddMonthlyDayOfWeekForwardEvent("alpha", true, DayOfWeek.Friday, 1, start, end);
			for (var currentDate = start.AddYears(-2); currentDate <= end.AddYears(2); currentDate = currentDate.AddDays(1))
			{
				if (!currentDate.IsBetweenEqual(start, end))
				{
					Assert.Empty(currentDate.EventsOnDate(true, true));
				}
			}
		}

		[Fact]
		public void AddMonthlyDayOfWeekForwardEventThrowsWeeksForwardInvalid()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddMonthlyDayOfWeekForwardEvent("alpha", true, DayOfWeek.Friday, 1);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddMonthlyDayOfWeekForwardEvent("beta", true, DayOfWeek.Friday, 0));
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddMonthlyDayOfWeekForwardEvent("delta", true, DayOfWeek.Friday, -1));
		}

		[Fact]
		public void AddMonthlyLastDayEventDuplicate()
		{
			const string eventName = "zink";
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddMonthlyLastDayEvent(eventName, true);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.AddMonthlyLastDayEvent(eventName, true));
		}

		[Fact]
		public void AddMonthlyLastDayEventNameNullEmpty()
		{
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddMonthlyLastDayEvent(null, true));
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddMonthlyLastDayEvent(String.Empty, true));
		}

		[Fact]
		public void AddMonthlyLastDayEventNameNullStartEndInOutbound()
		{
			const string eventName = "test";
			var start = DateTime.Now.AddYears(-2);
			var end = DateTime.Now.AddYears(2);
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddMonthlyLastDayEvent(eventName, true, start, end);

			for (var currentDate = start.Date.AddYears(-2); currentDate <= end.AddYears(2); currentDate = currentDate.AddDays(1))
			{
				var lastDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1).AddDays(-1);
				var actual = currentDate.EventsOnDate(true, true);
				if (currentDate.IsBetweenEqual(start, end) && currentDate == lastDayOfMonth)
				{
					Assert.Contains(eventName, actual);
				}
				else
				{
					Assert.Empty(actual);
				}
			}
		}

		[Fact]
		public void AddWeeklyEventDuplicateEventThrows()
		{
			const string dup = "dup";
			var dates = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday };
			CalendarDateTime.AddWeeklyEvent(dup, true, dates);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.AddWeeklyEvent(dup, true, dates));
		}

		[Fact]
		public void AddWeeklyEventEmptyDayOfWeekThrows()
		{
			var dates = new List<DayOfWeek>();
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddWeeklyEvent("beta", true, dates));
		}

		[Fact]
		public void AddWeeklyEventEmptyEventDupThrows()
		{
			const string dupEvent = "duplicate";
			CalendarDateTime.AddMonthlyDateEvent(dupEvent, true, 1);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.AddMonthlyDateEvent(dupEvent, true, 1));
		}

		[Fact]
		public void AddWeeklyEventEmptyEventThrows()
		{
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddMonthlyDateEvent("", true, 1));
		}

		[Fact]
		public void AddWeeklyEventIntervalInvalidSeedThrows()
		{
			var dates = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday };
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddWeeklyEvent("beta", true, dates, null, 2));
		}

		[Fact]
		public void AddWeeklyEventIntervalInvalidThrows()
		{
			var dates = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday };
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddWeeklyEvent("beta", true, dates, null, 0));
		}

		[Fact]
		public void AddWeeklyEventNullDayOfWeekThrows()
		{
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddWeeklyEvent("beta", true, null));
		}

		[Fact]
		public void AddWeeklyEventNullEventNameThrows()
		{
			var dates = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday };
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddWeeklyEvent(null, true, dates));
		}

		[Fact]
		public void AddWeeklyInMonthEvent13NotPayday24Payday()
		{
			const string reportDayEvent = "Report Day";
			const string payDayEvent = "Payday";
			var reportWeeks = new List<int> { 1, 3 };
			var paydayWeeks = new List<int> { 2, 4 };
			var testDayOfWeek = DayOfWeek.Thursday;
			var testStartRange = _startTestDate.AddYears(5);
			var testEndRange = _endTestDate.AddYears(-5);
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddWeeklyInMonthEvent(reportDayEvent, false, testDayOfWeek, reportWeeks, testStartRange, testEndRange);
			CalendarDateTime.AddWeeklyInMonthEvent(payDayEvent, false, testDayOfWeek, paydayWeeks, testStartRange, testEndRange);
			var currentWeek = -99;

			for (var currentDate = _startTestDate; currentDate <= _endTestDate; currentDate = currentDate.AddDays(1))
			{
				var actual = currentDate.EventsOnDate(true, true);
				if (!currentDate.IsBetweenEqual(testStartRange, testEndRange))
				{
					Assert.Empty(actual);
					continue;
				}

				if (currentDate.Day == 1)
				{
					currentWeek = 0;
				}

				if (testDayOfWeek == currentDate.DayOfWeek)
				{
					currentWeek++;
				}

				if (testDayOfWeek == currentDate.DayOfWeek && reportWeeks.Contains(currentWeek))
				{
					Assert.Contains(reportDayEvent, actual);
				}
				else if (testDayOfWeek == currentDate.DayOfWeek && paydayWeeks.Contains(currentWeek))
				{
					Assert.Contains(payDayEvent, actual);
				}
				else
				{
					Assert.Empty(actual);
				}
			}
		}

		[Fact]
		public void AddWeeklyInMonthEventEventMaxSixThrows()
		{
			// TODO: change to single int, remove collection.
			var weeks = new List<int> { 6, 5 };
			CalendarDateTime.ClearCalendar();
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddWeeklyInMonthEvent("test", true, DayOfWeek.Friday, weeks));
		}

		[Fact]
		public void AddWeeklyInMonthEventEventMinZeroThrows()
		{
			// TODO: chanbe to int overload on weeks.
			var weeks = new List<int> { 1, 0 };
			CalendarDateTime.ClearCalendar();
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddWeeklyInMonthEvent("test", true, DayOfWeek.Friday, weeks));
		}

		[Fact]
		public void AddWeeklyInMonthEventEventNameDuplicatesThrows()
		{
			const string duplicate = "duplicate";
			CalendarDateTime.ClearCalendar();
			var duplicateDayOfWeeks = DayOfWeek.Friday;
			CalendarDateTime.AddWeeklyInMonthEvent(duplicate, true, duplicateDayOfWeeks);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.AddWeeklyInMonthEvent(duplicate, false, duplicateDayOfWeeks));
		}

		[Fact]
		public void AddWeeklyInMonthEventEventNameNullEmptyThrows()
		{
			var dayOfWeeks = DayOfWeek.Monday;
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddWeeklyInMonthEvent(null, true, dayOfWeeks));
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddWeeklyInMonthEvent(string.Empty, true, dayOfWeeks));
		}

		[Fact]
		public void AddYearlyCalculatedEventThrowDupKey()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyCalculatedEvent(HolidayNames.EasterSundayText, false);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.AddYearlyCalculatedEvent(HolidayNames.EasterSundayText, false));
		}

		[Fact]
		public void AddYearlyCalculatedEventThrowInvalidKey()
		{
			CalendarDateTime.ClearCalendar();
			Assert.Throws<ArgumentException>(() => CalendarDateTime.AddYearlyCalculatedEvent("wrong", false));
		}

		[Fact]
		public void AddYearlyCalculatedEventThrowNullEventName()
		{
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddYearlyCalculatedEvent("", false));
		}

		[Fact]
		public void AddYearlyDateEventThrowsEmptyEvent()
		{
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddYearlyDateEvent("", false, 1, 1, false, false));
		}

		[Fact]
		public void AddYearlyDateEventThrowsHighMonth()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDateEvent("test0", false, 12, 1, false, false);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test2", false, 13, 1, false, false));
		}

		[Fact]
		public void AddYearlyDateEventThrowsLowMonth()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDateEvent("test4", false, 1, 1, false, false);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test8", false, 0, 1, false, false));
		}

		[Fact]
		public void AddYearlyDayOfWeekReverseEventDuplicate()
		{
			const string dup = "dup";
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(dup, true, 10, 1, DayOfWeek.Friday);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.AddYearlyDayOfWeekForwardEvent(dup, true, 11, 1, DayOfWeek.Friday));
		}

		[Fact]
		public void AddYearlyDayOfWeekReverseEventEventMonth0()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent("Jane", true, 1, 1, DayOfWeek.Friday);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDayOfWeekForwardEvent("Tarzan", true, 0, 1, DayOfWeek.Friday));
		}

		[Fact]
		public void AddYearlyDayOfWeekReverseEventEventMonth13()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent("Jane", true, 12, 1, DayOfWeek.Friday);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDayOfWeekForwardEvent("Tarzan", true, 13, 1, DayOfWeek.Friday));
		}

		[Fact]
		public void AddYearlyDayOfWeekReverseEventEventNameNull()
		{
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddYearlyDayOfWeekForwardEvent(null, true, 1, 1, DayOfWeek.Friday));
		}

		[Fact]
		public void AddYearlyDayOfWeekReverseEventEventWeekCount0()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent("Jane", true, 10, 1, DayOfWeek.Friday);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDayOfWeekForwardEvent("Tarzan", true, 11, 0, DayOfWeek.Friday));
		}

		[Fact]
		public void AddYearlyDayOfWeekReverseEventMonth0()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDayOfWeekReverseEvent("the", true, 1, 1, DayOfWeek.Monday);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDayOfWeekReverseEvent("ffj", false, 0, 1, DayOfWeek.Monday));
		}

		[Fact]
		public void AddYearlyDayOfWeekReverseEventMonth13()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDayOfWeekReverseEvent("the", true, 12, 1, DayOfWeek.Monday);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDayOfWeekReverseEvent("fjf", false, 13, 1, DayOfWeek.Monday));
		}

		[Fact]
		public void AddYearlyDayOfWeekReverseEventNameNull()
		{
			Assert.Throws<ArgumentNullException>(() => CalendarDateTime.AddYearlyDayOfWeekReverseEvent(string.Empty, false, 1, 1, DayOfWeek.Monday));
		}

		[Fact]
		public void AddYearlyDayOfWeekReverseEventWeekCount0()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDayOfWeekReverseEvent("the", true, 1, 1, DayOfWeek.Monday);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDayOfWeekReverseEvent("jf", false, 1, 0, DayOfWeek.Monday));
		}

		[Fact]
		public void AddYearlyDayOfWeekReverseEventWeekDuplicate()
		{
			const string duplicate = "duplicate";
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDayOfWeekReverseEvent(duplicate, true, 1, 1, DayOfWeek.Monday);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.AddYearlyDayOfWeekReverseEvent(duplicate, false, 1, 1, DayOfWeek.Monday));
		}

		[Fact]
		public void AddYearlyInvalidDateEventThrowsDays()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDateEvent("good0", false, 1, 1, false, false);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test0", false, 1, 0, false, false));
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test1", false, 1, 32, false, false));
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test2", false, 2, 30, false, false));
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test3", false, 3, 32, false, false));
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test4", false, 4, 31, false, false));
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test5", false, 5, 32, false, false));
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test6", false, 6, 31, false, false));
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test7", false, 7, 32, false, false));
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test8", false, 8, 32, false, false));
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test9", false, 9, 31, false, false));
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test10", false, 10, 32, false, false));
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test11", false, 11, 31, false, false));
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.AddYearlyDateEvent("test12", false, 12, 32, false, false));
		}

		[Fact]
		public void CalculatedTextEasterEqual()
		{
			Assert.Equal(CalculatedEventsText.EasterSunday, HolidayNames.EasterSundayText);
		}

		[Fact]
		public void CalculatedTextGoodFridayEqual()
		{
			Assert.Equal(CalculatedEventsText.GoodFriday, HolidayNames.GoodFridayText);
		}
	}
}