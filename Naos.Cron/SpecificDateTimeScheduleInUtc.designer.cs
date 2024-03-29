﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.178.0)
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

    using global::OBeautifulCode.Cloning.Recipes;
    using global::OBeautifulCode.Equality.Recipes;
    using global::OBeautifulCode.Type;
    using global::OBeautifulCode.Type.Recipes;

    using static global::System.FormattableString;

    [Serializable]
    public partial class SpecificDateTimeScheduleInUtc : IModel<SpecificDateTimeScheduleInUtc>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="SpecificDateTimeScheduleInUtc"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(SpecificDateTimeScheduleInUtc left, SpecificDateTimeScheduleInUtc right)
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
        /// Determines whether two objects of type <see cref="SpecificDateTimeScheduleInUtc"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(SpecificDateTimeScheduleInUtc left, SpecificDateTimeScheduleInUtc right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(SpecificDateTimeScheduleInUtc other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.SpecificDateTimeInUtc.IsEqualTo(other.SpecificDateTimeInUtc);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as SpecificDateTimeScheduleInUtc);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.SpecificDateTimeInUtc)
            .Value;

        /// <inheritdoc />
        public new SpecificDateTimeScheduleInUtc DeepClone() => (SpecificDateTimeScheduleInUtc)this.DeepCloneInternal();

        /// <summary>
        /// Deep clones this object with a new <see cref="SpecificDateTimeInUtc" />.
        /// </summary>
        /// <param name="specificDateTimeInUtc">The new <see cref="SpecificDateTimeInUtc" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="SpecificDateTimeScheduleInUtc" /> using the specified <paramref name="specificDateTimeInUtc" /> for <see cref="SpecificDateTimeInUtc" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
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
        public SpecificDateTimeScheduleInUtc DeepCloneWithSpecificDateTimeInUtc(DateTime specificDateTimeInUtc)
        {
            var result = new SpecificDateTimeScheduleInUtc
                             {
                                 SpecificDateTimeInUtc = specificDateTimeInUtc,
                             };

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override ScheduleBase DeepCloneInternal()
        {
            var result = new SpecificDateTimeScheduleInUtc
                             {
                                 SpecificDateTimeInUtc = this.SpecificDateTimeInUtc.DeepClone(),
                             };

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override string ToString()
        {
            var result = Invariant($"Naos.Cron.SpecificDateTimeScheduleInUtc: SpecificDateTimeInUtc = {this.SpecificDateTimeInUtc.ToString(CultureInfo.InvariantCulture) ?? "<null>"}.");

            return result;
        }
    }
}