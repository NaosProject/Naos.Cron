// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScheduleJsonSerializationTests.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Test
{
    using System;
    using Naos.Cron.Serialization.Json;
    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.Json;
    using Xunit;

    public class ScheduleJsonSerializationTests
    {
        private readonly ISerializeAndDeserialize jsonSerializer = new ObcJsonSerializer<CronJsonSerializationConfiguration>();

        [Fact]
        public void NullScheduleRoundtripWillDeserializeIntoBaseClass()
        {
            var schedule = new NullSchedule();
            var json = this.jsonSerializer.SerializeToString(schedule);
            var newScheduleBase = this.jsonSerializer.Deserialize<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<NullSchedule>(newScheduleBase);
            Assert.Equal(schedule, newScheduleTyped);
        }

        [Fact]
        public void ExpressionScheduleRoundtripWillDeserializeIntoBaseClass()
        {
            var schedule = new ExpressionSchedule { CronExpression = "* * * * *" };
            var json = this.jsonSerializer.SerializeToString(schedule);
            var newScheduleBase = this.jsonSerializer.Deserialize<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<ExpressionSchedule>(newScheduleBase);
            Assert.Equal(schedule.CronExpression, newScheduleTyped.CronExpression);
            Assert.Equal(schedule, newScheduleTyped);
        }

        [Fact]
        public void IntervalNoIntervalScheduleRoundtripWillDeserializeIntoBaseClass()
        {
            var schedule = new IntervalSchedule();
            var json = this.jsonSerializer.SerializeToString(schedule);
            var newScheduleBase = this.jsonSerializer.Deserialize<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<IntervalSchedule>(newScheduleBase);
            Assert.Equal(schedule.Interval, newScheduleTyped.Interval);
            Assert.Equal(schedule, newScheduleTyped);
        }

        [Fact]
        public void IntervalIntervalDefinedScheduleRoundtripWillDeserializeIntoBaseClass()
        {
            var schedule = new IntervalSchedule { Interval = TimeSpan.FromHours(2) };
            var json = this.jsonSerializer.SerializeToString(schedule);
            var newScheduleBase = this.jsonSerializer.Deserialize<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<IntervalSchedule>(newScheduleBase);
            Assert.Equal(schedule.Interval, newScheduleTyped.Interval);
            Assert.Equal(schedule, newScheduleTyped);
        }

        [Fact]
        public void HourlyScheduleRoundtripWillDeserializeIntoBaseClass()
        {
            var schedule = new HourlySchedule { Minute = 35 };
            var json = this.jsonSerializer.SerializeToString(schedule);
            var newScheduleBase = this.jsonSerializer.Deserialize<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<HourlySchedule>(newScheduleBase);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
            Assert.Equal(schedule, newScheduleTyped);
        }

        [Fact]
        public void DailyScheduleRoundtripWillDeserializeIntoBaseClass()
        {
            var schedule = new DailyScheduleInUtc { Hour = 3, Minute = 23 };
            var json = this.jsonSerializer.SerializeToString(schedule);
            var newScheduleBase = this.jsonSerializer.Deserialize<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<DailyScheduleInUtc>(newScheduleBase);
            Assert.Equal(schedule.Hour, newScheduleTyped.Hour);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
            Assert.Equal(schedule, newScheduleTyped);
        }

        [Fact]
        public void WeeklyScheduleRoundtripWillDeserializeIntoBaseClass()
        {
            var schedule = new WeeklyScheduleInUtc { DaysOfWeek = new[] { DayOfWeek.Tuesday }, Hour = 4, Minute = 22 };
            var json = this.jsonSerializer.SerializeToString(schedule);
            var newScheduleBase = this.jsonSerializer.Deserialize<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<WeeklyScheduleInUtc>(newScheduleBase);
            Assert.Equal(schedule.DaysOfWeek, newScheduleTyped.DaysOfWeek);
            Assert.Equal(schedule.Hour, newScheduleTyped.Hour);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
            Assert.Equal(schedule, newScheduleTyped);
        }

        [Fact]
        public void MonthlyScheduleRoundtripWillDeserializeIntoBaseClass()
        {
            var schedule = new MonthlyScheduleInUtc { DaysOfMonth = new[] { 4 }, Hour = 4, Minute = 22 };
            var json = this.jsonSerializer.SerializeToString(schedule);
            var newScheduleBase = this.jsonSerializer.Deserialize<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<MonthlyScheduleInUtc>(newScheduleBase);
            Assert.Equal(schedule.DaysOfMonth, newScheduleTyped.DaysOfMonth);
            Assert.Equal(schedule.Hour, newScheduleTyped.Hour);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
            Assert.Equal(schedule, newScheduleTyped);
        }

        [Fact]
        public void YearlyScheduleRoundtripWillDeserializeIntoBaseClass()
        {
            var schedule = new YearlyScheduleInUtc { MonthsOfYear = new[] { MonthOfYear.July }, DaysOfMonth = new[] { 6 }, Hour = 4, Minute = 22 };
            var json = this.jsonSerializer.SerializeToString(schedule);
            var newScheduleBase = this.jsonSerializer.Deserialize<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<YearlyScheduleInUtc>(newScheduleBase);
            Assert.Equal(schedule.MonthsOfYear, newScheduleTyped.MonthsOfYear);
            Assert.Equal(schedule.DaysOfMonth, newScheduleTyped.DaysOfMonth);
            Assert.Equal(schedule.Hour, newScheduleTyped.Hour);
            Assert.Equal(schedule.Minute, newScheduleTyped.Minute);
            Assert.Equal(schedule, newScheduleTyped);
        }
    }
}
