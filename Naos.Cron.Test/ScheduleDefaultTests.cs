﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScheduleDefaultTests.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Test
{
    using System;

    using Xunit;

    public class ScheduleDefaultTests
    {
        [Fact]
        public void WeeklyScheduleInUtc_DefaultDayOfWeek_IsSunday()
        {
            var o = new WeeklyScheduleInUtc();
            Assert.Equal(DayOfWeek.Sunday, o.DayOfWeek);
        }

        [Fact]
        public void MonthlyScheduleInUtc_DefaultDay_IsOne()
        {
            var o = new MonthlyScheduleInUtc();
            Assert.Equal(1, o.DayInMonth);
        }

        [Fact]
        public void YearlyScheduleInUtc_DefaultDay_IsOne()
        {
            var o = new YearlyScheduleInUtc();
            Assert.Equal(1, o.DayInMonth);
        }

        [Fact]
        public void YearlyScheduleInUtc_DefaultYear_IsJanuary()
        {
            var o = new YearlyScheduleInUtc();
            Assert.Equal(MonthOfYear.January, o.MonthOfYear);
        }
    }
}