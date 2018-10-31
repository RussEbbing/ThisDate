using JetBrains.Annotations;
using System;

namespace ThisDate.DateTimeDataLayer.Models
{
	/// <summary>	Time Dimension. </summary>
	[PublicAPI]
	public class TimeDimension
	{
		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the AM/PM. </summary>
		///
		/// <value>	The AM/PM. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string AmPm { get; set; } = String.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the 12 hour. </summary>
		///
		/// <value>	The hour. </value>
		///-------------------------------------------------------------------------------------------------
		public int Hour12 { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the 12 hour, leading zero. </summary>
		///
		/// <value>	The hour12 leading zero. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string Hour12LeadingZero { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the 24 hour. </summary>
		///
		/// <value>	The hour24. </value>
		///-------------------------------------------------------------------------------------------------
		public int Hour24 { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the 24-hour leading zero. </summary>
		///
		/// <value>	The hour24 leading zero. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string Hour24LeadingZero { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the minute. </summary>
		///
		/// <value>	The minute. </value>
		///-------------------------------------------------------------------------------------------------
		public int Minute { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the minute leading zero. </summary>
		///
		/// <value>	The minute leading zero. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string MinuteLeadingZero { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the Date/Time of the round to hour. </summary>
		///
		/// <value>	The round to hour. </value>
		///-------------------------------------------------------------------------------------------------
		public DateTime RoundToHour { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the Date/Time of the round to minute. </summary>
		///
		/// <value>	The round to minute. </value>
		///-------------------------------------------------------------------------------------------------
		public DateTime RoundToMinute { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the Date/Time of the round to second. </summary>
		///
		/// <value>	The round to second. </value>
		///-------------------------------------------------------------------------------------------------
		public DateTime RoundToSecond { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the second. </summary>
		///
		/// <value>	The second. </value>
		///-------------------------------------------------------------------------------------------------
		public int Second { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the second leading zero. </summary>
		///
		/// <value>	The second leading zero. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string SecondLeadingZero { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the DateTime time property. </summary>
		///
		/// <value>	The time. </value>
		///-------------------------------------------------------------------------------------------------
		public DateTime Time { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the time12 hour/minute (hh:mm). </summary>
		///
		/// <value>	The time 12-hour minimum. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string Time12HourMin { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the time 12-hour/minimum AM/PM (HH:MM AM/PM). </summary>
		///
		/// <value>	The time12 hour minimum am pm. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string Time12HourMinAmPm { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the time 12-hour/min/sec AM/PM (HH:MM:SS AM/PM). </summary>
		///
		/// <value>	The time12 hour minimum sec am pm. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string Time12HourMinSecAmPm { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the time 12 hour to millisecond (HH:MM:SS.fff). </summary>
		///
		/// <value>	The 12 hour time to milliseconds. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string Time12HourMinSecMiliAmPm { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the time24 hour military civilian format (HH:MM). </summary>
		///
		/// <value>	The time24 hour minimum civilian. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string Time24HourMinCivilian { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the time24 hour minimum military format (HHMM). </summary>
		///
		/// <value>	The time24 hour minimum military. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string Time24HourMinMilitary { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the 24 hour time (HH:MM:SS). </summary>
		///
		/// <value>	The time24 hour minimum sec civilian. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string Time24HourMinSecCivilian { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the time 24 hour minimum sec mili civilian. </summary>
		///
		/// <value>	The time24 hour minimum sec mili civilian. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string Time24HourMinSecMiliCivilian { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the 24 hour time (HHMMSSfff) format. </summary>
		///
		/// <value>	The time24 hour minimum sec mili. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string Time24HourMinSecMiliMilitary { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the time24 hour/min/sec (HHMMSS). </summary>
		///
		/// <value>	The time24 hour minimum sec. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string Time24HourMinSecMilitary { get; set; } = string.Empty;

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets time ID (HHMMSSFFF, 24 hour with mili seconds). </summary>
		///
		/// <value>	The time ID. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public string TimeId { get; set; } = string.Empty;
	}
}