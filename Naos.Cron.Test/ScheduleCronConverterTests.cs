// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScheduleCronConverterTests.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Test
{
    using System;
    using System.Linq;

    using Xunit;

    public class ScheduleCronConverterTests
    {
        [Fact]
        public void GetScheduleFromCronExpression_TooManyColumns_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() => ScheduleCronExpressionConverter.FromCronExpression("* * * * * *"));
            Assert.Equal("Incorrect number of values in the cron expression.\r\nParameter name: cronExpression", ex.Message);
        }

        [Fact]
        public void GetScheduleFromCronExpression_TooFewColumns_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() => ScheduleCronExpressionConverter.FromCronExpression("* * * *"));
            Assert.Equal("Incorrect number of values in the cron expression.\r\nParameter name: cronExpression", ex.Message);
        }

        [Fact]
        public void GetScheduleFromCronExpression_Null_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() => ScheduleCronExpressionConverter.FromCronExpression(null));
            Assert.Equal("Cannot have a null or empty cron expression.\r\nParameter name: cronExpression", ex.Message);
        }

        [Fact]
        public void GetScheduleFromCronExpression_Empty_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() => ScheduleCronExpressionConverter.FromCronExpression(string.Empty));
            Assert.Equal("Cannot have a null or empty cron expression.\r\nParameter name: cronExpression", ex.Message);
        }

        [Fact]
        public void GetScheduleFromCronExpression_UnsupportedExpression_Throws()
        {
            var expression = "a s d f g";
            var ex = Assert.Throws<NotSupportedException>(() => ScheduleCronExpressionConverter.FromCronExpression(expression));
            Assert.Equal("Expression is not supported for translation: " + expression, ex.Message);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_UnsupportedSchedule_Throws()
        {
            var schedule = new NullSchedule();
            var ex = Assert.Throws<NotSupportedException>(() => ScheduleCronExpressionConverter.ToCronExpression(schedule));
            Assert.Equal("Unsupported Schedule: " + schedule.GetType().AssemblyQualifiedName, ex.Message);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_NullClone_Works()
        {
            var schedule = new NullSchedule();
            var cloned = schedule.Clone();
            var clonedTyped = Assert.IsType<NullSchedule>(cloned);
            Assert.Equal(schedule.GetType(), clonedTyped.GetType());
            Assert.NotSame(schedule, clonedTyped);
        }

        [Fact]
        public void GetCronExpressionFromScheduleInterval_Expression_Works()
        {
            var schedule = new ExpressionSchedule { CronExpression = "*/2 * * * *" };
            var cron = ScheduleCronExpressionConverter.ToCronExpression(schedule);
            Assert.Equal("*/2 * * * *", cron);

            var newSchedule = ScheduleCronExpressionConverter.FromCronExpression(cron);
            var newScheduleTyped = Assert.IsType<IntervalSchedule>(newSchedule);
            Assert.Equal(typeof(IntervalSchedule), newScheduleTyped.GetType());
            Assert.Equal(2, newScheduleTyped.Interval.TotalMinutes);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_ExpressionClone_Works()
        {
            var schedule = new ExpressionSchedule { CronExpression = "* * * * *" };
            var cloned = schedule.Clone();
            var clonedTyped = Assert.IsType<ExpressionSchedule>(cloned);
            Assert.Equal(schedule.GetType(), clonedTyped.GetType());
            Assert.Equal(schedule.CronExpression, clonedTyped.CronExpression);
            Assert.NotSame(schedule, clonedTyped);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_Interval_Works()
        {
            var schedule = new IntervalSchedule { Interval = TimeSpan.FromMinutes(1) };
            var cron = ScheduleCronExpressionConverter.ToCronExpression(schedule);
            Assert.Equal("* * * * *", cron);

            var newSchedule = ScheduleCronExpressionConverter.FromCronExpression(cron);
            var newScheduleTyped = Assert.IsType<IntervalSchedule>(newSchedule);
            Assert.Equal(schedule.GetType(), newScheduleTyped.GetType());
            Assert.Equal(schedule.Interval, newScheduleTyped.Interval);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_Hourly_Works()
        {
            var schedule = new HourlySchedule { Minute = 7 };
            var cron = ScheduleCronExpressionConverter.ToCronExpression(schedule);
            Assert.Equal("7 * * * *", cron);

            var newSchedule = ScheduleCronExpressionConverter.FromCronExpression(cron);
            var newScheduleTyped = Assert.IsType<HourlySchedule>(newSchedule);
            Assert.Equal(schedule.GetType(), newScheduleTyped.GetType());
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_Daily_Works()
        {
            var schedule = new DailyScheduleInUtc { Hour = 22, Minute = 17 };
            var cron = ScheduleCronExpressionConverter.ToCronExpression(schedule);
            Assert.Equal("17 22 * * *", cron);

            var newSchedule = ScheduleCronExpressionConverter.FromCronExpression(cron);
            var newScheduleTyped = Assert.IsType<DailyScheduleInUtc>(newSchedule);
            Assert.Equal(schedule.GetType(), newScheduleTyped.GetType());
            Assert.Equal(schedule.Hour, newScheduleTyped.Hour);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_Weekly_Works()
        {
            var schedule = new WeeklyScheduleInUtc()
                               {
                                   DaysOfWeek = new[] { DayOfWeek.Thursday },
                                   Hour = 21,
                                   Minute = 17
                               };
            var cron = ScheduleCronExpressionConverter.ToCronExpression(schedule);
            Assert.Equal("17 21 * * 4", cron);

            var newSchedule = ScheduleCronExpressionConverter.FromCronExpression(cron);
            var newScheduleTyped = Assert.IsType<WeeklyScheduleInUtc>(newSchedule);
            Assert.Equal(schedule.GetType(), newScheduleTyped.GetType());
            Assert.Equal(schedule.DaysOfWeek.Single(), newScheduleTyped.DaysOfWeek.Single());
            Assert.Equal(schedule.Hour, newScheduleTyped.Hour);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_WeeklyMultipleDays_Works()
        {
            var schedule = new WeeklyScheduleInUtc()
                               {
                                   DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Thursday },
                                   Hour = 21,
                                   Minute = 17
                               };
            var cron = ScheduleCronExpressionConverter.ToCronExpression(schedule);
            Assert.Equal("17 21 * * 2,4", cron);

            var newSchedule = ScheduleCronExpressionConverter.FromCronExpression(cron);
            var newScheduleTyped = Assert.IsType<WeeklyScheduleInUtc>(newSchedule);
            Assert.Equal(schedule.GetType(), newScheduleTyped.GetType());
            Assert.Equal(schedule.DaysOfWeek.First(), newScheduleTyped.DaysOfWeek.First());
            Assert.Equal(schedule.DaysOfWeek.Skip(1).First(), newScheduleTyped.DaysOfWeek.Skip(1).First());
            Assert.Equal(schedule.Hour, newScheduleTyped.Hour);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_Monthly_Works()
        {
            var schedule = new MonthlyScheduleInUtc { DaysOfMonth = new[] { 20 }, Hour = 22, Minute = 48 };
            var cron = ScheduleCronExpressionConverter.ToCronExpression(schedule);
            Assert.Equal("48 22 20 * *", cron);

            var newSchedule = ScheduleCronExpressionConverter.FromCronExpression(cron);
            var newScheduleTyped = Assert.IsType<MonthlyScheduleInUtc>(newSchedule);
            Assert.Equal(schedule.GetType(), newScheduleTyped.GetType());
            Assert.Equal(schedule.DaysOfMonth.Single(), newScheduleTyped.DaysOfMonth.Single());
            Assert.Equal(schedule.Hour, newScheduleTyped.Hour);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_Yearly_Works()
        {
            var schedule = new YearlyScheduleInUtc
                               {
                                   MonthsOfYear = new[] { MonthOfYear.April },
                                   DaysOfMonth = new[] { 20 },
                                   Hour = 1,
                                   Minute = 38
                               };
            var cron = ScheduleCronExpressionConverter.ToCronExpression(schedule);
            Assert.Equal("38 1 20 4 *", cron);

            var newSchedule = ScheduleCronExpressionConverter.FromCronExpression(cron);
            var newScheduleTyped = Assert.IsType<YearlyScheduleInUtc>(newSchedule);
            Assert.Equal(schedule.GetType(), newScheduleTyped.GetType());
            Assert.Equal(schedule.MonthsOfYear.Single(), newScheduleTyped.MonthsOfYear.Single());
            Assert.Equal(schedule.DaysOfMonth.Single(), newScheduleTyped.DaysOfMonth.Single());
            Assert.Equal(schedule.Hour, newScheduleTyped.Hour);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
        }
    }
}
