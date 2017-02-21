// -----------------------------------------------------------------------
// <copyright file="RateCardUtil.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using DataModel;
    using Helper;
    using Newtonsoft.Json;

    /// <summary>
    /// Class that has static method to fetch the Azure CSP Rate Card via Partner Center API
    /// </summary>
    public class RateCardUtil
    {
        /// <summary>
        /// Gets the Meters for the Azure CSP Rate Card
        /// </summary>
        /// <param name="cspCreds">CSP Account credentials object. A token will be generated using these credentials and used for making the online Partner Center API call</param>
        /// <returns> Returns the list of Azure Meters</returns>
        public static List<Meter> GetRateCard(CSPAccountCreds cspCreds)
        {
            List<Meter> meterList = null;

            try
            {
                if (cspCreds == null)
                {
                    throw new Exception(ExceptionLogger.GenerateLoggerTextForInternalError("CSP Account Credentials is null"));
                }

                // Fetch the AzureAD Token
                string aadToken = AuthManager.GetAzureADTokenAppUser(cspCreds.CSPClientId, cspCreds.CSPAdminAgentUserName, cspCreds.CSPAdminAgentPassword, cspCreds.CSPResellerTenantID, true);

                // Create the HttpClient Object
                HttpClient client = new HttpClient();

                // Set the request header values
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + aadToken);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("MS-CorrelationId", Guid.NewGuid().ToString());
                client.DefaultRequestHeaders.Add("MS-RequestId", Guid.NewGuid().ToString());
                client.DefaultRequestHeaders.Add("X-Locale", Constants.CSPLocale);
                client.Timeout = new TimeSpan(0, APIResponseTimeLimitConstants.RateCardFetchLimit, 0);
                
                // Set the path
                var path = string.Format("{0}/v1/ratecards/azure?currency={1}&region={2}", APIURLConstants.PCAPIUrl, cspCreds.CSPCurrency, cspCreds.CSPRegion);
                Uri uri = new Uri(path);

                // Make the API Call to fetch the Rate Card
                HttpResponseMessage response = client.GetAsync(uri).Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = response.Content.ReadAsStringAsync().Result;
                    RateCard card = JsonConvert.DeserializeObject<RateCard>(jsonResult);
                    meterList = card.Meters;
                }
                else
                {
                    string jsonResult = response.Content.ReadAsStringAsync().Result;
                    new Exception(ExceptionLogger.GenerateLoggerTextForOnlineHelperCall("CSP Rate Card", string.Format("Error while fetching the Rate Card: {0}", jsonResult)));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return meterList;
        }
    }
}
