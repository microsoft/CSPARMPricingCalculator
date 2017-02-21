// -----------------------------------------------------------------------
// <copyright file="VMSKUVersion.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.DataModel
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class that defines the deserialized data for the JSON of the ARM API call to fetch the information of a Virtual machine image
    /// </summary>
    public class VMSKUVersion
    {
        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    /// <summary>
    /// OSDiskImage class
    /// </summary>
    public class OSDiskImage
    {
        [JsonProperty("operatingSystem")]
        public string OperatingSystem { get; set; }
    }

    /// <summary>
    /// Properties class
    /// </summary>
    public class Properties
    {
        [JsonProperty("osDiskImage")]
        public OSDiskImage OsDiskImage { get; set; }

        [JsonProperty("dataDiskImages")]
        public List<object> DataDiskImages { get; set; }
    } 
}
