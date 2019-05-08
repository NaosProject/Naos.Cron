// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntervalSchedule.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using OBeautifulCode.Math.Recipes;

    /// <summary>
    /// Interval version of the schedule (will repeat every specified interval).
    /// </summary>
    public class IntervalSchedule : ScheduleBase, IEquatable<IntervalSchedule>
    {
        /// <summary>
        /// Gets or sets the interval to repeat.
        /// </summary>
        public TimeSpan Interval { get; set; }

        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            if (this.Interval == default(TimeSpan) || this.Interval == TimeSpan.Zero)
            {
                throw new ArgumentException("The interval must be specified.");
            }
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are equal.</returns>
        public static bool operator ==(IntervalSchedule first, IntervalSchedule second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            return first.Interval == second.Interval;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are inequal.</returns>
        public static bool operator !=(IntervalSchedule first, IntervalSchedule second) => !(first == second);

        /// <inheritdoc />
        public bool Equals(IntervalSchedule other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as IntervalSchedule);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize().Hash(this.Interval).Value;
    }
}