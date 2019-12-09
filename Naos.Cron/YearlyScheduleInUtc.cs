// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YearlyScheduleInUtc.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using System.Linq;
    using OBeautifulCode.Equality.Recipes;

    /// <summary>
    /// Yearly on a specified month and a specified day in the month at a specified UTC time version of the schedule (will repeat on a specified month and a specified day in the month at a specified UTC time every year).
    /// </summary>
    public class YearlyScheduleInUtc : ScheduleBase, IEquatable<YearlyScheduleInUtc>
    {
        // this way the default will be January NOT an invalid empty... (MUST be an array for serialization to properly overwrite if specified)
        private MonthOfYear[] monthsOfYear = new[] { MonthOfYear.January };

        /// <summary>
        /// Gets or sets the month in the year (default is January).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Keeping this way for specific initialization.")]
        public MonthOfYear[] MonthsOfYear
        {
            get { return this.monthsOfYear; }
            set { this.monthsOfYear = value; }
        }

        // this way the default will be 1 NOT an invalid empty... (MUST be an array for serialization to properly overwrite if specified)
        private int[] daysOfMonth = new[] { 1 };

        /// <summary>
        /// Gets or sets the day in the month (default is 1st).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Keeping this way for specific initialization.")]
        public int[] DaysOfMonth
        {
            get { return this.daysOfMonth; }
            set { this.daysOfMonth = value; }
        }

        /// <summary>
        /// Gets or sets the UTC hour in the day.
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// Gets or sets the minute of the hour to run.
        /// </summary>
        public int Minute { get; set; }

        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            if (this.Minute < 0)
            {
                throw new ArgumentException("The minute of the hour cannot be less than 0.  It was " + this.Minute);
            }

            if (this.Minute > 59)
            {
                throw new ArgumentException("The minute of the hour cannot be more than 59.  It was " + this.Minute);
            }

            if (this.Hour < 0)
            {
                throw new ArgumentException("The hour of the day cannot be less than 0.  It was " + this.Hour);
            }

            if (this.Hour > 23)
            {
                throw new ArgumentException("The hour of the day cannot be more than 23.  It was " + this.Hour);
            }

            foreach (var dayinMonth in this.DaysOfMonth)
            {
                if (dayinMonth < 1)
                {
                    throw new ArgumentException("The day in the month cannot be less than 0.  It was " + dayinMonth);
                }

                if (dayinMonth > 31)
                {
                    throw new ArgumentException("The day in the month cannot be more than 31.  It was " + dayinMonth);
                }
            }
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are equal.</returns>
        public static bool operator ==(YearlyScheduleInUtc first, YearlyScheduleInUtc second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            return (first.MonthsOfYear ?? new MonthOfYear[0]).SequenceEqual(second.MonthsOfYear) && (first.DaysOfMonth ?? new int[0]).SequenceEqual(second.DaysOfMonth) && first.Hour == second.Hour && first.Minute == second.Minute;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are inequal.</returns>
        public static bool operator !=(YearlyScheduleInUtc first, YearlyScheduleInUtc second) => !(first == second);

        /// <inheritdoc />
        public bool Equals(YearlyScheduleInUtc other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as YearlyScheduleInUtc);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize().Hash(this.MonthsOfYear).Hash(this.DaysOfMonth).Hash(this.Hour).Hash(this.Minute).Value;
    }
}
