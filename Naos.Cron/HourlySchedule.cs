// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HourlySchedule.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using OBeautifulCode.Equality.Recipes;

    /// <summary>
    /// Hourly on specified minute version of the schedule (will repeat every hour on the specified minute).
    /// </summary>
    public class HourlySchedule : ScheduleBase, IEquatable<HourlySchedule>
    {
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
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are equal.</returns>
        public static bool operator ==(HourlySchedule first, HourlySchedule second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            return first.Minute == second.Minute;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are inequal.</returns>
        public static bool operator !=(HourlySchedule first, HourlySchedule second) => !(first == second);

        /// <inheritdoc />
        public bool Equals(HourlySchedule other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as HourlySchedule);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize().Hash(this.Minute).Value;
    }
}
