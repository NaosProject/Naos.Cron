// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HourlySchedule.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using OBeautifulCode.Type;

    /// <summary>
    /// Hourly on specified minute version of the schedule (will repeat every hour on the specified minute).
    /// </summary>
    public partial class HourlySchedule : ScheduleBase, IModelViaCodeGen
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
                throw new ArgumentException(FormattableString.Invariant($"The minute of the hour cannot be less than 0.  It was {this.Minute}"));
            }

            if (this.Minute > 59)
            {
                throw new ArgumentException(FormattableString.Invariant($"The minute of the hour cannot be more than 59.  It was {this.Minute}"));
            }
        }
    }
}
