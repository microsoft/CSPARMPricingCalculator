// -----------------------------------------------------------------------
// <copyright file="CSPAccountCreds.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.DataModel
{
    using System;

    /// <summary>
    /// Class that defines the configuration values including the CSP Partner Account credentials required to make Partner Center and ARM API calls
    /// </summary>
    public class CSPAccountCreds
    {
        /// <summary>
        /// Gets or sets the App Id for the Native App registered on Partner Center portal. This will be used for fetching CSP Rate Card.
        /// </summary>
        public string CSPClientId { get; set; }

        /// <summary>
        /// Gets or sets the Tenant Id or Microsoft Id for the CSP Partner.
        /// </summary>
        public string CSPResellerTenantID { get; set; }

        /// <summary>
        /// Gets or sets the Region for the CSP Partner.
        /// </summary>
        public string CSPRegion { get; set; }

        /// <summary>
        /// Gets or sets the Currency for the CSP Partner.
        /// </summary>
        public string CSPCurrency { get; set; }

        /// <summary>
        /// Gets or sets App Id for the Native App registered for ARM APIs with Pre-Consent. 
        /// </summary>
        public string CSPNativeAppClientId { get; set; }

        /// <summary>
        /// Gets or sets Tenant Id or Microsoft Id for a CSP Customer. 
        /// </summary>
        public string CSPCustomerTenantId { get; set; }

        /// <summary>
        /// Gets or sets the Subscription Id of an Azure CSP Subscription. 
        /// </summary>
        public string CSPAzureSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the Username of an Admin Agent user of the CSP Partner Org. 
        /// </summary>
        public string CSPAdminAgentUserName { get; set; }

        /// <summary>
        /// Gets or sets the Password of the Admin Agent user of the CSP Partner Org. 
        /// </summary>
        public string CSPAdminAgentPassword { get; set; }
    }
}
