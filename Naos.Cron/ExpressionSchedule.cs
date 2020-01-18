// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionSchedule.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Expression implementation of a schedule (serialized easily and converted into class version at any time).
    /// </summary>
    public partial class ExpressionSchedule : ScheduleBase, IModelViaCodeGen
    {
        /// <summary>
        /// Gets or sets the cron expression.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cron", Justification = "Spelling/name is correct.")]
        public string CronExpression { get; set; }

        /// <inheritdoc />
        public override void ThrowIfInvalid()
        {
            var objectVersion = ScheduleCronExpressionConverter.FromCronExpression(this.CronExpression);
            objectVersion.ThrowIfInvalid();
        }
    }
}
