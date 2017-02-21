// -----------------------------------------------------------------------
// <copyright file="StorageProperties.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.DataModel.ComponentModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class that defines the deserialized data for the JSON in properties section for the ARM resource of "type": "Microsoft.Storage/storageAccounts" in an ARM Template
    /// ARM Resource API Version: 2015-06-15
    /// </summary>
    public class StorageProperties
    {
        [JsonProperty("accountType")]
        public string AccountType { get; set; }
    }
}
