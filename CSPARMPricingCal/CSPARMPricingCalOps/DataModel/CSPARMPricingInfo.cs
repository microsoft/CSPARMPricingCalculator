// -----------------------------------------------------------------------
// <copyright file="CSPARMPricingInfo.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class that defines the output of the CSP ARM Pricing Calculation. Includes the list of individual components and their associated cost estimates
    /// </summary>
    public class CSPARMPricingInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSPARMPricingInfo" /> class.
        /// </summary>
        public CSPARMPricingInfo()
        {
            this.Log = new StringBuilder();
        }

        /// <summary>
        /// Gets or sets the Log with exception messages.
        /// </summary>
        public StringBuilder Log { get; set; }

        /// <summary>
        /// Gets or sets the output for the ARM Pricing Calculation in List of ResourceComponent objects.
        /// </summary>
        public List<ResourceComponent> CSPARMPricingList { get; set; }

        /// <summary>
        /// Gets or sets the currency for the ARM Pricing Calculation.
        /// </summary>
        public string Currency { get; set; }
    }
}
