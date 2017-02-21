// -----------------------------------------------------------------------
// <copyright file="AADTokenDetails.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.DataModel
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class that defines the deserialized data for the JSON of the Azure AD Token
    /// </summary>
    public class AADTokenDetails
    {
        [JsonProperty("token_type")]
        public string Token_type { get; set; }

        [JsonProperty("expires_in")]
        public int Expires_in { get; set; }

        [JsonProperty("expires_on")]
        public string Expires_on { get; set; }

        [JsonProperty("not_before")]
        public string Not_before { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("access_token")]
        public string Access_token { get; set; }
    }
}
