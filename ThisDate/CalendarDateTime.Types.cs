using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ThisDate
{
	/// <summary>	Internal types. </summary>
	public partial class CalendarDateTime
	{
		///-------------------------------------------------------------------------------------------------
		/// <summary>	Date event. </summary>
		///
		/// <seealso cref="T:ThisDate.CalendarDateTime.DayOffWorkDay"/>
		/// <seealso cref="T:ThisDate.CalendarDateTime.IDateEvent"/>
		///-------------------------------------------------------------------------------------------------
		private class DateEvent : DayOffWorkDay, IDateEvent
		{
			///-------------------------------------------------------------------------------------------------
			/// <summary>	Constructor. </summary>
			///
			/// <param name="date">  	The date Date/Time. </param>
			/// <param name="dayOff">	True if dayOff (not workday). </param>
			///-------------------------------------------------------------------------------------------------
			public DateEvent(DateTime date, bool dayOff)
			{
				Date = date.Date;
				DayOff = dayOff;
			}

			/// <inheritdoc/>
			public DateTime Date { get; }
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A monthly day event. </summary>
		///
		/// <seealso cref="T:ThisDate.CalendarDateTime.DateTimeRange"/>
		/// <seealso cref="T:ThisDate.CalendarDateTime.IMonthlyEvent"/>
		///-------------------------------------------------------------------------------------------------
		private class MonthlyDayEvent : DateTimeRange, IMonthlyEvent
		{
			/// <summary>	The day. </summary>
			private readonly int _day;

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Constructor. </summary>
			///
			/// <param name="dayOff">   	True if is a day off (not workday). </param>
			/// <param name="day">			The day. </param>
			/// <param name="startDate">	The date start Date/Time. </param>
			/// <param name="endDate">  	The date end Date/Time. </param>
			///-------------------------------------------------------------------------------------------------
			public MonthlyDayEvent(bool dayOff, int day, DateTime startDate, DateTime endDate)
			{
				_day = day;
				DayOff = dayOff;
				DateStart = startDate;
				DateEnd = endDate;
			}

			/// <inheritdoc/>
			public DateTime? Date(int year, int month)
			{
				var date = new DateTime(year, month, _day);
				return DateStart <= date && date <= DateEnd ? (DateTime?)date : null;
			}

			/// <inheritdoc/>
			public List<DateTime> EventDatesBetween(DateTime dateFrom, DateTime dateTo)
			{
				var result = new List<DateTime>();
				for (var date = dateFrom; date <= dateTo; date = date.AddMonths(1))
				{
					var d = Date(date.Year, date.Month);
					if (d != null)
					{
						result.Add((DateTime)d);
					}
				}

				return result;
			}
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A monthly day of week forward event. </summary>
		///
		/// <seealso cref="T:ThisDate.CalendarDateTime.DateTimeRange"/>
		/// <seealso cref="T:ThisDate.CalendarDateTime.IMonthlyEvent"/>
		///-------------------------------------------------------------------------------------------------
		private class MonthlyDayOfWeekForwardEvent : DateTimeRange, IMonthlyEvent
		{
			/// <summary>	The day of week. </summary>
			private readonly DayOfWeek _dayOfWeek;

			/// <summary>	The weeks forward. </summary>
			private readonly int _weeksForward;

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Constructor. </summary>
			///
			/// <param name="dayOff">	   	True if is a day off (not workday). </param>
			/// <param name="dayOfWeek">   	The day of week. </param>
			/// <param name="weeksForward">	The weeks forward. </param>
			/// <param name="startDate">   	The date start Date/Time. </param>
			/// <param name="endDate">	   	The date end Date/Time. </param>
			///-------------------------------------------------------------------------------------------------
			public MonthlyDayOfWeekForwardEvent(bool dayOff, DayOfWeek dayOfWeek, int weeksForward, DateTime startDate, DateTime endDate)
			{
				_dayOfWeek = dayOfWeek;
				_weeksForward = weeksForward;
				DayOff = dayOff;
				DateStart = startDate;
				DateEnd = endDate;
			}

			/// <inheritdoc/>
			public DateTime? Date(int year, int month)
			{
				var date = DayOfWeekMonthForward(year, month, _dayOfWeek, _weeksForward);
				return DateStart <= date && date <= DateEnd ? (DateTime?)date : null;
			}

			/// <inheritdoc/>
			public List<DateTime> EventDatesBetween(DateTime dateFrom, DateTime dateTo)
			{
				var result = new List<DateTime>();

				for (var date = dateFrom; date <= dateTo; date = date.AddMonths(1))
				{
					var d = Date(date.Year, date.Month);
					if (d != null)
					{
						result.Add((DateTime)d);
					}
				}

				return result;
			}
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A monthly day of week reverse event. </summary>
		///
		/// <seealso cref="T:ThisDate.CalendarDateTime.DateTimeRange"/>
		/// <seealso cref="T:ThisDate.CalendarDateTime.IMonthlyEvent"/>
		///-------------------------------------------------------------------------------------------------
		private class MonthlyDayOfWeekReverseEvent : DateTimeRange, IMonthlyEvent
		{
			/// <summary>	The day of week. </summary>
			private readonly DayOfWeek _dayOfWeek;

			/// <summary>	The weeks reverse. </summary>
			private readonly int _weeksReverse;

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Constructor. </summary>
			///
			/// <param name="dayOff">	   	True if is a day off (not workday). </param>
			/// <param name="dayOfWeek">   	The day of week. </param>
			/// <param name="weeksReverse">	The weeks reverse. </param>
			/// <param name="startDate">   	The date start Date/Time. </param>
			/// <param name="endDate">	   	The date end Date/Time. </param>
			///-------------------------------------------------------------------------------------------------
			public MonthlyDayOfWeekReverseEvent(bool dayOff, DayOfWeek dayOfWeek, int weeksReverse, DateTime startDate, DateTime endDate)
			{
				_dayOfWeek = dayOfWeek;
				_weeksReverse = weeksReverse;
				DayOff = dayOff;
				DateStart = startDate;
				DateEnd = endDate;
			}

			/// <inheritdoc/>
			public DateTime? Date(int year, int month)
			{
				var date = DayOfWeekMonthReverse(year, month, _dayOfWeek, _weeksReverse);
				return DateStart <= date && date <= DateEnd ? (DateTime?)date : null;
			}

			/// <inheritdoc/>
			public List<DateTime> EventDatesBetween(DateTime dateFrom, DateTime dateTo)
			{
				var result = new List<DateTime>();

				for (var date = dateFrom; date <= dateTo; date = date.AddMonths(1))
				{
					var d = Date(date.Year, date.Month);
					if (d != null)
					{
						result.Add((DateTime)d);
					}
				}

				return result;
			}
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A monthly last day event. </summary>
		///
		/// <seealso cref="T:ThisDate.CalendarDateTime.DateTimeRange"/>
		/// <seealso cref="T:ThisDate.CalendarDateTime.IMonthlyEvent"/>
		///-------------------------------------------------------------------------------------------------
		private class MonthlyLastDayEvent : DateTimeRange, IMonthlyEvent
		{
			///-------------------------------------------------------------------------------------------------
			/// <summary>	Constructor. </summary>
			///
			/// <param name="dayOff">   	True if is a day off (not workday). </param>
			/// <param name="startDate">	The date start Date/Time. </param>
			/// <param name="endDate">  	The date end Date/Time. </param>
			///-------------------------------------------------------------------------------------------------
			public MonthlyLastDayEvent(bool dayOff, DateTime startDate, DateTime endDate)
			{
				DayOff = dayOff;
				DateStart = startDate;
				DateEnd = endDate;
			}

			/// <inheritdoc/>
			public DateTime? Date(int year, int month)
			{
				var date = new DateTime(year, month, 1).LastDateOfMonth();
				return DateStart <= date && date <= DateEnd ? (DateTime?)date : null;
			}

			/// <inheritdoc/>
			public List<DateTime> EventDatesBetween(DateTime dateFrom, DateTime dateTo)
			{
				var result = new List<DateTime>();

				for (var date = dateFrom; date <= dateTo; date = date.AddMonths(1))
				{
					var d = Date(date.Year, date.Month);
					if (d != null)
					{
						result.Add((DateTime)d);
					}
				}

				return result;
			}
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A weekly event. </summary>
		///
		/// <seealso cref="T:ThisDate.CalendarDateTime.DateTimeRange"/>
		/// <seealso cref="T:ThisDate.CalendarDateTime.IWeeklyEvent"/>
		///-------------------------------------------------------------------------------------------------
		private class WeeklyEvent : DateTimeRange, IWeeklyEvent
		{
			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets the days of week. </summary>
			///
			/// <value>	The days of week. </value>
			///-------------------------------------------------------------------------------------------------
			[NotNull] private readonly IEnumerable<DayOfWeek> _daysOfWeek;

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets the days of week. </summary>
			/// <summary>	The interval. </summary>
			///-------------------------------------------------------------------------------------------------
			private readonly int _interval;

			/// <summary>	The seed date to base calculations on. </summary>
			private readonly DateTime _seedDate;

			///-------------------------------------------------------------------------------------------------
			/// <summary>
			/// 	The seed day-of-week, Monday chosen because this is the earliest day-of-week to avoid
			/// 	invalid dates.
			/// </summary>
			///-------------------------------------------------------------------------------------------------
			private readonly DayOfWeek _seedDayOfWeek = DateTime.MinValue.DayOfWeek;

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Constructor. </summary>
			///
			/// <param name="dayOff">	 	True if is a day off (not workday). </param>
			/// <param name="interval">  	The interval. </param>
			/// <param name="daysOfWeek">	The days of week. This cannot be null. </param>
			/// <param name="seedDate">  	A base date in the week of occurrence. </param>
			/// <param name="startDate"> 	The date start Date/Time. </param>
			/// <param name="endDate">   	The date end Date/Time. </param>
			///-------------------------------------------------------------------------------------------------
			public WeeklyEvent(bool dayOff, int interval, [NotNull] IEnumerable<DayOfWeek> daysOfWeek, DateTime seedDate, DateTime startDate, DateTime endDate)
			{
				DayOff = dayOff;
				_interval = interval;
				_daysOfWeek = daysOfWeek;
				_seedDate = seedDate.DayOfWeekShift(_seedDayOfWeek);
				DateStart = startDate;
				DateEnd = endDate;
			}

			/// <inheritdoc/>
			public List<DateTime> EventDatesBetween(DateTime dateFrom, DateTime dateTo)
			{
				var result = new List<DateTime>();
				foreach (var dayOfWeek in _daysOfWeek)
				{
					for (var date = dateFrom.DayOfWeekShift(dayOfWeek); date <= dateTo; date = date.AddDays(7))
					{
						if (IsEventDay(date))
						{
							result.Add(date);
						}
					}
				}

				return result.OrderBy(o => o).ToList();
			}

			/// <inheritdoc/>
			public bool IsEventDay(DateTime date)
			{
				var isDayOfWeek = _daysOfWeek.Contains(date.DayOfWeek);
				var withinDateRange = date.IsBetweenEqual(DateStart, DateEnd);
				var dateShifted = date.DayOfWeekShift(_seedDayOfWeek);
				var inInterval = (dateShifted - _seedDate).Days % _interval == 0;
				return withinDateRange && isDayOfWeek && inInterval;
			}
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A weekly in month event. </summary>
		///
		/// <seealso cref="T:ThisDate.CalendarDateTime.DateTimeRange"/>
		/// <seealso cref="T:ThisDate.CalendarDateTime.IWeeklyEvent"/>
		///-------------------------------------------------------------------------------------------------
		private class WeeklyInMonthEvent : DateTimeRange, IWeeklyEvent
		{
			/// <summary>	The day of week. </summary>
			private readonly DayOfWeek _dayOfWeek;

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets the month weeks. </summary>
			///
			/// <value>	The month weeks. </value>
			///-------------------------------------------------------------------------------------------------
			[NotNull] private readonly IEnumerable<int> _monthWeeks;

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Constructor. </summary>
			///
			/// <param name="dayOff">	 	True if is a day off (not workday). </param>
			/// <param name="dayOfWeek"> 	The day of week. </param>
			/// <param name="monthWeeks">	The month in weeks. This cannot be null. </param>
			/// <param name="startDate"> 	The date start Date/Time. </param>
			/// <param name="endDate">   	The date end Date/Time. </param>
			///-------------------------------------------------------------------------------------------------
			public WeeklyInMonthEvent(bool dayOff, DayOfWeek dayOfWeek, [NotNull] IEnumerable<int> monthWeeks, DateTime startDate, DateTime endDate)
			{
				DayOff = dayOff;
				_dayOfWeek = dayOfWeek;
				_monthWeeks = monthWeeks;
				DateStart = startDate;
				DateEnd = endDate;
			}

			/// <inheritdoc/>
			public List<DateTime> EventDatesBetween(DateTime dateFrom, DateTime dateTo)
			{
				var result = new List<DateTime>();
				for (var date = dateFrom.DayOfWeekShift(_dayOfWeek); date <= dateTo; date = date.AddDays(7))
				{
					if (IsEventDay(date))
					{
						result.Add(date);
					}
				}

				return result.OrderBy(o => o).ToList();
			}

			public bool IsEventDay(DateTime date)
			{
				return _dayOfWeek == date.DayOfWeek && _monthWeeks.Contains(date.DayOfWeekCountInMonth());
			}
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A yearly calculated event. </summary>
		///
		/// <seealso cref="T:ThisDate.CalendarDateTime.DateTimeRange"/>
		/// <seealso cref="T:ThisDate.CalendarDateTime.IYearlyEvent"/>
		///-------------------------------------------------------------------------------------------------
		private class YearlyCalculatedEvent : DateTimeRange, IYearlyEvent
		{
			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets the date method. </summary>
			///
			/// <value>	The date method. </value>
			///-------------------------------------------------------------------------------------------------
			[NotNull] private readonly Func<int, DateTime> _dateMethod;

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Constructor. </summary>
			///
			/// <param name="dayOff">	 	True if is a day off (not workday). </param>
			/// <param name="dateMethod">	The date method. This cannot be null. </param>
			/// <param name="startDate"> 	The date start Date/Time. </param>
			/// <param name="endDate">   	The date end Date/Time. </param>
			///-------------------------------------------------------------------------------------------------
			public YearlyCalculatedEvent(bool dayOff, [NotNull] Func<int, DateTime> dateMethod, DateTime startDate, DateTime endDate)
			{
				DayOff = dayOff;
				_dateMethod = dateMethod;
				DateStart = startDate;
				DateEnd = endDate;
			}

			/// <inheritdoc/>
			public DateTime? Date(int year)
			{
				var date = _dateMethod(year);
				return DateStart <= date && date <= DateEnd ? (DateTime?)date : null;
			}

			/// <inheritdoc/>
			public List<DateTime> EventDatesBetween(DateTime dateFrom, DateTime dateTo)
			{
				var result = new List<DateTime>();
				for (var date = dateFrom; date <= dateTo; date = date.AddYears(1))
				{
					var r = Date(date.Year);
					if (r != null)
					{
						result.Add((DateTime)r);
					}
				}

				return result;
			}
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A yearly day of week forward event. </summary>
		///
		/// <seealso cref="T:ThisDate.CalendarDateTime.DateTimeRange"/>
		/// <seealso cref="T:ThisDate.CalendarDateTime.IYearlyEvent"/>
		///-------------------------------------------------------------------------------------------------
		private class YearlyDayOfWeekForwardEvent : DateTimeRange, IYearlyEvent
		{
			/// <summary>	The day of week. </summary>
			private readonly DayOfWeek _dayOfWeek;

			/// <summary>	The month. </summary>
			private readonly int _month;

			/// <summary>	The weeks forward. </summary>
			private readonly int _weeksForward;

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Constructor. </summary>
			///
			/// <param name="dayOff">	   	True if is a day off (not workday). </param>
			/// <param name="month">	   	The month. </param>
			/// <param name="dayOfWeek">   	The day of week. </param>
			/// <param name="weeksForward">	The weeks forward. </param>
			/// <param name="startDate">   	The date start Date/Time. </param>
			/// <param name="endDate">	   	The date end Date/Time. </param>
			///-------------------------------------------------------------------------------------------------
			public YearlyDayOfWeekForwardEvent(bool dayOff, int month, DayOfWeek dayOfWeek, int weeksForward, DateTime startDate, DateTime endDate)
			{
				_month = month;
				_weeksForward = weeksForward;
				_dayOfWeek = dayOfWeek;
				DayOff = dayOff;
				DateStart = startDate;
				DateEnd = endDate;
			}

			/// <inheritdoc/>
			public DateTime? Date(int year)
			{
				var date = DayOfWeekMonthForward(year, _month, _dayOfWeek, _weeksForward);
				return DateStart <= date && date <= DateEnd ? (DateTime?)date : null;
			}

			/// <inheritdoc/>
			public List<DateTime> EventDatesBetween(DateTime dateFrom, DateTime dateTo)
			{
				var result = new List<DateTime>();
				for (var date = dateFrom; date <= dateTo; date = date.AddYears(1))
				{
					var r = Date(date.Year);
					if (r != null)
					{
						result.Add((DateTime)r);
					}
				}

				return result;
			}
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A yearly day of week reverse event. </summary>
		///
		/// <seealso cref="T:ThisDate.CalendarDateTime.DateTimeRange"/>
		/// <seealso cref="T:ThisDate.CalendarDateTime.IYearlyEvent"/>
		///-------------------------------------------------------------------------------------------------
		private class YearlyDayOfWeekReverseEvent : DateTimeRange, IYearlyEvent
		{
			/// <summary>	The day of week. </summary>
			private readonly DayOfWeek _dayOfWeek;

			/// <summary>	The month. </summary>
			private readonly int _month;

			/// <summary>	The week reverse. </summary>
			private readonly int _weekReverse;

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Constructor. </summary>
			///
			/// <param name="dayOff">	   	True if is a day off (not workday). </param>
			/// <param name="month">	   	The month. </param>
			/// <param name="dayOfWeek">   	The day of week. </param>
			/// <param name="weeksReverse">	The weeks reverse. </param>
			/// <param name="startDate">   	The date start Date/Time. </param>
			/// <param name="endDate">	   	The date end Date/Time. </param>
			///-------------------------------------------------------------------------------------------------
			public YearlyDayOfWeekReverseEvent(bool dayOff, int month, DayOfWeek dayOfWeek, int weeksReverse, DateTime startDate, DateTime endDate)
			{
				_month = month;
				_weekReverse = weeksReverse;
				_dayOfWeek = dayOfWeek;
				DayOff = dayOff;
				DateStart = startDate;
				DateEnd = endDate;
			}

			/// <inheritdoc/>
			public DateTime? Date(int year)
			{
				var date = DayOfWeekMonthReverse(year, _month, _dayOfWeek, _weekReverse);
				return DateStart <= date && date <= DateEnd ? (DateTime?)date : null;
			}

			/// <inheritdoc/>
			public List<DateTime> EventDatesBetween(DateTime dateFrom, DateTime dateTo)
			{
				var result = new List<DateTime>();
				for (var date = dateFrom; date <= dateTo; date = date.AddYears(1))
				{
					var r = Date(date.Year);
					if (r != null)
					{
						result.Add((DateTime)r);
					}
				}

				return result;
			}
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	A yearly month day event. </summary>
		///
		/// <seealso cref="T:ThisDate.CalendarDateTime.DateTimeRange"/>
		/// <seealso cref="T:ThisDate.CalendarDateTime.IYearlyEvent"/>
		///-------------------------------------------------------------------------------------------------
		private class YearlyMonthDatedEvent : DateTimeRange, IYearlyEvent
		{
			/// <summary>	The day. </summary>
			private readonly int _day;

			/// <summary>	The month. </summary>
			private readonly int _month;

			/// <summary>	True to saturday back. </summary>
			private readonly bool _saturdayBack;

			/// <summary>	True to sunday forward. </summary>
			private readonly bool _sundayForward;

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Constructor. </summary>
			///
			/// <param name="dayOff">			True if is a day off (not workday). </param>
			/// <param name="month">			The month. </param>
			/// <param name="day">				The day. </param>
			/// <param name="saturdayBack"> 	True to saturday back. </param>
			/// <param name="sundayForward">	True to sunday forward. </param>
			/// <param name="startDate">		The date start Date/Time. </param>
			/// <param name="endDate">			The date end Date/Time. </param>
			///-------------------------------------------------------------------------------------------------
			public YearlyMonthDatedEvent(bool dayOff, int month, int day, bool saturdayBack, bool sundayForward, DateTime startDate, DateTime endDate)
			{
				_month = month;
				_day = day;
				_saturdayBack = saturdayBack;
				_sundayForward = sundayForward;
				DayOff = dayOff;
				DateStart = startDate;
				DateEnd = endDate;
			}

			/// <inheritdoc/>

			public DateTime? Date(int year)
			{
				var date = WeekendAdjustDate(new DateTime(year, _month, _day), _saturdayBack, _sundayForward);
				return DateStart <= date && date <= DateEnd ? (DateTime?)date : null;
			}

			/// <inheritdoc/>
			public List<DateTime> EventDatesBetween(DateTime dateFrom, DateTime dateTo)
			{
				var result = new List<DateTime>();
				for (var date = dateFrom; date <= dateTo; date = date.AddYears(1))
				{
					var r = Date(date.Year);
					if (r != null)
					{
						result.Add((DateTime)r);
					}
				}

				return result;
			}
		}
	}
}