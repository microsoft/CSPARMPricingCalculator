// -----------------------------------------------------------------------
// <copyright file="ResourceComponent.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.DataModel
{
    using System;

    /// <summary>
    /// Class that defines the components of resource in an ARM template and its associated cost and estimates
    /// </summary>
    public class ResourceComponent
    {
        /// <summary>
        /// Gets or sets the name of the resource component.
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource component.
        /// </summary>
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the Azure Pricing Meter Name associated with the resource component.
        /// </summary>
        public string MeterName { get; set; }

        /// <summary>
        /// Gets or sets the Azure Pricing Meter Category associated with the resource component.
        /// </summary>
        public string MeterCategory { get; set; }

        /// <summary>
        /// Gets or sets the Azure Pricing Meter SubCategory associated with the resource component.
        /// </summary>
        public string MeterSubCategory { get; set; }

        /// <summary>
        /// Gets or sets the Quantity of the resource component.
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the resource component is chargeable or not.
        /// </summary>
        public bool IsChargeable { get; set; }

        /// <summary>
        /// Gets or sets the Azure Rate of the resource component.
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Gets or sets the estimated cost over the month for the resource component.
        /// </summary>
        public double CostPerMonth { get; set; }
    }
}
