// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullSchedule.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using OBeautifulCode.Equality.Recipes;

    /// <summary>
    /// Null implementation of a schedule (can be passed in without any consequences).
    /// </summary>
    public class NullSchedule : ScheduleBase, IEquatable<NullSchedule>
    {
        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            /* no-op - always valid */
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are equal.</returns>
        public static bool operator ==(NullSchedule first, NullSchedule second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are inequal.</returns>
        public static bool operator !=(NullSchedule first, NullSchedule second) => !(first == second);

        /// <inheritdoc />
        public bool Equals(NullSchedule other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as NullSchedule);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize().Hash(nameof(NullSchedule)).Value;
    }
}
