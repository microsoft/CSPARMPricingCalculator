// -----------------------------------------------------------------------
// <copyright file="VMSKUVersionListItem.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.DataModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class that defines the deserialized data for the list item object in the JSON of the ARM API call to fetch the list of Virtual machine image versions
    /// </summary>
    public class VMSKUVersionListItem
    {
        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
