﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScheduleValidTests.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Test
{
    using System;

    using Xunit;

    public static class ScheduleValidTests
    {
        [Fact]
        public static void IsValid_Valid_True()
        {
            var o = new NullSchedule();
            Assert.NotNull(o);
            Assert.True(o.IsValid());
        }

        [Fact]
        public static void IsValid_Invalid_False()
        {
            var o = new HourlySchedule { Minute = 100 };
            Assert.NotNull(o);
            Assert.False(o.IsValid());
        }

        [Fact]
        public static void NullSchedule_Valid()
        {
            var o = new NullSchedule();
            Assert.NotNull(o);
            o.ThrowIfInvalid();
        }

        [Fact]
        public static void IntervalScheduleZeroInterval_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new IntervalSchedule().ThrowIfInvalid());
            Assert.Equal("The interval must be specified.", ex.Message);
        }

        [Fact]
        public static void IntervalScheduleIntervalDefined_Valid()
        {
            var o = new IntervalSchedule { Interval = TimeSpan.FromHours(1) };
            Assert.NotNull(o);
            o.ThrowIfInvalid();
        }

        [Fact]
        public static void HourlySchedule_MinuteInRange_Valid()
        {
            var o = new HourlySchedule { Minute = 22 };
            Assert.NotNull(o);
            o.ThrowIfInvalid();
        }

        [Fact]
        public static void HourlySchedule_MinuteUnderZero_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new HourlySchedule { Minute = -1 }.ThrowIfInvalid());
            Assert.Equal("The minute of the hour cannot be less than 0.  It was -1", ex.Message);
        }

        [Fact]
        public static void HourlySchedule_MinuteSixtyOne_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new HourlySchedule { Minute = 60 }.ThrowIfInvalid());
            Assert.Equal("The minute of the hour cannot be more than 59.  It was 60", ex.Message);
        }

        [Fact]
        public static void DailySchedule_HourAndMinuteInRange_Valid()
        {
            var o = new DailyScheduleInUtc { Hour = 22, Minute = 23 };
            Assert.NotNull(o);
            o.ThrowIfInvalid();
        }

        [Fact]
        public static void DailySchedule_HourUnderZero_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new DailyScheduleInUtc { Hour = -1 }.ThrowIfInvalid());
            Assert.Equal("The hour of the day cannot be less than 0.  It was -1", ex.Message);
        }

        [Fact]
        public static void DailySchedule_HourTwentyFour_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new DailyScheduleInUtc { Hour = 24 }.ThrowIfInvalid());
            Assert.Equal("The hour of the day cannot be more than 23.  It was 24", ex.Message);
        }

        [Fact]
        public static void DailySchedule_MinuteUnderZero_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new DailyScheduleInUtc { Minute = -1 }.ThrowIfInvalid());
            Assert.Equal("The minute of the hour cannot be less than 0.  It was -1", ex.Message);
        }

        [Fact]
        public static void DailySchedule_MinuteSixtyOne_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new DailyScheduleInUtc { Minute = 60 }.ThrowIfInvalid());
            Assert.Equal("The minute of the hour cannot be more than 59.  It was 60", ex.Message);
        }

        [Fact]
        public static void WeeklySchedule_HourAndMinuteInRange_Valid()
        {
            var o = new WeeklyScheduleInUtc { Hour = 22, Minute = 23 };
            Assert.NotNull(o);
            o.ThrowIfInvalid();
        }

        [Fact]
        public static void WeeklySchedule_HourUnderZero_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new WeeklyScheduleInUtc { Hour = -1 }.ThrowIfInvalid());
            Assert.Equal("The hour of the day cannot be less than 0.  It was -1", ex.Message);
        }

        [Fact]
        public static void WeeklySchedule_HourTwentyFour_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new WeeklyScheduleInUtc { Hour = 24 }.ThrowIfInvalid());
            Assert.Equal("The hour of the day cannot be more than 23.  It was 24", ex.Message);
        }

        [Fact]
        public static void WeeklySchedule_MinuteUnderZero_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new WeeklyScheduleInUtc { Minute = -1 }.ThrowIfInvalid());
            Assert.Equal("The minute of the hour cannot be less than 0.  It was -1", ex.Message);
        }

        [Fact]
        public static void WeeklySchedule_MinuteSixtyOne_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new WeeklyScheduleInUtc { Minute = 60 }.ThrowIfInvalid());
            Assert.Equal("The minute of the hour cannot be more than 59.  It was 60", ex.Message);
        }

        [Fact]
        public static void MonthlySchedule_HourAndMinuteInRange_Valid()
        {
            var o = new MonthlyScheduleInUtc { Hour = 22, Minute = 23 };
            Assert.NotNull(o);
            o.ThrowIfInvalid();
        }

        [Fact]
        public static void MonthlySchedule_DayUnderZero_Invalid()
        {
            var ex =
                Assert.Throws<ArgumentException>(
                    () => new MonthlyScheduleInUtc { DaysOfMonth = new[] { -1 } }.ThrowIfInvalid());
            Assert.Equal("The day in the month cannot be less than 0.  It was -1", ex.Message);
        }

        [Fact]
        public static void MonthlySchedule_DayThirtyTwo_Invalid()
        {
            var ex =
                Assert.Throws<ArgumentException>(
                    () => new MonthlyScheduleInUtc { DaysOfMonth = new[] { 32 } }.ThrowIfInvalid());
            Assert.Equal("The day in the month cannot be more than 31.  It was 32", ex.Message);
        }

        [Fact]
        public static void MonthlySchedule_HourUnderZero_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new MonthlyScheduleInUtc { Hour = -1 }.ThrowIfInvalid());
            Assert.Equal("The hour of the day cannot be less than 0.  It was -1", ex.Message);
        }

        [Fact]
        public static void MonthlySchedule_HourTwentyFour_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new MonthlyScheduleInUtc { Hour = 24 }.ThrowIfInvalid());
            Assert.Equal("The hour of the day cannot be more than 23.  It was 24", ex.Message);
        }

        [Fact]
        public static void MonthlySchedule_MinuteUnderZero_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new MonthlyScheduleInUtc { Minute = -1 }.ThrowIfInvalid());
            Assert.Equal("The minute of the hour cannot be less than 0.  It was -1", ex.Message);
        }

        [Fact]
        public static void MonthlySchedule_MinuteSixtyOne_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new MonthlyScheduleInUtc { Minute = 60 }.ThrowIfInvalid());
            Assert.Equal("The minute of the hour cannot be more than 59.  It was 60", ex.Message);
        }

        [Fact]
        public static void YearlySchedule_HourAndMinuteInRange_Valid()
        {
            var o = new YearlyScheduleInUtc { Hour = 22, Minute = 23 };
            Assert.NotNull(o);
            o.ThrowIfInvalid();
        }

        [Fact]
        public static void YearlySchedule_DayUnderZero_Invalid()
        {
            var ex =
                Assert.Throws<ArgumentException>(
                    () => new YearlyScheduleInUtc { DaysOfMonth = new[] { -1 } }.ThrowIfInvalid());
            Assert.Equal("The day in the month cannot be less than 0.  It was -1", ex.Message);
        }

        [Fact]
        public static void YearlySchedule_DayThirtyTwo_Invalid()
        {
            var ex =
                Assert.Throws<ArgumentException>(
                    () => new YearlyScheduleInUtc { DaysOfMonth = new[] { 32 } }.ThrowIfInvalid());
            Assert.Equal("The day in the month cannot be more than 31.  It was 32", ex.Message);
        }

        [Fact]
        public static void YearlySchedule_HourUnderZero_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new YearlyScheduleInUtc { Hour = -1 }.ThrowIfInvalid());
            Assert.Equal("The hour of the day cannot be less than 0.  It was -1", ex.Message);
        }

        [Fact]
        public static void YearlySchedule_HourTwentyFour_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new YearlyScheduleInUtc { Hour = 24 }.ThrowIfInvalid());
            Assert.Equal("The hour of the day cannot be more than 23.  It was 24", ex.Message);
        }

        [Fact]
        public static void YearlySchedule_MinuteUnderZero_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new YearlyScheduleInUtc { Minute = -1 }.ThrowIfInvalid());
            Assert.Equal("The minute of the hour cannot be less than 0.  It was -1", ex.Message);
        }

        [Fact]
        public static void YearlySchedule_MinuteSixtyOne_Invalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => new YearlyScheduleInUtc { Minute = 60 }.ThrowIfInvalid());
            Assert.Equal("The minute of the hour cannot be more than 59.  It was 60", ex.Message);
        }
    }
}
