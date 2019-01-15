using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Xunit;
using ThisDate.DefinedCalendars.USA;

namespace ThisDate.Tests
{
	public partial class ThisDateTests
	{
		[Fact]
		public void ClearCalendar()
		{
			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountEvents);
			CalendarDateTime.AddYearlyDateEvent("yearlyKey", false, 1, 2, false, false);
			CalendarDateTime.AddMonthlyDateEvent("monthlyKey", true, 12);
			CalendarDateTime.AddWeeklyInMonthEvent("weekKey", false, DayOfWeek.Friday);
			CalendarDateTime.AddDateEvent("dateKey", false, DateTime.Now);

			Assert.True(CalendarDateTime.CountEvents == 4);
			CalendarDateTime.ClearCalendar();
			Assert.True(CalendarDateTime.CountEvents == 0);
		}

		[Fact]
		public void ClearDated()
		{
			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountDateEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);

			CalendarDateTime.AddDateEvent("d1", false, new DateTime(2020, 5, 7));
			Assert.Equal(1, CalendarDateTime.CountDateEvents);
			Assert.Equal(1, CalendarDateTime.CountEvents);

			CalendarDateTime.AddDateEvent("d2", false, new DateTime(2020, 5, 8));
			Assert.Equal(2, CalendarDateTime.CountDateEvents);
			Assert.Equal(2, CalendarDateTime.CountEvents);

			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountDateEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);
		}

		[Fact]
		public void ClearMonthly()
		{
			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountMonthlyEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);

			CalendarDateTime.AddMonthlyDateEvent("delta", false, 4);
			Assert.Equal(1, CalendarDateTime.CountMonthlyEvents);
			Assert.Equal(1, CalendarDateTime.CountEvents);

			CalendarDateTime.AddMonthlyDateEvent("force", false, 4);
			Assert.Equal(2, CalendarDateTime.CountMonthlyEvents);
			Assert.Equal(2, CalendarDateTime.CountEvents);

			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountMonthlyEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);
		}

		[Fact]
		public void ClearWeekly()
		{
			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountWeeklyEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);

			CalendarDateTime.AddWeeklyInMonthEvent("Week1", true, DayOfWeek.Friday);
			Assert.Equal(1, CalendarDateTime.CountWeeklyEvents);
			Assert.Equal(1, CalendarDateTime.CountEvents);

			CalendarDateTime.AddWeeklyInMonthEvent("Week2", true, DayOfWeek.Friday);
			Assert.Equal(2, CalendarDateTime.CountWeeklyEvents);
			Assert.Equal(2, CalendarDateTime.CountEvents);

			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountWeeklyEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);
		}

		[Fact]
		public void ClearYearly()
		{
			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountEvents);
			Assert.Equal(0, CalendarDateTime.CountYearlyEvents);

			CalendarDateTime.AddYearlyCalculatedEvent(CalculatedEventsText.EasterSunday, true);
			Assert.Equal(1, CalendarDateTime.CountEvents);
			Assert.Equal(1, CalendarDateTime.CountYearlyEvents);

			CalendarDateTime.AddYearlyCalculatedEvent(CalculatedEventsText.GoodFriday, true);
			Assert.Equal(2, CalendarDateTime.CountYearlyEvents);
			Assert.Equal(2, CalendarDateTime.CountEvents);

			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountYearlyEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);
		}

		[Fact]
		public void ContainsKeyNullEmptyReturnsFalse()
		{
			Assert.False(CalendarDateTime.ContainsEventKey(null));
			Assert.False(CalendarDateTime.ContainsEventKey(string.Empty));
		}

		[Fact]
		public void CountCalendar()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDateEvent("yearlyKey", false, 1, 2, false, false);
			CalendarDateTime.AddMonthlyDateEvent("month", true, 5);
			CalendarDateTime.AddWeeklyInMonthEvent("weekKey", false, DayOfWeek.Friday);
			CalendarDateTime.AddDateEvent("datedKey", false, DateTime.Now);

			var expected = 4;
			var actual = CalendarDateTime.CountEvents;
			Assert.Equal(expected, actual);
			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountEvents);
		}

		[Fact]
		public void CountDated()
		{
			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountDateEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);

			CalendarDateTime.AddDateEvent("d1", false, new DateTime(2020, 5, 20));
			Assert.Equal(1, CalendarDateTime.CountDateEvents);
			Assert.Equal(1, CalendarDateTime.CountEvents);

			CalendarDateTime.AddDateEvent("d2", false, new DateTime(2020, 5, 20));
			Assert.Equal(2, CalendarDateTime.CountDateEvents);
			Assert.Equal(2, CalendarDateTime.CountEvents);

			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountDateEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);
		}

		[Fact]
		public void CountMonthly()
		{
			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountMonthlyEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);

			CalendarDateTime.AddMonthlyDateEvent("m1", false, 1);
			Assert.Equal(1, CalendarDateTime.CountMonthlyEvents);
			Assert.Equal(1, CalendarDateTime.CountEvents);

			CalendarDateTime.AddMonthlyDateEvent("m2", false, 1);
			Assert.Equal(2, CalendarDateTime.CountMonthlyEvents);
			Assert.Equal(2, CalendarDateTime.CountEvents);

			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountMonthlyEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);
		}

		[Fact]
		public void CountWeekly()
		{
			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountWeeklyEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);

			CalendarDateTime.AddWeeklyInMonthEvent("w1", false, DayOfWeek.Friday);
			Assert.Equal(1, CalendarDateTime.CountWeeklyEvents);
			Assert.Equal(1, CalendarDateTime.CountEvents);

			CalendarDateTime.AddWeeklyInMonthEvent("w2", false, DayOfWeek.Saturday);
			Assert.Equal(2, CalendarDateTime.CountWeeklyEvents);
			Assert.Equal(2, CalendarDateTime.CountEvents);

			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountWeeklyEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);
		}

		[Fact]
		public void CountYearly()
		{
			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountYearlyEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);

			CalendarDateTime.AddYearlyDateEvent("y1", false, 1, 2, false, false);
			Assert.Equal(1, CalendarDateTime.CountYearlyEvents);
			Assert.Equal(1, CalendarDateTime.CountEvents);

			CalendarDateTime.AddYearlyDateEvent("y2", false, 1, 2, false, false);
			Assert.Equal(2, CalendarDateTime.CountYearlyEvents);
			Assert.Equal(2, CalendarDateTime.CountEvents);

			CalendarDateTime.ClearCalendar();
			Assert.Equal(0, CalendarDateTime.CountYearlyEvents);
			Assert.Equal(0, CalendarDateTime.CountEvents);
		}

		[Fact]
		public void DuplicateKeysThrows()
		{
			const string weekKey = "WeekKey";
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddWeeklyInMonthEvent(weekKey, false, DayOfWeek.Friday);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.AddYearlyDateEvent(weekKey, false, 1, 2, false, false));
		}

		[Fact]
		public void EventDatesBetweenEmptyEvent()
		{
			CalendarDateTime.ClearCalendar();
			Calendars.NewYorkStockExchange();
			var expected = new ImmutableArray<DateTime>();
			var actual = string.Empty.EventDatesBetween(DateTime.MaxValue.Year, DateTime.MinValue.Year);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void EventDatesBetweenYear1LessMinThrows()
		{
			const string eventName = "anything";
			var year1 = DateTime.MinValue.Year - 1;
			Assert.Throws<ArgumentOutOfRangeException>(() => eventName.EventDatesBetween(year1, null));
		}

		[Fact]
		public void EventDatesBetweenYear1GreaterMaxThrows() 
		{
			const string eventName = "anything";
			var year1 = DateTime.MaxValue.Year + 1;
			Assert.Throws<ArgumentOutOfRangeException>(() => eventName.EventDatesBetween(year1, null));
		}

		[Fact]
		public void EventDatesBetweenYear2LessMinThrows() 
		{
			const string eventName = "anything";
			var year2 = DateTime.MinValue.Year - 1;
			Assert.Throws<ArgumentOutOfRangeException>(() => eventName.EventDatesBetween(null, year2));
		}

		[Fact]
		public void EventDatesBetweenYear2GreaterMaxThrows() 
		{
			const string eventName = "anything";
			var year2 = DateTime.MaxValue.Year + 1;
			Assert.Throws<ArgumentOutOfRangeException>(() => eventName.EventDatesBetween(null, year2));
		}

		[Fact]
		public void EventDatesBetweenSwap()
		{
			CalendarDateTime.ClearCalendar();
			Holidays.NewYearsDay(true, false, false);
			var expected = Enumerable.Range(StartTestDate.Year, EndTestDate.Year - StartTestDate.Year + 1).Select(s => new DateTime(s, 1, 1));
			var actual = HolidayNames.NewYearsDayText.EventDatesBetween(EndTestDate.Year, StartTestDate.Year);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void EventDatesBetweenDated()
		{
			const string name = "Seventies";
			var expected = new List<DateTime> { new DateTime(1970, 1, 1) };
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddDateEvent(name, false, expected[0]);
			var actual = CalendarDateTime.EventDatesBetween(name);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void EventDatesBetweenEmpty()
		{
			CalendarDateTime.ClearCalendar();
			DefinedCalendars.USA.Holidays.ThanksgivingDay(true);
			var expected = new List<DateTime>();
			var actual = CalendarDateTime.EventDatesBetween(null);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void EventDatesBetweenLastDayOfMonth()
		{
			const string monthEnd = "Account Month End";
			var fromDate = new DateTime(1990, 5, 1);
			var toDate = new DateTime(2040, 6, 1);
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddMonthlyLastDayEvent(monthEnd, false, fromDate, toDate);
			var actual = monthEnd.EventDatesBetween(toDate, fromDate);
			var expected = new List<DateTime>();
			for (var date = fromDate.AddMonths(1); date <= toDate; date = date.AddMonths(1))
			{
				var monthend = (new DateTime(date.Year, date.Month, 1)).AddDays(-1);
				expected.Add(monthend);
			}

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void EventDatesBetweenMonthlyDateEvents()
		{
			var testDayName = "testDaDate";
			const int testDay = 12;
			var expected = new List<DateTime>();
			var start = new DateTime(StartTestDate.Year, StartTestDate.Month, testDay);
			for (var date = start; date <= EndTestDate; date = date.AddMonths(1))
			{
				expected.Add(date);
			}

			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddMonthlyDateEvent(testDayName, false, testDay, EndTestDate, StartTestDate);
			var actual = testDayName.EventDatesBetween(EndTestDate, StartTestDate);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void EventDatesBetweenWeekly()
		{
			const string meetingName = "Standup Meeting";
			var daysOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday };
			var startDate = new DateTime(2018, 1, 1);
			var endDate = new DateTime(2018, 3, 30);
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddWeeklyEvent(meetingName, false, daysOfWeeks, startDate, 2, startDate, endDate);

			var expected = new List<DateTime>();
			var week = 0;
			var startDayOfWeek = startDate.DayOfWeek;
			var d = startDate;
			while (d <= endDate)
			{
				if (d.DayOfWeek == startDayOfWeek)
				{
					week++;
				}

				if (week % 2 != 0 && daysOfWeeks.Contains(d.DayOfWeek))
				{
					expected.Add(d);
				}

				d = d.AddDays(1);
			}

			var actual = meetingName.EventDatesBetween(startDate, endDate);
			Assert.Equal(actual, expected);
		}

		[Fact]
		public void EventDatesBetweenWeeklyInMonthEvent()
		{
			const string eventName = "Payday";
			var dayOfWeekTarget = DayOfWeek.Thursday;
			var startDate = new DateTime(2018, 1, 1);
			var endDate = new DateTime(2018, 3, 30);
			var intervals = new List<int> { 1, 3 };
			var week = 1;
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddWeeklyInMonthEvent(eventName, false, DayOfWeek.Thursday, intervals, startDate, endDate);

			var expected = new List<DateTime>();
			for (var date = startDate; date <= endDate; date = date.AddDays(1))
			{
				if (date.Day == 1)
				{
					week = 1;
				}
				else if (date.Day != 1 && date.DayOfWeek == DayOfWeek.Sunday)
				{
					week++;
				}

				if (date.DayOfWeek == dayOfWeekTarget && intervals.Contains(week))
				{
					expected.Add(date);
				}
			}

			var actual = eventName.EventDatesBetween(startDate, endDate);
			Assert.Equal(actual, expected);
		}

		[Fact]
		public void EventDatesBetweenYearlyCalculatedEvents()
		{
			CalendarDateTime.ClearCalendar();
			DefinedCalendars.USA.Holidays.EasterSunday(false);
			var expected = EasterDays.Select(s => s.Date);
			var fromDate = EasterDays.Min(s => s.Date);
			var toDate = EasterDays.Max(s => s.Date);
			var actual = DefinedCalendars.USA.HolidayNames.EasterSundayText.EventDatesBetween(fromDate, toDate);
			Assert.NotStrictEqual(expected, actual);
		}

		[Fact]
		public void EventDatesBetweenYearlyDatedEvents()
		{
			CalendarDateTime.ClearCalendar();
			DefinedCalendars.USA.Calendars.NewYorkStockExchange();
			var expected = IndependenceDays.Select(s => s.Date);
			var fromDate = IndependenceDays.Min(s => s.Date);
			var toDate = IndependenceDays.Max(s => s.Date);
			var actual = DefinedCalendars.USA.HolidayNames.IndependentsDayText.EventDatesBetween(fromDate, toDate);
			Assert.NotStrictEqual(expected, actual);
		}

		[Fact]
		public void EventDatesBetweenYearlyDayOfWeekEvents()
		{
			CalendarDateTime.ClearCalendar();
			DefinedCalendars.USA.Holidays.ThanksgivingDay(true);
			var expected = ThanksgivingDays.Select(s => s.Date);
			var fromDate = ThanksgivingDays.Min(s => s.Date);
			var toDate = ThanksgivingDays.Max(s => s.Date);
			var actual = DefinedCalendars.USA.HolidayNames.ThanksgivingDayText.EventDatesBetween(fromDate, toDate);
			Assert.NotStrictEqual(expected, actual);
		}

		[Fact]
		public void EventDatesBetweenYearlyDayOfWeekReverse()
		{
			CalendarDateTime.ClearCalendar();
			DefinedCalendars.USA.Holidays.MemorialDay(true);
			var expected = MemorialDays.Select(s => s.Date);
			var fromDate = MemorialDays.Min(s => s.Date);
			var toDate = MemorialDays.Max(s => s.Date);
			var actual = DefinedCalendars.USA.HolidayNames.MemorialDayText.EventDatesBetween(fromDate, toDate);
			Assert.NotStrictEqual(expected, actual);
		}

		[Fact]
		public void EventDatesMonthlyDayOfWeekForwardEvent()
		{
			const string eventName = "Tango";
			const int targetWeek = 2;
			const DayOfWeek targetDayOfWeek = DayOfWeek.Wednesday;

			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddMonthlyDayOfWeekForwardEvent(eventName, true, targetDayOfWeek, targetWeek, StartTestDate, EndTestDate);
			var actual = eventName.EventDatesBetween(StartTestDate, EndTestDate);
			var expected = new List<DateTime>();
			var week = 0;
			for (var date = StartTestDate; date <= EndTestDate; date = date.AddDays(1))
			{
				if (date.IsFirstDayOfMonth())
				{
					week = 0;
				}

				if (date.DayOfWeek == targetDayOfWeek)
				{
					week++;
				}

				if (week == targetWeek && date.DayOfWeek == targetDayOfWeek)
				{
					expected.Add(date);
				}
			}

			expected = expected.OrderBy(s => s).ToList();
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void EventDatesMonthlyOfWeekReverseEvent()
		{
			const string eventName = "Tango";
			const int targetWeek = 2;
			const DayOfWeek targetDayOfWeek = DayOfWeek.Wednesday;

			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddMonthlyDayOfWeekReverseEvent(eventName, true, targetDayOfWeek, targetWeek, StartTestDate, EndTestDate);
			var actual = eventName.EventDatesBetween(StartTestDate, EndTestDate);
			var expected = new List<DateTime>();
			int week = 0;
			for (var date = EndTestDate; date >= StartTestDate; date = date.AddDays(-1))
			{
				if (date.IsLastDayOfMonth())
				{
					week = 0;
				}

				if (date.DayOfWeek == targetDayOfWeek)
				{
					week++;
				}

				if (week == targetWeek && date.DayOfWeek == targetDayOfWeek)
				{
					expected.Add(date);
				}
			}

			expected = expected.OrderBy(s => s).ToList();
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void KeysDatedEvents()
		{
			const string m1 = "m1";
			const string m2 = "m2";
			var mSet = new List<string> { m1, m2 };
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddDateEvent(m1, false, DateTime.Now);
			CalendarDateTime.AddDateEvent(m2, false, DateTime.Now);
			Assert.Equal(mSet, CalendarDateTime.KeysEvents.ToList());
			Assert.Equal(mSet, CalendarDateTime.KeysDateEvents.ToList());
		}

		[Fact]
		public void KeysEvents()
		{
			const string dated = "dated";
			const string weekKey = "WeekKey";
			const string yearlyKey = "YearlyKey";
			const string monthlyKey = "monthlyKey";

			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDateEvent(yearlyKey, false, 1, 2, false, false);
			CalendarDateTime.AddWeeklyInMonthEvent(weekKey, false, DayOfWeek.Friday);
			CalendarDateTime.AddDateEvent(dated, false, DateTime.Now);
			CalendarDateTime.AddMonthlyDateEvent(monthlyKey, true, 2);

			var expected = new List<string> { yearlyKey, weekKey, dated, monthlyKey }.OrderBy(s => s);
			var actual = CalendarDateTime.KeysEvents.ToList().OrderBy(s => s);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void KeysMonthlyEvents()
		{
			const string m1 = "m1";
			const string m2 = "m2";
			var mSet = new List<string> { m1, m2 };
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddMonthlyDateEvent(m1, true, 12);
			CalendarDateTime.AddMonthlyDateEvent(m2, true, 12);
			Assert.Equal(mSet, CalendarDateTime.KeysEvents.ToList());
			Assert.Equal(mSet, CalendarDateTime.KeysMonthlyEvents.ToList());
		}

		[Fact]
		public void KeysWeeklyEvents()
		{
			const string m1 = "m1";
			const string m2 = "m2";
			var mSet = new List<string> { m1, m2 };
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddWeeklyInMonthEvent(m1, false, DayOfWeek.Friday);
			CalendarDateTime.AddWeeklyInMonthEvent(m2, false, DayOfWeek.Friday);
			Assert.Equal(mSet, CalendarDateTime.KeysEvents.ToList());
			Assert.Equal(mSet, CalendarDateTime.KeysWeeklyEvents.ToList());
		}

		[Fact]
		public void KeysYearlyEvents()
		{
			const string m1 = "m1";
			const string m2 = "m2";
			var mSet = new List<string> { m1, m2 };
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDateEvent(m1, false, 1, 2, false, false);
			CalendarDateTime.AddYearlyDateEvent(m2, false, 1, 2, false, false);
			Assert.Equal(mSet, CalendarDateTime.KeysEvents.ToList());
			Assert.Equal(mSet, CalendarDateTime.KeysYearlyEvents.ToList());
		}

		[Fact]
		public void Remove()
		{
			const string monthlyKey = "monthlyKey";
			const string weekKey = "WeekKey";
			const string yearlyKey = "YearlyKey";
			const string dateKey = "dateKey";

			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDateEvent(yearlyKey, false, 1, 2, false, false);
			CalendarDateTime.AddMonthlyDateEvent(monthlyKey, true, 12);
			CalendarDateTime.AddWeeklyInMonthEvent(weekKey, false, DayOfWeek.Friday);
			CalendarDateTime.AddDateEvent(dateKey, false, DateTime.Now);

			Assert.False(CalendarDateTime.RemoveEvent("nothingRemoved"));
			Assert.True(CalendarDateTime.CountEvents == 4);
			Assert.True(CalendarDateTime.RemoveEvent(weekKey));
			Assert.True(CalendarDateTime.CountEvents == 3);
			Assert.True(CalendarDateTime.RemoveEvent(dateKey));
			Assert.True(CalendarDateTime.CountEvents == 2);
			Assert.True(CalendarDateTime.RemoveEvent(yearlyKey));
			Assert.True(CalendarDateTime.CountEvents == 1);
			Assert.False(CalendarDateTime.RemoveEvent("notHere"));
			Assert.True(CalendarDateTime.CountEvents == 1);
			Assert.True(CalendarDateTime.RemoveEvent(monthlyKey));
			Assert.True(CalendarDateTime.CountEvents == 0);
		}

		[Fact]
		public void RemoveEventNullEmptyReturnsFalse()
		{
			Assert.False(CalendarDateTime.RemoveEvent(null));
			Assert.False(CalendarDateTime.RemoveEvent(string.Empty));
		}
	}
}