using System;
using System.Collections.Generic;
using System.Linq;
using ThisDate.DefinedCalendars.USA;
using Xunit;

// ReSharper disable ExceptionNotDocumented
namespace ThisDate.Tests
{
	public partial class ThisDateTests
	{
		private static readonly DateTime EndTestDate = new DateTime(2100, 12, 31);
		private static readonly DateTime StartTestDate = new DateTime(1900, 1, 1);

		[Fact]
		public void AddWorkdaysNegToNewYearsNyse()
		{
			CalendarDateTime.ClearCalendar();
			Calendars.NewYorkStockExchange();
			var date = new DateTime(2016, 1, 26);
			var expected = new DateTime(2015, 12, 31);
			var addWorkDay = date.AddWorkdays(-16);
			Assert.True(expected == addWorkDay);
		}

		[Fact]
		public void AddWorkdaysNewYearsPlus16PastMartinNyse()
		{
			CalendarDateTime.ClearCalendar();
			Calendars.NewYorkStockExchange();
			var date = new DateTime(2016, 1, 1);
			var expected = new DateTime(2016, 1, 26);
			var addWorkDay = date.AddWorkdays(16);
			Assert.True(expected == addWorkDay);
		}

		[Fact]
		public void AddWorkdaysOverNewYearsNyse()
		{
			CalendarDateTime.ClearCalendar();
			Calendars.NewYorkStockExchange();
			var date = new DateTime(2016, 1, 1);
			var expected = new DateTime(2016, 1, 4);
			var addWorkDay = date.AddWorkdays(1);
			Assert.True(expected == addWorkDay);
		}

		[Fact]
		public void AllHolidaysNyse()
		{
			CalendarDateTime.ClearCalendar();
			Calendars.NewYorkStockExchange();
			foreach (var expectedHoliday in NewYorkStockExchangeCalendarTestRig)
			{
				var expected = new List<string> { expectedHoliday.Name };
				if (expectedHoliday.Date.DayOfWeek == DayOfWeek.Saturday || expectedHoliday.Date.DayOfWeek == DayOfWeek.Sunday)
				{
					expected.Add(expectedHoliday.Date.DayOfWeek.ToString());
				}

				var actual = expectedHoliday.Date.EventsOnDate(true, true);
				Assert.NotStrictEqual(actual.ToList(), expected);
				Assert.True(expectedHoliday.Date.IsWorkDay() == expectedHoliday.IsWorkday,
					"Error at: IsWorkDay() " + expectedHoliday.Date.IsWorkDay() + "/" + expectedHoliday.IsWorkday + " (" + expectedHoliday.Name + ", " + expectedHoliday.Date + ")");
			}
		}

		[Fact]
		public void AllHolidaysUsaFederal()
		{
			CalendarDateTime.ClearCalendar();
			Calendars.UsaFederal();
			foreach (var expectedHoliday in UsaGovernmentCalendarTestRig)
			{
				var expected = new List<string>();
				if (expectedHoliday.Date.DayOfWeek == DayOfWeek.Saturday || expectedHoliday.Date.DayOfWeek == DayOfWeek.Sunday)
				{
					expected.Add(expectedHoliday.Date.DayOfWeek.ToString());
				}

				if (!string.IsNullOrEmpty(expectedHoliday.Name))
				{
					expected.Add(expectedHoliday.Name);
				}

				var actual = expectedHoliday.Date.EventsOnDate(true, true);

				Assert.NotStrictEqual(actual.ToList(), expected);
				Assert.True(expectedHoliday.Date.IsWorkDay() == expectedHoliday.IsWorkday,
					"Error at: IsWorkDay() " + expectedHoliday.Date.IsWorkDay() + "/" + expectedHoliday.IsWorkday + " (" + expectedHoliday.Name + ", " + expectedHoliday.Date + ")");
			}
		}

		[Fact]
		public void AllHolidaysUsaObserved()
		{
			CalendarDateTime.ClearCalendar();
			Calendars.UsaObservance();
			foreach (var expectedHoliday in UsaObservedHolidaysTestRig)
			{
				var expected = new List<string>();

				if (!string.IsNullOrEmpty(expectedHoliday.Name))
				{
					expected.Add(expectedHoliday.Name);
				}

				if (expectedHoliday.Date.DayOfWeek == DayOfWeek.Saturday || expectedHoliday.Date.DayOfWeek == DayOfWeek.Sunday)
				{
					expected.Add(HolidayNames.WeekendText);
				}

				var actual = expectedHoliday.Date.EventsOnDate(true, true);
				Assert.NotStrictEqual(actual.ToList(), expected);
				Assert.True(expectedHoliday.Date.IsWorkDay() == expectedHoliday.IsWorkday,
					"Error at: IsWorkDay() " + expectedHoliday.Date.IsWorkDay() + "/" + expectedHoliday.IsWorkday + " (" + expectedHoliday.Name + ", " + expectedHoliday.Date + ")");
			}
		}

		[Fact]
		public void DateIdTest()
		{
			var date = new DateTime(2017, 1, 09);
			var expected = 20170109;
			var actual = date.DateId();
			Assert.True(actual == expected);
		}

		[Fact]
		public void DayOfWeekCountIntoMonth()
		{
			for (var date = StartTestDate; date <= EndTestDate; date = date.AddDays(1))
			{
				var expected = 1;
				for (var d = date.AddDays(-7); d.Month == date.Month; d = d.AddDays(-7))
				{
					expected++;
				}

				var actual = date.DayOfWeekCountInMonth();
				Assert.Equal(expected, actual);
			}
		}

		[Fact]
		public void DayOfWeekCountIntoYear()
		{
			for (var date = StartTestDate; date <= EndTestDate; date = date.AddDays(1))
			{
				var expected = 1;
				for (var d = date.AddDays(-7); d.Year == date.Year; d = d.AddDays(-7))
				{
					expected++;
				}

				var actual = date.DayOfWeekCountInYear();
				Assert.Equal(expected, actual);
			}
		}

		[Fact]
		public void DayOfWeekForwardNegInterval()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.DayOfWeekMonthForward(2016, 12, DayOfWeek.Friday, 1);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.DayOfWeekMonthForward(2016, 12, DayOfWeek.Friday, 0));
		}

		[Fact]
		public void DayOfWeekMonthForwardThrowsMaxMonth()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.DayOfWeekMonthForward(2020, 12, DayOfWeek.Friday, 1);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.DayOfWeekMonthForward(2008, 13, DayOfWeek.Friday, 1));
		}

		[Fact]
		public void DayOfWeekMonthForwardThrowsMonthMin()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.DayOfWeekMonthForward(2020, 1, DayOfWeek.Friday, 1);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.DayOfWeekMonthForward(2008, 0, DayOfWeek.Friday, 1));
		}

		[Fact]
		public void DayOfWeekMonthForwardThrowsNegInterval()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.DayOfWeekMonthForward(2016, 12, DayOfWeek.Friday, 1);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.DayOfWeekMonthForward(2016, 12, DayOfWeek.Friday, 0));
		}

		[Fact]
		public void DayOfWeekMonthForwardThrowsYearMax()
		{
			var date = DateTime.MaxValue;
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.DayOfWeekMonthForward(date.Year, 1, DayOfWeek.Friday, 1);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.DayOfWeekMonthForward(date.Year + 1, date.Month, date.DayOfWeek, 1));
		}

		[Fact]
		public void DayOfWeekMonthForwardThrowsYearMin()
		{
			var date = DateTime.MinValue;
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.DayOfWeekMonthForward(date.Year, 1, DayOfWeek.Friday, 1);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.DayOfWeekMonthForward(date.Year - 1, date.Month, date.DayOfWeek, 1));
		}

		[Fact]
		public void DayOfWeekMonthReverseThrowsMaxMonth()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.DayOfWeekMonthReverse(2020, 12, DayOfWeek.Friday, 1);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.DayOfWeekMonthReverse(2008, 13, DayOfWeek.Friday, 1));
		}

		[Fact]
		public void DayOfWeekMonthReverseThrowsMonthMin()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.DayOfWeekMonthReverse(2020, 1, DayOfWeek.Friday, 1);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.DayOfWeekMonthReverse(2008, 0, DayOfWeek.Friday, 1));
		}

		[Fact]
		public void DayOfWeekMonthReverseThrowsNegInterval()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.DayOfWeekMonthReverse(2016, 12, DayOfWeek.Friday, 1);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.DayOfWeekMonthReverse(2016, 12, DayOfWeek.Friday, 0));
		}

		[Fact]
		public void DayOfWeekMonthReverseThrowsYearMax()
		{
			var date = DateTime.MaxValue;
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.DayOfWeekMonthReverse(date.Year, 1, DayOfWeek.Friday, 1);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.DayOfWeekMonthReverse(date.Year + 1, date.Month, date.DayOfWeek, 1));
		}

		[Fact]
		public void DayOfWeekMonthReverseThrowsYearMin()
		{
			var date = DateTime.MinValue;
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.DayOfWeekMonthReverse(date.Year, date.Month, DayOfWeek.Friday, 1);
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.DayOfWeekMonthReverse(date.Year - 1, date.Month, date.DayOfWeek, 1));
		}

		[Fact]
		public void DayOfWeekReverseNegInterval()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.DayOfWeekMonthReverse(2016, 12, DayOfWeek.Friday, 1);
			Assert.Throws<ArgumentException>(() => CalendarDateTime.DayOfWeekMonthForward(2016, 12, DayOfWeek.Friday, 0));
		}

		[Fact]
		public void EventsOnDateAnnual()
		{
			var offKey = new List<string> { "key1" };
			var workKey = new List<string> { "key2" };
			var bothOffWork = new List<string>();
			bothOffWork.AddRange(offKey);
			bothOffWork.AddRange(workKey);
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddDateEvent(offKey[0], true, DateTime.Now);
			CalendarDateTime.AddDateEvent(workKey[0], false, DateTime.Now);

			var dayOff = DateTime.Now.EventsOnDate(false, true).ToList();
			var workday = DateTime.Now.EventsOnDate(true, false).ToList();
			var both = DateTime.Now.EventsOnDate(true, true).ToList();
			var neither = DateTime.Now.EventsOnDate(false, false).ToList();

			Assert.NotStrictEqual(dayOff, offKey);
			Assert.NotStrictEqual(workday, workKey);
			Assert.Empty(neither);
			Assert.NotStrictEqual(bothOffWork, both);
			Assert.Empty(DateTime.Now.AddDays(1).EventsOnDate(true, true));
		}

		[Fact]
		public void EventsOnDateDate()
		{
			var offKey = new List<string> { "dayOf" };
			var workKey = new List<string> { "dayOn" };
			var bothOffWork = new List<string>();
			bothOffWork.AddRange(offKey);
			bothOffWork.AddRange(workKey);
			var date = DateTime.Now;
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDateEvent(offKey[0], true, date.Month, date.Day, false, false);
			CalendarDateTime.AddYearlyDateEvent(workKey[0], false, date.Month, date.Day, false, false);

			var dayOff = date.EventsOnDate(false, true).ToList();
			var workday = date.EventsOnDate(true, false).ToList();
			var both = date.EventsOnDate(true, true).ToList();
			var neither = date.EventsOnDate(false, false).ToList();

			Assert.NotStrictEqual(dayOff, offKey);
			Assert.NotStrictEqual(workday, workKey);
			Assert.Empty(neither);
			Assert.NotStrictEqual(bothOffWork, both);
			Assert.Empty(date.AddDays(1).EventsOnDate(true, true));
		}

		[Fact]
		public void EventsOnDateMonthlyReverse()
		{
			var offKey = new List<string> { "rev1" };
			var workKey = new List<string> { "rev2" };
			var bothOffWork = new List<string>();
			bothOffWork.AddRange(offKey);
			bothOffWork.AddRange(workKey);
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDayOfWeekReverseEvent(offKey[0], true, 5, 2, DayOfWeek.Monday);
			CalendarDateTime.AddYearlyDayOfWeekReverseEvent(workKey[0], false, 5, 2, DayOfWeek.Monday);

			var testDate = new DateTime(2018, 5, 21);
			var dayOff = testDate.EventsOnDate(false, true).ToList();
			var workday = testDate.EventsOnDate(true, false).ToList();
			var both = testDate.EventsOnDate(true, true).ToList();
			var neither = testDate.EventsOnDate(false, false).ToList();

			Assert.NotStrictEqual(dayOff, offKey);
			Assert.NotStrictEqual(workday, workKey);
			Assert.Empty(neither);
			Assert.NotStrictEqual(bothOffWork, both);
			Assert.Empty(testDate.AddDays(1).EventsOnDate(true, true));
		}

		[Fact]
		public void EventsOnDateYearlyCalculated()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyCalculatedEvent(HolidayNames.EasterSundayText, true);
			CalendarDateTime.AddYearlyCalculatedEvent(HolidayNames.GoodFridayText, false);

			var goodFridayDate = new DateTime(2018, 3, 30);
			var goodFridayDayOff = goodFridayDate.EventsOnDate(false, true);
			var goodFridayWorkday = goodFridayDate.EventsOnDate(true, false);
			var goodFridayBoth = goodFridayDate.EventsOnDate(true, true);
			var goodFridayNeither = goodFridayDate.EventsOnDate(false, false);

			Assert.Empty(goodFridayDayOff);
			Assert.Contains(CalculatedEventsText.GoodFriday, goodFridayWorkday);
			Assert.Empty(goodFridayNeither);
			Assert.Contains(CalculatedEventsText.GoodFriday, goodFridayBoth);

			var easterDate = new DateTime(2018, 4, 1);
			var easterDayOff = easterDate.EventsOnDate(false, true);
			var easterWorkday = easterDate.EventsOnDate(true, false);
			var easterBoth = easterDate.EventsOnDate(true, true);
			var easterNeither = easterDate.EventsOnDate(false, false);

			Assert.Contains(CalculatedEventsText.EasterSunday, easterDayOff);
			Assert.Empty(easterWorkday);
			Assert.Empty(easterNeither);
			Assert.Contains(CalculatedEventsText.EasterSunday, easterBoth);
		}

		[Fact]
		public void EventsOnDateYearlyForward()
		{
			var offKey = new List<string> { "rev1" };
			var workKey = new List<string> { "rev2" };
			var bothOffWork = new List<string>();
			bothOffWork.AddRange(offKey);
			bothOffWork.AddRange(workKey);
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(offKey[0], true, 5, 2, DayOfWeek.Monday);
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(workKey[0], false, 5, 2, DayOfWeek.Monday);

			var testDate = new DateTime(2018, 5, 14);
			var dayOff = testDate.EventsOnDate(false, true).ToList();
			var workday = testDate.EventsOnDate(true, false).ToList();
			var both = testDate.EventsOnDate(true, true).ToList();
			var neither = testDate.EventsOnDate(false, false).ToList();

			Assert.NotStrictEqual(dayOff, offKey);
			Assert.NotStrictEqual(workday, workKey);
			Assert.Empty(neither);
			Assert.NotStrictEqual(bothOffWork, both);
			Assert.Empty(DateTime.Now.AddDays(1).EventsOnDate(true, true));
		}

		[Fact]
		public void EventsOnDateYearlyReverse()
		{
			var offKey = new List<string> { "rev1" };
			var workKey = new List<string> { "rev2" };
			var bothOffWork = new List<string>();
			bothOffWork.AddRange(offKey);
			bothOffWork.AddRange(workKey);
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddYearlyDayOfWeekReverseEvent(offKey[0], true, 5, 2, DayOfWeek.Monday);
			CalendarDateTime.AddYearlyDayOfWeekReverseEvent(workKey[0], false, 5, 2, DayOfWeek.Monday);

			var testDate = new DateTime(2018, 5, 21);
			var dayOff = testDate.EventsOnDate(false, true).ToList();
			var workday = testDate.EventsOnDate(true, false).ToList();
			var both = testDate.EventsOnDate(true, true).ToList();
			var neither = testDate.EventsOnDate(false, false).ToList();

			Assert.NotStrictEqual(dayOff, offKey);
			Assert.NotStrictEqual(workday, workKey);
			Assert.Empty(neither);
			Assert.NotStrictEqual(bothOffWork, both);
			Assert.Empty(testDate.AddDays(1).EventsOnDate(true, true));
		}

		[Fact]
		public void GetWeekOfMonthCurrentDate()
		{
			var currentDate = DateTime.Now;
			var testDate = new DateTime(currentDate.Year, currentDate.Month, 1);
			var week = testDate.DayOfWeek == DayOfWeek.Sunday ? 0 : 1;
			while (testDate < DateTime.Now)
			{
				if (testDate.DayOfWeek == DayOfWeek.Sunday)
					week++;

				testDate = testDate.AddDays(1);
			}

			var x = currentDate.WeekOfMonth();
			Assert.True(x == week, "Fail on: " + testDate);
		}

		[Fact]
		public void GetWeekOfMonthMultipleYears()
		{
			var date = StartTestDate;
			var week = 0;

			while (date.Year <= EndTestDate.Year)
			{
				if (date.Day == 1)
					week = 1;

				Assert.Equal(date.WeekOfMonth(), week);
				date = date.AddDays(1);
				if (date.DayOfWeek == DayOfWeek.Sunday)
					week++;
			}
		}

		[Fact]
		public void GetWeekOfYearCurrentDate()
		{
			var currentDate = DateTime.Now;
			var testDate = new DateTime(currentDate.Year, 1, 1);
			var week = testDate.DayOfWeek == DayOfWeek.Sunday ? 0 : 1;
			while (testDate < currentDate)
			{
				if (testDate.DayOfWeek == DayOfWeek.Sunday)
					week++;

				testDate = testDate.AddDays(1);
			}

			Assert.Equal(currentDate.WeekOfYear(), week);
		}

		[Fact]
		public void GetWeekOfYearMultipleYears()
		{
			var date = StartTestDate;
			var week = 0;

			while (date.Year <= EndTestDate.Year)
			{
				if (date.Month == 1 && date.Day == 1)
					week = 1;

				Assert.Equal(date.WeekOfYear(), week);
				date = date.AddDays(1);
				if (date.DayOfWeek == DayOfWeek.Sunday)
					week++;
			}
		}

		[Fact]
		public void IsBetweenEqualBetweenCase()
		{
			var date = new DateTime(2017, 1, 30, 16, 10, 0);
			var date1 = new DateTime(2016, 1, 30, 16, 18, 0);
			var date2 = new DateTime(2018, 1, 30, 16, 18, 0);
			var actual = date.IsBetweenEqual(date1, date2);
			Assert.True(actual);
		}

		[Fact]
		public void IsBetweenEqualEqualCase()
		{
			var date = new DateTime(2017, 1, 30, 16, 0, 0);
			var actual = date.IsBetweenEqual(date, date);
			Assert.True(actual);
		}

		[Fact]
		public void IsBetweenEqualGreaterCase()
		{
			var date = new DateTime(2020, 1, 30, 16, 10, 0);
			var date1 = new DateTime(2018, 1, 30, 16, 18, 0);
			var date2 = new DateTime(2000, 1, 30, 17, 0, 0);
			var actual = date.IsBetweenEqual(date1, date2);
			Assert.False(actual);
		}

		[Fact]
		public void IsBetweenEqualLesserCase()
		{
			var date = new DateTime(2010, 1, 30, 16, 10, 0);
			var date2 = new DateTime(2017, 1, 30, 16, 18, 0);
			var date1 = new DateTime(2017, 1, 30, 17, 0, 0);
			var actual = date.IsBetweenEqual(date1, date2);
			Assert.False(actual);
		}

		[Fact]
		public void IsBetweenEqualWithWithTime()
		{
			var sDate = DateTime.Now.AddTicks(-1);
			var date = DateTime.Now;
			var eDate = date;
			Assert.True(date.IsBetweenEqual(sDate, eDate));
			Assert.False(date.AddTicks(1).IsBetweenEqual(sDate, eDate));
		}

		[Fact]
		public void IsBusinessDayNyse()
		{
			CalendarDateTime.ClearCalendar();
			Calendars.NewYorkStockExchange();
			var currentDate = _holidayMinDateTestRig;
			while (currentDate <= _holidayMaxDateTestRig)
			{
				var expected = currentDate.DayOfWeek != DayOfWeek.Saturday
							   && currentDate.DayOfWeek != DayOfWeek.Sunday
							   && NewYorkStockExchangeCalendarTestRig.All(a => a.Date != currentDate);
				var actual = currentDate.IsWorkDay();
				Assert.True(expected == actual, "Fail on: " + currentDate);
				currentDate = currentDate.AddDays(1);
			}
		}

		[Fact]
		public void IsDayOffAnnualFalse()
		{
			const string may1Name = "A Test";
			var may1Date = DateTime.Now;
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddDateEvent(may1Name, false, may1Date);
			Assert.False(may1Date.IsDayOff());
		}

		[Fact]
		public void IsDayOffAnnualTrue()
		{
			const string may1Name = "A Test";
			var may1Date = DateTime.Now;
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddDateEvent(may1Name, true, may1Date);
			Assert.True(may1Date.IsDayOff());
		}

		[Fact]
		public void IsFirstDayOfMonth()
		{
			for (var currentDate = StartTestDate; currentDate <= EndTestDate; currentDate = currentDate.AddDays(1))
			{
				var expected = currentDate.Day == 1;
				var actual = currentDate.IsFirstDayOfMonth();
				Assert.True(expected == actual);
			}
		}

		[Fact]
		public void IsFirstDayOfMonthWithTime()
		{
			var date = new DateTime(2018, 8, 1, 12, 32, 5);
			Assert.True(date.IsFirstDayOfMonth());
		}

		[Fact]
		public void IsFirstWeekOfMonth()
		{
			var currentDate = StartTestDate;
			var currentWeek = 1;

			while (currentDate <= EndTestDate)
			{
				var expected = currentWeek == 1;
				var actual = currentDate.IsFirstWeekOfMonth();
				Assert.True(expected == actual);

				currentDate = currentDate.AddDays(1);
				if (currentDate.Day == 1)
					currentWeek = 1;
				else if (currentDate.DayOfWeek == DayOfWeek.Sunday)
					currentWeek++;
			}
		}

		[Fact]
		public void IsFirstWeekOfMonthWithTime()
		{
			var date = new DateTime(2018, 8, 4, 23, 32, 5);
			Assert.True(date.IsFirstWeekOfMonth());
		}

		[Fact]
		public void IsLastDayOfMonthMultipleYears()
		{
			var date = StartTestDate;
			while (date <= StartTestDate)
			{
				var lastMonthDay = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
				var expected = lastMonthDay == date;
				var actual = date.IsLastDayOfMonth();
				Assert.Equal(expected, actual);
				date = date.AddDays(1);
			}
		}

		[Fact]
		public void IsLastDayOfMonthWithTime()
		{
			var date = new DateTime(2018, 8, 31, 12, 32, 5);
			Assert.True(date.IsLastDayOfMonth());
		}

		[Fact]
		public void IsLastWeekOfMonthMultipleYears()
		{
			var date = StartTestDate;
			while (date <= StartTestDate)
			{
				var lastMonthDay = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
				var lastSunday = lastMonthDay;
				while (lastSunday.DayOfWeek != DayOfWeek.Sunday)
					lastSunday = lastSunday.AddDays(-1);

				var isLastWeekExpected = date >= lastSunday && date <= lastMonthDay;
				var isLastWeek = date.IsLastWeekOfMonth();
				Assert.Equal(isLastWeek, isLastWeekExpected);
				date = date.AddDays(1);
			}
		}

		[Fact]
		public void IsLastWeekOfMonthWithTime()
		{
			var date = new DateTime(2018, 8, 31, 12, 32, 5);
			Assert.True(date.IsLastWeekOfMonth());
		}

		[Fact]
		public void IsNthDayOfWeek3RdThursdayAllMonths()
		{
			const int count = 3;
			var currentDate = StartTestDate;
			var nthThursday = 0;

			while (currentDate <= EndTestDate)
			{
				if (currentDate.Day == 1)
					nthThursday = 0;

				if (currentDate.DayOfWeek == DayOfWeek.Thursday)
					nthThursday++;

				var expected = currentDate.DayOfWeek == DayOfWeek.Thursday && nthThursday == count;
				var actual = currentDate.IsNthDayOfWeek(count, DayOfWeek.Thursday);
				Assert.True(
					expected == actual,
					"Date: " + currentDate + ", " + currentDate.DayOfWeek + ", expected: " + expected + ", actual: " + actual);
				currentDate = currentDate.AddDays(1);
			}
		}

		[Fact]
		public void IsNthDayOfWeekWithTime()
		{
			var date = new DateTime(2018, 8, 31, 12, 32, 5);
			Assert.True(date.IsNthDayOfWeek(5, DayOfWeek.Friday));
		}

		[Fact]
		public void IsOffDay1And3Wednesday()
		{
			var testDayOfWeek = DayOfWeek.Wednesday;
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddWeeklyInMonthEvent("FirstThird", true, testDayOfWeek, new List<int> { 1, 3 });
			var occurrence = -99;
			var currentDate = StartTestDate;
			var dayOfWeekInc = currentDate.DayOfWeek;
			var dateEnd = EndTestDate;
			while (currentDate <= dateEnd)
			{
				if (currentDate.Day == 1)
				{
					occurrence = 1;
					dayOfWeekInc = currentDate.DayOfWeek;
				}
				else if (currentDate.DayOfWeek == dayOfWeekInc)
				{
					occurrence++;
				}

				var expected = currentDate.DayOfWeek == testDayOfWeek && (occurrence == 1 || occurrence == 3);
				var actual = currentDate.IsDayOff();
				Assert.True(expected == actual, "Failed on, " + currentDate);
				currentDate = currentDate.AddDays(1);
			}
		}

		[Fact]
		public void IsOffDaySaturdayOnly()
		{
			CalendarDateTime.ClearCalendar();
			CalendarDateTime.AddWeeklyInMonthEvent(DayOfWeek.Saturday.ToString(), true, DayOfWeek.Saturday);
			var currentDate = StartTestDate;
			var dateEnd = EndTestDate;
			while (currentDate <= dateEnd)
			{
				var expected = currentDate.DayOfWeek == DayOfWeek.Saturday;
				var actual = currentDate.IsDayOff();
				Assert.True(expected == actual, "Failed on, " + currentDate);
				currentDate = currentDate.AddDays(1);
			}
		}

		[Fact]
		public void IsOffDayWeekends()
		{
			CalendarDateTime.ClearCalendar();
			Holidays.WeeklyDayOff(DayOfWeek.Saturday);
			Holidays.WeeklyDayOff(DayOfWeek.Sunday);
			var currentDate = StartTestDate;
			var dateEnd = EndTestDate;
			while (currentDate <= dateEnd)
			{
				var expected = currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday;
				var actual = currentDate.IsDayOff();
				Assert.True(expected == actual, "Failed on, " + currentDate);
				currentDate = currentDate.AddDays(1);
			}
		}

		[Fact]
		public void IsWeekDayAll()
		{
			var currentDate = StartTestDate;

			while (currentDate <= EndTestDate)
			{
				var expected = currentDate.DayOfWeek == DayOfWeek.Monday || currentDate.DayOfWeek == DayOfWeek.Tuesday
							   || currentDate.DayOfWeek == DayOfWeek.Wednesday || currentDate.DayOfWeek == DayOfWeek.Thursday
							   || currentDate.DayOfWeek == DayOfWeek.Friday;
				Assert.True(expected == currentDate.IsWeekDay(), "Fail on: " + currentDate);
				currentDate = currentDate.AddDays(1);
			}
		}

		[Fact]
		public void IsWeekDayWithTime()
		{
			var date = new DateTime(2018, 8, 17, 23, 32, 5);
			Assert.True(date.IsWeekDay());
		}

		[Fact]
		public void IsWeekend()
		{
			var currentDate = StartTestDate;

			while (currentDate <= EndTestDate)
			{
				var expected = currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday;
				Assert.True(expected == currentDate.IsWeekend());
				currentDate = currentDate.AddDays(1);
			}
		}

		[Fact]
		public void IsWeekEndWithTime()
		{
			var date = new DateTime(2018, 8, 18, 23, 32, 5);
			Assert.True(date.IsWeekend());
		}

		[Fact]
		public void LastDateOfMonthThrowsMonth0()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.LastDateOfMonth(2018, 0));
		}

		[Fact]
		public void LastDateOfMonthThrowsMonth13()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.LastDateOfMonth(2018, 13));
		}

		[Fact]
		public void LastDateOfMonthThrowsYeaOutOfRangeHigh()
		{
			var highYear = DateTime.MaxValue.Year + 1;
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.LastDateOfMonth(highYear, 5));
		}

		[Fact]
		public void LastDateOfMonthThrowsYeaOutOfRangeLow()
		{
			var lowYear = DateTime.MinValue.Year - 1;
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.LastDateOfMonth(lowYear, 5));
		}

		[Fact]
		public void LastDayOfMonthDateTime()
		{
			var month = -1;
			var lastDay = DateTime.MaxValue;

			for (var date = EndTestDate; date >= StartTestDate; date = date.AddDays(-1))
			{
				if (date.Month != month)
				{
					month = date.Month;
					lastDay = date;
				}

				var actual = date.LastDateOfMonth();
				Assert.True(actual == lastDay, "Fail at: " + date);
			}
		}

		[Fact]
		public void LeapYears()
		{
			for (var year = LeapYearMinTestRig; year <= LeapYearMaxTestRig; year++)
			{
				var expected = LeapYearsTestRig.Contains(year);
				var date = new DateTime(year, 1, 1);
				var actual = date.IsLeapYear();
				Assert.True(expected == actual, "Incorrect leap year: " + year);
			}
		}

		[Fact]
		public void MinMaxSwap()
		{
			// Test date swap a>b => b>a
			const string testName = "Test";
			var a = new DateTime(2018, 6, 1);
			var b = new DateTime(2018, 5, 1);
			ThisDate.CalendarDateTime.AddMonthlyDateEvent(testName, false, 1, a, b);
			Assert.Contains(testName, a.EventsOnDate(true, true));
		}

		[Fact]
		public void NthDayOfWeekOccurrenceOfMonth()
		{
			var dayOccurrence = new int[7];
			var currentDate = _holidayMinDateTestRig;

			while (currentDate <= _holidayMaxDateTestRig)
			{
				if (currentDate.Day == 1)
					Array.Clear(dayOccurrence, 0, 7);

				var dayNo = (int)currentDate.DayOfWeek;
				dayOccurrence[dayNo]++;
				var actual = currentDate.IsNthDayOfWeek(dayOccurrence[dayNo], currentDate.DayOfWeek);
				Assert.True(actual, "Fail on: " + currentDate);
				currentDate = currentDate.AddDays(1);
			}
		}

		[Fact]
		public void Quarter()
		{
			for (var date = new DateTime(2017, 1, 2); date <= new DateTime(2017, 12, 15); date = date.AddMonths(1))
			{
				var actual = date.Quarter();
				if (date.Month <= 3)
					Assert.True(actual == 1, "Mismatch Quarter, 1");
				else if (date.Month <= 6)
					Assert.True(actual == 2, "Mismatch quarter 2");
				else if (date.Month <= 9)
					Assert.True(actual == 3, "Mismatch quarter 3");
				else if (date.Month <= 12)
					Assert.True(actual == 4, "Mismatch quarter 4");
			}
		}

		[Fact]
		public void QuarterLong()
		{
			for (var date = new DateTime(2017, 1, 2); date <= new DateTime(2017, 12, 15); date = date.AddMonths(1))
			{
				var actual = date.QuarterLong();
				if (date.Month <= 3)
					Assert.True(actual == "Quarter 1", "Mismatch Quarter 1");
				else if (date.Month <= 6)
					Assert.True(actual == "Quarter 2", "Mismatch quarter 2");
				else if (date.Month <= 9)
					Assert.True(actual == "Quarter 3", "Mismatch quarter 3");
				else if (date.Month <= 12)
					Assert.True(actual == "Quarter 4", "Mismatch quarter 4");
			}
		}

		[Fact]
		public void QuarterShort()
		{
			for (var date = new DateTime(2017, 1, 2); date <= new DateTime(2017, 12, 15); date = date.AddMonths(1))
			{
				var actual = date.QuarterShort();
				if (date.Month <= 3)
					Assert.True(actual == "Q1", "Mismatch Quarter, 1");
				else if (date.Month <= 6)
					Assert.True(actual == "Q2", "Mismatch quarter 2");
				else if (date.Month <= 9)
					Assert.True(actual == "Q3", "Mismatch quarter 3");
				else if (date.Month <= 12)
					Assert.True(actual == "Q4", "Mismatch quarter 4");
			}
		}

		[Fact]
		public void RoundToHour()
		{
			var timeNow = DateTime.Now;
			var rounded = timeNow + new TimeSpan(0, 0, 30, 0);
			var expected = new DateTime(rounded.Year, rounded.Month, rounded.Day, rounded.Hour, 0, 0, 0);
			var actual = timeNow.RoundToHour();
			Assert.True(actual == expected);
		}

		[Fact]
		public void RoundToInterval()
		{
			var testTime = new DateTime(2017, 1, 15, 19, 55, 55);
			var interval = new TimeSpan(0, 0, 15, 0);
			var expected = new DateTime(2017, 1, 15, 20, 00, 00);
			var actual = testTime.RoundToInterval(interval);
			Assert.True(expected == actual);
		}

		[Fact]
		public void RoundToMinute()
		{
			var timeNow = DateTime.Now;
			var rounded = timeNow + new TimeSpan(0, 0, 0, 30);
			var expected = new DateTime(rounded.Year, rounded.Month, rounded.Day, rounded.Hour, rounded.Minute, 0, 0);
			var actual = timeNow.RoundToMinute();
			Assert.True(actual == expected);
		}

		[Fact]
		public void RoundToSecond()
		{
			var timeNow = DateTime.Now;
			var rounded = timeNow + new TimeSpan(0, 0, 0, 0, 500);
			var expected = new DateTime(rounded.Year, rounded.Month, rounded.Day, rounded.Hour, rounded.Minute, rounded.Second, 0);
			var actual = timeNow.RoundToSecond();
			Assert.True(actual == expected);
		}

		[Fact]
		public void TimeId()
		{
			var date = new DateTime(2017, 1, 10, 9, 2, 1, 999);
			const string expected = "090201999";
			var actual = date.TimeId();
			Assert.True(expected == actual);
		}

		[Fact]
		public void TimeIdMilitary()
		{
			var date = new DateTime(2017, 1, 10, 19, 58, 52, 999);
			const string expected = "195852999";
			var actual = date.TimeId();
			Assert.True(expected == actual);
		}

		[Fact]
		public void TimeIdToHour()
		{
			var current = DateTime.Now;
			var rounded = current + new TimeSpan(0, 0, 30, 0, 0);
			var expected = (new DateTime(rounded.Year, rounded.Month, rounded.Day, rounded.Hour, 0, 0, 0)).ToString("HHmmssfff");
			var actual = current.TimeIdToHour();
			Assert.True(actual == expected, "actual: " + actual + ", expected: " + expected);
		}

		[Fact]
		public void TimeIdToInterval()
		{
			var current = DateTime.Now;
			var interval = new TimeSpan(0, 0, 15, 0);
			var rounded = current.RoundToInterval(interval);
			var expected = (new DateTime(rounded.Year, rounded.Month, rounded.Day, rounded.Hour, rounded.Minute, 0)).ToString("HHmmssfff");
			var actual = current.TimeIdToInterval(interval);
			Assert.True(actual == expected, "actual: " + actual + ", expected: " + expected);
		}

		[Fact]
		public void TimeIdToMinute()
		{
			var current = DateTime.Now;
			var rounded = current + new TimeSpan(0, 0, 0, 30);
			var expected = (new DateTime(rounded.Year, rounded.Month, rounded.Day, rounded.Hour, rounded.Minute, 0, 0)).ToString("HHmmssfff");
			var actual = current.TimeIdToMinute();
			Assert.True(actual == expected, "actual: " + actual + ", expected: " + expected);
		}

		[Fact]
		public void TimeIdToSecond()
		{
			var current = DateTime.Now;
			var rounded = current + new TimeSpan(0, 0, 0, 0, 500);
			var expected = (new DateTime(rounded.Year, rounded.Month, rounded.Day, rounded.Hour, rounded.Minute, rounded.Second, 0)).ToString("HHmmssfff");
			var actual = current.TimeIdToSecond();
			Assert.True(actual == expected, "actual: " + actual + ", expected: " + expected);
		}

		[Fact]
		public void ToLastTick()
		{
			var random = new Random();
			for (var i = 0; i < 50; i++)
			{
				var r = RandomDate(random);
				var a = r.ToLastTick().AddTicks(1);
				var e = r.AddDays(1).Date;
				Assert.Equal(e, a);
			}
		}

		[Fact]
		public void WeekendAdjustDate()
		{
			for (var date = StartTestDate; date <= EndTestDate; date = date.AddDays(1))
			{
				var expected = date;

				switch (date.DayOfWeek)
				{
					case DayOfWeek.Saturday:
						expected = date.AddDays(-1);
						break;

					case DayOfWeek.Sunday:
						expected = date.AddDays(1);
						break;
				}

				var actual = date.WeekendAdjustDate(true, true);
				Assert.True(actual == expected, "Fail, actual = " + actual + " expected = " + expected);
			}
		}

		[Fact]
		public void WeeksInMonthDateTime()
		{
			var currentMonth = -1;
			var weekCounter = -1;

			for (var date = StartTestDate; date <= EndTestDate; date = date.AddDays(1))
			{
				if (date.Month != currentMonth)
				{
					currentMonth = date.Month;
					weekCounter = 0;
				}

				if (date.Day % 7 == 0)
					weekCounter++;

				if (date.Month != date.AddDays(1).Month)
				{
					var actual = date.WeeksInMonth();
					Assert.True(actual == weekCounter, "Week count mismatch, actual = " + actual + ", expected = " + weekCounter + ", " + date);
				}
			}
		}

		[Fact]
		public void WeeksInMonthThrowsMonth0()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.WeeksInMonth(2018, 0));
		}

		[Fact]
		public void WeeksInMonthThrowsMonth13()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.WeeksInMonth(2018, 13));
		}

		[Fact]
		public void WeeksInMonthThrowsYeaOutOfRangeHigh()
		{
			var highYear = DateTime.MaxValue.Year + 1;
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.WeeksInMonth(highYear, 5));
		}

		[Fact]
		public void WeeksInMonthThrowsYeaOutOfRangeLow()
		{
			var lowYear = DateTime.MinValue.Year - 1;
			Assert.Throws<ArgumentOutOfRangeException>(() => CalendarDateTime.WeeksInMonth(lowYear, 5));
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Random date generator. </summary>
		///
		/// <param name="random">	The random object. </param>
		///
		/// <returns>	A DateTime. </returns>
		///-------------------------------------------------------------------------------------------------
		private DateTime RandomDate(Random random)
		{
			var year = random.Next(DateTime.MinValue.Year, DateTime.MaxValue.Year);
			var month = random.Next(1, 12);
			var monthMax = ThisDate.CalendarDateTime.LastDateOfMonth(year, month).Day;
			var day = random.Next(1, monthMax);
			var hour = random.Next(1, 23);
			var min = random.Next(1, 59);
			var sec = random.Next(1, 59);
			var mSec = random.Next(1, 999);
			var date = new DateTime(year: year, month: month, day: day, hour: hour, minute: min, second: sec, millisecond: mSec);
			return date;
		}
	}
}