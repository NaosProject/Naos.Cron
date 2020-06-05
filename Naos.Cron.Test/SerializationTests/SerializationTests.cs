// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializationTests.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron.Test
{
    using System;
    using FakeItEasy;
    using FluentAssertions;
    using Naos.Cron;
    using Naos.Cron.Serialization.Bson;
    using Naos.Cron.Serialization.Json;
    using OBeautifulCode.Serialization.Bson;
    using OBeautifulCode.Serialization.Json;
    using Xunit;

    public static class SerializationTests
    {
        [Fact]
        public static void Roundtrip_DefaultScheduleBase()
        {
            // Arrange
            var expected = default(ScheduleBase);
            var bsonSerializer = new ObcBsonSerializer<CronBsonSerializationConfiguration>();
            var jsonSerializer = new ObcJsonSerializer<CronJsonSerializationConfiguration>();

            // Act
            var actualBsonString = bsonSerializer.SerializeToString(expected);
            var actualFromBsonString = bsonSerializer.Deserialize<ScheduleBase>(actualBsonString);

            var actualJsonString = jsonSerializer.SerializeToString(expected);
            var actualFromJsonString = jsonSerializer.Deserialize<ScheduleBase>(actualJsonString);

            // Assert
            actualFromBsonString.Should().Be(expected);
            actualFromJsonString.Should().Be(expected);
        }

        [Fact]
        public static void Roundtrip_DummyScheduleBase()
        {
            // Arrange
            var expected = A.Dummy<ScheduleBase>();
            var bsonSerializer = new ObcBsonSerializer<CronBsonSerializationConfiguration>();
            var jsonSerializer = new ObcJsonSerializer<CronJsonSerializationConfiguration>();

            // Act
            var actualBsonString = bsonSerializer.SerializeToString(expected);
            var actualFromBsonString = bsonSerializer.Deserialize<ScheduleBase>(actualBsonString);

            var actualJsonString = jsonSerializer.SerializeToString(expected);
            var actualFromJsonString = jsonSerializer.Deserialize<ScheduleBase>(actualJsonString);

            // Assert
            actualFromBsonString.Should().Be(expected);
            actualFromJsonString.Should().Be(expected);
        }
    }

    public static class SerializationConfigurationTypes
    {
        public static BsonSerializationConfigurationType BsonSerializationConfigurationType => typeof(CronBsonSerializationConfiguration).ToBsonSerializationConfigurationType();

        public static JsonSerializationConfigurationType JsonSerializationConfigurationType => typeof(CronJsonSerializationConfiguration).ToJsonSerializationConfigurationType();

        public static BsonSerializationConfigurationType BsonConfigurationType => typeof(CronBsonSerializationConfiguration).ToBsonSerializationConfigurationType();

        public static JsonSerializationConfigurationType JsonConfigurationType => typeof(CronJsonSerializationConfiguration).ToJsonSerializationConfigurationType();
    }
}
