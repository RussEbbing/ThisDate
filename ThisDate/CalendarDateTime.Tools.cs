using System;

namespace ThisDate
{
	/// <summary>	Mainly private tools. </summary>
	public partial class CalendarDateTime
	{
		///-------------------------------------------------------------------------------------------------
		/// <summary>	Easter sunday date. </summary>
		///
		/// <remarks>
		/// 	Crazy calculation, same and variant solutions allover the internet. Copy/pasted this code
		/// 	from https://www.codeproject.com/Articles/10860/WebControls/  and
		/// 	https://stackoverflow.com/questions/2510383/how-can-i-calculate-what-date-good-friday-falls-on-given-a-year.
		/// </remarks>
		///
		/// <param name="year">	The year. </param>
		///
		/// <returns>	A DateTime. </returns>
		///-------------------------------------------------------------------------------------------------
		private static DateTime EasterSundayDate(int year)
		{
			var g = year % 19;
			var c = year / 100;
			var h = (c - c / 4 - (8 * c + 13) / 25 + 19 * g + 15) % 30;
			var i = h - h / 28 * (1 - h / 28 * (29 / (h + 1)) * ((21 - g) / 11));
			var day = i - ((year + year / 4 + i + 2 - c + c / 4) % 7) + 28;
			var month = 3;
			if (day > 31)
			{
				month++;
				day -= 31;
			}

			return new DateTime(year, month, day);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Good friday date. </summary>
		///
		/// <param name="year">	The year. </param>
		///
		/// <returns>	A DateTime. </returns>
		///-------------------------------------------------------------------------------------------------
		private static DateTime GoodFridayDate(int year)
		{
			return EasterSundayDate(year).AddDays(-2);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Maximum days in month (estimated). Without the year this cannot be exact. </summary>
		///
		/// <exception cref="ArgumentException">	Thrown when month is out of range. </exception>
		///
		/// <param name="month">	The month. </param>
		///
		/// <returns>	An int. </returns>
		///-------------------------------------------------------------------------------------------------
		private static int MaxDaysInMonthEstimated(int month)
		{
			switch (month)
			{
				case 1:
					return 31;

				case 2:
					return 29;  // Leap years 28 but without year this can not be validated.
				case 3:
					return 31;

				case 4:
					return 30;

				case 5:
					return 31;

				case 6:
					return 30;

				case 7:
					return 31;

				case 8:
					return 31;

				case 9:
					return 30;

				case 10:
					return 31;

				case 11:
					return 30;

				case 12:
					return 31;

				default:
					throw new ArgumentException(ErrorMessageMinMaxOutOfRange(month, nameof(month), 1, 12));
			}
		}

		/// <summary>	DateTime de-nullifies and min/max swapper. </summary>
		private struct MinMaxSwapDate
		{
			/// <summary>	Maximum DateTime. </summary>
			public readonly DateTime Max;

			/// <summary>	Minimum DateTime. </summary>
			public readonly DateTime Min;

			///-------------------------------------------------------------------------------------------------
			/// <summary>	Min/Max DateTime swapper Constructor. </summary>
			///
			/// <param name="minDateTime">	The minimum date, set to DateTime.MinValue if null. </param>
			/// <param name="maxDateTime">  The maximum date, set to DateTime.MaxValue if null. </param>
			///-------------------------------------------------------------------------------------------------
			internal MinMaxSwapDate(DateTime? minDateTime, DateTime? maxDateTime)
			{
				Min = minDateTime ?? DateTime.MinValue;
				Max = maxDateTime ?? DateTime.MaxValue;
				if (Min > Max)
				{
					var temp = Min;
					Min = Max;
					Max = temp;
				}
			}
		}
	}
}