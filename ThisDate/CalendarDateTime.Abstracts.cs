using System;

namespace ThisDate
{
	/// <summary>	Abstract classes. </summary>
	public static partial class CalendarDateTime
	{
		///-------------------------------------------------------------------------------------------------
		/// <summary>	A date time range. </summary>
		///
		/// <seealso cref="T:ThisDate.CalendarDateTime.DayOffWorkDay"/>
		///-------------------------------------------------------------------------------------------------
		private abstract class DateTimeRange : DayOffWorkDay
		{
			/// <summary>	The date end Date/Time. </summary>
			private DateTime _dateEnd;

			/// <summary>	The date start Date/Time. </summary>
			private DateTime _dateStart;

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets or sets the Date/Time of the date end. </summary>
			///
			/// <value>	The date end. </value>
			///-------------------------------------------------------------------------------------------------
			public DateTime DateEnd
			{
				get => _dateEnd;
				protected set => _dateEnd = value.Date;
			}

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets or sets the Date/Time of the date start. </summary>
			///
			/// <value>	The date start. </value>
			///-------------------------------------------------------------------------------------------------
			public DateTime DateStart
			{
				get => _dateStart;
				protected set => _dateStart = value.Date;
			}
		}

		/// <summary>	A day off work day. </summary>
		private abstract class DayOffWorkDay
		{
			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets or sets a value indicating whether is a day off. </summary>
			///
			/// <value>	True if day off, false if not. </value>
			///-------------------------------------------------------------------------------------------------
			public bool DayOff { get; protected set; }

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Gets a value indicating whether the work day. </summary>
			///
			/// <value>	True if work day, false if not. </value>
			///-------------------------------------------------------------------------------------------------
			public bool WorkDay => !DayOff;
		}
	}
}