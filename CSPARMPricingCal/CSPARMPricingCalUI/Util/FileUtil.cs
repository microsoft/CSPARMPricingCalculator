// -----------------------------------------------------------------------
// <copyright file="FileUtil.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalUI.Util
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using CSPARMPricingCalOps.DataModel;
    using Newtonsoft.Json;

    /// <summary>
    /// Class that has static method to do file operations including read template and parameter files and write the output to a CSV
    /// </summary>
    public class FileUtil
    {
        /// <summary>
        /// Read the ARM Template JSON file and convert to object
        /// </summary>
        /// <param name="armTemplateFilePath">Path to the file of the ARM Template</param>
        /// <returns> Deserialized Object of Type ARMTemplate for the ARM Template file</returns> 
        public static ARMTemplate GetResourceList(string armTemplateFilePath)
        {
            ARMTemplate template;
            try
            {
                // Read the ARM Template file and convert to the template object
                using (StreamReader r = new StreamReader(armTemplateFilePath))
                {
                    string json = r.ReadToEnd();
                    template = JsonConvert.DeserializeObject<ARMTemplate>(json);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return template;
        }

        /// <summary>
        /// Read the ARM Parameter JSON file and convert to object
        /// </summary>
        /// <param name="armParamFilePath">Path to the file of the ARM Parameters values</param>
        /// <returns>Deserialized Object of Type ARMParamValue for the ARM Template file</returns> 
        public static ARMParamValue GetParamValueList(string armParamFilePath)
        {
            ARMParamValue paramValue = null;

            // Check if filepath null, return null
            if (armParamFilePath == null)
            {
                return null;
            }

            try
            {
                // Read the ARM Parameter value file and convert to the parameter object
                using (StreamReader r = new StreamReader(armParamFilePath))
                {
                    string json = r.ReadToEnd();
                    paramValue = JsonConvert.DeserializeObject<ARMParamValue>(json);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return paramValue;
        }

        /// <summary>
        /// Writes the Result of the ARM Pricing Calculation to a file
        /// </summary>
        /// <param name="fileName">Path to the file</param>
        /// <param name="cspARMPricingInfoOutput">Object containing the result of the ARM Pricing Calculation</param>
        /// <param name="totalCost">Total Cost</param>
        public static void ExportCSPARMPricingToCSV(string fileName, CSPARMPricingInfo cspARMPricingInfoOutput, double totalCost)
        {
            List<ResourceComponent> cspARMPricingList = cspARMPricingInfoOutput.CSPARMPricingList;
            StringBuilder sb = new StringBuilder();

            // Add header row
            string line = "Resource Name,Resource Type,Meter Name,Meter Category,Meter SubCategory,Is Chargeable?,Quantity,Rate,Cost Per Month";
            sb.AppendLine(line);

            // Add Resource Component rows with pricing
            for (int i = 0; i < cspARMPricingList.Count; i++)
            {
                line = string.Format("\"{0}\",{1},\"{2}\",\"{3}\",\"{4}\",{5},{6},{7},{8}", cspARMPricingList[i].ResourceName, cspARMPricingList[i].ResourceType, cspARMPricingList[i].MeterName, cspARMPricingList[i].MeterCategory, cspARMPricingList[i].MeterSubCategory, cspARMPricingList[i].IsChargeable.ToString(), cspARMPricingList[i].Quantity.ToString(), cspARMPricingList[i].Rate, cspARMPricingList[i].CostPerMonth.ToString());
                sb.AppendLine(line);
            }

            // Add Total row
            line = string.Format("Total Cost:,,,,,,,,{0}", totalCost.ToString());
            sb.AppendLine(line);

            try
            {
                // Write to File
                File.WriteAllText(fileName, sb.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
