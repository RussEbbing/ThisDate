using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using ThisDate.DefinedCalendars.USA;
using Xunit;
using Xunit.Sdk;

namespace ThisDate.Tests
{
	/// <summary>
	/// Test rig and tests related to the test rig.
	/// </summary>
	public partial class ThisDateTests
	{
		[NotNull]
		private static readonly List<Holiday> ChristmasDays = new List<Holiday>
		{
			new Holiday(HolidayNames.ChristmasDayText, new DateTime(2016, 12, 26), false),
			new Holiday(HolidayNames.ChristmasDayText, new DateTime(2017, 12, 25), false),
			new Holiday(HolidayNames.ChristmasDayText, new DateTime(2018, 12, 25), false),
			new Holiday(HolidayNames.ChristmasDayText, new DateTime(2019, 12, 25), false),
			new Holiday(HolidayNames.ChristmasDayText, new DateTime(2020, 12, 25), false),
			new Holiday(HolidayNames.ChristmasDayText, new DateTime(2021, 12, 24), false),
			new Holiday(HolidayNames.ChristmasDayText, new DateTime(2022, 12, 26), false),
			new Holiday(HolidayNames.ChristmasDayText, new DateTime(2023, 12, 25), false),
			new Holiday(HolidayNames.ChristmasDayText, new DateTime(2024, 12, 25), false),
			new Holiday(HolidayNames.ChristmasDayText, new DateTime(2025, 12, 25), false),
			new Holiday(HolidayNames.ChristmasDayText, new DateTime(2026, 12, 25), false),
			new Holiday(HolidayNames.ChristmasDayText, new DateTime(2027, 12, 24), false)
		};

		[NotNull]
		private static readonly List<Holiday> ColumbusDayAsDayOff = new List<Holiday>
		{
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2016, 10, 10), false),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2017, 10, 9), false),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2018, 10, 8), false),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2019, 10, 14), false),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2020, 10, 12), false),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2021, 10, 11), false),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2022, 10, 10), false),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2023, 10, 9), false),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2024, 10, 14), false),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2025, 10, 13), false),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2026, 10, 12), false),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2027, 10, 11), false)
		};

		[NotNull]
		private static readonly List<Holiday> ColumbusDayAsWorkday = new List<Holiday>
		{
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2016, 10, 10), true),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2017, 10, 9), true),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2018, 10, 8), true),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2019, 10, 14), true),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2020, 10, 12), true),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2021, 10, 11), true),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2022, 10, 10), true),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2023, 10, 9), true),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2024, 10, 14), true),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2025, 10, 13), true),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2026, 10, 12), true),
			new Holiday(HolidayNames.ColumbusDayText, new DateTime(2027, 10, 11), true)
		};

		[NotNull]
		private static readonly List<Holiday> EasterDays = new List<Holiday>
		{
			new Holiday(HolidayNames.EasterSundayText, new DateTime(2016, 3, 27), false),
			new Holiday(HolidayNames.EasterSundayText, new DateTime(2017, 4, 16), false),
			new Holiday(HolidayNames.EasterSundayText, new DateTime(2018, 4, 1), false),
			new Holiday(HolidayNames.EasterSundayText, new DateTime(2019, 4, 21), false),
			new Holiday(HolidayNames.EasterSundayText, new DateTime(2020, 4, 12), false),
			new Holiday(HolidayNames.EasterSundayText, new DateTime(2021, 4, 4), false),
			new Holiday(HolidayNames.EasterSundayText, new DateTime(2022, 4, 17), false),
			new Holiday(HolidayNames.EasterSundayText, new DateTime(2023, 4, 9), false),
			new Holiday(HolidayNames.EasterSundayText, new DateTime(2024, 3, 31), false),
			new Holiday(HolidayNames.EasterSundayText, new DateTime(2025, 4, 20), false),
			new Holiday(HolidayNames.EasterSundayText, new DateTime(2026, 4, 5), false),
			new Holiday(HolidayNames.EasterSundayText, new DateTime(2027, 3, 28), false),
		};

		[NotNull]
		private static readonly List<Holiday> FatherDay = new List<Holiday>
		{
			new Holiday(HolidayNames.FathersDayText, new DateTime(2016, 6, 19), false),
			new Holiday(HolidayNames.FathersDayText, new DateTime(2017, 6, 18), false),
			new Holiday(HolidayNames.FathersDayText, new DateTime(2018, 6, 17), false),
			new Holiday(HolidayNames.FathersDayText, new DateTime(2019, 6, 16), false),
			new Holiday(HolidayNames.FathersDayText, new DateTime(2020, 6, 21), false),
			new Holiday(HolidayNames.FathersDayText, new DateTime(2021, 6, 20), false),
			new Holiday(HolidayNames.FathersDayText, new DateTime(2022, 6, 19), false),
			new Holiday(HolidayNames.FathersDayText, new DateTime(2023, 6, 18), false),
			new Holiday(HolidayNames.FathersDayText, new DateTime(2024, 6, 16), false),
			new Holiday(HolidayNames.FathersDayText, new DateTime(2025, 6, 15), false),
			new Holiday(HolidayNames.FathersDayText, new DateTime(2026, 6, 21), false),
			new Holiday(HolidayNames.FathersDayText, new DateTime(2027, 6, 20), false)
		};

		[NotNull]
		private static readonly List<Holiday> GoodFridaysAsDayOff = new List<Holiday>
		{
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2016, 3, 25), false),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2017, 4, 14), false),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2018, 3, 30), false),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2019, 4, 19), false),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2020, 4, 10), false),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2021, 4, 2), false),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2022, 4, 15), false),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2023, 4, 7), false),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2024, 3, 29), false),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2025, 4, 18), false),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2026, 4, 3), false),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2027, 3, 26), false),
		};

		[NotNull]
		private static readonly List<Holiday> GoodFridaysAsWorkday = new List<Holiday>
		{
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2016, 3, 25), true),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2017, 4, 14), true),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2018, 3, 30), true),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2019, 4, 19), true),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2020, 4, 10), true),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2021, 4, 2), true),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2022, 4, 15), true),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2023, 4, 7), true),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2024, 3, 29), true),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2025, 4, 18), true),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2026, 4, 3), true),
			new Holiday(HolidayNames.GoodFridayText, new DateTime(2027, 3, 26), true),
		};

		[NotNull]
		private static readonly List<Holiday> Halloween = new List<Holiday>
		{
			new Holiday(HolidayNames.HalloweenText, new DateTime(2016, 10, 31), true),
			new Holiday(HolidayNames.HalloweenText, new DateTime(2017, 10, 31), true),
			new Holiday(HolidayNames.HalloweenText, new DateTime(2018, 10, 31), true),
			new Holiday(HolidayNames.HalloweenText, new DateTime(2019, 10, 31), true),
			new Holiday(HolidayNames.HalloweenText, new DateTime(2020, 10, 31), false),
			new Holiday(HolidayNames.HalloweenText, new DateTime(2021, 10, 31), false),
			new Holiday(HolidayNames.HalloweenText, new DateTime(2022, 10, 31), true),
			new Holiday(HolidayNames.HalloweenText, new DateTime(2023, 10, 31), true),
			new Holiday(HolidayNames.HalloweenText, new DateTime(2024, 10, 31), true),
			new Holiday(HolidayNames.HalloweenText, new DateTime(2025, 10, 31), true),
			new Holiday(HolidayNames.HalloweenText, new DateTime(2026, 10, 31), false),
			new Holiday(HolidayNames.HalloweenText, new DateTime(2027, 10, 31), false)
		};

		// independence
		[NotNull]
		private static readonly List<Holiday> IndependenceDays = new List<Holiday>
		{
			new Holiday(HolidayNames.IndependentsDayText, new DateTime(2016, 7, 4), false),
			new Holiday(HolidayNames.IndependentsDayText, new DateTime(2017, 7, 4), false),
			new Holiday(HolidayNames.IndependentsDayText, new DateTime(2018, 7, 4), false),
			new Holiday(HolidayNames.IndependentsDayText, new DateTime(2019, 7, 4), false),
			new Holiday(HolidayNames.IndependentsDayText, new DateTime(2020, 7, 3), false),
			new Holiday(HolidayNames.IndependentsDayText, new DateTime(2021, 7, 5), false),
			new Holiday(HolidayNames.IndependentsDayText, new DateTime(2022, 7, 4), false),
			new Holiday(HolidayNames.IndependentsDayText, new DateTime(2023, 7, 4), false),
			new Holiday(HolidayNames.IndependentsDayText, new DateTime(2024, 7, 4), false),
			new Holiday(HolidayNames.IndependentsDayText, new DateTime(2025, 7, 4), false),
			new Holiday(HolidayNames.IndependentsDayText, new DateTime(2026, 7, 3), false),
			new Holiday(HolidayNames.IndependentsDayText, new DateTime(2027, 7, 5), false),
		};

		[NotNull]
		private static readonly List<Holiday> LaborDays = new List<Holiday>
		{
			new Holiday(HolidayNames.LaborDayText, new DateTime(2016, 9, 5), false),
			new Holiday(HolidayNames.LaborDayText, new DateTime(2017, 9, 4), false),
			new Holiday(HolidayNames.LaborDayText, new DateTime(2018, 9, 3), false),
			new Holiday(HolidayNames.LaborDayText, new DateTime(2019, 9, 2), false),
			new Holiday(HolidayNames.LaborDayText, new DateTime(2020, 9, 7), false),
			new Holiday(HolidayNames.LaborDayText, new DateTime(2021, 9, 6), false),
			new Holiday(HolidayNames.LaborDayText, new DateTime(2022, 9, 5), false),
			new Holiday(HolidayNames.LaborDayText, new DateTime(2023, 9, 4), false),
			new Holiday(HolidayNames.LaborDayText, new DateTime(2024, 9, 2), false),
			new Holiday(HolidayNames.LaborDayText, new DateTime(2025, 9, 1), false),
			new Holiday(HolidayNames.LaborDayText, new DateTime(2026, 9, 7), false),
			new Holiday(HolidayNames.LaborDayText, new DateTime(2027, 9, 6), false),
		};

		[NotNull]
		private static readonly List<int> LeapYearsTestRig = new List<int>
		{
			1804, 1808, 1812, 1816, 1820, 1824, 1828, 1832, 1836, 1840, 1844, 1848, 1852, 1856, 1860, 1864, 1868, 1872, 1876, 1880, 1884, 1888, 1892, 1896, 1904, 1908, 1912,
			1916, 1920, 1924, 1928, 1932, 1936, 1940, 1944, 1948, 1952, 1956, 1960, 1964, 1968, 1972, 1976, 1980, 1984, 1988, 1992, 1996, 2000, 2004, 2008, 2012, 2016, 2020,
			2024, 2028, 2032, 2036, 2040, 2044, 2048, 2052, 2056, 2060, 2064, 2068, 2072, 2076, 2080, 2084, 2088, 2092, 2096, 2104, 2108, 2112, 2116, 2120, 2124, 2128, 2132,
			2136, 2140, 2144, 2148, 2152, 2156, 2160, 2164, 2168, 2172, 2176, 2180, 2184, 2188, 2192, 2196, 2204, 2208, 2212, 2216, 2220, 2224, 2228, 2232, 2236, 2240, 2244,
			2248, 2252, 2256, 2260, 2264, 2268, 2272, 2276, 2280, 2284, 2288, 2292, 2296, 2304, 2308, 2312, 2316, 2320, 2324, 2328, 2332, 2336, 2340, 2344, 2348, 2352, 2356,
			2360, 2364, 2368, 2372, 2376, 2380, 2384, 2388, 2392, 2396, 2400
		};

		[NotNull]
		private static readonly List<Holiday> MartinLutherKingDays = new List<Holiday>
		{
			new Holiday(HolidayNames.MartinLutherKingText, new DateTime(2016, 1, 18), false),
			new Holiday(HolidayNames.MartinLutherKingText, new DateTime(2017, 1, 16), false),
			new Holiday(HolidayNames.MartinLutherKingText, new DateTime(2018, 1, 15), false),
			new Holiday(HolidayNames.MartinLutherKingText, new DateTime(2019, 1, 21), false),
			new Holiday(HolidayNames.MartinLutherKingText, new DateTime(2020, 1, 20), false),
			new Holiday(HolidayNames.MartinLutherKingText, new DateTime(2021, 1, 18), false),
			new Holiday(HolidayNames.MartinLutherKingText, new DateTime(2022, 1, 17), false),
			new Holiday(HolidayNames.MartinLutherKingText, new DateTime(2023, 1, 16), false),
			new Holiday(HolidayNames.MartinLutherKingText, new DateTime(2024, 1, 15), false),
			new Holiday(HolidayNames.MartinLutherKingText, new DateTime(2025, 1, 20), false),
			new Holiday(HolidayNames.MartinLutherKingText, new DateTime(2026, 1, 19), false),
			new Holiday(HolidayNames.MartinLutherKingText, new DateTime(2027, 1, 18), false),
		};

		[NotNull]
		private static readonly List<Holiday> MemorialDays = new List<Holiday>
		{
			new Holiday(HolidayNames.MemorialDayText, new DateTime(2016, 5, 30), false),
			new Holiday(HolidayNames.MemorialDayText, new DateTime(2017, 5, 29), false),
			new Holiday(HolidayNames.MemorialDayText, new DateTime(2018, 5, 28), false),
			new Holiday(HolidayNames.MemorialDayText, new DateTime(2019, 5, 27), false),
			new Holiday(HolidayNames.MemorialDayText, new DateTime(2020, 5, 25), false),
			new Holiday(HolidayNames.MemorialDayText, new DateTime(2021, 5, 31), false),
			new Holiday(HolidayNames.MemorialDayText, new DateTime(2022, 5, 30), false),
			new Holiday(HolidayNames.MemorialDayText, new DateTime(2023, 5, 29), false),
			new Holiday(HolidayNames.MemorialDayText, new DateTime(2024, 5, 27), false),
			new Holiday(HolidayNames.MemorialDayText, new DateTime(2025, 5, 26), false),
			new Holiday(HolidayNames.MemorialDayText, new DateTime(2026, 5, 25), false),
			new Holiday(HolidayNames.MemorialDayText, new DateTime(2027, 5, 31), false),
		};

		[NotNull]
		private static readonly List<Holiday> MothersDay = new List<Holiday>
		{
			new Holiday(HolidayNames.MothersDayText, new DateTime(2016, 5, 8), false),
			new Holiday(HolidayNames.MothersDayText, new DateTime(2017, 5, 14), false),
			new Holiday(HolidayNames.MothersDayText, new DateTime(2018, 5, 13), false),
			new Holiday(HolidayNames.MothersDayText, new DateTime(2019, 5, 12), false),
			new Holiday(HolidayNames.MothersDayText, new DateTime(2020, 5, 10), false),
			new Holiday(HolidayNames.MothersDayText, new DateTime(2021, 5, 9), false),
			new Holiday(HolidayNames.MothersDayText, new DateTime(2022, 5, 8), false),
			new Holiday(HolidayNames.MothersDayText, new DateTime(2023, 5, 14), false),
			new Holiday(HolidayNames.MothersDayText, new DateTime(2024, 5, 12), false),
			new Holiday(HolidayNames.MothersDayText, new DateTime(2025, 5, 11), false),
			new Holiday(HolidayNames.MothersDayText, new DateTime(2026, 5, 10), false),
			new Holiday(HolidayNames.MothersDayText, new DateTime(2027, 5, 9), false)
		};

		[NotNull]
		private static readonly List<Holiday> NewYearsDaysNyse = new List<Holiday>
		{
			new Holiday(HolidayNames.NewYearsDayText, new DateTime(2016, 1, 1), false),
			new Holiday(HolidayNames.NewYearsDayText, new DateTime(2017, 1, 2), false),
			new Holiday(HolidayNames.NewYearsDayText, new DateTime(2018, 1, 1), false),
			new Holiday(HolidayNames.NewYearsDayText, new DateTime(2019, 1, 1), false),
			new Holiday(HolidayNames.NewYearsDayText, new DateTime(2020, 1, 1), false),
			new Holiday(HolidayNames.NewYearsDayText, new DateTime(2021, 1, 1), false),
			new Holiday(HolidayNames.NewYearsDayText, new DateTime(2022, 1, 1), false),
			new Holiday(HolidayNames.NewYearsDayText, new DateTime(2023, 1, 2), false),
			new Holiday(HolidayNames.NewYearsDayText, new DateTime(2024, 1, 1), false),
			new Holiday(HolidayNames.NewYearsDayText, new DateTime(2025, 1, 1), false),
			new Holiday(HolidayNames.NewYearsDayText, new DateTime(2026, 1, 1), false),
			new Holiday(HolidayNames.NewYearsDayText, new DateTime(2027, 1, 1), false)
		};

		[NotNull]
		private static readonly List<Holiday> PresidentsDaysAsDayOff = new List<Holiday>
		{
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2016, 2, 15), false),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2017, 2, 20), false),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2018, 2, 19), false),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2019, 2, 18), false),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2020, 2, 17), false),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2021, 2, 15), false),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2022, 2, 21), false),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2023, 2, 20), false),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2024, 2, 19), false),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2025, 2, 17), false),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2026, 2, 16), false),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2027, 2, 15), false),
		};

		[NotNull]
		private static readonly List<Holiday> PresidentsDaysAsWorkday = new List<Holiday>
		{
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2016, 2, 15), true),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2017, 2, 20), true),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2018, 2, 19), true),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2019, 2, 18), true),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2020, 2, 17), true),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2021, 2, 15), true),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2022, 2, 21), true),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2023, 2, 20), true),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2024, 2, 19), true),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2025, 2, 17), true),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2026, 2, 16), true),
			new Holiday(HolidayNames.PresidentsDayText, new DateTime(2027, 2, 15), true),
		};

		[NotNull]
		private static readonly List<Holiday> SaintPatrickDay = new List<Holiday>
		{
			new Holiday(HolidayNames.SaintPatrickDayText, new DateTime(2016, 3, 17), true),
			new Holiday(HolidayNames.SaintPatrickDayText, new DateTime(2017, 3, 17), true),
			new Holiday(HolidayNames.SaintPatrickDayText, new DateTime(2018, 3, 17), false),
			new Holiday(HolidayNames.SaintPatrickDayText, new DateTime(2019, 3, 17), false),
			new Holiday(HolidayNames.SaintPatrickDayText, new DateTime(2020, 3, 17), true),
			new Holiday(HolidayNames.SaintPatrickDayText, new DateTime(2021, 3, 17), true),
			new Holiday(HolidayNames.SaintPatrickDayText, new DateTime(2022, 3, 17), true),
			new Holiday(HolidayNames.SaintPatrickDayText, new DateTime(2023, 3, 17), true),
			new Holiday(HolidayNames.SaintPatrickDayText, new DateTime(2024, 3, 17), false),
			new Holiday(HolidayNames.SaintPatrickDayText, new DateTime(2025, 3, 17), true),
			new Holiday(HolidayNames.SaintPatrickDayText, new DateTime(2026, 3, 17), true),
			new Holiday(HolidayNames.SaintPatrickDayText, new DateTime(2027, 3, 17), true),
		};

		[NotNull]
		private static readonly List<Holiday> ThanksgivingDays = new List<Holiday>
		{
			new Holiday(HolidayNames.ThanksgivingDayText, new DateTime(2016, 11, 24), false),
			new Holiday(HolidayNames.ThanksgivingDayText, new DateTime(2017, 11, 23), false),
			new Holiday(HolidayNames.ThanksgivingDayText, new DateTime(2018, 11, 22), false),
			new Holiday(HolidayNames.ThanksgivingDayText, new DateTime(2019, 11, 28), false),
			new Holiday(HolidayNames.ThanksgivingDayText, new DateTime(2020, 11, 26), false),
			new Holiday(HolidayNames.ThanksgivingDayText, new DateTime(2021, 11, 25), false),
			new Holiday(HolidayNames.ThanksgivingDayText, new DateTime(2022, 11, 24), false),
			new Holiday(HolidayNames.ThanksgivingDayText, new DateTime(2023, 11, 23), false),
			new Holiday(HolidayNames.ThanksgivingDayText, new DateTime(2024, 11, 28), false),
			new Holiday(HolidayNames.ThanksgivingDayText, new DateTime(2025, 11, 27), false),
			new Holiday(HolidayNames.ThanksgivingDayText, new DateTime(2026, 11, 26), false),
			new Holiday(HolidayNames.ThanksgivingDayText, new DateTime(2027, 11, 25), false),
		};

		[NotNull]
		private static readonly List<Holiday> Valentines = new List<Holiday>
		{
			new Holiday(HolidayNames.ValentinesDayText, new DateTime(2016, 2, 14), false),
			new Holiday(HolidayNames.ValentinesDayText, new DateTime(2017, 2, 14), true),
			new Holiday(HolidayNames.ValentinesDayText, new DateTime(2018, 2, 14), true),
			new Holiday(HolidayNames.ValentinesDayText, new DateTime(2019, 2, 14), true),
			new Holiday(HolidayNames.ValentinesDayText, new DateTime(2020, 2, 14), true),
			new Holiday(HolidayNames.ValentinesDayText, new DateTime(2021, 2, 14), false),
			new Holiday(HolidayNames.ValentinesDayText, new DateTime(2022, 2, 14), true),
			new Holiday(HolidayNames.ValentinesDayText, new DateTime(2023, 2, 14), true),
			new Holiday(HolidayNames.ValentinesDayText, new DateTime(2024, 2, 14), true),
			new Holiday(HolidayNames.ValentinesDayText, new DateTime(2025, 2, 14), true),
			new Holiday(HolidayNames.ValentinesDayText, new DateTime(2026, 2, 14), false),
			new Holiday(HolidayNames.ValentinesDayText, new DateTime(2027, 2, 14), false)
		};

		[NotNull]
		private static readonly List<Holiday> VeteransDayAsDayOff = new List<Holiday>
		{
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2016, 11, 11), false),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2017, 11, 10), false),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2018, 11, 12), false),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2019, 11, 11), false),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2020, 11, 11), false),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2021, 11, 11), false),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2022, 11, 11), false),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2023, 11, 10), false),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2024, 11, 11), false),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2025, 11, 11), false),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2026, 11, 11), false),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2027, 11, 11), false)
		};

		[NotNull]
		private static readonly List<Holiday> VeteransDayAsWorkDay = new List<Holiday>
		{
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2016, 11, 11), true),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2017, 11, 10), true),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2018, 11, 12), true),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2019, 11, 11), true),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2020, 11, 11), true),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2021, 11, 11), true),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2022, 11, 11), true),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2023, 11, 10), true),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2024, 11, 11), true),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2025, 11, 11), true),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2026, 11, 11), true),
			new Holiday(HolidayNames.VeteransDayText, new DateTime(2027, 11, 11), true)
		};

		/// <summary>
		/// The maximum test year
		/// </summary>
		private readonly DateTime _holidayMaxDateTestRig = NewYorkStockExchangeCalendarTestRig.Max(m => m.Date);

		/// <summary>
		/// The minimum test year
		/// </summary>
		private readonly DateTime _holidayMinDateTestRig = NewYorkStockExchangeCalendarTestRig.Min(m => m.Date);

		private static int LeapYearMaxTestRig => LeapYearsTestRig.Max();

		private static int LeapYearMinTestRig => LeapYearsTestRig.Min();

		[NotNull]
		private static List<Holiday> NewYorkStockExchangeCalendarTestRig
		{
			get
			{
				var c = new List<Holiday>();
				c.AddRange(NewYearsDaysNyse);
				c.AddRange(MartinLutherKingDays);
				c.AddRange(PresidentsDaysAsDayOff);
				c.AddRange(GoodFridaysAsDayOff);
				c.AddRange(MemorialDays);
				c.AddRange(IndependenceDays);
				c.AddRange(LaborDays);
				c.AddRange(ThanksgivingDays);
				c.AddRange(ChristmasDays);
				return c;
			}
		}

		[NotNull]
		private static List<Holiday> UsaGovernmentCalendarTestRig
		{
			get
			{
				var c = new List<Holiday>();
				c.AddRange(NewYearsDaysNyse);
				c.AddRange(MartinLutherKingDays);
				c.AddRange(PresidentsDaysAsDayOff);
				c.AddRange(MemorialDays);
				c.AddRange(IndependenceDays);
				c.AddRange(LaborDays);
				c.AddRange(ColumbusDayAsDayOff);
				c.AddRange(VeteransDayAsDayOff);
				c.AddRange(ThanksgivingDays);
				c.AddRange(ChristmasDays);
				return c;
			}
		}

		[NotNull]
		private static List<Holiday> UsaObservedHolidaysTestRig
		{
			get
			{
				var c = new List<Holiday>();
				c.AddRange(NewYearsDaysNyse);
				c.AddRange(MartinLutherKingDays);
				c.AddRange(PresidentsDaysAsDayOff);
				c.AddRange(GoodFridaysAsWorkday);
				c.AddRange(EasterDays);
				c.AddRange(MemorialDays);
				c.AddRange(IndependenceDays);
				c.AddRange(LaborDays);
				c.AddRange(VeteransDayAsWorkDay);
				c.AddRange(ThanksgivingDays);
				c.AddRange(ChristmasDays);
				c.AddRange(Halloween);
				c.AddRange(Valentines);
				c.AddRange(FatherDay);
				c.AddRange(ColumbusDayAsWorkday);
				c.AddRange(MothersDay);
				c.AddRange(SaintPatrickDay);
				return c;
			}
		}

		/// <summary>
		/// Tests the set Christmas.
		/// </summary>
		[Fact]
		public void TestSetChristmas()
		{
			var christmasData = NewYorkStockExchangeCalendarTestRig.Where(s => s.Name == HolidayNames.ChristmasDayText).ToList();
			TestSetYearRange(christmasData);
			Assert.True(christmasData.TrueForAll(d => d.Date.Month == 12), "Christmas is not always in December?");
			Assert.True(christmasData.TrueForAll(d => d.Date.Day >= 24 && d.Date.Day <= 26), "Christmas dates out of range.");
		}

		/// <summary>
		/// Tests the set easter.
		/// </summary>
		/// <exception cref="TrueException">Thrown when the condition is false</exception>
		[Fact]
		public void TestSetEaster()
		{
			var assertHolidays = EasterDays.Where(w => w.Name == HolidayNames.EasterSundayText).ToList();
			TestSetYearRange(assertHolidays);
			Assert.True(assertHolidays.TrueForAll(t => t.Date.DayOfWeek == DayOfWeek.Sunday), "Easter not all on Sunday.");
			Assert.True(assertHolidays.TrueForAll(t => t.Date.Month == 3 || t.Date.Month == 4), "Easter not in March or April.");
		}

		/// <summary>
		/// Tests the set good friday.
		/// </summary>
		/// <exception cref="TrueException">Thrown when the condition is false</exception>
		[Fact]
		public void TestSetGoodFriday()
		{
			var assertHolidays = GoodFridaysAsDayOff.Where(w => w.Name == HolidayNames.GoodFridayText).ToList();
			TestSetYearRange(assertHolidays);
			Assert.True(assertHolidays.TrueForAll(t => t.Date.DayOfWeek == DayOfWeek.Friday), "Good Friday is not all on Friday.");
			Assert.True(assertHolidays.TrueForAll(t => t.Date.Month == 3 || t.Date.Month == 4), "Good Friday is not in March or April.");
		}

		/// <summary>
		/// Tests the set independence day.
		/// </summary>
		/// <exception cref="TrueException">Thrown when the condition is false</exception>
		[Fact]
		public void TestSetIndependenceDay()
		{
			var data = NewYorkStockExchangeCalendarTestRig.Where(s => s.Name == HolidayNames.IndependentsDayText).ToList();
			TestSetYearRange(data);
			Assert.True(data.TrueForAll(t => t.Date.Month == 7), "Not all 4th July is in July.");
			Assert.True(data.TrueForAll(t => t.Date.Day >= 3 && t.Date.Day <= 5), "Some dates out of range.");
		}

		/// <summary>
		/// Tests the set labor day.
		/// </summary>
		/// <exception cref="TrueException">Thrown when the condition is false</exception>
		[Fact]
		public void TestSetLaborDay()
		{
			var assertHolidays = NewYorkStockExchangeCalendarTestRig.Where(w => w.Name == HolidayNames.LaborDayText).ToList();
			TestSetYearRange(assertHolidays);
			Assert.True(assertHolidays.TrueForAll(a => a.Date.Month == 9), "Wrong month.");
			TestSetForwardWeekDayHolidays(assertHolidays, 1, DayOfWeek.Monday, HolidayNames.MartinLutherKingText);
		}

		/// <summary>
		/// Test Martin Luther King set, all 3rd week on Monday.
		/// </summary>
		/// <exception cref="TrueException">Thrown when the condition is false</exception>
		[Fact]
		public void TestSetMartinLutherKing()
		{
			var assertHolidays = NewYorkStockExchangeCalendarTestRig.Where(w => w.Name == HolidayNames.MartinLutherKingText).ToList();
			Assert.True(assertHolidays.TrueForAll(t => t.Date.Day >= 12 && t.Date.Day <= 25), "Date out of range.");
			TestSetYearRange(assertHolidays);
			TestSetForwardWeekDayHolidays(assertHolidays, 3, DayOfWeek.Monday, HolidayNames.MartinLutherKingText);
		}

		/// <summary>
		/// Tests the Memorial Day test set.
		/// </summary>
		[Fact]
		public void TestSetMemorialDay()
		{
			var assertHolidays = NewYorkStockExchangeCalendarTestRig.Where(w => w.Name == HolidayNames.MemorialDayText).ToList();
			TestSetYearRange(assertHolidays);
			TestSetReverseWeekDayHolidays(assertHolidays, 1, DayOfWeek.Monday, HolidayNames.MemorialDayText);
		}

		/// <summary>
		/// Tests New Years, all Jan 1.
		/// </summary>
		/// <exception cref="TrueException">Thrown when the condition is false</exception>
		[Fact]
		public void TestSetNewYears()
		{
			var data = NewYorkStockExchangeCalendarTestRig.Where(s => s.Name == HolidayNames.NewYearsDayText).ToList();
			Assert.True(data.TrueForAll(t => t.Date.Month == 1), "Not all are in January.");
			Assert.True(data.TrueForAll(t => t.Date.Day >= 1 && t.Date.Day <= 2), "Date mismatch.");
		}

		/// <summary>
		/// Tests the set presidents day.
		/// </summary>
		/// <exception cref="TrueException">Thrown when the condition is false</exception>
		[Fact]
		public void TestSetPresidentsDay()
		{
			var assertHolidays = NewYorkStockExchangeCalendarTestRig.Where(w => w.Name == HolidayNames.PresidentsDayText).ToList();
			Assert.True(assertHolidays.TrueForAll(t => t.Date.Month == 2), "Not all are in February?");
			TestSetForwardWeekDayHolidays(assertHolidays, 3, DayOfWeek.Monday, HolidayNames.PresidentsDayText);
		}

		[Fact]
		public void TestSetThanksgiving()
		{
			var assertHolidays = NewYorkStockExchangeCalendarTestRig.Where(w => w.Name == HolidayNames.ThanksgivingDayText).ToList();
			Assert.True(assertHolidays.TrueForAll(t => t.Date.Month == 11), "Not all in November");
			TestSetForwardWeekDayHolidays(assertHolidays, 4, DayOfWeek.Thursday, HolidayNames.ThanksgivingDayText);
		}

		private void TestSetForwardWeekDayHolidays([NotNull] List<Holiday> assertHolidays, int expectedWeek, DayOfWeek expectedDayOfWeek, [NotNull] string expectedHoliday)
		{
			TestSetYearRange(assertHolidays);
			foreach (var assertHoliday in assertHolidays)
			{
				var expectedDate = CalendarDateTime.DayOfWeekMonthForward(assertHoliday.Date.Year, assertHoliday.Date.Month, expectedDayOfWeek, expectedWeek);
				Assert.True(assertHoliday.Date == expectedDate, "Date miss match in the " + expectedHoliday + " set");
			}
		}

		private void TestSetReverseWeekDayHolidays([NotNull] List<Holiday> assertHolidays, int expectedWeek, DayOfWeek expectedDayOfWeek, [NotNull] string expectedHoliday)
		{
			TestSetYearRange(assertHolidays);
			foreach (var assertHoliday in assertHolidays)
			{
				var expectedDate = CalendarDateTime.DayOfWeekMonthReverse(
					assertHoliday.Date.Year,
					assertHoliday.Date.Month, expectedDayOfWeek, expectedWeek);
				Assert.True(assertHoliday.Date == expectedDate, "Date miss match in the " + expectedHoliday + " set");
			}
		}

		private void TestSetYearRange([NotNull] List<Holiday> assertHolidays)
		{
			var filtered = assertHolidays
				.Where(w => w.Date.Year >= _holidayMinDateTestRig.Year && w.Date.Year <= _holidayMaxDateTestRig.Year)
				.Distinct()
				.ToList();
			Assert.True(assertHolidays.Count == filtered.Count, "Extra years found.");
			Assert.True(filtered.Count == _holidayMaxDateTestRig.Year - _holidayMinDateTestRig.Year + 1, "Years missing.");
		}

		private struct Holiday
		{
			public readonly DateTime Date;

			public readonly bool IsWorkday;

			[CanBeNull]
			public readonly string Name;

			public Holiday([NotNull] string name, DateTime date, bool isWorkday)
			{
				Date = date;
				Name = name;
				IsWorkday = isWorkday;
			}
		}
	}
}