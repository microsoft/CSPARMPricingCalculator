// -----------------------------------------------------------------------
// <copyright file="ARMAPIHelper.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util.Helper.Online
{
    using System;
    using System.Net.Http;
    using Newtonsoft.Json;

    /// <summary>
    /// Helper class that has static method to assist in making ARM API calls
    /// </summary>
    public class ARMAPIHelper
    {
        /// <summary>
        /// Performs a "GET" ARM REST API call
        /// </summary>
        /// <param name="token">Azure AD Token for making the ARM API call in String format</param>
        /// <param name="url">URL of ARM API in a String format</param>
        /// <param name="timeOut">Timeout in minutes that needs to be set for the client making HTTP request</param>
        /// <typeparam name="T">The class type to be returned from the API call.</typeparam>
        /// <returns> Deserialized Object of Type T for the JSON response obtained from the ARM API call</returns>
        public static T GetARMCall<T>(string token, string url, int timeOut)
        {
            T obj = default(T);
            try
            {
                // Create the HTTPClient object to be used for performing the ARM API call
                HttpClient client = new HttpClient();

                // Set the headers of the HTTP request to be sent
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("x-ms-request-id", Guid.NewGuid().ToString());

                // Set the timeout of the HTTP request
                client.Timeout = new TimeSpan(0, timeOut, 0);

                // Create the URI object for the HTTP request with the specified URL String
                Uri uri = new Uri(url);

                // Make the GET ARM REST API call
                HttpResponseMessage response = client.GetAsync(uri).Result;
                
                // Check if HTTP response is success
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = response.Content.ReadAsStringAsync().Result;

                    // Deserialize and get the object
                    obj = JsonConvert.DeserializeObject<T>(jsonResult);
                }
                else
                {
                    // Get the HTTP error message and throw an exception
                    string jsonResult = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(string.Format("ARM API Call Response: {0}", jsonResult));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return obj;
        }
    }
}
