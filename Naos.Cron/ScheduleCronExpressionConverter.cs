// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScheduleCronExpressionConverter.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Methods to convert cron expressions to appropriate schedules and vice versa.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cron", Justification = "Spelling/name is correct.")]
    public static class ScheduleCronExpressionConverter
    {
        /// <summary>
        /// Converts a schedule into a cron expression.
        /// </summary>
        /// <param name="schedule">Schedule to convert to a cron expression.</param>
        /// <returns>A cron expression of the schedule.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cron", Justification = "Spelling/name is correct.")]
        public static string ToCronExpression(ScheduleBase schedule)
        {
            schedule.ThrowIfInvalid();

            var scheduleType = schedule.GetType();
            if (scheduleType == typeof(IntervalSchedule))
            {
                var intervalSchedule = (IntervalSchedule)schedule;
                if (intervalSchedule.Interval.TotalMinutes.Equals(1))
                {
                    return "* * * * *";
                }
                else
                {
                    return "*/" + intervalSchedule.Interval.TotalMinutes + " * * * *";
                }
            }
            else if (scheduleType == typeof(HourlySchedule))
            {
                var hourlySchedule = (HourlySchedule)schedule;
                return string.Format(CultureInfo.InvariantCulture, "{0} * * * *", hourlySchedule.Minute);
            }
            else if (scheduleType == typeof(DailyScheduleInUtc))
            {
                var dailySchedule = (DailyScheduleInUtc)schedule;
                return string.Format(CultureInfo.InvariantCulture, "{0} {1} * * *", dailySchedule.Minute, dailySchedule.Hour);
            }
            else if (scheduleType == typeof(WeeklyScheduleInUtc))
            {
                var weeklySchedule = (WeeklyScheduleInUtc)schedule;
                var daysOfWeekString = string.Join(",", weeklySchedule.DaysOfWeek.Select(_ => (int)_));
                return string.Format(CultureInfo.InvariantCulture, "{0} {1} * * {2}", weeklySchedule.Minute, weeklySchedule.Hour, daysOfWeekString);
            }
            else if (scheduleType == typeof(MonthlyScheduleInUtc))
            {
                var monthlySchedule = (MonthlyScheduleInUtc)schedule;
                var daysOfMonthString = string.Join(",", monthlySchedule.DaysOfMonth.Select(_ => (int)_));
                return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2} * *", monthlySchedule.Minute, monthlySchedule.Hour, daysOfMonthString);
            }
            else if (scheduleType == typeof(YearlyScheduleInUtc))
            {
                var yearlySchedule = (YearlyScheduleInUtc)schedule;
                var daysOfMonthString = string.Join(",", yearlySchedule.DaysOfMonth.Select(_ => (int)_));
                var monthsOfYearString = string.Join(",", yearlySchedule.MonthsOfYear.Select(_ => (int)_));
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "{0} {1} {2} {3} *",
                    yearlySchedule.Minute,
                    yearlySchedule.Hour,
                    daysOfMonthString,
                    monthsOfYearString);
            }
            else if (scheduleType == typeof(ExpressionSchedule))
            {
                var expressionSchedule = (ExpressionSchedule)schedule;
                return expressionSchedule.CronExpression;
            }

            throw new NotSupportedException("Unsupported Schedule: " + scheduleType.AssemblyQualifiedName);
        }

        /// <summary>
        /// Creates the appropriate ScheduleBase implementation from the specified cron expression.
        /// </summary>
        /// <param name="cronExpression">Cron expression to convert into a class.</param>
        /// <returns>Correct implementation of ScheduleBase to honor the expression.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "cron", Justification = "Spelling/name is correct.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cron", Justification = "Spelling/name is correct.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This will be re-written with Regex in the future.")]
        public static ScheduleBase FromCronExpression(string cronExpression)
        {
            if (string.IsNullOrEmpty(cronExpression))
            {
                throw new ArgumentException("Cannot have a null or empty cron expression.", "cronExpression");
            }

            var trimmedCronExpression = cronExpression.Trim();
            var cronExpressionItems = trimmedCronExpression.Split(' ');
            if (cronExpressionItems.Length != 5)
            {
                throw new ArgumentException("Incorrect number of values in the cron expression.", "cronExpression");
            }

            // probably should do this with regex but my regex is so bad...
            if (cronExpressionItems.All(_ => _ == "*"))
            {
                var ret = new IntervalSchedule { Interval = TimeSpan.FromMinutes(1) };
                return ret;
            }
            else if (cronExpressionItems.First().Contains("/"))
            {
                var minuteString = cronExpressionItems.First();
                var minuteItems = minuteString.Split('/');
                if (minuteItems[0] != "*")
                {
                    throw new ArgumentException("Invalid interval format, should be '*/[interval minutes]', not '" + minuteString + "'");
                }

                var intervalMinutesString = minuteItems[1];
                var intervalMinutesInt = int.Parse(intervalMinutesString, CultureInfo.InvariantCulture);
                var intervalMinutesTimeSpan = TimeSpan.FromMinutes(intervalMinutesInt);
                var ret = new IntervalSchedule { Interval = intervalMinutesTimeSpan };
                return ret;
            }
            else if (cronExpressionItems.First().IsNumeric() && cronExpressionItems.Skip(1).All(_ => _ == "*"))
            {
                var minuteString = cronExpressionItems.First();
                var minute = int.Parse(minuteString, CultureInfo.InvariantCulture);

                var ret = new HourlySchedule { Minute = minute };
                return ret;
            }
            else if (cronExpressionItems.First().IsNumeric() && cronExpressionItems.Second().IsNumeric() && cronExpressionItems.Skip(2).All(_ => _ == "*"))
            {
                var minuteString = cronExpressionItems.First();
                var minute = int.Parse(minuteString, CultureInfo.InvariantCulture);

                var hourString = cronExpressionItems.Second();
                var hour = int.Parse(hourString, CultureInfo.InvariantCulture);

                var ret = new DailyScheduleInUtc { Hour = hour, Minute = minute };
                return ret;
            }
            else if (
                cronExpressionItems.First().IsNumeric() &&
                cronExpressionItems.Second().IsNumeric() &&
                cronExpressionItems.Third() == "*" &&
                cronExpressionItems.Fourth() == "*" &&
                cronExpressionItems.Fifth().IsNumeric())
            {
                var minuteString = cronExpressionItems.First();
                var minute = int.Parse(minuteString, CultureInfo.InvariantCulture);

                var hourString = cronExpressionItems.Second();
                var hour = int.Parse(hourString, CultureInfo.InvariantCulture);

                var daysOfWeekString = cronExpressionItems.Fifth();
                var daysOfWeek = daysOfWeekString.Split(',').Select(int.Parse).Cast<DayOfWeek>().ToArray();

                var ret = new WeeklyScheduleInUtc { Hour = hour, Minute = minute, DaysOfWeek = daysOfWeek };
                return ret;
            }
            else if (
                cronExpressionItems.First().IsNumeric() &&
                cronExpressionItems.Second().IsNumeric() &&
                cronExpressionItems.Third().IsNumeric() &&
                cronExpressionItems.Fourth() == "*" &&
                cronExpressionItems.Fifth() == "*")
            {
                var minuteString = cronExpressionItems.First();
                var minute = int.Parse(minuteString, CultureInfo.InvariantCulture);

                var hourString = cronExpressionItems.Second();
                var hour = int.Parse(hourString, CultureInfo.InvariantCulture);

                var daysOfMonthString = cronExpressionItems.Third();
                var daysOfMonthInt = daysOfMonthString.Split(',').Select(int.Parse).ToArray();

                var ret = new MonthlyScheduleInUtc { Hour = hour, Minute = minute, DaysOfMonth = daysOfMonthInt };
                return ret;
            }
            else if (
                cronExpressionItems.First().IsNumeric() &&
                cronExpressionItems.Second().IsNumeric() &&
                cronExpressionItems.Third().IsNumeric() &&
                cronExpressionItems.Fourth().IsNumeric() &&
                cronExpressionItems.Fifth() == "*")
            {
                var minuteString = cronExpressionItems.First();
                var minute = int.Parse(minuteString, CultureInfo.InvariantCulture);

                var hourString = cronExpressionItems.Second();
                var hour = int.Parse(hourString, CultureInfo.InvariantCulture);

                var daysOfMonthString = cronExpressionItems.Third();
                var daysOfMonthInt = daysOfMonthString.Split(',').Select(int.Parse).ToArray();

                var monthsOfYearString = cronExpressionItems.Fourth();
                var monthsOfYear = monthsOfYearString.Split(',').Select(int.Parse).Cast<MonthOfYear>().ToArray();

                var ret = new YearlyScheduleInUtc { Hour = hour, Minute = minute, DaysOfMonth = daysOfMonthInt, MonthsOfYear = monthsOfYear };
                return ret;
            }
            else
            {
                throw new NotSupportedException("Expression is not supported for translation: " + cronExpression);
            }
        }

        private static bool IsNumeric(this string item)
        {
            var items = item.Split(',');
            foreach (var splitItem in items)
            {
                int test;
                var success = int.TryParse(splitItem, out test);
                if (!success)
                {
                    return false;
                }
            }

            return true;
        }

        private static string Second(this IEnumerable<string> items)
        {
            return items.Skip(1).First();
        }

        private static string Third(this IEnumerable<string> items)
        {
            return items.Skip(2).First();
        }

        private static string Fourth(this IEnumerable<string> items)
        {
            return items.Skip(3).First();
        }

        private static string Fifth(this IEnumerable<string> items)
        {
            return items.Skip(4).First();
        }
    }
}
