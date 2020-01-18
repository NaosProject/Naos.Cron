// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntervalSchedule.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using OBeautifulCode.Type;

    /// <summary>
    /// Interval version of the schedule (will repeat every specified interval).
    /// </summary>
    public partial class IntervalSchedule : ScheduleBase, IModelViaCodeGen
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
    }
}
