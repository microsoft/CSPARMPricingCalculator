// -----------------------------------------------------------------------
// <copyright file="VMSizeList.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.DataModel
{
    using System.Collections.Generic;
    using Newtonsoft.Json;    

    /// <summary>
    /// Class that defines the deserialized data for the JSON of the ARM API call to fetch the list of all available virtual machine sizes
    /// </summary>
    public class VMSizeList
    {
        [JsonProperty("value")]
        public List<VMSizeListItem> Value { get; set; }
    }

    /// <summary>
    /// VMSizeListItem class
    /// </summary>
    public class VMSizeListItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("numberOfCores")]
        public int NumberOfCores { get; set; }

        [JsonProperty("osDiskSizeInMB")]
        public int OsDiskSizeInMB { get; set; }

        [JsonProperty("resourceDiskSizeInMB")]
        public int ResourceDiskSizeInMB { get; set; }

        [JsonProperty("memoryInMB")]
        public int MemoryInMB { get; set; }

        [JsonProperty("maxDataDiskCount")]
        public int MaxDataDiskCount { get; set; }
    }
}
