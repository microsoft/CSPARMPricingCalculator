// -----------------------------------------------------------------------
// <copyright file="ExceptionLogger.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util.Helper
{
    using System;

    /// <summary>
    /// Class that has static methods to generate Exception Log messages
    /// </summary>
    public class ExceptionLogger
    {
        /// <summary>
        /// Generates the Exception message for missing values for the resources in the ARM Template
        /// </summary>
        /// <param name="fieldName">Name of the field missing</param>
        /// <param name="resourceName">Name of the resource containing the missing field</param>
        /// <returns> Returns the exception message in String format</returns>
        public static string GenerateLoggerTextForMissingField(string fieldName, string resourceName)
        {
            return string.Format(LogMessagesConstants.MissingFieldMsg, fieldName, resourceName);
        }

        /// <summary>
        /// Generates the Exception message for invalid values for the resources in the ARM Template
        /// </summary>
        /// <param name="fieldName">Name of the field having invalid value</param>
        /// <param name="fieldValue">Current invalid value of the field</param>
        /// <param name="resourceName">Name of the resource containing the invalid value</param>
        /// <returns> Returns the exception message in String format</returns>
        public static string GenerateLoggerTextForInvalidField(string fieldName, string fieldValue, string resourceName)
        {
            return string.Format(LogMessagesConstants.InvalidFieldMsg, fieldName, fieldValue, resourceName);
        }

        /// <summary>
        /// Generates the Exception message for unsupported resource types for the resource in the ARM Template
        /// </summary>
        /// <param name="resourceName">Name of the resource associated with unsupported resource type</param>
        /// <returns> Returns the exception message in String format</returns>
        public static string GenerateLoggerTextForUnSupportedResource(string resourceName)
        {
            return string.Format(LogMessagesConstants.ResourceNotSupportedMsg, resourceName);
        }

        /// <summary>
        /// Generates the Exception message for no output for the resource in the ARM Template
        /// </summary>
        /// <param name="resourceName">Name of the resource associated with no resource output</param>
        /// <returns> Returns the exception message in String format</returns>
        public static string GenerateLoggerTextForNoResourceOutput(string resourceName)
        {
            return string.Format(LogMessagesConstants.ResourceComponentsNoOutputMsg, resourceName);
        }

        /// <summary>
        /// Generates the Exception message when fails to read properties section of the resource in the ARM Template
        /// </summary>
        /// <param name="resourceName">Name of the resource for which failing to read the properties section</param>
        /// <returns> Returns the exception message in String format</returns>
        public static string GenerateLoggerTextForFailedReadProperties(string resourceName)
        {
            return string.Format(LogMessagesConstants.PropertiesReadFailedMsg, resourceName);
        }

        /// <summary>
        /// Generates the Exception message for Failed Online Helper Call
        /// </summary>
        /// <param name="field">Name of the field being fetched Online</param>
        /// <param name="exceptionMsg">Error with the fetch online</param>
        /// <returns> Returns the exception message in String format</returns>
        public static string GenerateLoggerTextForOnlineHelperCall(string field, string exceptionMsg)
        {
            return string.Format(LogMessagesConstants.PropertiesOnlineCallFailedMsg, field, exceptionMsg);
        }

        /// <summary>
        /// Generates the Exception message for Internal Error in the Application
        /// </summary>
        /// <param name="exceptionMsg">Details on the internal error</param>
        /// <returns> Returns the exception message in String format</returns>
        public static string GenerateLoggerTextForInternalError(string exceptionMsg)
        {
            return string.Format(LogMessagesConstants.InternalErrorMsg, exceptionMsg);
        }

        /// <summary>
        /// Generates the Exception message for Meter Rate Fetch Failure
        /// </summary>
        /// <param name="reason">Error while fetching Meter Rate</param>
        /// <param name="resourceName">Name of the resource for which Meter Rate fetch failed</param>
        /// <returns> Returns the exception message in String format</returns>
        public static string GenerateLoggerTextForMeterRateFetchFailed(string reason, string resourceName)
        {
            return string.Format(LogMessagesConstants.MeterRateFetchFailedMsg, reason, resourceName);
        }
    }
}
