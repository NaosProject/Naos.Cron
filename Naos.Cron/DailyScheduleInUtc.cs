// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DailyScheduleInUtc.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using OBeautifulCode.Type;

    /// <summary>
    /// Daily at a UTC time (hour/minute) version of the schedule (will repeat at the specified UTC time every day).
    /// </summary>
    public partial class DailyScheduleInUtc : ScheduleBase, IModelViaCodeGen
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
                throw new ArgumentException(FormattableString.Invariant($"The minute of the hour cannot be less than 0.  It was {this.Minute}"));
            }

            if (this.Minute > 59)
            {
                throw new ArgumentException(FormattableString.Invariant($"The minute of the hour cannot be more than 59.  It was {this.Minute}"));
            }

            if (this.Hour < 0)
            {
                throw new ArgumentException(FormattableString.Invariant($"The hour of the day cannot be less than 0.  It was {this.Hour}"));
            }

            if (this.Hour > 23)
            {
                throw new ArgumentException(FormattableString.Invariant($"The hour of the day cannot be more than 23.  It was {this.Hour}"));
            }
        }
    }
}
