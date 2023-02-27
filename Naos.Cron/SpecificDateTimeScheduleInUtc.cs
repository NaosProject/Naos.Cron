// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecificDateTimeScheduleInUtc.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using OBeautifulCode.Type;

    /// <summary>
    /// Once at a specific UTC time version of the schedule (will not repeat).
    /// </summary>
    public partial class SpecificDateTimeScheduleInUtc : ScheduleBase, IModelViaCodeGen
    {
        /// <summary>
        /// Gets or sets the specific date time in UTC format.
        /// </summary>
        public DateTime SpecificDateTimeInUtc { get; set; }

        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            if (this.SpecificDateTimeInUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException(FormattableString.Invariant($"{nameof(this.SpecificDateTimeInUtc)} must be in UTC format."));
            }
        }
    }
}
