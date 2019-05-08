// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DailyScheduleInUtc.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using OBeautifulCode.Math.Recipes;

    /// <summary>
    /// Daily at a UTC time (hour/minute) version of the schedule (will repeat at the specified UTC time every day).
    /// </summary>
    public class DailyScheduleInUtc : ScheduleBase, IEquatable<DailyScheduleInUtc>
    {
        /// <summary>
        /// Gets or sets the UTC hour in the day (default is 0/midnight).
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
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are equal.</returns>
        public static bool operator ==(DailyScheduleInUtc first, DailyScheduleInUtc second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            return first.Hour == second.Hour && first.Minute == second.Minute;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are inequal.</returns>
        public static bool operator !=(DailyScheduleInUtc first, DailyScheduleInUtc second) => !(first == second);

        /// <inheritdoc />
        public bool Equals(DailyScheduleInUtc other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as DailyScheduleInUtc);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize().Hash(this.Hour).Hash(this.Minute).Value;
    }
}