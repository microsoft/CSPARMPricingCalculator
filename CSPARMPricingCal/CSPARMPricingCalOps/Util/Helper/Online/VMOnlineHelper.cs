// -----------------------------------------------------------------------
// <copyright file="VMOnlineHelper.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util.Helper.Online
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CSPARMPricingCalOps.DataModel;

    /// <summary>
    /// Helper class that has static method to fetch Azure VM Images details and Azure VM Instance Type information
    /// </summary>
    public class VMOnlineHelper
    {
        /// <summary>
        /// Fetches the OS Type for the specified Publisher, Offer and SKU of the Azure VM Image
        /// </summary>
        /// <param name="cspCreds">CSP Account credentials object. A token will be generated using these credentials and used for making the online ARM API call</param>
        /// <param name="publisher">Publisher of the Azure VM Image</param>
        /// <param name="offer">Offer of the Azure VM Image</param>
        /// <param name="sku">SKU of the Azure VM Image</param>
        /// <param name="location">Azure Location</param>
        /// <returns> Returns the Operating System type in String format</returns>
        public static string GetVMImageOSType(CSPAccountCreds cspCreds, string publisher, string offer, string sku, string location)
        {
            string osType = null;

            try
            {
                // Get AAD Token
                string aadToken = AuthManager.GetAzureADTokenAppUser(cspCreds.CSPNativeAppClientId, cspCreds.CSPAdminAgentUserName, cspCreds.CSPAdminAgentPassword, cspCreds.CSPCustomerTenantId, false);

                // Get SKUList 
                List<VMSKUVersionListItem> skuVersionList = null;
                skuVersionList = GetVMImageSKUS(aadToken, cspCreds.CSPAzureSubscriptionId, publisher, offer, sku, location);

                // Get SKUVersionDetails & OSType from it
                if (skuVersionList != null && skuVersionList.Count != 0)
                {
                    VMSKUVersionListItem versionListItem = skuVersionList.FirstOrDefault<VMSKUVersionListItem>();
                    if (versionListItem != null && versionListItem.Id != null)
                    {
                        VMSKUVersion skuVersion = GetVMImageSKUVersionDetails(aadToken, versionListItem.Id);
                        if (skuVersion != null && skuVersion.Properties != null && skuVersion.Properties.OsDiskImage != null)
                        {
                            osType = skuVersion.Properties.OsDiskImage.OperatingSystem;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return osType; 
        }

        /// <summary>
        /// Get the number of cores for specified Azure VM Size
        /// </summary>
        /// <param name="cspCreds">CSP Account credentials object. A token will be generated using these credentials and used for making the online ARM API call</param>
        /// <param name="vmSize">Azure VM Size</param>
        /// <param name="location">Azure Location</param>
        /// <returns> Returns the number of cores for the specified Azure VM Size</returns>
        public static int GetCoresForVmSize(CSPAccountCreds cspCreds, string vmSize, string location)
        {
            int numberOfCores = -1;
            try
            {
                // Get AAD Token
                string aadToken = AuthManager.GetAzureADTokenAppUser(cspCreds.CSPNativeAppClientId, cspCreds.CSPAdminAgentUserName, cspCreds.CSPAdminAgentPassword, cspCreds.CSPCustomerTenantId, false);

                string url = APIURLConstants.VMGetVMSizesAPIsUrl;
                var path = string.Format(url, APIURLConstants.ARMAPIURL, cspCreds.CSPAzureSubscriptionId, location, APIURLConstants.ARMComputeAPIVersion);
                
                // Make the ARM API call using the Online Helper class method
                VMSizeList sizeList = ARMAPIHelper.GetARMCall<VMSizeList>(aadToken, path, APIResponseTimeLimitConstants.APICallDefaultLimit);

                VMSizeListItem listItem = sizeList.Value.FirstOrDefault(x => x.Name.Equals(vmSize, StringComparison.OrdinalIgnoreCase));
                if (listItem != null)
                {
                    numberOfCores = listItem.NumberOfCores;
                }
            }
            catch (Exception e)
            {
                throw new Exception(ExceptionLogger.GenerateLoggerTextForOnlineHelperCall("Number of Cores", e.Message));
            }

            return numberOfCores;
        }

        /// <summary>
        /// Fetches the VM image SKU Version Info for the specified SKU Version of the Azure VM Image
        /// </summary>
        /// <param name="token">Azure AD Token to make the ARM API Call</param>
        /// <param name="skuVersionID">ID of the SKU Version</param>
        /// <returns> Returns the SKU Version info of the specified Azure VM Image SKU</returns>
        private static VMSKUVersion GetVMImageSKUVersionDetails(string token, string skuVersionID)
        {
            VMSKUVersion skuVersion = null;
            try
            {
                string url = APIURLConstants.VMSKUGetVersionDetailsAPIsUrl;
                var path = string.Format(url, APIURLConstants.ARMAPIURL, skuVersionID, APIURLConstants.ARMComputeAPIVersion);

                // Make the ARM API call using the Online Helper class method
                skuVersion = ARMAPIHelper.GetARMCall<VMSKUVersion>(token, path, APIResponseTimeLimitConstants.APICallDefaultLimit);
            }
            catch (Exception e)
            {
                throw new Exception(ExceptionLogger.GenerateLoggerTextForOnlineHelperCall("VM SKU Version Details", e.Message));
            }

            return skuVersion;
        }

        /// <summary>
        /// Fetches the VM image SKU List for the specified Publisher, Offer and SKU of the Azure VM Image
        /// </summary>
        /// <param name="token">Azure AD Token to make the ARM API Call</param>
        /// <param name="subscriptionId">CSP Azure Subscription Id</param>
        /// <param name="publisher">Publisher of the Azure VM Image</param>
        /// <param name="offer">Offer of the Azure VM Image</param>
        /// <param name="sku">SKU of the Azure VM Image</param>
        /// <param name="location">Azure Location</param>
        /// <returns> Returns the list of Azure VM Image SKUs</returns>
        private static List<VMSKUVersionListItem> GetVMImageSKUS(string token, string subscriptionId, string publisher, string offer, string sku, string location)
        {
            List<VMSKUVersionListItem> skuVersionList = null;
            try
            {
                string url = APIURLConstants.VMSKUGetVersionsAPIsUrl;
                var path = string.Format(url, APIURLConstants.ARMAPIURL, subscriptionId, location, publisher, offer, sku, APIURLConstants.ARMComputeAPIVersion);

                // Make the ARM API call using the Online Helper class method
                skuVersionList = ARMAPIHelper.GetARMCall<List<VMSKUVersionListItem>>(token, path, APIResponseTimeLimitConstants.APICallDefaultLimit);
            }
            catch (Exception e)
            {
                throw new Exception(ExceptionLogger.GenerateLoggerTextForOnlineHelperCall("VM SKU Version", e.Message));
            }

            return skuVersionList;
        }
    }
}
