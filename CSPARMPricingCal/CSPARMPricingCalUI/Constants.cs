// -----------------------------------------------------------------------
// <copyright file="Constants.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalUI
{
    /// <summary>
    /// Class that contains constants used in the UI project
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Key String in app.config for the Partner Center Native App 
        /// </summary>
        public const string ConfigSettingsFieldCSPPCNativeAppClientId = "CSP:PCNativeAppClientId";

        /// <summary>
        /// Key String in app.config for the Tenant Id of the CSP Partner's Azure AD
        /// </summary>
        public const string ConfigSettingsFieldCSPPartnerTenantID = "CSP:PartnerTenantID";

        /// <summary>
        /// Key String in app.config for the App Id of the Native App configured with pre-consent for ARM APIs
        /// </summary>
        public const string ConfigSettingsFieldCSPARMNativeAppClientId = "CSP:ARMNativeAppClientId";

        /// <summary>
        /// Key String in app.config for the Tenant Id of the CSP Customer
        /// </summary>
        public const string ConfigSettingsFieldCSPCustomerTenantId = "CSP:CustomerTenantId";

        /// <summary>
        /// Key String in app.config for the Subscription Id of the Azure CSP Subscription
        /// </summary>
        public const string ConfigSettingsFieldCSPAzureSubscriptionId = "CSP:AzureSubscriptionId";

        /// <summary>
        /// Key String in app.config for the UserName of the Admin Agent user in CSP Partner's Azure AD
        /// </summary>
        public const string ConfigSettingsFieldCSPAdminAgentUserName = "CSP:AdminAgentUserName";

        /// <summary>
        /// Key String in app.config for the Password of the Admin Agent user in CSP Partner's Azure AD
        /// </summary>
        public const string ConfigSettingsFieldCSPAdminAgentPassword = "CSP:AdminAgentPassword";
    }

    /// <summary>
    /// Class that contains Log Message constants used in the UI project
    /// </summary>
    public static class UIMessageConstants
    {
        /// <summary>
        /// Message displayed before initiating the Rate Card Fetch - Line 1
        /// </summary>
        public const string RateCardFetchInitiateMsgL1 = "Fetching Ratecard for {0}...\n";

        /// <summary>
        /// Message displayed before initiating the Rate Card Fetch - Line 2
        /// </summary>
        public const string RateCardFetchInitiateMsgL2 = "Please be patient. This may take a few minutes...\n";

        /// <summary>
        /// Failed Rate Card Fetch Error Message
        /// </summary>
        public const string RateCardFetchFailedMsg = "Error Occured while initiating RateCard Fetch. {0}\n";

        /// <summary>
        /// Validation Message - No ARM Template specified
        /// </summary>
        public const string NoARMTemplateProvided = "Browse an ARM Template File to proceed...\n";

        /// <summary>
        /// Validation Message - No ARM Template specified
        /// </summary>
        public const string NoDeploymentLocationProvided = "Select a Deployment Location to proceed...\n";

        /// <summary>
        /// CSP ARM Pricing Calculation Initiated Message
        /// </summary>
        public const string CSPARMPricingCalculationInitiateMsg = "Calculating for ARM Template: {0} and Location: {1}...\n";

        /// <summary>
        /// CSP ARM Pricing Calculation Initiated Message
        /// </summary>
        public const string CSPARMPricingCalculationFailedMsg = "Error Occured while initiating CSP ARM Pricing Calculation. {0}\n";

        /// <summary>
        /// CSP ARM Pricing Calculation Progress Message - After ARM Template File and Parameter file read complete
        /// </summary>
        public const string CSPARMPricingCalculationProgressFileReadCompleteMsg = "ARM Template and Param File (if provided) has been read...";

        /// <summary>
        /// CSP ARM Pricing Calculation Progress Message - Calculation complete
        /// </summary>
        public const string CSPARMPricingCalculationProgressAllCompleteMsg = "CSP ARM Pricing Calculation Complete...";

        /// <summary>
        /// CSP ARM Pricing Calculation Progress Message - Calculation complete
        /// </summary>
        public const string CSPARMPricingCalculationInvalidJSONMsg = "Error Occured: Invalid JSON File, Error Details:{0}";

        /// <summary>
        /// Load Config Failed Message
        /// </summary>
        public const string LoadConfigFailedMsg = "Error Occured while fetching config values. {0}\n";

        /// <summary>
        /// Error while browsing file path Message
        /// </summary>
        public const string BrowseFailedMsg = "Error Occured. {0}\n";

        /// <summary>
        /// Error during background operation Message
        /// </summary>
        public const string BackgroundOperationFailedMsg = "Error Occured. {0}\n";

        /// <summary>
        /// Error during background operation Message
        /// </summary>
        public const string BackgroundOperationCancelledMsg = "Operation Cancelled.";

        /// <summary>
        /// Rate Card Fetch Complete Message
        /// </summary>
        public const string RateCardFetchCompleteMsg = "Rate Card Fetch complete.\n";

        /// <summary>
        /// Grid View Showing Results Message
        /// </summary>
        public const string GridShowingResultsMsg = "Displaying Estimated Cost information...\n";

        /// <summary>
        /// Total Cost String
        /// </summary>
        public const string TotalCostStringMsg = "Estimated Cost: {0:0.00} {1} /month";

        /// <summary>
        /// Error String in Log Messages
        /// </summary>
        public const string ErrorStringToCheck = "ERROR OCCURED";

        /// <summary>
        /// Progress Change Message
        /// </summary>
        public const string ProgressChangeMsg = "Current Progress: {0}%, Status: {1}\n";

        /// <summary>
        /// Error during Export Results
        /// </summary>
        public const string ExportCSVCompleteMsg = "Export Complete, File: {0}";

        /// <summary>
        /// Error during Export Results
        /// </summary>
        public const string ExportCSVFailedMsg = "Error Occured while exporting to CSV File. {0}\n";
    }
}
