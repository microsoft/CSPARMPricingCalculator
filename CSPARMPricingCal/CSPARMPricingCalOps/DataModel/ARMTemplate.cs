// -----------------------------------------------------------------------
// <copyright file="ARMTemplate.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.DataModel
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that defines the deserialized data for the JSON of the ARM Template file for an ARM Template deployment
    /// </summary>
    public class ARMTemplate
    {
        [JsonProperty("contentVersion")]
        public string ContentVersion { get; set; }

        [JsonProperty("parameters")]
        public JObject Parameters { get; set; }

        [JsonProperty("variables")]
        public JObject Variables { get; set; }

        [JsonProperty("resources")]
        public List<Resource> Resources { get; set; }
    }
}
