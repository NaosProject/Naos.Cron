// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScheduleSerializationTests.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Test
{
    using System;

    using Newtonsoft.Json;

    using Spritely.Recipes;

    using Xunit;

    public class ScheduleSerializationTests
    {
        static ScheduleSerializationTests()
        {
            JsonConvert.DefaultSettings = () => JsonConfiguration.DefaultSerializerSettings;
        }

        [Fact]
        public void NullScheduleRoundTripDeserializesIntoBaseClass()
        {
            var schedule = new NullSchedule();
            var json = JsonConvert.SerializeObject(schedule);
            var newScheduleBase = JsonConvert.DeserializeObject<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<NullSchedule>(newScheduleBase);
        }

        [Fact]
        public void ExpressionScheduleRoundTripDeserializesIntoBaseClass()
        {
            var schedule = new ExpressionSchedule { CronExpression = "* * * * *" };
            var json = JsonConvert.SerializeObject(schedule);
            var newScheduleBase = JsonConvert.DeserializeObject<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<ExpressionSchedule>(newScheduleBase);
            Assert.Equal(schedule.CronExpression, newScheduleTyped.CronExpression);
        }

        [Fact]
        public void IntervalNoIntervalScheduleRoundTripDeserializesIntoBaseClass()
        {
            var schedule = new IntervalSchedule();
            var json = JsonConvert.SerializeObject(schedule);
            var newScheduleBase = JsonConvert.DeserializeObject<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<IntervalSchedule>(newScheduleBase);
            Assert.Equal(schedule.Interval, newScheduleTyped.Interval);
        }

        [Fact]
        public void IntervalIntervalDefinedScheduleRoundTripDeserializesIntoBaseClass()
        {
            var schedule = new IntervalSchedule { Interval = TimeSpan.FromHours(2) };
            var json = JsonConvert.SerializeObject(schedule);
            var newScheduleBase = JsonConvert.DeserializeObject<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<IntervalSchedule>(newScheduleBase);
            Assert.Equal(schedule.Interval, newScheduleTyped.Interval);
        }

        [Fact]
        public void HourlyScheduleRoundTripDeserializesIntoBaseClass()
        {
            var schedule = new HourlySchedule { Minute = 35 };
            var json = JsonConvert.SerializeObject(schedule);
            var newScheduleBase = JsonConvert.DeserializeObject<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<HourlySchedule>(newScheduleBase);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
        }

        [Fact]
        public void DailyScheduleRoundTripDeserializesIntoBaseClass()
        {
            var schedule = new DailyScheduleInUtc { Hour = 3, Minute = 23 };
            var json = JsonConvert.SerializeObject(schedule);
            var newScheduleBase = JsonConvert.DeserializeObject<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<DailyScheduleInUtc>(newScheduleBase);
            Assert.Equal(schedule.Hour, newScheduleTyped.Hour);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
        }

        [Fact]
        public void WeeklyScheduleRoundTripDeserializesIntoBaseClass()
        {
            var schedule = new WeeklyScheduleInUtc { DaysOfWeek = new[] { DayOfWeek.Tuesday }, Hour = 4, Minute = 22 };
            var json = JsonConvert.SerializeObject(schedule);
            var newScheduleBase = JsonConvert.DeserializeObject<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<WeeklyScheduleInUtc>(newScheduleBase);
            Assert.Equal(schedule.DaysOfWeek, newScheduleTyped.DaysOfWeek);
            Assert.Equal(schedule.Hour, newScheduleTyped.Hour);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
        }

        [Fact]
        public void MontlyScheduleRoundTripDeserializesIntoBaseClass()
        {
            var schedule = new MonthlyScheduleInUtc { DaysOfMonth = new[] { 4 }, Hour = 4, Minute = 22 };
            var json = JsonConvert.SerializeObject(schedule);
            var newScheduleBase = JsonConvert.DeserializeObject<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<MonthlyScheduleInUtc>(newScheduleBase);
            Assert.Equal(schedule.DaysOfMonth, newScheduleTyped.DaysOfMonth);
            Assert.Equal(schedule.Hour, newScheduleTyped.Hour);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
        }

        [Fact]
        public void YearlyScheduleRoundTripDeserializesIntoBaseClass()
        {
            var schedule = new YearlyScheduleInUtc { MonthsOfYear = new[] { MonthOfYear.July }, DaysOfMonth = new[] { 6 }, Hour = 4, Minute = 22 };
            var json = JsonConvert.SerializeObject(schedule);
            var newScheduleBase = JsonConvert.DeserializeObject<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<YearlyScheduleInUtc>(newScheduleBase);
            Assert.Equal(schedule.MonthsOfYear, newScheduleTyped.MonthsOfYear);
            Assert.Equal(schedule.DaysOfMonth, newScheduleTyped.DaysOfMonth);
            Assert.Equal(schedule.Hour, newScheduleTyped.Hour);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
        }
    }
}
