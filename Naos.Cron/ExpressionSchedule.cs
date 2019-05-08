// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionSchedule.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using OBeautifulCode.Math.Recipes;

    /// <summary>
    /// Expression implementation of a schedule (serialized easily and converted into class version at any time).
    /// </summary>
    public class ExpressionSchedule : ScheduleBase, IEquatable<ExpressionSchedule>
    {
        /// <summary>
        /// Gets or sets the cron expression.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cron", Justification = "Spelling/name is correct.")]
        public string CronExpression { get; set; }

        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            var objectVersion = ScheduleCronExpressionConverter.FromCronExpression(this.CronExpression);
            objectVersion.ThrowIfInvalid();
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are equal.</returns>
        public static bool operator ==(ExpressionSchedule first, ExpressionSchedule second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            return first.CronExpression == second.CronExpression;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are inequal.</returns>
        public static bool operator !=(ExpressionSchedule first, ExpressionSchedule second) => !(first == second);

        /// <inheritdoc />
        public bool Equals(ExpressionSchedule other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as ExpressionSchedule);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize().Hash(this.CronExpression).Value;
    }
}