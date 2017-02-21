// -----------------------------------------------------------------------
// <copyright file="VMHelper.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util.Helper
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Helper Class that has static methods to operate on Azure VM information
    /// </summary>
    public class VMHelper
    {
        /// <summary>
        /// Modifies the VM Size String as per the Azure Pricing Specs
        /// </summary>
        /// <param name="vmSize">VM Size String</param>
        /// <returns> Returns the modified VM Size in String format</returns>
        public static string ModifyVMSizeStringAsPerPricingSpecs(string vmSize)
        {
            string modifiedVMSize = vmSize;

            if (vmSize != null)
            {
                // Flag to set if modify is complete
                bool isCompletedProcessing = false;

                // Check if A Size and Not v2, Modify
                if (!isCompletedProcessing)
                {
                    string modifiedSize = null;
                    if (!vmSize.Contains("v2") && GetModifiedVMSizeForASizes(vmSize, out modifiedSize))
                    {
                        isCompletedProcessing = true;
                        modifiedVMSize = modifiedSize;
                    }
                }

                // Check if DS series, modify string accordingly
                if (!isCompletedProcessing)
                {
                    string modifiedSize = null;
                    if (GetModifiedVMSizeForPremiumSSizes(vmSize, out modifiedSize, VMResourceConstants.VMDGPremiumSSizeRegexString))
                    {
                        isCompletedProcessing = true;
                        modifiedVMSize = modifiedSize;
                    }
                }

                // Check if FS series, modify string accordingly
                if (!isCompletedProcessing)
                {
                    string modifiedSize = null;
                    if (GetModifiedVMSizeForPremiumSSizes(vmSize, out modifiedSize, VMResourceConstants.VMFPremiumSSizeRegexString))
                    {
                        isCompletedProcessing = true;
                        modifiedVMSize = modifiedSize;
                    }
                }
            }

            return modifiedVMSize;
        }

        /// <summary>
        /// Modifies the VM Size String for A Sizes as per the Azure Pricing Specs
        /// </summary>
        /// <param name="vmSize">VM Size String</param>
        /// <param name="modifiedSize">Out parameter with Modified Size in String format</param>
        /// <returns> Returns the value indicating if a match was found and size was modified or not</returns>
        private static bool GetModifiedVMSizeForASizes(string vmSize, out string modifiedSize)
        {
            bool isCompletedProcessing = false;
            modifiedSize = null;
            Regex r = new Regex(VMResourceConstants.VMASizeRegexString, RegexOptions.IgnoreCase);
            Match m = r.Match(vmSize);

            // Split the String to Basic or Standard and Size
            if (m.Success)
            {
                isCompletedProcessing = true;
                if (m.Groups.Count > 2)
                {
                    string basicOrStandard = m.Groups[1].Value;
                    string size = m.Groups[2].Value;

                    // If VM Size is Basic, Change String to Basic.Size
                    if (string.Equals(basicOrStandard, VMResourceConstants.VMSizeBasicString, StringComparison.OrdinalIgnoreCase))
                    {
                        modifiedSize = VMResourceConstants.VMSizeBasicString + "." + size;
                    }
                    else
                    {
                        modifiedSize = size;
                    }
                }
            }

            return isCompletedProcessing;
        }

        /// <summary>
        /// Modifies the VM Size String for Premium S Sizes as per the Azure Pricing Specs
        /// </summary>
        /// <param name="vmSize">VM Size String</param>
        /// <param name="modifiedSize">Out parameter with Modified Size in String format</param>
        /// <param name="premiumSRegexString">Regex that contains the String pattern to be matched in String format</param>
        /// <returns> Returns the value indicating if a match was found and size was modified or not</returns>
        private static bool GetModifiedVMSizeForPremiumSSizes(string vmSize, out string modifiedSize, string premiumSRegexString)
        {
            bool isCompletedProcessing = false;
            modifiedSize = null;

            Regex r = new Regex(premiumSRegexString, RegexOptions.IgnoreCase);
            Match m = r.Match(vmSize);

            // Check if VM Size is having premium disks - DS/FS/GS, Remove S and return VM Size String
            if (m.Success)
            {
                isCompletedProcessing = true;
                if (m.Groups.Count > 2)
                {
                    modifiedSize = m.Groups[1].Value + m.Groups[2].Value;
                }
            }

            return isCompletedProcessing;
        }
    }
}
