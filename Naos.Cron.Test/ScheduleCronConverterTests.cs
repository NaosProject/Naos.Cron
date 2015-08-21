// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScheduleCronConverterTests.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Test
{
    using System;

    using Xunit;

    public class ScheduleCronConverterTests
    {
        [Fact]
        public void GetCronExpressionFromSchedule_UnsupportedSchedule_Throws()
        {
            var schedule = new NullSchedule();
            var ex = Assert.Throws<NotSupportedException>(() => schedule.ToCronExpression());
            Assert.Equal("Unsupported Schedule: " + schedule.GetType().AssemblyQualifiedName, ex.Message);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_Minutely_Works()
        {
            var schedule = new MinutelySchedule();
            var cron = schedule.ToCronExpression();
            Assert.Equal("* * * * *", cron);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_Hourly_Works()
        {
            var schedule = new HourlySchedule { Minute = 7 };
            var cron = schedule.ToCronExpression();
            Assert.Equal("7 * * * *", cron);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_Daily_Works()
        {
            var schedule = new DailyScheduleInUtc { Hour = 22, Minute = 17 };
            var cron = schedule.ToCronExpression();
            Assert.Equal("17 22 * * *", cron);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_Weekly_Works()
        {
            var schedule = new WeeklyScheduleInUtc() { DayOfWeek = DayOfWeek.Thursday, Hour = 21, Minute = 17 };
            var cron = schedule.ToCronExpression();
            Assert.Equal("17 21 * * 4", cron);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_Monthly_Works()
        {
            var schedule = new MonthlyScheduleInUtc { DayInMonth = 20, Hour = 22, Minute = 48 };
            var cron = schedule.ToCronExpression();
            Assert.Equal("48 22 20 * *", cron);
        }

        [Fact]
        public void GetCronExpressionFromSchedule_Yearly_Works()
        {
            var schedule = new YearlyScheduleInUtc { MonthOfYear = MonthOfYear.April, DayInMonth = 20, Hour = 1, Minute = 38 };
            var cron = schedule.ToCronExpression();
            Assert.Equal("38 1 20 4 *", cron);
        }
    }
}
