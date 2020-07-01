﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.88.0)
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using global::System;
    using global::System.CodeDom.Compiler;
    using global::System.Collections.Concurrent;
    using global::System.Collections.Generic;
    using global::System.Collections.ObjectModel;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System.Globalization;
    using global::System.Linq;

    using global::OBeautifulCode.Equality.Recipes;
    using global::OBeautifulCode.Type;
    using global::OBeautifulCode.Type.Recipes;

    using static global::System.FormattableString;

    [Serializable]
    public partial class MonthlyScheduleInUtc : IModel<MonthlyScheduleInUtc>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="MonthlyScheduleInUtc"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(MonthlyScheduleInUtc left, MonthlyScheduleInUtc right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = left.Equals(right);

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="MonthlyScheduleInUtc"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(MonthlyScheduleInUtc left, MonthlyScheduleInUtc right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(MonthlyScheduleInUtc other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.DaysOfMonth.IsEqualTo(other.DaysOfMonth)
                      && this.Hour.IsEqualTo(other.Hour)
                      && this.Minute.IsEqualTo(other.Minute);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as MonthlyScheduleInUtc);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.DaysOfMonth)
            .Hash(this.Hour)
            .Hash(this.Minute)
            .Value;

        /// <inheritdoc />
        public new MonthlyScheduleInUtc DeepClone() => (MonthlyScheduleInUtc)this.DeepCloneInternal();

        /// <summary>
        /// Deep clones this object with a new <see cref="DaysOfMonth" />.
        /// </summary>
        /// <param name="daysOfMonth">The new <see cref="DaysOfMonth" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="MonthlyScheduleInUtc" /> using the specified <paramref name="daysOfMonth" /> for <see cref="DaysOfMonth" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002: DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public MonthlyScheduleInUtc DeepCloneWithDaysOfMonth(int[] daysOfMonth)
        {
            var result = new MonthlyScheduleInUtc
                             {
                                 DaysOfMonth = daysOfMonth,
                                 Hour        = this.Hour,
                                 Minute      = this.Minute,
                             };

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="Hour" />.
        /// </summary>
        /// <param name="hour">The new <see cref="Hour" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="MonthlyScheduleInUtc" /> using the specified <paramref name="hour" /> for <see cref="Hour" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002: DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public MonthlyScheduleInUtc DeepCloneWithHour(int hour)
        {
            var result = new MonthlyScheduleInUtc
                             {
                                 DaysOfMonth = this.DaysOfMonth?.Select(i => i).ToArray(),
                                 Hour        = hour,
                                 Minute      = this.Minute,
                             };

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="Minute" />.
        /// </summary>
        /// <param name="minute">The new <see cref="Minute" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="MonthlyScheduleInUtc" /> using the specified <paramref name="minute" /> for <see cref="Minute" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002: DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public MonthlyScheduleInUtc DeepCloneWithMinute(int minute)
        {
            var result = new MonthlyScheduleInUtc
                             {
                                 DaysOfMonth = this.DaysOfMonth?.Select(i => i).ToArray(),
                                 Hour        = this.Hour,
                                 Minute      = minute,
                             };

            return result;
        }

        /// <inheritdoc />
        protected override ScheduleBase DeepCloneInternal()
        {
            var result = new MonthlyScheduleInUtc
                             {
                                 DaysOfMonth = this.DaysOfMonth?.Select(i => i).ToArray(),
                                 Hour        = this.Hour,
                                 Minute      = this.Minute,
                             };

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override string ToString()
        {
            var result = Invariant($"Naos.Cron.MonthlyScheduleInUtc: DaysOfMonth = {this.DaysOfMonth?.ToString() ?? "<null>"}, Hour = {this.Hour.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, Minute = {this.Minute.ToString(CultureInfo.InvariantCulture) ?? "<null>"}.");

            return result;
        }
    }
}