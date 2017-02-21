// -----------------------------------------------------------------------
// <copyright file="ARMParamValue.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.DataModel
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that defines the deserialized data for the JSON of the Parameter file for an ARM Template deployment
    /// </summary>
    public class ARMParamValue
    {
        [JsonProperty("contentVersion")]
        public string ContentVersion { get; set; }

        [JsonProperty("parameters")]
        public JObject Parameters { get; set; }
    }
}
