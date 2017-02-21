// -----------------------------------------------------------------------
// <copyright file="ComponentFetcher.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util.ResourceComponents
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataModel;
    using Helper;

    /// <summary>
    /// Class that has static methods to fetch the resource components for the resources in an ARM template
    /// </summary>
    public class ComponentFetcher
    {
        /// <summary>
        /// Gets the resource components for the resources in an ARM template
        /// </summary>
        /// <param name="template">The object containing the ARM Template</param>
        /// <param name="paramvalue">The object containing the values in the Parameter file</param>
        /// <param name="location">The Azure Location</param>
        /// <param name="cspCreds">CSP Account credentials object. A token will be generated using these credentials and used for making the online ARM API call</param>
        /// <param name="log">The object that will contain the exception messages</param>
        /// <returns> Returns the list of resource components</returns>
        public static List<ResourceComponent> GetResourceComponentsForTemplate(ARMTemplate template, ARMParamValue paramvalue, string location, CSPAccountCreds cspCreds, out StringBuilder log)
        {
            List<ResourceComponent> components = new List<ResourceComponent>();
            log = new StringBuilder(string.Empty);
            string locationAsPerARMSpecs = null;

            try
            {
                // Fetch the location as per ARM Specs from the mapping
                if (!LocationConstants.LocationAsPerARMSpecsMap.TryGetValue(location, out locationAsPerARMSpecs))
                {
                    throw new Exception(ExceptionLogger.GenerateLoggerTextForInvalidField("Location", location, "ARMTemplate"));
                }

                if (template.Resources != null && template.Resources.Count > 0)
                {
                    // Loop thru each resource in the ARM Template
                    foreach (Resource res in template.Resources)
                    {
                        ARMResourceType resType = null;
                        string nameOfResource = string.Empty;
                        if (res != null && res.Name != null)
                        {
                            // Fetch the name of the current resource
                            nameOfResource = PropertyHelper.GetValueIfVariableOrParam(res.Name, template.Variables, template.Parameters, paramvalue.Parameters);
                        }

                        // Check if resource or resource type is not null
                        if (res != null && res.Type != null)
                        {
                            // Check if resource type is in supported by this application
                            resType = ResourceTypeHelper.ResourceTypeList.Find(x => res.Type.Equals(x.ARMResourceTypeText, StringComparison.OrdinalIgnoreCase));

                            if (resType != null)
                            {
                                List<ResourceComponent> currentResourceComponents = new List<ResourceComponent>();
                                
                                // Check if the resource type of the current resource does not have chargeable components
                                if (!resType.HasChargableComponents)
                                {
                                    currentResourceComponents.Add(new ResourceComponent()
                                    {
                                        ResourceType = res.Type,
                                        Quantity = 0,
                                        IsChargeable = false
                                    });
                                }
                                else
                                {
                                    IComponentFetcher resCompFetcher = null;

                                    // Create the appropriate object to fetch the components of the resource
                                    switch (resType.ARMResourceTypeText)
                                    {
                                        // Public IP Resource
                                        case ARMResourceTypeConstants.ARMPublicIPResourceType:
                                            resCompFetcher = new PublicIPComponentFetcher();
                                            break;

                                        // Virtual Machine Resource
                                        case ARMResourceTypeConstants.ARMVMResourceType:
                                            resCompFetcher = new VMComponentFetcher();
                                            break;

                                        default:

                                            // Has Chargable Components but not yet supported
                                            log.AppendLine(ExceptionLogger.GenerateLoggerTextForUnSupportedResource(res.Type));
                                            break;
                                    }

                                    StringBuilder resLog = new StringBuilder(string.Empty);

                                    // Call the method to fetch the resource components
                                    List<ResourceComponent> resComp = resCompFetcher.GetResourceComponents(res, template, paramvalue, locationAsPerARMSpecs, cspCreds, out resLog);
                                    if (resLog != null)
                                    {
                                        log.Append(resLog);
                                    }

                                    if (resComp != null && resComp.Count > 0)
                                    {
                                        currentResourceComponents.AddRange(resComp);
                                    }
                                    else
                                    {
                                        log.AppendLine(ExceptionLogger.GenerateLoggerTextForNoResourceOutput(nameOfResource));
                                    }
                                }

                                foreach (ResourceComponent component in currentResourceComponents)
                                { 
                                    component.ResourceName = nameOfResource;
                                }

                                components.AddRange(currentResourceComponents);
                            }
                            else
                            {
                                log.AppendLine(ExceptionLogger.GenerateLoggerTextForUnSupportedResource(res.Type));
                            }
                        }
                        else
                        {
                            if (res != null)
                            {
                                // Type of the resourse is missing/null, generate the message
                                log.AppendLine(ExceptionLogger.GenerateLoggerTextForMissingField("Type", nameOfResource));
                            }
                            else
                            {
                                // Resourse is missing/null, generate the message
                                log.AppendLine(ExceptionLogger.GenerateLoggerTextForMissingField("Resource", "ARMTemplate"));
                            }
                        }
                    }
                }
                else
                {
                    // Resources section in ARM template is missing/null, generate a message
                    throw new Exception(ExceptionLogger.GenerateLoggerTextForMissingField("RESOURCES", "ARMTemplate"));
                }
            }
            catch (Exception ex)
            {
                // Catch any exception and log the message
                components = null;
                log.AppendLine(ex.Message);
            }

            // Return the list of components obtained for the resources in the ARM template
            return components;
        }
    }
}
