// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CronBsonSerializationConfiguration.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Serialization.Bson
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Serialization.Bson;

    /// <summary>
    /// Implementation for the <see cref="Cron" /> domain.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cron", Justification = "Spelling/name is correct.")]
    public class CronBsonSerializationConfiguration : BsonSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<string> TypeToRegisterNamespacePrefixFilters => new[]
                                                                                               {
                                                                                                   typeof(ScheduleBase).Namespace,
                                                                                               };

        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForBson> TypesToRegisterForBson => new[]
        {
            typeof(ScheduleBase).ToTypeToRegisterForBson(),
        };
    }
}
