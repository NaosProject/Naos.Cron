﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CronJsonSerializationConfiguration.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Serialization.Json
{
    using System;
    using System.Collections.Generic;
    using Naos.Cron;
    using OBeautifulCode.Serialization.Json;

    /// <summary>
    /// Implementation for the <see cref="Cron" /> domain.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cron", Justification = "Spelling/name is correct.")]
    public class CronJsonSerializationConfiguration : JsonSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<string> TypeToRegisterNamespacePrefixFilters => new[]
                                                                                               {
                                                                                                   typeof(ScheduleBase).Namespace,
                                                                                               };

        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForJson> TypesToRegisterForJson => new[]
        {
            typeof(ISchedule).ToTypeToRegisterForJson(),
            typeof(ScheduleBase).ToTypeToRegisterForJson(),
        };
    }
}
