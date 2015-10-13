// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScheduleSerializationTests.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Test
{
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
        public void ExpressionScheduleDeserializesIntoBaseClass()
        {
            var schedule = new ExpressionSchedule { CronExpression = "* * * * *" };
            var json = JsonConvert.SerializeObject(schedule);
            var newScheduleBase = JsonConvert.DeserializeObject<ScheduleBase>(json);
            var newScheduleTyped = Assert.IsType<ExpressionSchedule>(newScheduleBase);
            Assert.Equal(schedule.CronExpression, newScheduleTyped.CronExpression);
        }
    }
}
