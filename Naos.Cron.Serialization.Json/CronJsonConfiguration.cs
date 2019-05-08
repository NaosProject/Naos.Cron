// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CronJsonConfiguration.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Serialization.Json
{
    using System;
    using System.Collections.Generic;
    using Naos.Cron;
    using Naos.Serialization.Json;

    /// <summary>
    /// Implementation for the <see cref="Cron" /> domain.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cron", Justification = "Spelling/name is correct.")]
    public class CronJsonConfiguration : JsonConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<Type> TypesToAutoRegister => new[]
        {
            typeof(ScheduleBase),
        };
    }
}
