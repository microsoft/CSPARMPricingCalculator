// -----------------------------------------------------------------------
// <copyright file="ResourceTypeHelper.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util.Helper
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Helper Class that has the list of supported ARM resource types
    /// </summary>
    public class ResourceTypeHelper
    {
        /// <summary>
        /// List of resource types supported
        /// </summary>
        public static List<ARMResourceType> ResourceTypeList = new List<ARMResourceType>()
        {
          new ARMResourceType() { ARMResourceTypeText = ARMResourceTypeConstants.ARMStorageResourceType, HasChargableComponents = false },
          new ARMResourceType() { ARMResourceTypeText = ARMResourceTypeConstants.ARMPublicIPResourceType, HasChargableComponents = true },
          new ARMResourceType() { ARMResourceTypeText = ARMResourceTypeConstants.ARMVirtualNetworkResourceType, HasChargableComponents = false },
          new ARMResourceType() { ARMResourceTypeText = ARMResourceTypeConstants.ARMNetworkInterfaceResourceType, HasChargableComponents = false },
          new ARMResourceType() { ARMResourceTypeText = ARMResourceTypeConstants.ARMVMResourceType, HasChargableComponents = true },
          new ARMResourceType() { ARMResourceTypeText = ARMResourceTypeConstants.ARMAvailabilitySetsResourceType, HasChargableComponents = false },
          new ARMResourceType() { ARMResourceTypeText = ARMResourceTypeConstants.ARMLoadBalancerResourceType, HasChargableComponents = false }
          
          // , new ARMResourceType() {ARMResourceTypeText = Constants. ,HasChargableComponents = false }
        };
    }

    /// <summary>
    /// ARMResourceType Class
    /// </summary>
    public class ARMResourceType
    {
        /// <summary>
        /// Gets or sets the resource type text
        /// </summary>
        public string ARMResourceTypeText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the resource type has chargeable components
        /// </summary>
        public bool HasChargableComponents { get; set; }
    }
}
