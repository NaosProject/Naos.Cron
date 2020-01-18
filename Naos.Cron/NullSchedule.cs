// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullSchedule.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using OBeautifulCode.Type;

    /// <summary>
    /// Null implementation of a schedule (can be passed in without any consequences).
    /// </summary>
    public partial class NullSchedule : ScheduleBase, IModelViaCodeGen
    {
        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            /* no-op - always valid */
        }
    }
}
