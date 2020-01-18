// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScheduleBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Cron
{
    using System;
    using System.ComponentModel;
    using OBeautifulCode.Type;

    /// <summary>
    /// Base class of the schedule for a recurring message sequence.
    /// </summary>
    [Bindable(BindableSupport.Default)]
    public abstract partial class ScheduleBase : IModelViaCodeGen
    {
        /// <summary>
        /// Checks to see if the schedule is valid (i.e. there aren't 61 minutes in an hour).
        /// </summary>
        /// <returns>True if valid and false if not.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is actually part of the contract.")]
        public bool IsValid()
        {
            try
            {
                this.ThrowIfInvalid();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks to make sure the construct is valid (i.e. there aren't 61 minutes in an hour).
        /// </summary>
        public abstract void ThrowIfInvalid();

        /// <summary>
        /// Generates a cron expression from the schedule object.
        /// </summary>
        /// <returns>Generated cron expression from object.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cron", Justification = "Spelling/name is correct.")]
        public string ToCronExpression()
        {
            var ret = ScheduleCronExpressionConverter.ToCronExpression(this);
            return ret;
        }
    }
}
