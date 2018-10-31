using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThisDate.DateTimeDataLayer.Migrations
{
	public partial class Init : Migration
	{
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "DateDimension");

			migrationBuilder.DropTable(
				name: "TimeDimension");
		}

		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "DateDimension",
				columns: table => new
				{
					DateEuro = table.Column<string>(maxLength: 10, nullable: false),
					DateId = table.Column<int>(maxLength: 8, nullable: false),
					DateTime = table.Column<DateTime>(nullable: false),
					DateUsa = table.Column<string>(maxLength: 10, nullable: false),
					DayOfMonth = table.Column<int>(maxLength: 2, nullable: false),
					DayOfMonthLeadingZero = table.Column<string>(maxLength: 2, nullable: false),
					DayOfWeek2LetterName = table.Column<string>(maxLength: 2, nullable: false),
					DayOfWeek3LetterName = table.Column<string>(maxLength: 3, nullable: false),
					DayOfWeekCountInMonth = table.Column<int>(maxLength: 1, nullable: false),
					DayOfWeekFullName = table.Column<string>(maxLength: 9, nullable: false),
					DayOfWeekNumber = table.Column<int>(maxLength: 1, nullable: false),
					DayOfYear = table.Column<int>(maxLength: 3, nullable: false),
					DayOfYearLeadingZeros = table.Column<string>(maxLength: 3, nullable: false),
					EventsDayOff = table.Column<string>(maxLength: 200, nullable: false),
					EventsToday = table.Column<string>(maxLength: 200, nullable: false),
					EventsWorkday = table.Column<string>(maxLength: 200, nullable: false),
					IsDayOff = table.Column<bool>(nullable: false),
					IsFirstDayOfMonth = table.Column<bool>(nullable: false),
					IsFirstWeekOfMonth = table.Column<bool>(nullable: false),
					IsLastDayOfMonth = table.Column<bool>(nullable: false),
					IsLastWeekOfMonth = table.Column<bool>(nullable: false),
					IsLeapYear = table.Column<bool>(nullable: false),
					IsWeekDay = table.Column<bool>(nullable: false),
					IsWeekend = table.Column<bool>(nullable: false),
					IsWorkDay = table.Column<bool>(nullable: false),
					MonthFull = table.Column<string>(maxLength: 10, nullable: false),
					MonthShort = table.Column<string>(maxLength: 3, nullable: false),
					MonthNumber = table.Column<int>(maxLength: 2, nullable: false),
					MonthNumberLeadZero = table.Column<string>(maxLength: 2, nullable: false),
					Quarter = table.Column<int>(maxLength: 1, nullable: false),
					QuarterLong = table.Column<string>(maxLength: 10, nullable: false),
					QuarterShort = table.Column<string>(maxLength: 2, nullable: false),
					WeekNumber = table.Column<int>(maxLength: 2, nullable: false),
					WeekNumberLeadingZero = table.Column<string>(maxLength: 2, nullable: false),
					Year = table.Column<int>(maxLength: 4, nullable: false),
					YearShort = table.Column<string>(maxLength: 2, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DateDimension", x => x.DateId);
					table.UniqueConstraint("AK_DateDimension_DateTime", x => x.DateTime);
				});

			migrationBuilder.CreateTable(
				name: "TimeDimension",
				columns: table => new
				{
					AmPm = table.Column<string>(maxLength: 2, nullable: false),
					Hour12 = table.Column<int>(maxLength: 2, nullable: false),
					Hour12LeadingZero = table.Column<string>(maxLength: 2, nullable: false),
					Hour24 = table.Column<int>(maxLength: 2, nullable: false),
					Hour24LeadingZero = table.Column<string>(maxLength: 2, nullable: false),
					Minute = table.Column<int>(maxLength: 2, nullable: false),
					MinuteLeadingZero = table.Column<string>(maxLength: 2, nullable: false),
					RoundToMinute = table.Column<DateTime>(nullable: false),
					RoundToHour = table.Column<DateTime>(nullable: false),
					RoundToSecond = table.Column<DateTime>(nullable: false),
					Second = table.Column<int>(maxLength: 2, nullable: false),
					SecondLeadingZero = table.Column<string>(maxLength: 2, nullable: false),
					Time = table.Column<DateTime>(nullable: false),
					Time12HourMin = table.Column<string>(maxLength: 5, nullable: false),
					Time12HourMinAmPm = table.Column<string>(maxLength: 8, nullable: false),
					Time12HourMinSecAmPm = table.Column<string>(maxLength: 11, nullable: false),
					Time12HourMinSecMiliAmPm = table.Column<string>(maxLength: 15, nullable: false),
					Time24HourMinCivilian = table.Column<string>(maxLength: 5, nullable: false),
					Time24HourMinMilitary = table.Column<string>(maxLength: 4, nullable: false),
					Time24HourMinSecCivilian = table.Column<string>(maxLength: 8, nullable: false),
					Time24HourMinSecMiliCivilian = table.Column<string>(maxLength: 12, nullable: false),
					Time24HourMinSecMiliMilitary = table.Column<string>(maxLength: 10, nullable: false),
					Time24HourMinSecMilitary = table.Column<string>(maxLength: 6, nullable: false),
					TimeId = table.Column<string>(maxLength: 9, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TimeDimension", x => x.TimeId);
					table.UniqueConstraint("AK_TimeDimension_Time", x => x.Time);
				});

			migrationBuilder.CreateIndex(
				name: "IX_DateDimension_DayOfMonth",
				table: "DateDimension",
				column: "DayOfMonth");

			migrationBuilder.CreateIndex(
				name: "IX_DateDimension_DayOfWeekNumber",
				table: "DateDimension",
				column: "DayOfWeekNumber");

			migrationBuilder.CreateIndex(
				name: "IX_DateDimension_DayOfYear",
				table: "DateDimension",
				column: "DayOfYear");

			migrationBuilder.CreateIndex(
				name: "IX_DateDimension_MonthNumber",
				table: "DateDimension",
				column: "MonthNumber");

			migrationBuilder.CreateIndex(
				name: "IX_DateDimension_Quarter",
				table: "DateDimension",
				column: "Quarter");

			migrationBuilder.CreateIndex(
				name: "IX_DateDimension_WeekNumber",
				table: "DateDimension",
				column: "WeekNumber");

			migrationBuilder.CreateIndex(
				name: "IX_DateDimension_Year",
				table: "DateDimension",
				column: "Year");

			migrationBuilder.CreateIndex(
				name: "IX_TimeDimension_Hour24",
				table: "TimeDimension",
				column: "Hour24");

			migrationBuilder.CreateIndex(
				name: "IX_TimeDimension_RoundToHour",
				table: "TimeDimension",
				column: "RoundToHour");

			migrationBuilder.CreateIndex(
				name: "IX_TimeDimension_RoundToMinute",
				table: "TimeDimension",
				column: "RoundToMinute");

			migrationBuilder.CreateIndex(
				name: "IX_TimeDimension_RoundToSecond",
				table: "TimeDimension",
				column: "RoundToSecond");
		}
	}
}