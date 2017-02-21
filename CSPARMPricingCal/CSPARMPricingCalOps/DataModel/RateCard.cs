// -----------------------------------------------------------------------
// <copyright file="RateCard.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.DataModel
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class that defines the deserialized data for the JSON of the response of CSP Azure Partner Center Rate Card API call
    /// </summary>
    public class RateCard
    {
        [JsonProperty("Meters")]
        public List<Meter> Meters { get; set; }

        [JsonProperty("Currency")]
        public string Currency { get; set; }

        [JsonProperty("Locale")]
        public string Locale { get; set; }

        [JsonProperty("IsTaxIncluded")]
        public bool IsTaxIncluded { get; set; }
    }
}
