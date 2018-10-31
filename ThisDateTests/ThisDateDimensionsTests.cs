namespace ThisDate.Tests
{
	/// <summary>
	/// Date Time Dimension Tests
	/// </summary>
	public class ThisDateDimensionsTests
	{
		// TODO: Move to data layer because this is where the DateDimensionInstance is defined.

		///// <summary>
		///// Dates the dimension instance string tests.
		///// </summary>
		///// <exception cref="TrueException">Thrown when the condition is false</exception>
		///// <exception cref="FalseException">Thrown if the condition is not false</exception>
		//[Fact]
		//public void DateDimensionInstanceStringTests()
		//{
		//	var date = new DateTime(2017, 1, 9, 23, 14, 59, 123);
		//	var a = MarketDateTimeDimensions.DateDimensionInstance(date);

		//	Assert.True(a.DateEuro == "2017/1/9", "DateEuro failed");
		//	Assert.True(a.DateId == 20170109);
		//	Assert.True(a.DateTime == date, "DateTime failed");
		//	Assert.True(a.DateEuro == "2017/1/9", "DateEuro failed");
		//	Assert.True(a.DateUsa == "1/9/2017", "DateUsa failed");
		//	Assert.True(a.DayOfMonth == 9, "DayOfMonth failed");
		//	Assert.True(a.DayOfMonthLeadingZero == "09", "DayOfMonth failed");
		//	Assert.True(a.DayOfWeekFullName == "Monday", "DayOfWeekFullName failed");
		//	Assert.True(a.DayOfWeek3LetterName == "Mon", "DayOfWeek3LetterName failed");
		//	Assert.True(a.DayOfWeek2LetterName == "Mo", "DayOfWeek2LetterName failed");
		//	Assert.True(a.DayOfWeekNumber == 1, "DayOfWeekNUmber failed");
		//	Assert.True(a.HolidayName == string.Empty);
		//	Assert.True(a.IsBusinessDay, "IsBusinessDay failed");
		//	Assert.True(a.IsFirstDayOfMonth == false, "IsFirstDayOfMonth failed");
		//	Assert.False(a.IsFirstWeekOfMonth, "IsFirstWeekOfMonth failed");
		//	Assert.False(a.IsHoliday, "IsHoliday failed");
		//	Assert.False(a.IsLastDayOfMonth, "IsLastDayOfMonth failed");
		//	Assert.False(a.IsLastWeekOfMonth, "IsLastWeekOfMonth failed");
		//	Assert.False(a.IsLeapYear, "IsLeapYear failed");
		//	Assert.True(a.IsWeekDay, "IsWeekDay failed");
		//	Assert.False(a.IsWeekend, "IsWeekend failed");
		//	Assert.True(a.DayOfYear == 9, "DayOfYear failed");
		//	Assert.True(a.DayOfYearLeadingZeros == "009", "DayOfYear failed");
		//	Assert.True(a.MonthNameFull == "January", "MonthNameFull failed");
		//	Assert.True(a.MonthNameShort == "Jan", "MonthNameShort failed");
		//	Assert.True(a.MonthNumber == 1, "MonthNUmber failed");
		//	Assert.True(a.MonthNumberLeadZero == "01", "MonthNUmberLeadZero failed");
		//	Assert.True(a.Quarter == 1, "Quarter failed");
		//	Assert.True(a.QuarterLongName == "Quarter 1", "QuarterLoneName failed");
		//	Assert.True(a.QuarterShortName == "Q1");
		//	Assert.True(a.WeekNumber == 2, "WeekNumber failed");
		//	Assert.True(a.WeekNumberLeadingZero == "02", "WeekNUmberLeadingZero failed");
		//	Assert.True(a.Year == 2017, "Year failed");
		//	Assert.True(a.YearShort == "17", "YearShort failed");
		//}

		///// <summary>
		///// Time Dimension Instance test.
		///// </summary>
		///// <exception cref="TrueException">Thrown when the condition is false</exception>
		//[Fact]
		//public void TimeDimensionInstanceTest()
		//{
		//	var date = new DateTime(2017, 1, 9, 3, 4, 9, 123);
		//	var t = MarketDateTimeDimensions.TimeDimensionInstance(date);

		//	Assert.True(t.AmPm == "AM", "t.AmPm == 'AM'");
		//	Assert.True(t.Hour12 == 3, "t.Hour12 == 3");
		//	Assert.True(t.Hour12LeadingZero == "03", "t.Hour12LeadingZero == '04'");
		//	Assert.True(t.Hour24 == 3, "t.Hour24 == 3");
		//	Assert.True(t.Hour24LeadingZero == "03", "t.Hour24LeadingZero");
		//	Assert.True(t.Minute == 4, "t.Minute == 4");
		//	Assert.True(t.MinuteLeadingZero == "04", "t.MinuteLeadingZero == '04'");
		//	Assert.True(t.Second == 9, "t.Second == 9");
		//	Assert.True(t.SecondLeadingZero == "09", "t.SecondLeadingZero == '09'");
		//	Assert.True(t.Time == date, "t.Time == t");
		//	Assert.True(t.Time12HourMin == "3:04", "t.Time12HourMin == '3:04'");
		//	Assert.True(t.Time12HourMinAmPm == "3:04 AM", "t.Time12HourMinAmPm == '3:04 AM'");
		//	Assert.True(t.Time12HourMinSecAmPm == "3:04:09 AM", "t.Time12HourMinSecAmPm == '3:04:09 AM'");
		//	Assert.True(t.Time12HourMinSecMiliAmPm == "3:04:09.123 AM", "t.Time12HourMinSecMiliAmPm == '3:04:09.123 AM'");
		//	Assert.True(t.Time24HourMinCivilian == "03:04", "t.Time24HourMinCivilian == '03:04'");
		//	Assert.True(t.Time24HourMinSecCivilian == "03:04:09", "t.Time24HourMinSecCivilian == '03:04:09'");
		//	Assert.True(t.Time24HourMinSecMiliCivilian == "03:04:09.123", "t.Time24HourMinSecMiliCivilian == '03:04:09.123'");
		//	Assert.True(t.Time24HourMinMilitary == "0304", "t.Time24HourMinMilitary == '0304'");
		//	Assert.True(t.Time24HourMinSecMilitary == "030409", "t.Time24HourMinSecMilitary == '030409'");
		//	Assert.True(t.Time24HourMinSecMiliMilitary == "030409.123", "t.Time24HourMinSecMiliMilitary == '030409.123'");
		//	Assert.True(t.TimeId == "030409123", "t.TimeId == '030409123'");
		//}
	}
}