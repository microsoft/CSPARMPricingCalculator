// -----------------------------------------------------------------------
// <copyright file="PublicIPComponentFetcher.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util.ResourceComponents
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataModel;
    using DataModel.ComponentModel;
    using Helper;

    /// <summary>
    /// Class that has method to fetch the resource components for the Public IP resource: "Microsoft.Network/publicIPAddresses" in an ARM template
    /// ARM Resource API Version: 2015-06-15
    /// </summary>
    public class PublicIPComponentFetcher : IComponentFetcher
    {
        /// <summary>
        /// Variable to store the Public IP resource from the ARM Template
        /// </summary>
        private Resource resource;

        /// <summary>
        /// Variable to store the ARM Template
        /// </summary>
        private ARMTemplate template;

        /// <summary>
        /// Variable to store the values of parameters from the Parameter file
        /// </summary>
        private ARMParamValue paramValue;

        /// <summary>
        /// Variable to store the name of the resource
        /// </summary>
        private string nameOfResource = string.Empty;

        /// <summary>
        /// Variable to store the properties of the Public IP resource from the ARM Template
        /// </summary>
        private PublicIPProperties prop;

        /// <summary>
        /// Gets the resource components for the Public IP resources in an ARM template
        /// </summary>
        /// <param name="resource">The object containing the resource</param>
        /// <param name="template">The object containing the ARM Template</param>
        /// <param name="paramValue">The object containing the values in the Parameter file</param>
        /// <param name="location">The Azure Location</param>
        /// <param name="cspCreds">CSP Account credentials object. This is not used in current version.</param>
        /// <param name="log">The object that will contain the exception messages</param>
        /// <returns> Returns the list of resource components</returns>
        public List<ResourceComponent> GetResourceComponents(Resource resource, ARMTemplate template, ARMParamValue paramValue, string location, CSPAccountCreds cspCreds, out StringBuilder log)
        {
            this.resource = resource;
            this.template = template;
            this.paramValue = paramValue;
            List<ResourceComponent> componentList = new List<ResourceComponent>();
            log = new StringBuilder(string.Empty);

            try
            {
                if (resource != null && resource.Name != null)
                {
                    // Fetch the name of the resource
                    this.nameOfResource = PropertyHelper.GetValueIfVariableOrParam(resource.Name, template.Variables, template.Parameters, paramValue.Parameters);
                }

                // Convert Resource Properties to PublicIPProperties
                if (resource != null && resource.Properties != null)
                {
                    this.prop = resource.Properties.ToObject<PublicIPProperties>();

                    if (this.prop != null)
                    {
                        // Get the resource components for Public IP Resource
                        componentList.AddRange(this.GetResourceComponentForPublicIP());
                    }
                    else
                    {
                        throw new Exception(ExceptionLogger.GenerateLoggerTextForFailedReadProperties(this.nameOfResource));
                    }
                }
                else
                {
                    throw new Exception(ExceptionLogger.GenerateLoggerTextForMissingField("Properties", this.nameOfResource));
                }
            }
            catch (Exception ex)
            {
                componentList = null;
                log.AppendLine(ex.Message);
            }

            return componentList;
        }

        /// <summary>
        /// Gets the resource components for the Public IP resource by checking if the Public IP Allocation method is static
        /// </summary>
        /// <returns> Returns the list of resource components</returns>
        private List<ResourceComponent> GetResourceComponentForPublicIP()
        {
            List<ResourceComponent> componentList = new List<ResourceComponent>();

            ResourceComponent publicIPComponent = new ResourceComponent()
            {
                ResourceType = this.resource.Type,
                Quantity = 0,
                IsChargeable = false,
                ResourceName = this.nameOfResource
            };

            string publicIPAllocationMethod = null;

            // Get the Allocation method
            publicIPAllocationMethod = PropertyHelper.GetValueIfVariableOrParam(this.prop.PublicIPAllocationMethod, this.template.Variables, this.template.Parameters, this.paramValue.Parameters);

            if (publicIPAllocationMethod != null)
            {
                // Check if Static Allocation, Add resource component with associated meter
                if (publicIPAllocationMethod.Equals(PublicIPResourceConstants.PublicPublicIPAllocationMethodStatic, StringComparison.OrdinalIgnoreCase))
                {
                    publicIPComponent.MeterCategory = NetworkingResourceConstants.NetworkingMeterCategory;
                    publicIPComponent.MeterSubCategory = PublicIPResourceConstants.NetworkingMeterSubCategoryForPublicIP;
                    publicIPComponent.MeterName = PublicIPResourceConstants.NetworkingMeterNameForPublicIP;
                    publicIPComponent.IsChargeable = true;
                    publicIPComponent.Quantity = Constants.HoursinaMonth;
                }
            }
            else
            {
                throw new Exception(ExceptionLogger.GenerateLoggerTextForMissingField("publicIPAllocationMethod", this.nameOfResource));
            }

            componentList.Add(publicIPComponent);

            return componentList;
        }
    }
}
