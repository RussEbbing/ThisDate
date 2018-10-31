using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ThisDate.DateTimeDataLayer.Models;

namespace ThisDate.DateTimeDataLayer.Context
{
	///-------------------------------------------------------------------------------------------------
	/// <summary>	Data access data context. </summary>
	///
	/// <seealso cref="T:Microsoft.EntityFrameworkCore.DbContext"/>
	///-------------------------------------------------------------------------------------------------
	public class DateTimeDimensionContext : DbContext
	{
		///-------------------------------------------------------------------------------------------------
		/// <summary>	Date Time Dimension data Context. </summary>
		///
		/// <param name="options">	. </param>
		///-------------------------------------------------------------------------------------------------
		public DateTimeDimensionContext([NotNull] DbContextOptions<DateTimeDimensionContext> options) : base(options)
		{
		}

		/// <summary>	Date Time Dimension context. </summary>
		public DateTimeDimensionContext()
		{
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the date dimension. </summary>
		///
		/// <value>	The date dimension. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		[PublicAPI]
		public DbSet<DateDimension> DateDimension { get; set; }

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Gets or sets the time dimensions. </summary>
		///
		/// <value>	The time dimension. This will never be null. </value>
		///-------------------------------------------------------------------------------------------------
		[NotNull]
		[PublicAPI]
		public DbSet<TimeDimension> TimeDimension { get; set; }

		/// <inheritdoc/>
		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
		{
			dbContextOptionsBuilder.UseSqlServer(AppSettings.ConnectionString);
		}

		/// <inheritdoc/>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			CreateTimeDimension(modelBuilder);
			CreateDateDimension(modelBuilder);
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Creates date dimension. </summary>
		///
		/// <param name="modelBuilder">	The model builder. This cannot be null. </param>
		///-------------------------------------------------------------------------------------------------
		private static void CreateDateDimension([NotNull] ModelBuilder modelBuilder)
		{
			var m = modelBuilder.Entity<DateDimension>();

			m.Property(p => p.DateEuro).IsRequired().HasMaxLength(10);                  // 2016/12/31
			m.Property(p => p.DateId).ValueGeneratedNever().HasMaxLength(8);            // 20170102
			m.HasKey(p => p.DateId);                                                    // DateId index
			m.Property(p => p.DateTime).IsRequired();                                   // DateTime
			m.HasAlternateKey(p => p.DateTime);                                         // DateTime index
			m.Property(p => p.DateUsa).IsRequired().HasMaxLength(10);                   // 12/31/2016
			m.Property(p => p.DayOfMonth).IsRequired().HasMaxLength(2);                 // 31
			m.HasIndex(p => p.DayOfMonth);
			m.Property(p => p.DayOfMonthLeadingZero).IsRequired().HasMaxLength(2);      // 01
			m.Property(p => p.DayOfWeekFullName).IsRequired().HasMaxLength(9);          // Wednesday
			m.Property(p => p.DayOfWeek2LetterName).IsRequired().HasMaxLength(2);       // Th
			m.Property(p => p.DayOfWeek3LetterName).IsRequired().HasMaxLength(3);       // Wed
			m.Property(p => p.DayOfWeekCountInMonth).IsRequired().HasMaxLength(1);      // 2
			m.Property(p => p.DayOfWeekNumber).IsRequired().HasMaxLength(1);            // 7
			m.HasIndex(p => p.DayOfWeekNumber);
			m.Property(p => p.DayOfYear).IsRequired().HasMaxLength(3);                  // 352
			m.HasIndex(p => p.DayOfYear);
			m.Property(p => p.DayOfYearLeadingZeros).IsRequired().HasMaxLength(3);      // 001
			m.Property(p => p.EventsDayOff).IsRequired().HasMaxLength(200);             // Martin Luther King Jr. Day|Store Closed...
			m.Property(p => p.EventsToday).IsRequired().HasMaxLength(200);              // Martin Luther King Jr. Day|Store Closed|Saturday...
			m.Property(p => p.EventsWorkday).IsRequired().HasMaxLength(200);            // Fathers Day|...
			m.Property(p => p.IsDayOff).IsRequired();                                   // True/False
			m.Property(p => p.IsFirstDayOfMonth).IsRequired();                          // True/False
			m.Property(p => p.IsFirstWeekOfMonth).IsRequired();                         // True/False
			m.Property(p => p.IsLastDayOfMonth).IsRequired();                           // True/False
			m.Property(p => p.IsLastWeekOfMonth).IsRequired();                          // True/False
			m.Property(p => p.IsLeapYear).IsRequired();                                 // True/False
			m.Property(p => p.IsWeekDay).IsRequired();                                  // True/False
			m.Property(p => p.IsWeekend).IsRequired();                                  // True/False
			m.Property(p => p.IsWorkDay).IsRequired();                                  // True/False
			m.Property(p => p.MonthFull).IsRequired().HasMaxLength(10);                 // September
			m.Property(p => p.MonthShort).IsRequired().HasMaxLength(3);                 // Jan
			m.Property(p => p.MonthNumber).IsRequired().HasMaxLength(2);                // 12																				// MonthShort { get; set; }
			m.HasIndex(p => p.MonthNumber);
			m.Property(p => p.MonthNumberLeadZero).IsRequired().HasMaxLength(2);        // 01
			m.Property(p => p.Quarter).IsRequired().HasMaxLength(1);                    // 4
			m.HasIndex(p => p.Quarter);
			m.Property(p => p.QuarterLong).IsRequired().HasMaxLength(10);               // Quarter 1
			m.Property(p => p.QuarterShort).IsRequired().HasMaxLength(2);               // Q4
			m.Property(p => p.WeekNumber).IsRequired().HasMaxLength(2);                 // 52
			m.HasIndex(p => p.WeekNumber);
			m.Property(p => p.WeekNumberLeadingZero).IsRequired().HasMaxLength(2);      // 01
			m.Property(p => p.Year).IsRequired().HasMaxLength(4);                       // 2018
			m.HasIndex(p => p.Year);
			m.Property(p => p.YearShort).IsRequired().HasMaxLength(2);                  // 18
		}

		///-------------------------------------------------------------------------------------------------
		/// <summary>	Creates the time dimension. </summary>
		///
		/// <param name="modelBuilder">	The model builder. </param>
		///-------------------------------------------------------------------------------------------------
		private static void CreateTimeDimension([NotNull] ModelBuilder modelBuilder)
		{
			var m = modelBuilder.Entity<TimeDimension>();

			m.Property(p => p.AmPm).IsRequired().HasMaxLength(2);                           // AM
			m.Property(p => p.Hour12).IsRequired().HasMaxLength(2);                         // 12
			m.Property(p => p.Hour12LeadingZero).IsRequired().HasMaxLength(2);              // 12
			m.Property(p => p.Hour24).IsRequired().HasMaxLength(2);                         // 24
			m.HasIndex(p => p.Hour24);
			m.Property(p => p.Hour24LeadingZero).IsRequired().HasMaxLength(2);              // 12
			m.Property(p => p.Minute).IsRequired().HasMaxLength(2);                         // 59
			m.Property(p => p.MinuteLeadingZero).IsRequired().HasMaxLength(2);              // 59
			m.Property(p => p.RoundToHour).IsRequired();                                    // DateTime
			m.HasIndex(p => p.RoundToHour);
			m.Property(p => p.RoundToMinute).IsRequired();                                  // DateTime
			m.HasIndex(p => p.RoundToMinute);
			m.Property(p => p.RoundToSecond).IsRequired();                                  // DateTime
			m.HasIndex(p => p.RoundToSecond);
			m.Property(p => p.Second).IsRequired().HasMaxLength(2);                         // 59
			m.Property(p => p.SecondLeadingZero).IsRequired().HasMaxLength(2);              // 59
			m.Property(p => p.Time).IsRequired();                                           // DateTime
			m.HasAlternateKey(k => k.Time);
			m.Property(p => p.Time12HourMin).IsRequired().HasMaxLength(5);                  // 12:59
			m.Property(p => p.Time12HourMinAmPm).IsRequired().HasMaxLength(8);              // 12:59 AM
			m.Property(p => p.Time12HourMinSecAmPm).IsRequired().HasMaxLength(11);          // 12:59:59 AM
			m.Property(p => p.Time12HourMinSecMiliAmPm).IsRequired().HasMaxLength(15);      // 12:59:59.123 PM
			m.Property(p => p.Time24HourMinCivilian).IsRequired().HasMaxLength(5);          // 23:59
			m.Property(p => p.Time24HourMinSecCivilian).IsRequired().HasMaxLength(8);       // 23:59:59
			m.Property(p => p.Time24HourMinSecMiliCivilian).IsRequired().HasMaxLength(12);  // 23:59.59.121
			m.Property(p => p.Time24HourMinMilitary).IsRequired().HasMaxLength(4);          // 2359
			m.Property(p => p.Time24HourMinSecMilitary).IsRequired().HasMaxLength(6);       // 235959
			m.Property(p => p.Time24HourMinSecMiliMilitary).IsRequired().HasMaxLength(10);  // 235959.123
			m.Property(p => p.TimeId).IsRequired().HasMaxLength(9);                         // 235959123
			m.HasKey(k => k.TimeId);
		}
	}
}