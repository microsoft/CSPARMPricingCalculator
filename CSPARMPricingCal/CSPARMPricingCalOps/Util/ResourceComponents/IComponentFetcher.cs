// -----------------------------------------------------------------------
// <copyright file="IComponentFetcher.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util.ResourceComponents
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataModel;

    /// <summary>
    /// Interface defining the method signature to fetch the components of a resource in an ARM template
    /// This interface is implemented by each resource type that is supported by this application and has chargeable components
    /// </summary>
    public interface IComponentFetcher
    {
        /// <summary>
        /// Gets the resource components for the resources in an ARM template
        /// </summary>
        /// <param name="resource">The object containing the resource</param>
        /// <param name="template">The object containing the ARM Template</param>
        /// <param name="paramValue">The object containing the values in the Parameter file</param>
        /// <param name="location">The Azure Location</param>
        /// <param name="cspCreds">CSP Account credentials object. A token will be generated using these credentials and used for making the online ARM API call</param>
        /// <param name="log">The object that will contain the exception messages</param>
        /// <returns> Returns the list of resource components</returns>
        List<ResourceComponent> GetResourceComponents(Resource resource, ARMTemplate template, ARMParamValue paramValue, string location, CSPAccountCreds cspCreds, out StringBuilder log);
    }
}
