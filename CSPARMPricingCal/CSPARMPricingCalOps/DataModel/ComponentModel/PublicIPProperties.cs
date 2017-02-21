// -----------------------------------------------------------------------
// <copyright file="PublicIPProperties.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.DataModel.ComponentModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class that defines the deserialized data for the JSON in properties section for the ARM resource of "type": "Microsoft.Network/publicIPAddresses" in an ARM Template
    /// ARM Resource API Version: 2015-06-15
    /// </summary>
    public class PublicIPProperties
    {
        [JsonProperty("publicIPAllocationMethod")]
        public string PublicIPAllocationMethod { get; set; }

        [JsonProperty("dnsSettings")]
        public DNSSettings DnsSettings { get; set; }
    }

    /// <summary>
    /// DNSSettings class
    /// </summary>
    public class DNSSettings
    {
        [JsonProperty("domainNameLabel")]
        public string DomainNameLabel { get; set; }
    }
}
