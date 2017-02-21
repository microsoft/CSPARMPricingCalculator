// -----------------------------------------------------------------------
// <copyright file="AuthManager.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Runtime.Caching;
    using DataModel;
    using Helper;
    using Newtonsoft.Json;

    /// <summary>
    /// Class that has static methods to fetch the Azure AD Token
    /// </summary>
    public class AuthManager
    {
        /// <summary>
        /// Gets the Azure AD Token using the App + User Authentication option
        /// </summary>
        /// <param name="appId">The Application ID</param>
        /// <param name="userName">UserName of the User</param>
        /// <param name="password">Password of the User</param>
        /// <param name="tenantID">TenantID of the Azure AD from which the token is to be fetched</param>
        /// <param name="isResourcePCAPI">Set to true if resource for token is Partner Center API, false if resource if ARM API</param>
        /// <returns> Returns the Azure AD Token in String format</returns>
        public static string GetAzureADTokenAppUser(string appId, string userName, string password, string tenantID, bool isResourcePCAPI)
        {
            string token = null;
            string cacheitemNameForToken = string.Empty;
            if (isResourcePCAPI)
            {
                cacheitemNameForToken = "AzureADTokenAppUserAuthPC";
            }
            else
            {
                cacheitemNameForToken = "AzureADTokenAppUserAuthARM";
            }

            try
            {
                ObjectCache cache = MemoryCache.Default;

                // Fetch from cache if available
                if (cache.Contains(cacheitemNameForToken))
                {
                    token = cache.Get(cacheitemNameForToken) as string;
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                        client.Timeout = new TimeSpan(0, APIResponseTimeLimitConstants.TokenFetchLimit, 0);
                        string resourceURL;

                        // Set the Resource URL
                        if (isResourcePCAPI)
                        {
                            resourceURL = APIURLConstants.PCAPIUrl;
                        }
                        else
                        {
                            resourceURL = APIURLConstants.ARMAPIResourceURL;
                        }

                        var content = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("resource", resourceURL),
                            new KeyValuePair<string, string>("client_id", appId),
                            new KeyValuePair<string, string>("grant_type", "password"),
                            new KeyValuePair<string, string>("username", userName),
                            new KeyValuePair<string, string>("password", password),
                            new KeyValuePair<string, string>("scope", "openid")
                        });

                        string aadTokenURL = string.Format("{0}/{1}/oauth2/token", APIURLConstants.GraphAPILoginURL, tenantID);
                        Uri uri = new Uri(aadTokenURL);
                        var response = client.PostAsync(uri, content).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            // Get the result and Add to cache
                            string result = response.Content.ReadAsStringAsync().Result;
                            AADTokenDetails details = JsonConvert.DeserializeObject<AADTokenDetails>(result);
                            token = details.Access_token;

                            DateTimeOffset expiryTime = DateTime.Now.AddSeconds(details.Expires_in).AddSeconds(-60);
                            cache.Add(cacheitemNameForToken, token, expiryTime);
                        }
                        else
                        {
                            string jsonResult = response.Content.ReadAsStringAsync().Result;
                            throw new Exception(jsonResult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionLogger.GenerateLoggerTextForOnlineHelperCall("Azure AD Token", ex.Message));
            }

            return token;
        }
    }
}