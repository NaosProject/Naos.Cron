// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonthOfYear.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    /// <summary>
    /// The months in the year.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "Keeping this way for only valid values to make it easier in conversion.")]
    public enum MonthOfYear
    {
        /// <summary>
        /// First month.
        /// </summary>
        January = 1,

        /// <summary>
        /// Second month.
        /// </summary>
        February = 2,

        /// <summary>
        /// Third month.
        /// </summary>
        March = 3,

        /// <summary>
        /// Fourth month.
        /// </summary>
        April = 4,

        /// <summary>
        /// Fifth month.
        /// </summary>
        May = 5,

        /// <summary>
        /// Sixth month.
        /// </summary>
        June = 6,

        /// <summary>
        /// Seventh month.
        /// </summary>
        July = 7,

        /// <summary>
        /// Eighth month.
        /// </summary>
        August = 8,

        /// <summary>
        /// Ninth month.
        /// </summary>
        September = 9,

        /// <summary>
        /// Tenth month.
        /// </summary>
        October = 10,

        /// <summary>
        /// Eleventh month.
        /// </summary>
        November = 11,

        /// <summary>
        /// Twelfth month.
        /// </summary>
        December = 12,
    }
}
