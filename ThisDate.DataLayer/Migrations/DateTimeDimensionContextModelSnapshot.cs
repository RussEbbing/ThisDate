﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using ThisDate.DateTimeDataLayer.Context;

namespace ThisDate.DateTimeDataLayer.Migrations
{
    [DbContext(typeof(DateTimeDimensionContext))]
    partial class DateTimeDimensionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ThisDate.DateTimeDataLayer.Models.DateDimension", b =>
                {
                    b.Property<int>("DateId")
                        .HasMaxLength(8);

                    b.Property<string>("DateEuro")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("DateUsa")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int>("DayOfMonth")
                        .HasMaxLength(2);

                    b.Property<string>("DayOfMonthLeadingZero")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<string>("DayOfWeek2LetterName")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<string>("DayOfWeek3LetterName")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<int>("DayOfWeekCountInMonth")
                        .HasMaxLength(1);

                    b.Property<string>("DayOfWeekFullName")
                        .IsRequired()
                        .HasMaxLength(9);

                    b.Property<int>("DayOfWeekNumber")
                        .HasMaxLength(1);

                    b.Property<int>("DayOfYear")
                        .HasMaxLength(3);

                    b.Property<string>("DayOfYearLeadingZeros")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<string>("EventsDayOff")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("EventsToday")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("EventsWorkday")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("IsDayOff");

                    b.Property<bool>("IsFirstDayOfMonth");

                    b.Property<bool>("IsFirstWeekOfMonth");

                    b.Property<bool>("IsLastDayOfMonth");

                    b.Property<bool>("IsLastWeekOfMonth");

                    b.Property<bool>("IsLeapYear");

                    b.Property<bool>("IsWeekDay");

                    b.Property<bool>("IsWeekend");

                    b.Property<bool>("IsWorkDay");

                    b.Property<string>("MonthFull")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int>("MonthNumber")
                        .HasMaxLength(2);

                    b.Property<string>("MonthNumberLeadZero")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<string>("MonthShort")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<int>("Quarter")
                        .HasMaxLength(1);

                    b.Property<string>("QuarterLong")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("QuarterShort")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<int>("WeekNumber")
                        .HasMaxLength(2);

                    b.Property<string>("WeekNumberLeadingZero")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<int>("Year")
                        .HasMaxLength(4);

                    b.Property<string>("YearShort")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.HasKey("DateId");

                    b.HasAlternateKey("DateTime");

                    b.HasIndex("DayOfMonth");

                    b.HasIndex("DayOfWeekNumber");

                    b.HasIndex("DayOfYear");

                    b.HasIndex("MonthNumber");

                    b.HasIndex("Quarter");

                    b.HasIndex("WeekNumber");

                    b.HasIndex("Year");

                    b.ToTable("DateDimension");
                });

            modelBuilder.Entity("ThisDate.DateTimeDataLayer.Models.TimeDimension", b =>
                {
                    b.Property<string>("TimeId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(9);

                    b.Property<string>("AmPm")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<int>("Hour12")
                        .HasMaxLength(2);

                    b.Property<string>("Hour12LeadingZero")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<int>("Hour24")
                        .HasMaxLength(2);

                    b.Property<string>("Hour24LeadingZero")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<int>("Minute")
                        .HasMaxLength(2);

                    b.Property<string>("MinuteLeadingZero")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<DateTime>("RoundToHour");

                    b.Property<DateTime>("RoundToMinute");

                    b.Property<DateTime>("RoundToSecond");

                    b.Property<int>("Second")
                        .HasMaxLength(2);

                    b.Property<string>("SecondLeadingZero")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<DateTime>("Time");

                    b.Property<string>("Time12HourMin")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<string>("Time12HourMinAmPm")
                        .IsRequired()
                        .HasMaxLength(8);

                    b.Property<string>("Time12HourMinSecAmPm")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("Time12HourMinSecMiliAmPm")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("Time24HourMinCivilian")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<string>("Time24HourMinMilitary")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<string>("Time24HourMinSecCivilian")
                        .IsRequired()
                        .HasMaxLength(8);

                    b.Property<string>("Time24HourMinSecMiliCivilian")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<string>("Time24HourMinSecMiliMilitary")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Time24HourMinSecMilitary")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.HasKey("TimeId");

                    b.HasAlternateKey("Time");

                    b.HasIndex("Hour24");

                    b.HasIndex("RoundToHour");

                    b.HasIndex("RoundToMinute");

                    b.HasIndex("RoundToSecond");

                    b.ToTable("TimeDimension");
                });
#pragma warning restore 612, 618
        }
    }
}