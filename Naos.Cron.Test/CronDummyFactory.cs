// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CronDummyFactory.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Recipes
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
    public class CronDummyFactory : IDummyFactory
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
                        CronExpression = "* * * * *",
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
                    var result = new IntervalSchedule()
                    {
                        Interval = new TimeSpan(A.Dummy<long>().ThatIsInRange(0, 10000))
                    };

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = new MonthlyScheduleInUtc()
                    {
                        DaysOfMonth = new[]
                        {
                            A.Dummy<int>().ThatIsInRange(0, 28),
                            A.Dummy<int>().ThatIsInRange(0, 28),
                            A.Dummy<int>().ThatIsInRange(0, 28),
                            A.Dummy<int>().ThatIsInRange(0, 28),
                            A.Dummy<int>().ThatIsInRange(0, 28),
                        }.Distinct().ToArray(),
                        Hour = A.Dummy<int>().ThatIsInRange(0, 23),
                        Minute = A.Dummy<int>().ThatIsInRange(0, 59),
                    };

                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var result = new WeeklyScheduleInUtc()
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
                    var result = new YearlyScheduleInUtc()
                    {
                        MonthsOfYear = Some.Dummies<MonthOfYear>(4).ToArray(),
                        DaysOfMonth = new[]
                        {
                            A.Dummy<int>().ThatIsInRange(0, 28),
                            A.Dummy<int>().ThatIsInRange(0, 28),
                            A.Dummy<int>().ThatIsInRange(0, 28),
                            A.Dummy<int>().ThatIsInRange(0, 28),
                            A.Dummy<int>().ThatIsInRange(0, 28),
                        }.Distinct().ToArray(),
                        Hour = A.Dummy<int>().ThatIsInRange(0, 23),
                        Minute = A.Dummy<int>().ThatIsInRange(0, 59),
                    };

                    return result;
                });
        }

        /// <inheritdoc />
        public Priority Priority => new FakeItEasy.Priority(1);

        /// <inheritdoc />
        public bool CanCreate(Type type)
        {
            return false;
        }

        /// <inheritdoc />
        public object Create(Type type)
        {
            return null;
        }
    }
}
