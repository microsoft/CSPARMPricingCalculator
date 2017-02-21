// -----------------------------------------------------------------------
// <copyright file="CSPARMPricingCalculator.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataModel;
    using Util;
    using Util.ResourceComponents;

    /// <summary>
    /// Class that has methods to fetch Rate card and perform CSP ARM Pricing Calculation for an ARM Template
    /// </summary>
    public class CSPARMPricingCalculator
    {
        /// <summary>
        /// Variable to store the ARM Template object
        /// </summary>
        private ARMTemplate template;

        /// <summary>
        /// Variable to store the list of meters in the CSP Rate Card 
        /// </summary>
        private List<Meter> meterlist;

        /// <summary>
        /// Variable to store the currency of the Azure CSP Partner
        /// </summary>
        private string currency;

        /// <summary>
        /// Gets the resource components for the Virtual machine resource in an ARM template
        /// </summary>
        /// <param name="template">The object containing the ARM Template</param>
        /// <param name="paramValue">The object containing the values in the Parameter file</param>
        /// <param name="location">The Azure Location</param>
        /// <param name="cspCreds">CSP Account credentials object. A token will be generated using these credentials and used for making the online API call.</param>
        /// <returns> Returns the result of the CSP ARM Pricing Calculation</returns>
        public CSPARMPricingInfo CalculateCSPARMPricing(ARMTemplate template, ARMParamValue paramValue, string location, CSPAccountCreds cspCreds)
        {
            CSPARMPricingInfo info = new CSPARMPricingInfo();
            StringBuilder log = new StringBuilder(string.Empty);
            info.Log = new StringBuilder(string.Empty);
            this.template = template;

            // Fetch the list of resource components for the ARM Template
            List<ResourceComponent> components = ComponentFetcher.GetResourceComponentsForTemplate(template, paramValue, location, cspCreds, out log);

            if (log != null)
            {
                // Append log of exception messages if any
                info.Log.Append(log.ToString());
            }

            // Fetch the Rates for the Resource components and Calculate monthly Estimates
            List<ResourceComponent> ratedComponents = null;
            if (components != null && components.Count > 0)
            {
                ratedComponents = ResourceRateCalc.CalculateResourceComponentRates(components, location, this.meterlist, out log);
            }

            if (log != null)
            {
                // Append log of exception messages if any
                info.Log.Append(log.ToString());
            }

            // Set currency and object of resource component with pricing and estimation details
            info.Currency = this.currency;
            info.CSPARMPricingList = ratedComponents;

            return info;
        }

        /// <summary>
        /// Load the Azure CSP Rate Card
        /// </summary>
        /// <param name="cspCreds">CSP Account credentials object. A token will be generated using these credentials and used for making the online Partner Center API call.</param>
        public void FetchRateCard(CSPAccountCreds cspCreds)
        {
            try
            {
                // Load CSP Azure RateCard
                this.meterlist = RateCardUtil.GetRateCard(cspCreds);
            }
            catch (Exception)
            {
                throw;
            }
            
            this.currency = cspCreds.CSPCurrency;
        }
    }
}
