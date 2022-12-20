using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTCommon.Utils
{
    public static class DateTimeHelper
    {
		public enum DateInterval
		{
			Day,
			DayOfYear,
			Hour,
			Minute,
			Month,
			Quarter,
			Second,
			Weekday,
			WeekOfYear,
			Year
		}

		public static DateTime DateAdd(DateInterval interval, double number, DateTime dateVal)
		{
			DateTime dtm = dateVal;
			checked
			{
				switch (interval)
				{
					case DateInterval.Day:
					case DateInterval.DayOfYear:
					case DateInterval.Weekday:
						dtm = dateVal.AddDays(number);
						break;
					case DateInterval.Hour:
						dtm = dateVal.AddHours(number);
						break;
					case DateInterval.Minute:
						dtm = dateVal.AddMinutes(number);
						break;
					case DateInterval.Month:
						{
							int months = Convert.ToInt32(number);
							dtm = dateVal.AddMonths(months);
							break;
						}
					case DateInterval.Quarter:
						{
							int quarters = Convert.ToInt32(number);
							dtm = dateVal.AddMonths(quarters * 3);
							break;
						}
					case DateInterval.Second:
						dtm = dateVal.AddSeconds(number);
						break;
					case DateInterval.WeekOfYear:
						{
							int weekOfYear = Convert.ToInt32(number);
							dtm = dateVal.AddDays(weekOfYear * 7);
							break;
						}
					case DateInterval.Year:
						{
							int years = Convert.ToInt32(number);
							dtm = dateVal.AddYears(years);
							break;
						}
				}
				return dtm;
			}
		}

		public static DateTime DateAdd(string interval, double number, DateTime dateValue)
		{
			Dictionary<string, DateInterval> dct = new Dictionary<string, DateInterval>
			{
				{
					"d",
					DateInterval.Day
				},
				{
					"y",
					DateInterval.DayOfYear
				},
				{
					"h",
					DateInterval.Hour
				},
				{
					"n",
					DateInterval.Minute
				},
				{
					"m",
					DateInterval.Month
				},
				{
					"q",
					DateInterval.Quarter
				},
				{
					"s",
					DateInterval.Second
				},
				{
					"w",
					DateInterval.Weekday
				},
				{
					"ww",
					DateInterval.WeekOfYear
				},
				{
					"yyyy",
					DateInterval.Year
				}
			};
			DateInterval di = DateInterval.Day;
			if (dct.ContainsKey(interval))
			{
				di = dct[interval];
				return DateAdd(di, number, dateValue);
			}
			throw new ArgumentException("Argument 'interval' is not a valid value.");
		}

		public static long DateDiff(DateInterval intervalType, DateTime dateOne, DateTime dateTwo)
		{
			checked
			{
				switch (intervalType)
				{
					case DateInterval.Day:
					case DateInterval.DayOfYear:
						return (long)System.Math.Round((dateTwo - dateOne).TotalDays);
					case DateInterval.Hour:
						return (long)System.Math.Round((dateTwo - dateOne).TotalHours);
					case DateInterval.Minute:
						return (long)System.Math.Round((dateTwo - dateOne).TotalMinutes);
					case DateInterval.Month:
						return (dateTwo.Year - dateOne.Year) * 12 + (dateTwo.Month - dateOne.Month);
					case DateInterval.Quarter:
						{
							long dateOneQuarter = (long)System.Math.Ceiling((double)dateOne.Month / 3.0);
							long dateTwoQuarter = (long)System.Math.Ceiling((double)dateTwo.Month / 3.0);
							return 4 * (dateTwo.Year - dateOne.Year) + dateTwoQuarter - dateOneQuarter;
						}
					case DateInterval.Second:
						return (long)System.Math.Round((dateTwo - dateOne).TotalSeconds);
					case DateInterval.Weekday:
						return (long)System.Math.Round((dateTwo - dateOne).TotalDays / 7.0);
					case DateInterval.WeekOfYear:
						{
							DateTime dateOneModified = dateOne;
							DateTime dateTwoModified = dateTwo;
							while (dateTwoModified.DayOfWeek != DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
							{
								dateTwoModified = dateTwoModified.AddDays(-1.0);
							}
							while (dateOneModified.DayOfWeek != DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
							{
								dateOneModified = dateOneModified.AddDays(-1.0);
							}
							return (long)System.Math.Round((dateTwoModified - dateOneModified).TotalDays / 7.0);
						}
					case DateInterval.Year:
						return dateTwo.Year - dateOne.Year;
					default:
						return 0L;
				}
			}
		}

		public static bool IsDate(this string input)
		{
			if (!string.IsNullOrEmpty(input))
			{
				DateTime dt;
				return (DateTime.TryParse(input, out dt));
			}
			else
			{
				return false;
			}
		}

		public static int YearsDiff(DateTime start, DateTime end)
		{
			return (end.Year - start.Year - 1) +
				(((end.Month > start.Month) ||
				((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
		}

		//This is for whole months
		public static int MonthsDiff(DateTime startDate, DateTime endDate)
		{
			int monthsApart = 12 * (endDate.Year - startDate.Year) + endDate.Month - startDate.Month;
			if (endDate.Day < startDate.Day)
				monthsApart -= 1;

			return (monthsApart);
		}
	}
}
