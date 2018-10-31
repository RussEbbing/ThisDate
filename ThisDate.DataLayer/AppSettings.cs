using JetBrains.Annotations;

namespace ThisDate.DateTimeDataLayer
{
	/// <summary>
	/// App settings.
	/// </summary>
	public static class AppSettings
	{
		/// <summary>
		/// The connection string, may want to put this in a web.config file.
		/// </summary>
		[NotNull]
		public static string ConnectionString => @"Data Source=INTLT1311\SQL2014; Initial Catalog=DateTimeDims; Integrated Security=True; Pooling=False";
	}
}