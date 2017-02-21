// -----------------------------------------------------------------------
// <copyright file="Resource.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.DataModel
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that defines the deserialized data for the JSON of an ARM resource in an ARM Template
    /// </summary>
    public class Resource
    {
        /// <summary>
        /// Gets or sets the type of the resource in the ARM Template.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the resource in the ARM Template.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the API Version of the resource in the ARM Template.
        /// </summary>
        [JsonProperty("apiVersion")]
        public string ApiVersion { get; set; }

        /// <summary>
        /// Gets or sets the Location of the resource in the ARM Template.
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the properties section of the resource in the ARM Template.
        /// </summary>
        [JsonProperty("properties")]
        public JObject Properties { get; set; }

        /// <summary>
        /// Gets or sets the depends On section of the resource in the ARM Template.
        /// </summary>
        [JsonProperty("dependsOn")]
        public List<string> DependsOn { get; set; }
    }
}
