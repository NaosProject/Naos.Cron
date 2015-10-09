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
    using System.Text.RegularExpressions;

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
                return string.Format("{0} {1} * * {2}", weeklySchedule.Minute, weeklySchedule.Hour, (int)weeklySchedule.DayOfWeek);
            }
            else if (scheduleType == typeof(MonthlyScheduleInUtc))
            {
                var monthlySchedule = (MonthlyScheduleInUtc)schedule;
                return string.Format("{0} {1} {2} * *", monthlySchedule.Minute, monthlySchedule.Hour, monthlySchedule.DayInMonth);
            }
            else if (scheduleType == typeof(YearlyScheduleInUtc))
            {
                var yearlySchedule = (YearlyScheduleInUtc)schedule;
                return string.Format("{0} {1} {2} {3} *", yearlySchedule.Minute, yearlySchedule.Hour, yearlySchedule.DayInMonth, (int)yearlySchedule.MonthOfYear);
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

                var dayOfWeekString = cronExpressionItems.Fifth();
                var dayOfWeekInt = int.Parse(dayOfWeekString);
                var dayOfWeek = (DayOfWeek)dayOfWeekInt;

                var ret = new WeeklyScheduleInUtc { Hour = hour, Minute = minute, DayOfWeek = dayOfWeek };
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

                var dayOfMonthString = cronExpressionItems.Third();
                var dayOfMonthInt = int.Parse(dayOfMonthString);

                var ret = new MonthlyScheduleInUtc { Hour = hour, Minute = minute, DayInMonth = dayOfMonthInt };
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

                var dayOfMonthString = cronExpressionItems.Third();
                var dayOfMonthInt = int.Parse(dayOfMonthString);

                var monthInYearString = cronExpressionItems.Fourth();
                var monthInYearInt = int.Parse(monthInYearString);
                var monthInYear = (MonthOfYear)monthInYearInt;

                var ret = new YearlyScheduleInUtc { Hour = hour, Minute = minute, DayInMonth = dayOfMonthInt, MonthOfYear = monthInYear };
                return ret;
            }
            else
            {
                throw new NotSupportedException("Expression is not supported for translation: " + cronExpression);
            }
        }

        private static bool IsNumeric(this string item)
        {
            int test;
            var success = int.TryParse(item, out test);
            return success;
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

        /// <inheritdoc />
        public object Clone()
        {
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
        /// <summary>
        /// Gets or sets the day of the week (default is Sunday).
        /// </summary>
        public DayOfWeek DayOfWeek { get; set; }

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
        // this way the default will be 1 NOT an invalid 0...
        private int dayInMonth = 1;

        /// <summary>
        /// Gets or sets the day in the month (default is 1st).
        /// </summary>
        public int DayInMonth
        {
            get { return this.dayInMonth; }
            set { this.dayInMonth = value; }
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

            if (this.DayInMonth < 1)
            {
                throw new ArgumentException("The day in the month cannot be less than 0.  It was " + this.DayInMonth);
            }

            if (this.DayInMonth > 31)
            {
                throw new ArgumentException("The day in the month cannot be more than 31.  It was " + this.DayInMonth);
            }
        }
    }

    /// <summary>
    /// Yearly on a specified month and a specified day in the month at a specified UTC time version of the schedule (will repeat on a specified month and a specified day in the month at a specified UTC time every year).
    /// </summary>
    public class YearlyScheduleInUtc : ScheduleBase
    {
        // this way the default will be January NOT an invalid 0...
        private MonthOfYear monthOfYear = MonthOfYear.January;

        /// <summary>
        /// Gets or sets the month in the year (default is January).
        /// </summary>
        public MonthOfYear MonthOfYear
        {
            get { return this.monthOfYear; }
            set { this.monthOfYear = value; }
        }

        // this way the default will be 1 NOT an invalid 0...
        private int dayInMonth = 1;

        /// <summary>
        /// Gets or sets the day in the month (default is 1st).
        /// </summary>
        public int DayInMonth
        {
            get { return this.dayInMonth; }
            set { this.dayInMonth = value; }
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

            if (this.DayInMonth < 1)
            {
                throw new ArgumentException("The day in the month cannot be less than 0.  It was " + this.DayInMonth);
            }

            if (this.DayInMonth > 31)
            {
                throw new ArgumentException("The day in the month cannot be more than 31.  It was " + this.DayInMonth);
            }
        }
    }
}
