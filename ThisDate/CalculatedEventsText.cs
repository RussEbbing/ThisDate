using JetBrains.Annotations;

namespace ThisDate
{
	/// <summary>	Calculated events texts. </summary>
	public static class CalculatedEventsText
	{
		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets the Easter Sunday text. </summary>
		///
		/// <value>	The Easter Sunday text. This will never be null. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public static string EasterSunday => "Easter Sunday";

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets the Good Friday text. </summary>
		///
		/// <value>	The Good Friday text. This will never be null. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull] public static string GoodFriday => "Good Friday";
	}
}