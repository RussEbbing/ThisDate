using JetBrains.Annotations;

namespace ThisDate
{
	/// <summary>	Common error messages. </summary>
	public partial class CalendarDateTime
	{
		///-------------------------------------------------------------------------------------------------
		/// <summary>	Error message days null or empty. </summary>
		///
		/// <param name="parameterName">	Name of the parameter. This cannot be null. </param>
		///
		/// <returns>	A string. This will never be null. </returns>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		private static string ErrorMessageCannotBeNullOrEmpty([NotNull] string parameterName)
		{
			return parameterName + " cannot be null or empty.";
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Error message easter sunday good friday. </summary>
		///
		/// <param name="paramName">	Name of the parameter. This cannot be null. </param>
		/// <param name="value">		The value. </param>
		///
		/// <returns>	A string. This will never be null. </returns>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		private static string ErrorMessageEasterSundayGoodFriday([NotNull] string paramName, [NotNull] string value)
		{
			return paramName + ": " + value + ", is invalid, only \"" + CalculatedEventsText.EasterSunday + "\" or \"" + CalculatedEventsText.GoodFriday + "\" are supported.";
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Error message key already added. </summary>
		///
		/// <param name="paramName">	Name of the parameter. This cannot be null. </param>
		/// <param name="value">		The value. </param>
		///
		/// <returns>	A string. This will never be null. </returns>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		private static string ErrorMessageKeyAlreadyAdded([NotNull] string paramName, [NotNull] string value)
		{
			return paramName + ", " + value + ", key has already been added.";
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Error message minimum maximum out of range. </summary>
		///
		/// <param name="value">		The value. </param>
		/// <param name="paramName">	Name of the parameter. This cannot be null. </param>
		/// <param name="min">			The minimum. </param>
		/// <param name="max">			The maximum. </param>
		///
		/// <returns>	A string. This will never be null. </returns>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		private static string ErrorMessageMinMaxOutOfRange(int value, [NotNull] string paramName, int min, int max)
		{
			return paramName + ", " + value + ", is out of range. Valid range is  [" + min + "..." + max + "].";
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Error message zero or negative. </summary>
		///
		/// <param name="paraName">	Name of the para. This cannot be null. </param>
		/// <param name="value">   	The value. </param>
		///
		/// <returns>	A string. This will never be null. </returns>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		private static string ErrorMessageZeroOrNegative([NotNull] string paraName, int value)
		{
			return paraName + " = " + value + ", cannot be zero or negative.";
		}
	}
}