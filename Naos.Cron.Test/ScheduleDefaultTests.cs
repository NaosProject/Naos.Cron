// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScheduleDefaultTests.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Test
{
    using System;
    using System.Linq;

    using Xunit;

    public static class ScheduleDefaultTests
    {
        [Fact]
        public static void WeeklyScheduleInUtc_DefaultDayOfWeek_IsSunday()
        {
            var o = new WeeklyScheduleInUtc();
            Assert.Equal(DayOfWeek.Sunday, o.DaysOfWeek.Single());
        }

        [Fact]
        public static void MonthlyScheduleInUtc_DefaultDay_IsOne()
        {
            var o = new MonthlyScheduleInUtc();
            Assert.Equal(1, o.DaysOfMonth.Single());
        }

        [Fact]
        public static void YearlyScheduleInUtc_DefaultDay_IsOne()
        {
            var o = new YearlyScheduleInUtc();
            Assert.Equal(1, o.DaysOfMonth.Single());
        }

        [Fact]
        public static void YearlyScheduleInUtc_DefaultYear_IsJanuary()
        {
            var o = new YearlyScheduleInUtc();
            Assert.Equal(MonthOfYear.January, o.MonthsOfYear.Single());
        }
    }
}
