using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace ThisDate
{
	/// <summary>	A calendar date time. </summary>
	public static partial class CalendarDateTime
	{
		/// <summary>	Interface for date event. </summary>
		private interface IDateEvent
		{
			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets the Date/Time of the date. </summary>
			///
			/// <value>	The date. </value>
			///-------------------------------------------------------------------------------------------------
			DateTime Date { get; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets a value indicating whether the day off. </summary>
			///
			/// <value>	True if day off, false if not. </value>
			///-------------------------------------------------------------------------------------------------
			bool DayOff { get; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets a value indicating whether the work day. </summary>
			///
			/// <value>	True if work day, false if not. </value>
			///-------------------------------------------------------------------------------------------------
			bool WorkDay { get; }
		}

		/// <summary>	Interface for monthly event. </summary>
		private interface IMonthlyEvent
		{
			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets the Date/Time of the date end. </summary>
			///
			/// <value>	The date end. </value>
			///-------------------------------------------------------------------------------------------------
			DateTime DateEnd { get; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets the Date/Time of the date start. </summary>
			///
			/// <value>	The date start. </value>
			///-------------------------------------------------------------------------------------------------
			DateTime DateStart { get; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets a value indicating whether the day off. </summary>
			///
			/// <value>	True if day off, false if not. </value>
			///-------------------------------------------------------------------------------------------------
			bool DayOff { get; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets a value indicating whether the work day. </summary>
			///
			/// <value>	True if work day, false if not. </value>
			///-------------------------------------------------------------------------------------------------
			bool WorkDay { get; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Dates. </summary>
			///
			/// <param name="year"> 	The year. </param>
			/// <param name="month">	The month. </param>
			///
			/// <returns>	A DateTime? </returns>
			///-------------------------------------------------------------------------------------------------
			DateTime? Date(int year, int month);

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Event dates between date range (unsorted). </summary>
			///
			/// <param name="dateFrom">	The date 1 Date/Time. </param>
			/// <param name="dateTo">	The date 2 Date/Time. </param>
			///
			/// <returns>	A List&lt;DateTime&gt; </returns>
			///-------------------------------------------------------------------------------------------------
			[NotNull] List<DateTime> EventDatesBetween(DateTime dateFrom, DateTime dateTo);
		}

		/// <summary>	Interface for weekly event. </summary>
		private interface IWeeklyEvent
		{
			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets the Date/Time of the date end. </summary>
			///
			/// <value>	The date end. </value>
			///-------------------------------------------------------------------------------------------------
			DateTime DateEnd { get; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets the Date/Time of the date start. </summary>
			///
			/// <value>	The date start. </value>
			///-------------------------------------------------------------------------------------------------
			DateTime DateStart { get; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets a value indicating whether the day off. </summary>
			///
			/// <value>	True if day off, false if not. </value>
			///-------------------------------------------------------------------------------------------------
			bool DayOff { get; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets a value indicating whether the work day. </summary>
			///
			/// <value>	True if work day, false if not. </value>
			///-------------------------------------------------------------------------------------------------
			bool WorkDay { get; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Event dates between date range. </summary>
			///
			/// <param name="dateFrom">	The date 1 Date/Time. </param>
			/// <param name="dateTo">	The date 2 Date/Time. </param>
			///
			/// <returns>	A List&lt;DateTime&gt; </returns>
			///-------------------------------------------------------------------------------------------------
			[NotNull] List<DateTime> EventDatesBetween(DateTime dateFrom, DateTime dateTo);

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Query if 'date' is event day. </summary>
			///
			/// <param name="date">	The date Date/Time. </param>
			///
			/// <returns>	True if event day, false if not. </returns>
			///-------------------------------------------------------------------------------------------------
			bool IsEventDay(DateTime date);
		}

		/// <summary>	Interface for yearly event. </summary>
		private interface IYearlyEvent
		{
			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets the Date/Time of the date end. </summary>
			///
			/// <value>	The date end. </value>
			///-------------------------------------------------------------------------------------------------
			DateTime DateEnd { get; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets the Date/Time of the date start. </summary>
			///
			/// <value>	The date start. </value>
			///-------------------------------------------------------------------------------------------------
			DateTime DateStart { get; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets a value indicating whether the day off. </summary>
			///
			/// <value>	True if day off, false if not. </value>
			///-------------------------------------------------------------------------------------------------
			bool DayOff { get; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets a value indicating whether the work day. </summary>
			///
			/// <value>	True if work day, false if not. </value>
			///-------------------------------------------------------------------------------------------------
			bool WorkDay { get; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Dates. </summary>
			///
			/// <param name="year">	The year. </param>
			///
			/// <returns>	A DateTime? </returns>
			///-------------------------------------------------------------------------------------------------
			DateTime? Date(int year);

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Event dates between date range. </summary>
			///
			/// <param name="dateFrom">	The date 1 Date/Time. </param>
			/// <param name="dateTo">	The date 2 Date/Time. </param>
			///
			/// <returns>	A List&lt;DateTime&gt; </returns>
			///-------------------------------------------------------------------------------------------------
			[NotNull] List<DateTime> EventDatesBetween(DateTime dateFrom, DateTime dateTo);
		}
	}
}