// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CronDummyFactory.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FakeItEasy;
    using Naos.Cron;
    using OBeautifulCode.AutoFakeItEasy;

    /// <summary>
    /// A dummy factory for Accounting Time types.
    /// </summary>
#if !NaosCronRecipesProject
    [System.Diagnostics.DebuggerStepThrough]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("Naos.Cron", "See package version number")]
#endif
#if !NaosCronSolution
    internal
#else
    public
#endif
    partial class CronDummyFactory : DefaultCronDummyFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CronDummyFactory"/> class.
        /// </summary>
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This is not excessively complex.  Dummy factories typically wire-up many types.")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This is not excessively complex.  Dummy factories typically wire-up many types.")]
        public CronDummyFactory()
        {
            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<ScheduleBase>();

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () => new SpecificDateTimeScheduleInUtc
                      {
                          SpecificDateTimeInUtc = A.Dummy<UtcDateTime>(),
                      });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = new DailyScheduleInUtc
                    {
                        Hour = A.Dummy<int>().ThatIsInRange(0, 23),
                        Minute = A.Dummy<int>().ThatIsInRange(0, 59),
                    };

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = new ExpressionSchedule
                    {
                        CronExpression = "*/" + A.Dummy<int>().ThatIsInRange(1, 4) + " * * * *",
                    };

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = new HourlySchedule
                    {
                        Minute = A.Dummy<int>().ThatIsInRange(0, 59),
                    };

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = new IntervalSchedule
                    {
                        Interval = new TimeSpan(A.Dummy<long>().ThatIsInRange(0, 10000))
                    };

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = new MonthlyScheduleInUtc
                    {
                        DaysOfMonth = new[]
                        {
                            A.Dummy<int>().ThatIsInRange(1, 28),
                            A.Dummy<int>().ThatIsInRange(1, 28),
                            A.Dummy<int>().ThatIsInRange(1, 28),
                            A.Dummy<int>().ThatIsInRange(1, 28),
                            A.Dummy<int>().ThatIsInRange(1, 28),
                        }.Distinct().ToArray(),
                        Hour = A.Dummy<int>().ThatIsInRange(0, 23),
                        Minute = A.Dummy<int>().ThatIsInRange(0, 59),
                    };

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = new WeeklyScheduleInUtc
                    {
                        DaysOfWeek = Some.Dummies<DayOfWeek>(3).ToArray(),
                        Hour = A.Dummy<int>().ThatIsInRange(0, 23),
                        Minute = A.Dummy<int>().ThatIsInRange(0, 59),
                    };

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = new YearlyScheduleInUtc
                    {
                        MonthsOfYear = Some.Dummies<MonthOfYear>(4).ToArray(),
                        DaysOfMonth = new[]
                        {
                            A.Dummy<int>().ThatIsInRange(1, 28),
                            A.Dummy<int>().ThatIsInRange(1, 28),
                            A.Dummy<int>().ThatIsInRange(1, 28),
                            A.Dummy<int>().ThatIsInRange(1, 28),
                            A.Dummy<int>().ThatIsInRange(1, 28),
                        }.Distinct().ToArray(),
                        Hour = A.Dummy<int>().ThatIsInRange(0, 23),
                        Minute = A.Dummy<int>().ThatIsInRange(0, 59),
                    };

                    return result;
                });
        }
    }
}
