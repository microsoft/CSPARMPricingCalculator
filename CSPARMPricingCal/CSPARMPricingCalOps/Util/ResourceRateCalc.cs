// -----------------------------------------------------------------------
// <copyright file="ResourceRateCalc.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using DataModel;
    using Helper;

    /// <summary>
    /// Class that has static method to fetch the Azure Meters for the Resource components and perform calculation of monthly estimates
    /// </summary>
    public class ResourceRateCalc
    {
        /// <summary>
        /// Variable to store the meter list for Azure CSP Rate Card
        /// </summary>
        private static List<Meter> meterList;

        /// <summary>
        /// Calculates the monthly estimates for the resource components
        /// </summary>
        /// <param name="components">List of Resource Components</param>
        /// <param name="location">Azure Location</param>
        /// <param name="meterList">List of Meters for Azure CSP Rate Card</param>
        /// <param name="log">The object that will contain the exception messages</param>
        /// <returns> Returns the list of resource components with associated meters and monthly estimates</returns>
        public static List<ResourceComponent> CalculateResourceComponentRates(List<ResourceComponent> components, string location, List<Meter> meterList, out StringBuilder log)
        {
            ResourceRateCalc.meterList = meterList;
            log = new StringBuilder(string.Empty);

            string locationAsPerPricingSpecs = null;
            try
            {
                // Fetch the Location as per Pricing Specs, Throw exception is cannot be mapped
                if (!LocationConstants.LocationAsPerPricingSpecsMap.TryGetValue(location, out locationAsPerPricingSpecs))
                {
                    throw new Exception(ExceptionLogger.GenerateLoggerTextForInternalError("Location could not be mapped as per pricing specs"));
                }

                // Loop thru the list of resource components
                foreach (ResourceComponent component in components)
                {
                    // Check if the component is chargeable
                    if (component.IsChargeable)
                    {
                        string meterRegion = locationAsPerPricingSpecs;
                        
                        // If Current resource component if for Networking Bandwidth, Set the region to Zone by getting value from mapping, Throw exception is not found
                        if (NetworkingResourceConstants.NetworkingMeterNameForVMNetwork.Equals(component.MeterName, StringComparison.OrdinalIgnoreCase))
                        {
                            string zone = null;
                            if (LocationConstants.LocationZoneMap.TryGetValue(locationAsPerPricingSpecs, out zone))
                            {
                                meterRegion = zone;
                            }
                            else
                            {
                                log.AppendLine(ExceptionLogger.GenerateLoggerTextForInternalError("Location could not be mapped to a networking zone as per pricing specs"));
                                component.CostPerMonth = 0;
                                component.Rate = 0;
                                continue;
                            }
                        }

                        double rate = 0;

                        // Get the Rate and Monthly Cost estimates for the current resource component
                        component.CostPerMonth = GetResourceRate(component.MeterCategory, component.MeterSubCategory, component.MeterName, meterRegion, component.Quantity, out rate, ref log, component.ResourceName);
                        component.Rate = rate;
                    }
                    else
                    {
                        // If component is not chargeable, set to rate and monthly cost to zero
                        component.CostPerMonth = 0;
                        component.Rate = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                components = null;
                log.AppendLine(ex.Message);
            }

            return components;
        }

        /// <summary>
        /// Gets the associated Rate and calculates the monthly estimates for the specified Meter details
        /// </summary>
        /// <param name="meterCategory">Meter Category of the Meter</param>
        /// <param name="meterSubCategory">Meter SubCategory of the Meter</param>
        /// <param name="meterName">Meter Name of the Meter</param>
        /// <param name="region">Azure Location as per Pricing Specs</param>
        /// <param name="quantity">Quantity of the resource usage in a month</param>
        /// <param name="rate">Rate will be set based on the associated meter in CSP Rate Card</param>
        /// <param name="log">The object that will contain the exception messages</param>
        /// <param name="nameOfResource">Name of the Azure resource</param>
        /// <returns> Returns the Monthly Cost for the resource</returns>
        private static double GetResourceRate(string meterCategory, string meterSubCategory, string meterName, string region, double quantity, out double rate, ref StringBuilder log, string nameOfResource)
        {
            double totalCost = 0;
            rate = 0;

            try
            {
                // If any meter fields are missing throw an exception
                if (meterCategory == null)
                {
                    throw new Exception(ExceptionLogger.GenerateLoggerTextForMeterRateFetchFailed("MeterCategory is null", nameOfResource));
                }

                if (meterSubCategory == null)
                {
                    throw new Exception(ExceptionLogger.GenerateLoggerTextForMeterRateFetchFailed("MeterSubCategory is null", nameOfResource));
                }

                if (meterName == null)
                {
                    throw new Exception(ExceptionLogger.GenerateLoggerTextForMeterRateFetchFailed("MeterName is null", nameOfResource));
                }

                if (region == null)
                {
                    throw new Exception(ExceptionLogger.GenerateLoggerTextForMeterRateFetchFailed("Region is null", nameOfResource));
                }

                // Fetch the rate for the resource component by matching the meter fields provided
                Meter ratesForComponent = meterList.Find(x => meterCategory.Equals(x.MeterCategory, StringComparison.OrdinalIgnoreCase)
                                        && meterSubCategory.Equals(x.MeterSubCategory, StringComparison.OrdinalIgnoreCase)
                                        && meterName.Equals(x.MeterName, StringComparison.OrdinalIgnoreCase)
                                        && region.Equals(x.MeterRegion, StringComparison.OrdinalIgnoreCase));

                // If the Meter is missing in CSP Rate Card, check if there is a global rate (Rate without region specified)
                if (ratesForComponent == null)
                {
                    ratesForComponent = meterList.Find(x => meterCategory.Equals(x.MeterCategory, StringComparison.OrdinalIgnoreCase)
                                        && meterSubCategory.Equals(x.MeterSubCategory, StringComparison.OrdinalIgnoreCase)
                                        && meterName.Equals(x.MeterName, StringComparison.OrdinalIgnoreCase)
                                        && x.MeterRegion.Equals(string.Empty, StringComparison.OrdinalIgnoreCase));
                }

                // Check if Meter found
                if (ratesForComponent != null)
                {
                    // Fetch the Rates from the meter found
                    OrderedDictionary meterRates = ratesForComponent.MeterRates;

                    // If consumed quantity if less that included quantity, set rate and cost to zero
                    if (quantity <= ratesForComponent.IncludedQuantity)
                    {
                        totalCost = 0;
                        rate = 0;
                    }
                    else if (meterRates.Count == 1)
                    {
                        // If linear rating, Calculate Monthly Cost as quantity x rate
                        rate = (double)meterRates[0];
                        totalCost = (quantity - ratesForComponent.IncludedQuantity) * rate;
                    }
                    else
                    {
                        // If tiered rating, calculate Monthly Cost based on tiered pricing
                        double quantitytobeRated = quantity - ratesForComponent.IncludedQuantity;
                        totalCost = 0;

                        for (int i = 1; i < meterRates.Count; i++)
                        {
                            string currentKeyString = (string)meterRates.Cast<DictionaryEntry>().ElementAt(i).Key;
                            string previousKeyString = (string)meterRates.Cast<DictionaryEntry>().ElementAt(i - 1).Key;
                            double currentKey = double.Parse(currentKeyString);
                            double previousKey = double.Parse(previousKeyString);

                            if (quantitytobeRated <= currentKey)
                            {
                                totalCost = totalCost + ((quantitytobeRated - previousKey) * (double)meterRates[i - 1]);
                                rate = (double)meterRates[i - 1];
                                break;
                            }
                            else
                            {
                                totalCost = totalCost + ((currentKey - previousKey) * (double)meterRates[i - 1]);
                                if (i == meterRates.Count - 1)
                                {
                                    totalCost = totalCost + ((quantitytobeRated - currentKey) * (double)meterRates[i]);
                                    rate = (double)meterRates[i];
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Meter for specified parameters not found, Throw an exception
                    throw new Exception(ExceptionLogger.GenerateLoggerTextForMeterRateFetchFailed(string.Format("Meter not found in Rate Card. MeterCategory:{0},MeterSubCategory:{1},MeterName:{2},Region:{3} ", meterCategory, meterSubCategory, meterName, region), nameOfResource));
                }
            }
            catch (Exception ex)
            {
                totalCost = 0;
                log.AppendLine(ex.Message);
            }

            return totalCost;
        }
    }
}
