// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Naos.Cron.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using System.ComponentModel;

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
    /// Base class of the schedule for a recurring message sequence.
    /// </summary>
    [Bindable(BindableSupport.Default)]
    public abstract class ScheduleBase
    {
    }

    /// <summary>
    /// Null implementation of a schedule (can be passed in without any consequences).
    /// </summary>
    public class NullSchedule : ScheduleBase
    {
    }

    /// <summary>
    /// Minutely version of the schedule (will repeat every minute).
    /// </summary>
    public class MinutelySchedule : ScheduleBase
    {
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
    }

    /// <summary>
    /// Code to convert a schedule into a cron string.
    /// </summary>
    public class ScheduleCronConverter
    {
        /// <summary>
        /// Converts a schedule into a cron string.
        /// </summary>
        /// <param name="schedule">The schedule to convert.</param>
        /// <returns>A cron string of the schedule.</returns>
        public static string GetCronExpressionFromSchedule(ScheduleBase schedule)
        {
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
    }
}
