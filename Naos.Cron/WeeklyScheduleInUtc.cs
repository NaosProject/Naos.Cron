// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeeklyScheduleInUtc.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using System.Linq;
    using OBeautifulCode.Type;

    /// <summary>
    /// On a specific day of week at a specific UTC time version of the schedule (will repeat on the specified day of week at the specified UTC time every week).
    /// </summary>
    public partial class WeeklyScheduleInUtc : ScheduleBase, IModelViaCodeGen
    {
        // this way the default will be Sunday NOT an invalid empty... (MUST be an array for serialization to properly overrite if specified)
        private DayOfWeek[] daysOfWeek = new[] { DayOfWeek.Sunday };

        /// <summary>
        /// Gets or sets the days of the week (default is Sunday).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Keeping this way for specific initialization.")]
        public DayOfWeek[] DaysOfWeek
        {
            get { return this.daysOfWeek; }
            set { this.daysOfWeek = value; }
        }

        /// <summary>
        /// Gets or sets the UTC hour in the day.
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
                throw new ArgumentException("The minute of the hour cannot be less than 0.  It was " + this.Minute);
            }

            if (this.Minute > 59)
            {
                throw new ArgumentException("The minute of the hour cannot be more than 59.  It was " + this.Minute);
            }

            if (this.Hour < 0)
            {
                throw new ArgumentException("The hour of the day cannot be less than 0.  It was " + this.Hour);
            }

            if (this.Hour > 23)
            {
                throw new ArgumentException("The hour of the day cannot be more than 23.  It was " + this.Hour);
            }
        }
    }
}
