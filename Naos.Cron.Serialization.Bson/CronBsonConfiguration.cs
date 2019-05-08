// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CronBsonConfiguration.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Serialization.Bson
{
    using System;
    using System.Collections.Generic;
    using Naos.Serialization.Bson;

    /// <summary>
    /// Implementation for the <see cref="Cron" /> domain.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cron", Justification = "Spelling/name is correct.")]
    public class CronBsonConfiguration : BsonConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<Type> TypesToAutoRegister => new[]
        {
            typeof(ScheduleBase),
        };
    }
}
