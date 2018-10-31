<img src="Images\Clock500x500.png" width="160px" />

# ThisDate

ThisDate extends DateTime with additional time, date, and calendar methods. A few examples are IsWorkday(), AddWorkdays(), time rounding, IsLastWeekMonth(), get the 3rd Thursday of November (US Thanksgiving), EventsOnDate(), EventDatesBetween(), and more. ThisDate started as a New York Stock Exchange (NYSE) date and time dimension table builder and has evolved into a toolset with broader uses and capability.     



ThisDate was built on .net Core 2.x, Visual Studio 2017, Resharper, xUnit tested, documented using Atomineer and DocFx.  Installable using NuGit, and the source code is available on GitHub which includes a date/time dimension table builder using Entity Framework Core. 



## Install:

To install ThisDate, run the following command in the Package Manage Console

	PM> Install-Package ThisDate

## Quick Start:

1. Install using the Package Manager Console as described above.
2. Create a console app on Visual Studio.
3. Copy/paste the sample code below. 
4. The sample code loads a a predefined New York Stock Exchange (NYSE) calendar.   

##### Quick Start Code Examples:

```
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ThisDate;												// ThisDate main reference.
using ThisDate.DefinedCalendars.USA;	// (optional if defining your own)

namespace ConsoleApp1
{
 internal class Program
 {
  private static void Main()
  {
   // Configuration for the NYSE calendar.
   // See documentation for custom configuration options.
   ThisDate.DefinedCalendars.USA.Calendars.NewYorkStockExchange();
	 
   // some random test date/time.
   var aDate = new DateTime(2018, 5, 31, 21, 44, 50);  

   var isLastday = aDate.IsLastWeekOfMonth(); 					// True
   var isWorkday = aDate.IsWorkDay();         					// True
   var isDayOff = aDate.IsDayOff();           					// False
   var weekOfYear = aDate.WeekOfYear();       					// 22
   var weekOfMonth = aDate.WeekOfMonth();     					// 5
   var quarterNo = aDate.Quarter();           					// 2
   var quarterShort = aDate.QuarterShort();   					// "Q2"
   var quarterLong = aDate.QuarterLong();     					// "Quarter 2" 
   var isWeekend = aDate.IsWeekend();         					// False
   var roundToMinute = aDate.RoundToMinute(); 					// 2018/5/1 21:45:00.0
   var roundToHour = aDate.RoundToHour();	    					// 2018/5/1 22:00:00.0
   var isLastWeek = aDate.IsLastWeekOfMonth();					// True
   var thirtyWorkDaysForward = aDate.AddWorkdays(30);  // Skips holidays, weekends...
   var thirtyWorkdaysBack = aDate.AddWorkdays(-30);    // 
   var events = aDate.EventsOnDate(true, true);        // {}, no holidays on this date.
   var weeks = CalendarDateTime.WeeksInMonth(2018, 5); // 
   var nyseHolidayNames = CalendarDateTime.KeysEvents; // All the NYSE holidays
   var isLastDayOfMonth = aDate.IsLastDayOfMonth();    // False
   
   // "Martin Luther King Day"
   var h = HolidayNames.MartinLutherKingText.EventDatesBetween(2010, 2020);
   
   var ForthThurday2018Date = 
     			CalendarDateTime.DayOfWeekMonthForward(2018, 11, DayOfWeek.Thursday, 4); 
     			// Thursday, November 22 (2018/11/22), US Thanksgiving.
	
   var lastDayOfMonth = CalendarDateTime.LastDateOfMonth(2018, 5);             // 31
   var isLastWeekOfMonth = aDate.IsLastWeekOfMonth();                          // True
   var aIsFithWendnesdayOfMonth = aDate.IsNthDayOfWeek(5, DayOfWeek.Wednesday);// True

   var anotherDate = new DateTime(2018, 11, 22);
   var eventsOnThisDay = CalendarDateTime.EventsOnDate(anotherDate, true, true);   
       // {"Thanksgiving Day"}

   // LINQ example.
   // Test list of all days in May, 2018
   var allDaysInMay = Enumerable.Range(1, DateTime.DaysInMonth(2018, 5))
   										.Select(day => new DateTime(2018, 5, day));
			
   var workdays = allDaysInMay.Where(d => d.IsWorkDay()); // All workdays
   var daysOff = allDaysInMay.Where(d => d.IsDayOff());		// All days off

   
   var fromDate = new DateTime(2018, 5, 1);
   var toDate = new DateTime(2018, 5, 31);
   var golfDays = new List<DayOfWeek> { DayOfWeek.Wednesday, DayOfWeek.Friday };
   
   // Add golf days off Wednesdays and Fridays in May to the calendar.
   CalendarDateTime
   .AddWeeklyEvent("Golf days off in May!",true,golfDays,fromDate,1,fromDate, toDate); 

   // Add Easter Sunday, events starts in the year 30 to DateTime.MaxValue.
   CalendarDateTime.AddYearlyCalculatedEvent(HolidayNames.EasterSundayText, true, 
   		new DateTime(30, 1, 1)); // Add easter,

   // The calendar is static so once defined it's global.
   var someClass = new SomeClass();
   var allHolidays = someClass.GetHolidayList();
   var easterDateList = someClass.EasterDateRange(1983, 2020);

   // Remove Easter Sunday from the calendar.
   CalendarDateTime.RemoveEvent(HolidayNames.EasterSundayText);

   var countOfCalendarEvents = CalendarDateTime.CountEvents;
   var countOfWeeklyEvents = 
   		CalendarDateTime.CountWeeklyEvents; // 2, Wednesday & Friday golfdays.

   // ...
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
```



## Working with ThisDate

##### Conventions/calling methods

1) Other than configuration, all methods are static extension methods, LINQ compatible. Only calendar related methods will require a defined calendar. There are a few predefined calendars but mostly these serve as templates for building a custom calendar and testing.    

```
// A typical method
var IsWorkDay(this dateTime) ;	// The 'this' identifies extension method. 
```

Extension methods can be called in the following ways:

```
var date = new DateTime.Now; 		// some date
var result1 = IsWorkDate(date);
var result2 = date.IsWorkDate();

// Using LINQ, get all the working days from a list.
var allWorkDays = DatesList.Where(s => s.IsWorkDate());

```

### Configuration

Methods not related to a calendar require no configuration while calendar methods depend on a calendar definition. ThisDate comes with a few US Government and the New York Stock Exchange (NYSE) calendars, others may be added however most will need to need to build a custom configuration. 



### Configuration Pattern Template

##### A literal string class (optional but recommended)

```
namespace ThisDate.DefinedCalendars.USA
{
	public static class HolidayNames
	{
		public static string ChristmasDayText => "Christmas Day";
		public static string ColumbusDayText => "Columbus Day";
		public static string EasterSundayText => "Easter Sunday";
		public static string FathersDayText => "Father's Day";
		public static string GoodFridayText => "Good Friday";
		public static string GroundHogDayText => "Groundhog Day";
		public static string HalloweenText => "Halloween";
		public static string IndependentsDayText => "Independence Day";
		public static string LaborDayText => "Labor Day";
		public static string MartinLutherKingText => "Martin Luther King Jr. Day";
		public static string MemorialDayText => "Memorial Day";
		public static string MothersDayText => "Mother's Day";
		public static string NewYearsDayText => "New Year's Day";
		public static string PresidentsDayText => "Presidents Day";
		public static string SaintPatrickDayText => "Saint Patrick's Day";
		public static string ThanksgivingDayText => "Thanksgiving Day";
		public static string ValentinesDayText => "Valentine's Day";
		public static string VeteransDayText => "Veteran's Day";
		public static string WeekendText => "Weekend";
	}
}

Although not required, its good practice to avoid literal "text" literal strings to avoid sneaky typos.
```



##### Calendar definition:

```
	public static class Calendars
	{
		public static void NewYorkStockExchange()
		{
			Holidays.NewYearsDay(false, true, true);
			Holidays.MartinLutherKingDay(true);
			Holidays.PresidentsDay(true);
			Holidays.GoodFriday(true);
			Holidays.MemorialDay(true);
			Holidays.IndependenceDay(true, true, true);
			Holidays.LaborDay(true);
			Holidays.ThanksgivingDay(true);
			Holidays.ChristmasDay(true, true, true);
			Holidays.Weekends();
		}
	}
```

##### Holiday definitions:

```
using System;

namespace ThisDate.DefinedCalendars.USA
{
		public static class Holidays
	{
		public static void ChristmasDay(bool saturdayBack, bool sundayForward, bool dayOff)
		{
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.ChristmasDayText, dayOff, 12, 																						25, saturdayBack, sundayForward);
		}

		public static void ColumbusDay(bool dayOff)
		{
			var start = new DateTime(1492, 1, 1);
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(HolidayNames.ColumbusDayText, 																							dayOff, 10, 2, DayOfWeek.Monday, start);
		}

		public static void EasterSunday(bool dayOff)
		{
			var start = new DateTime(30, 1, 1);
			CalendarDateTime.AddYearlyCalculatedEvent(HolidayNames.EasterSundayText, dayOff, 																									start);
		}

		public static void FathersDay(bool dayOff)
		{
			var start = new DateTime(1910, 1, 1);
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(HolidayNames.FathersDayText, 																					dayOff, 6, 3, DayOfWeek.Sunday, start);
		}

		public static void GoodFriday(bool dayOff)
		{
			var start = new DateTime(30, 1, 1); 
			CalendarDateTime.AddYearlyCalculatedEvent(HolidayNames.GoodFridayText, dayOff, 																									start);
		}

		public static void GroundhogDay(bool dayOff)
		{
			var start = new DateTime(1887, 1, 1);
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.GroundHogDayText, dayOff, 2, 2, 																						false, false, start);
		}

		public static void Halloween(bool dayOff)
		{
			var start = new DateTime(1850, 1, 1);   // Seems no official start year
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.HalloweenText, dayOff, 10, 31, 																					false, false, start);
		}

		public static void IndependenceDay(bool saturdayBack, bool sundayForward, bool 																						dayOff)
		{
			var start = new DateTime(1776, 1, 1);   // Seems no official start year
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.IndependentsDayText, dayOff, 7, 																						4, saturdayBack, sundayForward, start);
		}

		public static void LaborDay(bool dayOff)
		{
			var start = new DateTime(1894, 1, 1);
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(HolidayNames.LaborDayText, 																									dayOff, 9, 1, DayOfWeek.Monday, start);
		}

		public static void MartinLutherKingDay(bool dayOff)
		{
			var start = new DateTime(1986, 1, 1);
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(HolidayNames.MartinLutherKingText, 																		dayOff, 1, 3, DayOfWeek.Monday, start);
		}

		public static void MemorialDay(bool dayOff)
		{
			var startDate = new DateTime(1868, 1, 1);
			CalendarDateTime.AddYearlyDayOfWeekReverseEvent(HolidayNames.MemorialDayText, 																		dayOff, 5, 1, DayOfWeek.Monday, startDate);
		}

		public static void MothersDay(bool dayOff)
		{
			var start = new DateTime(1914, 1, 1);
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(HolidayNames.MothersDayText, 																					dayOff, 5, 2, DayOfWeek.Sunday, start);
		}

		public static void NewYearsDay(bool saturdayBack, bool sundayForward, bool dayOff)
		{
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.NewYearsDayText, dayOff, 1, 1, 																	saturdayBack, sundayForward);
		}

		public static void PresidentsDay(bool dayOff)
		{
			var start = new DateTime(1971, 1, 1);
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(HolidayNames.PresidentsDayText, 																		dayOff, 2, 3, DayOfWeek.Monday, start);
		}

		public static void SaintPatrickDay(bool dayOff)
		{
			var start = new DateTime(1762, 1, 1);
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.SaintPatrickDayText, dayOff, 3, 																		17, false, false, start);
		}

		public static void ThanksgivingDay(bool dayOff)
		{
			var start = new DateTime(1619, 1, 1);   // No exact year, 1621, 1619 comes up.
			CalendarDateTime.AddYearlyDayOfWeekForwardEvent(HolidayNames.ThanksgivingDayText, 																dayOff, 11, 4, DayOfWeek.Thursday, start);
		}

		public static void ValentinesDay(bool dayOff)
		{
			var start = new DateTime(300, 1, 1);
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.ValentinesDayText, dayOff, 2, 																			14, false, false, start);
		}

		public static void VeteransDay(bool saturdayBack, bool sundayForward, bool dayOff)
		{
			var start = new DateTime(1954, 1, 1);
			CalendarDateTime.AddYearlyDateEvent(HolidayNames.VeteransDayText, dayOff, 11, 11, 																	saturdayBack, sundayForward, start);
		}

		public static void Weekends()
		{
			var daysOfWeek = new DayOfWeek[] { DayOfWeek.Sunday, DayOfWeek.Saturday };
			CalendarDateTime.AddWeeklyEvent(HolidayNames.WeekendText, true, daysOfWeek);
		}

		public static void WeeklyDayOff(DayOfWeek dayOfWeek)
		{
			CalendarDateTime.AddWeeklyInMonthEvent(dayOfWeek.ToString(), true, dayOfWeek);
		}
	}
}
```

##### Declare the calendar in the start of your app

```
namespace ConsoleApp1
{
	internal class Program
 	{
  	private static void Main()
  	{
    	ThisDate.DefinedCalendars.USA.Calendars.NewYorkStockExchange();
			var h = HolidayNames.MartinLutherKingText.EventDatesBetween(2010, 2020);
			...
		}
  }
}
```



#### Configuration Convention

All of the configuration methods start as Add-xxxx-Event(parameters) pattern. 

where the general parameter patterns are:

```
string eventName - Required, event name, must be unique, case insentive.
bool isDayOff - true if is a day off, false if is a workday.
DateTime? startDate - optional start date, if null, DateTime.MinValue.
DateTime? endDate - optional end date, if null, DateTime.MaxValue.

```

##### Add Dated Events (specific single date events)

Events that occur on a certain date.

```
AddDateEvent(string eventName, bool dayOff, DateTime date)

// Example: Grand opening day, Octobor 1, 2018. 
var date = new DateTime(2018, 10, 1);
AddDateEvent("Grand Opening Day", false, date, date);

```

#### Monthly Events

Events that repeat every month on some pattern.



##### Add Monthly dated events

Events that occur on certain date of every month.

```
AddMonthlyDateEvent(string eventName, bool dayOff, int monthDay, 
										DateTime? startDate = null, DateTime? endDate = null)
```

	where: monthDay = the date each month. 

```
// Example: Rent is due on the 15th of every month.
AddMonthlyDateEvent("Rent Due", 15, startDate); // Note, end date is null, 					                                                   // DateTime.MaxValue

```

##### Add Monthly Last Day of the Month Event

Event that occurs on the last day of a month.

```
AddMonthlyLastDayEvent(string eventName, bool dayOff, 
											 DateTime? startDate = null, DateTime? endDate = null)
```



##### Add Yearly nth Day-Of-Week Events (Forward and Reverse)

These are events that occur on some Day-of-Week interval every year. US Thanksgiving for example occurs on the third Thursday in November. There are two versions, Forward and Reverse. Forward counts from the start of the month, reverse counts from the end of the month. US Labor Day is the last Monday of the month.

```
AddYearlyDayOfWeekForwardEvent(string eventName, bool dayOff, int month, 
					int weeksForward, DayOfWeek dayOfWeek, 
					DateTime? startDate = null, DateTime? endDate = null)
```

```
AddMonthlyDayOfWeekReverseEvent(string eventName, bool dayOff, int month, 
					int weeksForward, DayOfWeek dayOfWeek, 
					DateTime? startDate = null, DateTime? endDate = null)
```

```
// Martin Luther King day occurs on the third Monday in January, which
// began in 1986. This example uses the text helper class menstioned above 
// (HolidayNames.MartinLutherKingText = "Martin Luther King Day")
```

	// month = 1 (January), DayOfWeek.Monday, 3rd week...
	var start = new DateTime(1986, 1, 1); 
	CalendarDateTime.AddYearlyDayOfWeekForwardEvent(
									HolidayNames.MartinLutherKingText, // "Martin Luther King Day"
	                true,															 // as day off 
	                1,																 // Month = January
	                3,																 // Third Monday of the month.
	                DayOfWeek.Monday,									 // Day of week (Monday)
	                start															 // Starting date, 1/1/1986
	                                                   // null defaults as DateTime.MaxVlue 
	                );

```
// US Memorial day occurs on the last Monday in May staring in 1868.
var startDate = new DateTime(1868, 1, 1);
CalendarDateTime.AddYearlyDayOfWeekReverseEvent(HolidayNames.MemorialDayText, true
													, 5, 1, DayOfWeek.Monday, startDate); 

```

#### Weekly Events

These methods add weekly occurrences to some pattern. 



##### Add weekly events that occur on a list of days-of-week

This occurs on interval, independent of the month. Example: Typically payday is every two weeks, some months payday may occur three times in a month. 

```
AddWeeklyEvent(string eventName, bool dayOff, IEnumerable<DayOfWeek> daysOfWeek,
							 DateTime? seedWeek = null, int interval = 1,  
							 DateTime? startDate = null, DateTime? endDate = null)


Where:
	daysOfWeek:	list or array of days-of-week. Could be every Tuesday and Thursday...
	seedWeek:		First week the event starts, interval takes effect from this date.
	Interval:		1 = every week, 2 is every 2 weeks, 3 = every 3 weeks... 

Example:
```

	// Saturdays and Sundays off every week 
	var daysOfWeeks = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday};
	CalendarDateTime.AddWeeklyEvent(HolidayNames.WeekendText, true, daysOfWeeks);


​	
​	// Example using the seed date and interval.
​	// Every other week (every 2 weeks), 
​	// 			on Tuesdays & Thursdays, 
​	//			seed date 2018/10/7, 
​	// 			Between October 1 and throgh and end of November 31.
​	
​	var daysOfWeek = new List<DayOfWeek> {DayOfWeek.Tuesday, DayOfWeek.Thursday};
​	var firstSeedWeek = new DateTime (2018, 10, 7);
​	var start = new DateTime(2018, 10, 7);
​	var end = new DateTime(2018, 11, 31);
​	int interval = 2; // every two weeks.
​	
​	CalendarDateTime.AddWeeklyEvent("EventName", false, daysOfWeek, 
​																	firstSeedWeek, interval, start, end);



##### Add in month, weekly event that occurs on a day-of-week on interval

This occurs on interval within a month. Example: A book club might meet on the second and forth Monday every month (event though some months may have 4 or 5 weeks.)

```
AddWeeklyInMonthEvent(string eventName, bool dayOff, DayOfWeek dayOfWeek, 
											IEnumerable<int> weekIntervals = null, 
											DateTime? startDate = null, DateTime? endDate = null)
											
// Say a gold date every Monday, first and third week every month.
var weekIntervals = new List<int> {1, 3};
CalendarDateTime.AddWeeklyInMonthEvent("Monday biweekly", false, DayOfWeek.Monday, 																						weekIntervals, start, end);
											
```

#### Add Yearly Events

##### Add Easter Sunday/Good Friday (calculated events)

```
public static void AddYearlyCalculatedEvent([CanBeNull] string eventName, bool dayOff, 																	DateTime? startDate = null, DateTime? endDate = null)

where: eventName = "Easter Sunday" or "Good Friday"

// where HolidayNames.EasterSundayText => "Easter Sunday";
var start = new DateTime(30, 1, 1); // Some schoolars believe start year was 30 or 33.
CalendarDateTime.AddYearlyCalculatedEvent(HolidayNames.EasterSundayText, true, start);
```



##### Add Yearly Events

Events that occur every year. There are options to shift the celebration date from Saturday to Friday or shift Sunday to Monday.

```
AddYearlyDateEvent([CanBeNull] string eventName, bool dayOff, int month, int day, 
									 bool saturdayBack, bool sundayForward, 
									 DateTime? startDate = null, DateTime? endDate = null)

Where:
saturdayBack	if true, if the event lands on Saturday celebration on Friday
sundayForward	if true, if the event lands on Sunday celebrate on Monday.

Example:
Many companies in the US take Monday off if New Years day is on Sunday because everybody is too hung over to work.

var saturdayBack = false;
var sundayForward = true;
CalendarDateTime.AddYearlyDateEvent(HolidayNames.NewYearsDayText, true, 1, 1, 																						saturdayBack, sundayForward);

```

#### Collections Methods

```
Internally ThisDate builds on a set of dictionaries. The following are collection related methods.

int CountDateEvents 
int CountEvents
int CountMonthlyEvents
int CountWeeklyEvents
int CountYearlyEvents
ImmutableArray<string> KeysDateEvents
ImmutableArray<string> KeysEvents
ImmutableArray<string> KeysMonthlyEvents
ImmutableArray<string> KeysWeeklyEvents
ImmutableArray<string> KeysYearlyEvents
ClearCalendar()

// Returns all events between two dates.
ImmutableArray<DateTime> EventDatesBetween(this string eventName, 
                               DateTime? date1 = null, DateTime? date2 = null)

// Returns all events withing two years.
ImmutableArray<DateTime> EventDatesBetween(this string eventName, 
                              int? year1, int? year2) 
```

### ThisDate Core Methods

The following are the 'core' methods. See technical documentation for nitty gritty details. 

```
// Returnd date, x (+/-) days from the current date, skipping all non work days.
// This method depends on a calendar.
DateTime AddWorkdays(this DateTime date, int days)

// Convert date to yyyyMMdd format. Often used as a date dimension key.
int DateId(this DateTime date);

// Returns the day-of-week count in month. Example: Feb 12, 2019 is the second
// Tuesday of the month, this date will return 2.
int DayOfWeekCountInMonth(this DateTime date)

// Returns the day-of-week count from the start of the year. Example: Feb 4, 2019 
// is 5 Mondays into the year and Feb 5, 2019 is 6 Tuesdays into the year.
int DayOfWeekCountInYear(this DateTime date)

// Returns date, on nth day-of-week, of month, and year, from the start of the month.
// Example, 2nd Monday.
DateTime DayOfWeekMonthForward(int year, int month, DayOfWeek dayOfWeek, 
                               int weeksForward)

// Returns date, on nth day-of-week, of month, and year, from the end of the month.
// Example, Last Tuesday of the month.
DateTime DayOfWeekMonthReverse(int year, int month, DayOfWeek dayOfWeek, 
                               nt weeksReverse)

// Shift to target day-of-week from date within the week.
// Example, if Date is on Tuesday, shift date to Sunday of the same week (-2 days).
// if date is Tuesday, shift to Saturday is +4 days.
DateTime DayOfWeekShift(this DateTime date, DayOfWeek target)

// Returns list of all events on a date.
ImmutableArray<string> EventsOnDate(this DateTime date, bool includeWorkdays, 
                                    bool includeDaysOff)

// True is date is between date range (date1, date2).
// date1 <= date <= date2, or date1 >= date >= date2. 
bool IsBetweenEqual(this DateTime date, DateTime date1, DateTime date2)

// True if a date is a day off in the calendar.
bool IsDayOff(this DateTime date)

// date is on the first, Day == 1. Trivial, privided for consistency.
bool IsFirstDayOfMonth(this DateTime date)

// Date is in the first week of the month.
bool IsFirstWeekOfMonth(this DateTime date)

// Date is the last day of the month
// return date == new DateTime(date.year, date.month, 1).AddMonths(1).AddDays(-1);
bool IsLastDayOfMonth(this DateTime date)

bool IsLastWeekOfMonth(this DateTime date)

bool IsLeapYear(this int year)
bool IsLeapYear(this DateTime date)

// Example: true if the date is the 3rd Monday of the month. 
bool IsNthDayOfWeek(this DateTime date, int weekNumber, DayOfWeek dayOfWeek)

bool IsWeekDay(this DateTime date)
bool IsWeekend(this DateTime date)

// true is date is a workday in the calendar.
bool IsWorkDay(this DateTime date)

// Get the last day of month.
// return new DateTime(year, month, 1).AddMonths(1).AddDays(-1)
DateTime LastDateOfMonth(int year, int month)

// Get the last day of month.
// return new DateTime(date.year, date.month, 1).AddMonths(1).AddDays(-1)
DateTime LastDateOfMonth(this DateTime date)

// Date quater, 1, 2, 3, or 4.
int Quarter(this DateTime date)

string QuarterLong(this DateTime date)	// "Quarter 1", "Quarter 2"...
string QuarterShort(this DateTime date)	// "Q1", "Q2"...

DateTime RoundToHour(this DateTime date)

// Time rounding functions.
DateTime RoundToInterval(this DateTime date, TimeSpan interval)
DateTime RoundToMinute(this DateTime date)
DateTime RoundToSecond(this DateTime date)

// Time ID functions. Generally used for Time Dimension table Keys.
string TimeId(this DateTime date)
string TimeIdToHour(this DateTime date)
string TimeIdToInterval(this DateTime date, TimeSpan interval)
string TimeIdToMinute(this DateTime date)
string TimeIdToSecond(this DateTime date)

// Adjust date back to Friday if Saturday, forward to Monday if Sunday.
DateTime WeekendAdjustDate(this DateTime date, bool saturdaysBack, bool sundaysForward)

// returns the date week number of the month
int WeekOfMonth(this DateTime dateTime)

// return the date week of year.
int WeekOfYear(this DateTime dateTime)

// return week number of month
int WeeksInMonth(int year, int month)
int WeeksInMonth(this DateTime date)
```



## Source Code

The source code is available on GitHub. The solution includes a project for building date/time dimension tables, additional documentation, and xUint testing, templates for building custom calendar.. 



GitHub Address:   xxxxxxxxxxxxxxx

## Built With

- Visual Studio 2017, .net Core 2.x
- Resharper
- xUnit
- Atomineer
- DocFx

## Author

- **Russell D Ebbing** - RussEbbing@gmail.com 

## License

MIT License

Copyright (c) 2018 Russell Dion Ebbing (RussEbbing@ProtonMail.ch)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
