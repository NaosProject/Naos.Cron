// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Naos.Cron.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// The months in the year.
    /// </summary>
    public enum MonthOfYear
    {
        /// <summary>
        /// First month.
        /// </summary>
        January = 1,

        /// <summary>
        /// Second month.
        /// </summary>
        February = 2,

        /// <summary>
        /// Third month.
        /// </summary>
        March = 3,

        /// <summary>
        /// Fourth month.
        /// </summary>
        April = 4,

        /// <summary>
        /// Fifth month.
        /// </summary>
        May = 5,

        /// <summary>
        /// Sixth month.
        /// </summary>
        June = 6,

        /// <summary>
        /// Seventh month.
        /// </summary>
        July = 7,

        /// <summary>
        /// Eighth month.
        /// </summary>
        August = 8,

        /// <summary>
        /// Ninth month.
        /// </summary>
        September = 9,

        /// <summary>
        /// Tenth month.
        /// </summary>
        October = 10,

        /// <summary>
        /// Eleventh month.
        /// </summary>
        November = 11,

        /// <summary>
        /// Twelfth month.
        /// </summary>
        December = 12
    }

    /// <summary>
    /// Methods to convert cron expressions to appropriate schedules and vice versa.
    /// </summary>
    public static class ScheduleCronExpressionConverter
    {
        /// <summary>
        /// Converts a schedule into a cron expression.
        /// </summary>
        /// <param name="schedule">Schedule to convert to a cron expression.</param>
        /// <returns>A cron expression of the schedule.</returns>
        public static string ToCronExpression(ScheduleBase schedule)
        {
            schedule.ThrowIfInvalid();

            var scheduleType = schedule.GetType();
            if (scheduleType == typeof(MinutelySchedule))
            {
                return "* * * * *";
            }
            else if (scheduleType == typeof(HourlySchedule))
            {
                var hourlySchedule = (HourlySchedule)schedule;
                return string.Format("{0} * * * *", hourlySchedule.Minute);
            }
            else if (scheduleType == typeof(DailyScheduleInUtc))
            {
                var dailySchedule = (DailyScheduleInUtc)schedule;
                return string.Format("{0} {1} * * *", dailySchedule.Minute, dailySchedule.Hour);
            }
            else if (scheduleType == typeof(WeeklyScheduleInUtc))
            {
                var weeklySchedule = (WeeklyScheduleInUtc)schedule;
                var daysOfWeekString = string.Join(",", weeklySchedule.DaysOfWeek.Select(_ => (int)_));
                return string.Format("{0} {1} * * {2}", weeklySchedule.Minute, weeklySchedule.Hour, daysOfWeekString);
            }
            else if (scheduleType == typeof(MonthlyScheduleInUtc))
            {
                var monthlySchedule = (MonthlyScheduleInUtc)schedule;
                var daysOfMonthString = string.Join(",", monthlySchedule.DaysOfMonth.Select(_ => (int)_));
                return string.Format("{0} {1} {2} * *", monthlySchedule.Minute, monthlySchedule.Hour, daysOfMonthString);
            }
            else if (scheduleType == typeof(YearlyScheduleInUtc))
            {
                var yearlySchedule = (YearlyScheduleInUtc)schedule;
                var daysOfMonthString = string.Join(",", yearlySchedule.DaysOfMonth.Select(_ => (int)_));
                var monthsOfYearString = string.Join(",", yearlySchedule.MonthsOfYear.Select(_ => (int)_));
                return string.Format(
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
                var ret = new MinutelySchedule();
                return ret;
            }
            else if (cronExpressionItems.First().IsNumeric() && cronExpressionItems.Skip(1).All(_ => _ == "*"))
            {
                var minuteString = cronExpressionItems.First();
                var minute = int.Parse(minuteString);

                var ret = new HourlySchedule { Minute = minute };
                return ret;
            }
            else if (cronExpressionItems.First().IsNumeric() && cronExpressionItems.Second().IsNumeric() && cronExpressionItems.Skip(2).All(_ => _ == "*"))
            {
                var minuteString = cronExpressionItems.First();
                var minute = int.Parse(minuteString);

                var hourString = cronExpressionItems.Second();
                var hour = int.Parse(hourString);

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
                var minute = int.Parse(minuteString);

                var hourString = cronExpressionItems.Second();
                var hour = int.Parse(hourString);

                var daysOfWeekString = cronExpressionItems.Fifth();
                var daysOfWeek = daysOfWeekString.Split(',').Select(int.Parse).Cast<DayOfWeek>().ToList();

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
                var minute = int.Parse(minuteString);

                var hourString = cronExpressionItems.Second();
                var hour = int.Parse(hourString);

                var daysOfMonthString = cronExpressionItems.Third();
                var daysOfMonthInt = daysOfMonthString.Split(',').Select(int.Parse).ToList();

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
                var minute = int.Parse(minuteString);

                var hourString = cronExpressionItems.Second();
                var hour = int.Parse(hourString);

                var daysOfMonthString = cronExpressionItems.Third();
                var daysOfMonthInt = daysOfMonthString.Split(',').Select(int.Parse).ToList();

                var monthsOfYearString = cronExpressionItems.Fourth();
                var monthsOfYear = monthsOfYearString.Split(',').Select(int.Parse).Cast<MonthOfYear>().ToList();

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

    /// <summary>
    /// Base class of the schedule for a recurring message sequence.
    /// </summary>
    [Bindable(BindableSupport.Default)]
    public abstract class ScheduleBase : ICloneable
    {
        /// <summary>
        /// Checks to see if the schedule is valid (i.e. there aren't 61 minutes in an hour).
        /// </summary>
        /// <returns>True if valid and false if not.</returns>
        public bool IsValid()
        {
            try
            {
                this.ThrowIfInvalid();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks to make sure the construct is valid (i.e. there aren't 61 minutes in an hour).
        /// </summary>
        public abstract void ThrowIfInvalid();

        /// <summary>
        /// Generates a cron expression from the schedule object.
        /// </summary>
        /// <returns>Generated cron expression from object.</returns>
        public string ToCronExpression()
        {
            var ret = ScheduleCronExpressionConverter.ToCronExpression(this);
            return ret;
        }

        /// <inheritdoc />
        public object Clone()
        {
            // special case for expression and null...
            if (this.GetType() == typeof(NullSchedule))
            {
                return new NullSchedule();
            }

            if (this.GetType() == typeof(ExpressionSchedule))
            {
                var typed = (ExpressionSchedule)this;
                return new ExpressionSchedule { CronExpression = typed.CronExpression };
            }

            var expression = ScheduleCronExpressionConverter.ToCronExpression(this);
            var ret = ScheduleCronExpressionConverter.FromCronExpression(expression);
            return ret;
        }
    }

    /// <summary>
    /// Null implementation of a schedule (can be passed in without any consequences).
    /// </summary>
    public class NullSchedule : ScheduleBase
    {
        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            /* no-op - always valid */
        }
    }

    /// <summary>
    /// Expression implementation of a schedule (serialized easily and converted into class version at any time).
    /// </summary>
    public class ExpressionSchedule : ScheduleBase
    {
        /// <summary>
        /// Gets or sets the cron expression.
        /// </summary>
        public string CronExpression { get; set; }

        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            var objectVersion = ScheduleCronExpressionConverter.FromCronExpression(this.CronExpression);
            objectVersion.ThrowIfInvalid();
        }
    }

    /// <summary>
    /// Minutely version of the schedule (will repeat every minute).
    /// </summary>
    public class MinutelySchedule : ScheduleBase
    {
        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            /* no-op - always valid */
        }
    }

    /// <summary>
    /// Hourly on specified minute version of the schedule (will repeat every hour on the specified minute).
    /// </summary>
    public class HourlySchedule : ScheduleBase
    {
        /// <summary>
        /// Gets or sets the minute of the hour to run.
        /// </summary>
        public int Minute { get; set; }

        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            if (this.Minute < 0)
            {
                throw new ArgumentException("The minute of the hour cannot be less than 0.  It was " + this.Minute);
            }

            if (this.Minute > 59)
            {
                throw new ArgumentException("The minute of the hour cannot be more than 59.  It was " + this.Minute);
            }
        }
    }

    /// <summary>
    /// Daily at a UTC time (hour/minute) version of the schedule (will repeat at the specified UTC time every day).
    /// </summary>
    public class DailyScheduleInUtc : ScheduleBase
    {
        /// <summary>
        /// Gets or sets the UTC hour in the day (default is 0/midnight).
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// Gets or sets the minute of the hour to run.
        /// </summary>
        public int Minute { get; set; }

        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            if (this.Minute < 0)
            {
                throw new ArgumentException("The minute of the hour cannot be less than 0.  It was " + this.Minute);
            }

            if (this.Minute > 59)
            {
                throw new ArgumentException("The minute of the hour cannot be more than 59.  It was " + this.Minute);
            }

            if (this.Hour < 0)
            {
                throw new ArgumentException("The hour of the day cannot be less than 0.  It was " + this.Hour);
            }

            if (this.Hour > 23)
            {
                throw new ArgumentException("The hour of the day cannot be more than 23.  It was " + this.Hour);
            }
        }
    }

    /// <summary>
    /// On a specific day of week at a specific UTC time version of the schedule (will repeat on the specified day of week at the specified UTC time every week).
    /// </summary>
    public class WeeklyScheduleInUtc : ScheduleBase
    {
        // this way the default will be Sunday NOT an invalid empty...
        private IReadOnlyCollection<DayOfWeek> daysOfWeek = new List<DayOfWeek>(new[] { DayOfWeek.Sunday });

        /// <summary>
        /// Gets or sets the days of the week (default is Sunday).
        /// </summary>
        public IReadOnlyCollection<DayOfWeek> DaysOfWeek
        {
            get { return this.daysOfWeek; }
            set { this.daysOfWeek = value; }
        }

        /// <summary>
        /// Gets or sets the UTC hour in the day.
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// Gets or sets the minute of the hour to run.
        /// </summary>
        public int Minute { get; set; }

        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            if (this.Minute < 0)
            {
                throw new ArgumentException("The minute of the hour cannot be less than 0.  It was " + this.Minute);
            }

            if (this.Minute > 59)
            {
                throw new ArgumentException("The minute of the hour cannot be more than 59.  It was " + this.Minute);
            }

            if (this.Hour < 0)
            {
                throw new ArgumentException("The hour of the day cannot be less than 0.  It was " + this.Hour);
            }

            if (this.Hour > 23)
            {
                throw new ArgumentException("The hour of the day cannot be more than 23.  It was " + this.Hour);
            }
        }
    }

    /// <summary>
    /// Monthly on a day in the month at a specified UTC time version of the schedule (will repeat on the specified day of the month at the specified UTC time ever month).
    /// </summary>
    public class MonthlyScheduleInUtc : ScheduleBase
    {
        // this way the default will be 1 NOT an invalid empty...
        private IReadOnlyCollection<int> daysOfMonth = new List<int>(new[] { 1 });

        /// <summary>
        /// Gets or sets the day in the month (default is 1st).
        /// </summary>
        public IReadOnlyCollection<int> DaysOfMonth
        {
            get { return this.daysOfMonth; }
            set { this.daysOfMonth = value; }
        }

        /// <summary>
        /// Gets or sets the UTC hour in the day.
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// Gets or sets the minute of the hour to run.
        /// </summary>
        public int Minute { get; set; }

        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            if (this.Minute < 0)
            {
                throw new ArgumentException("The minute of the hour cannot be less than 0.  It was " + this.Minute);
            }

            if (this.Minute > 59)
            {
                throw new ArgumentException("The minute of the hour cannot be more than 59.  It was " + this.Minute);
            }

            if (this.Hour < 0)
            {
                throw new ArgumentException("The hour of the day cannot be less than 0.  It was " + this.Hour);
            }

            if (this.Hour > 23)
            {
                throw new ArgumentException("The hour of the day cannot be more than 23.  It was " + this.Hour);
            }

            foreach (var dayinMonth in this.DaysOfMonth)
            {
                if (dayinMonth < 1)
                {
                    throw new ArgumentException("The day in the month cannot be less than 0.  It was " + dayinMonth);
                }

                if (dayinMonth > 31)
                {
                    throw new ArgumentException("The day in the month cannot be more than 31.  It was " + dayinMonth);
                }
            }
        }
    }

    /// <summary>
    /// Yearly on a specified month and a specified day in the month at a specified UTC time version of the schedule (will repeat on a specified month and a specified day in the month at a specified UTC time every year).
    /// </summary>
    public class YearlyScheduleInUtc : ScheduleBase
    {
        // this way the default will be January NOT an invalid empty...
        private IReadOnlyCollection<MonthOfYear> monthsOfYear = new List<MonthOfYear>(new[] { MonthOfYear.January });

        /// <summary>
        /// Gets or sets the month in the year (default is January).
        /// </summary>
        public IReadOnlyCollection<MonthOfYear> MonthsOfYear
        {
            get { return this.monthsOfYear; }
            set { this.monthsOfYear = value; }
        }

        // this way the default will be 1 NOT an invalid empty...
        private IReadOnlyCollection<int> daysOfMonth = new List<int>(new[] { 1 });

        /// <summary>
        /// Gets or sets the day in the month (default is 1st).
        /// </summary>
        public IReadOnlyCollection<int> DaysOfMonth
        {
            get { return this.daysOfMonth; }
            set { this.daysOfMonth = value; }
        }

        /// <summary>
        /// Gets or sets the UTC hour in the day.
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// Gets or sets the minute of the hour to run.
        /// </summary>
        public int Minute { get; set; }

        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            if (this.Minute < 0)
            {
                throw new ArgumentException("The minute of the hour cannot be less than 0.  It was " + this.Minute);
            }

            if (this.Minute > 59)
            {
                throw new ArgumentException("The minute of the hour cannot be more than 59.  It was " + this.Minute);
            }

            if (this.Hour < 0)
            {
                throw new ArgumentException("The hour of the day cannot be less than 0.  It was " + this.Hour);
            }

            if (this.Hour > 23)
            {
                throw new ArgumentException("The hour of the day cannot be more than 23.  It was " + this.Hour);
            }

            foreach (var dayinMonth in this.DaysOfMonth)
            {
                if (dayinMonth < 1)
                {
                    throw new ArgumentException("The day in the month cannot be less than 0.  It was " + dayinMonth);
                }

                if (dayinMonth > 31)
                {
                    throw new ArgumentException("The day in the month cannot be more than 31.  It was " + dayinMonth);
                }
            }
        }
    }
}
