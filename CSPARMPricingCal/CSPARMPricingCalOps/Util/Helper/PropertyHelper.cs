// -----------------------------------------------------------------------
// <copyright file="PropertyHelper.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using DataModel;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Helper Class that has static methods to fetch property fields from the ARM Template
    /// </summary>
    public class PropertyHelper
    {
        /// <summary>
        /// Fetches the Property value for the specified property from the Properties section
        /// </summary>
        /// <param name="properties">Properties object of the resource</param>
        /// <param name="propertyPath">Property whose value is to be fetched</param>
        /// <returns> Returns the Property value in String format</returns>
        public static string GetPropertyValue(JObject properties, string propertyPath)
        {
            string propertyValue = null;

            // Check if property exists, and then, fetch the value
            if (IsPropertyExists(properties, propertyPath))
            {
                propertyValue = (string)properties.SelectToken(propertyPath);
            }

            return propertyValue;
        }

        /// <summary>
        /// Fetches the property value from a Variable or Parameter
        /// </summary>
        /// <param name="variableOrParamName">Name of the property containing the variable or parameter</param>
        /// <param name="variables">The Variables object of the ARM template</param>
        /// <param name="parameters">The Parameters object of the ARM template</param>
        /// <param name="paramValue">The object containing the Parameter values</param>
        /// <returns> Returns the Property value in String format</returns>
        public static string GetValueIfVariableOrParam(string variableOrParamName, JObject variables, JObject parameters, JObject paramValue)
        {
            string propertyValue = null;
            if (variableOrParamName != null)
            {
                // Check if its a variable or parameter
                if (variableOrParamName.ToUpper().Contains("VARIABLES"))
                {
                    propertyValue = GetPropertyValueFromVariables(variableOrParamName, variables);
                }
                else if (variableOrParamName.ToUpper().Contains("PARAMETERS"))
                {
                    propertyValue = GetPropertyValueFromParamValue(variableOrParamName, parameters, paramValue);
                }
                else
                {
                    propertyValue = variableOrParamName;
                }
            }

            return propertyValue;
        }

        /// <summary>
        /// Fetches the resource in the list of resources of the ARM Template
        /// </summary>
        /// <param name="resourceList">List of the resources of the ARM Template</param>
        /// <param name="propertyContainingName">The property that has the name of the resource</param>
        /// <returns> Returns the resource object</returns>
        public static Resource SearchResourceInListByName(List<Resource> resourceList, string propertyContainingName)
        {
            Resource res = null;

            // Check if property has parameter or variable
            if (propertyContainingName.ToUpper().Contains("PARAMETERS") || propertyContainingName.ToUpper().Contains("VARIABLES"))
            {
                // Remove Square Brackets from PropertyName
                Regex r = new Regex("^[[](.+)[]]$", RegexOptions.IgnoreCase);

                // Iterate thru the resource list
                foreach (var resource in resourceList)
                {
                    Match m = r.Match(resource.Name);

                    // Check if success and get the matching resource
                    if (m.Success)
                    {
                        if ((m.Groups.Count > 1) && propertyContainingName.Contains(m.Groups[1].Value))
                        {
                            res = resource;
                            break;
                        }
                    }
                }
            }
            else
            {
                // Get the resource from the resource list
                res = resourceList.Find(x => propertyContainingName.Contains(x.Name));
            }

            return res;
        }

        /// <summary>
        /// Check if specified property exists in the properties section of the resource of the ARM Template
        /// </summary>
        /// <param name="properties"> Properties object in the resource</param>
        /// <param name="propertyPath"> Property to be checked</param>
        /// <returns> Returns a value indicating if the property exists or not</returns>
        public static bool IsPropertyExists(JObject properties, string propertyPath)
        {
            bool isExists = false;

                // Check if property exists
                if (properties != null && properties.SelectToken(propertyPath) != null)
                {
                    isExists = true;
                }

            return isExists;
        }

        /// <summary>
        /// Fetches the property value from a Variable
        /// </summary>
        /// <param name="propertyValue">Name of the property containing the variable</param>
        /// <param name="variables">The Variables object of the ARM template</param>
        /// <returns> Returns the Property value in String format</returns>
        private static string GetPropertyValueFromVariables(string propertyValue, JObject variables)
        {
            string variableValue = null;
            if (variables != null)
            {
                Regex r = new Regex(Constants.VariableNameRegexString, RegexOptions.IgnoreCase);
                Match m = r.Match(propertyValue);

                // Extract the variable name from the property and get the value of the variable
                if (m.Success)
                {
                    if (m.Groups.Count > 1)
                    {
                        string variableName = m.Groups[1].Value;
                        variableValue = GetPropertyValue(variables, variableName);
                    }
                }
            }

            return variableValue;
        }

        /// <summary>
        /// Fetches the property value from a Parameter
        /// </summary>
        /// <param name="propertyValue">Name of the property containing the parameter</param>
        /// <param name="parameters">The Parameter object of the ARM template</param>
        /// <param name="paramValue">The object containing the Parameter values</param>
        /// <returns> Returns the Property value in String format</returns>
        private static string GetPropertyValueFromParamValue(string propertyValue, JObject parameters, JObject paramValue)
        {
            string paramValueExtract = null;
            if (parameters != null)
            {
                Regex r = new Regex(Constants.ParamNameRegexString, RegexOptions.IgnoreCase);
                Match m = r.Match(propertyValue);

                // Extract the parameter name from the property and get the value of the parameter
                if (m.Success)
                {
                    if (m.Groups.Count > 1)
                    {
                        string paramName = m.Groups[1].Value;
                        string paramPropertyValue = paramName + Constants.ParamValueString;
                        string paramDefaultPropertyName = paramName + Constants.ParamDefValueString;

                        if (paramValue != null)
                        {
                            paramValueExtract = GetPropertyValue(paramValue, paramPropertyValue);
                        }

                        if (paramValueExtract == null)
                        {
                            paramValueExtract = GetPropertyValue(parameters, paramDefaultPropertyName);
                        }
                    }
                }
            }

            return paramValueExtract;
        }
    }
}
