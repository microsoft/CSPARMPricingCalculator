// -----------------------------------------------------------------------
// <copyright file="VMImageHelper.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util.Helper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using DataModel;
    using Online;

    /// <summary>
    /// Class that has static methods to fetch Azure VM Image information
    /// </summary>
    public class VMImageHelper
    {
        /// <summary>
        /// Contains the list of VM Images with Software Cost Component
        /// </summary>
        private static List<VMImageHavingSoftwareCost> vmImageHavingSoftwareCostList = new List<VMImageHavingSoftwareCost>()
        {
            new VMImageHavingSoftwareCost()
            {
                VMImagePublisherName = "MicrosoftSQLServer",
                VMImageOfferNames = null,
                VMImageOfferNamesExceptions = new List<string>()
                {
                    "SQL2012SP3-WS2012R2-BYOL", "SQL2014SP1-WS2012R2-BYOL", "SQL2016-WS2012R2-BYOL"
                },
                VMImageSKUNames = new List<string>()
                {
                    "Standard", "Web"
                },
                VMImageSKUNamesExceptions = null,
                MeterSubCategoryCoresList = new int[] 
                {
                    4, 6, 8, 12, 16, 20, 24, 32
                },
                VMMeterSubCategoryStrTemplateForFirst = "SQL Server {0} (up to 4 cores)",
                StrTemplateItemsForFirst = new OrderedDictionary()
                {
                    {
                        StrTemplateItemKeys.SKU.ToString(), null
                    }
                },
                VMMeterSubCategoryStrTemplate = "SQL Server {0} ({1} core)",
                StrTemplateItems = new OrderedDictionary()
                {
                    {
                        StrTemplateItemKeys.SKU.ToString(), null
                    },
                    {
                        StrTemplateItemKeys.Cores.ToString(), null
                    }
                }
            },
            new VMImageHavingSoftwareCost()
            {
                VMImagePublisherName = "MicrosoftSQLServer",
                VMImageOfferNames = null,
                VMImageOfferNamesExceptions = new List<string>()
                {
                    "SQL2012SP3-WS2012R2-BYOL", "SQL2014SP1-WS2012R2-BYOL", "SQL2016-WS2012R2-BYOL"
                },
                VMImageSKUNames = new List<string>()
                {
                    "Enterprise"
                },
                VMImageSKUNamesExceptions = null,
                MeterSubCategoryCoresList = new int[] 
                {
                    4, 6, 8, 12, 16, 20, 24, 32
                },
                VMMeterSubCategoryStrTemplateForFirst = "SQL Server {0} (up to 4c)",
                StrTemplateItemsForFirst = new OrderedDictionary()
                {
                    {
                        StrTemplateItemKeys.SKU.ToString(), null
                    }
                },
                VMMeterSubCategoryStrTemplate = "SQL Server {0} ({1} core)",
                StrTemplateItems = new OrderedDictionary()
                {
                    {
                        StrTemplateItemKeys.SKU.ToString(), null
                    },
                    {
                        StrTemplateItemKeys.Cores.ToString(), null
                    }
                }
            },

            new VMImageHavingSoftwareCost()
            {
                VMImagePublisherName = "MicrosoftBizTalkServer",
                VMImageOfferNames = new List<string>()
                {
                    "BizTalk-Server"
                },
                VMImageOfferNamesExceptions = null,
                VMImageSKUNames = new List<string>()
                {
                    "2013-R2-Standard" /*, "2013-Standard" */
                },
                VMImageSKUNamesExceptions = null,
                MeterSubCategoryCoresList = new int[] 
                {
                    4, 6, 8, 12, 16, 20, 24, 32
                },
                VMMeterSubCategoryStrTemplateForFirst = "BizTalk Server Standard (up to 4c)",
                StrTemplateItemsForFirst = null,
                VMMeterSubCategoryStrTemplate = "BizTalk Server Standard ({0} core)",
                StrTemplateItems = new OrderedDictionary()
                {
                    {
                        StrTemplateItemKeys.Cores.ToString(), null
                    }
                }
            },

            new VMImageHavingSoftwareCost()
            {
                VMImagePublisherName = "MicrosoftBizTalkServer",
                VMImageOfferNames = new List<string>()
                {
                    "BizTalk-Server"
                },
                VMImageOfferNamesExceptions = null,
                VMImageSKUNames = new List<string>()
                {
                    "2013-R2-Enterprise" /*, "2013-Enterprise", "2016-Enterprise"  */
                },
                VMImageSKUNamesExceptions = null,
                MeterSubCategoryCoresList = new int[] 
                {
                    4, 6, 8, 12, 16, 20, 24, 32
                },
                VMMeterSubCategoryStrTemplateForFirst = "BizTalk Server Enterprise (upto 4c)",
                StrTemplateItemsForFirst = null,
                VMMeterSubCategoryStrTemplate = "BizTalk Server Enterprise ({0} core)",
                StrTemplateItems = new OrderedDictionary()
                {
                    {
                        StrTemplateItemKeys.Cores.ToString(), null
                    }
                }
            },

            new VMImageHavingSoftwareCost()
            {
                VMImagePublisherName = "MicrosoftRServer",
                VMImageOfferNames = new List<string>()
                {
                    "RServer-Linux"
                },
                VMImageOfferNamesExceptions = null,
                VMImageSKUNames = new List<string>()
                {
                    "Enterprise"
                },
                VMImageSKUNamesExceptions = null,
                MeterSubCategoryCoresList = new int[] 
                {
                    4, 6, 8, 12, 16, 20, 24, 32
                },
                VMMeterSubCategoryStrTemplateForFirst = "MSFT R Server for Linux (up to 4 c)",
                StrTemplateItemsForFirst = null,
                VMMeterSubCategoryStrTemplate = "MSFT R Server for Linux ({0} core)",
                StrTemplateItems = new OrderedDictionary()
                {
                    {
                        StrTemplateItemKeys.Cores.ToString(), null
                    }
                }
            },

            new VMImageHavingSoftwareCost()
            {
                VMImagePublisherName = "RedHat",
                VMImageOfferNames = new List<string>()
                {
                    "RHEL"
                },
                VMImageOfferNamesExceptions = null,
                VMImageSKUNames = null,
                VMImageSKUNamesExceptions = null,
                MeterSubCategoryCoresList = new int[] 
                {
                    1, 2, 4, 6, 8, 12, 16, 20, 24, 32, 64
                },
                VMMeterSubCategoryStrTemplateForFirst = "Red Hat Enterprise Linux ({0} core)",
                StrTemplateItemsForFirst = new OrderedDictionary()
                {
                    {
                        StrTemplateItemKeys.Cores.ToString(), null
                    }
                },
                VMMeterSubCategoryStrTemplate = "Red Hat Enterprise Linux ({0} core)",
                StrTemplateItems = new OrderedDictionary()
                {
                    {
                        StrTemplateItemKeys.Cores.ToString(), null
                    }
                }
            }

            // , new VMImageHavingSoftwareCost()
            // { }
        };

        /// <summary>
        /// Types of template fields which are variables in the Meter SubCategory for Software costs of the Azure VM.
        /// </summary>
        private enum StrTemplateItemKeys
        {
            /// <summary>
            /// Indicates number of Cores.
            /// </summary>
            Cores,

            /// <summary>
            /// Indicates SKU
            /// </summary>
            SKU
        }

        /// <summary>
        /// Gets the Meter SubCategory String for the specified Azure VM Image having Software Cost
        /// </summary>
        /// <param name="cspCreds">CSP Account credentials object. A token will be generated using these credentials and used for making the online ARM API call</param>
        /// <param name="publisher">Publisher of the Azure VM Image</param>
        /// <param name="offer">Offer of the Azure VM Image</param>
        /// <param name="sku">SKU of the Azure VM Image</param>
        /// <param name="vmSize">VM Size String</param>
        /// <param name="location">Azure Location</param>
        /// <returns> Returns the Meter SubCategory for the Software Cost associated with the Azure VM Image</returns>
        public static string GetMeterSubCategoryForVMImageWithSoftwareCost(CSPAccountCreds cspCreds, string publisher, string offer, string sku, string vmSize, string location)
        {
            string meterSubCategory = null;

            try
            {
                // Get Filtered List by Publisher
                List<VMImageHavingSoftwareCost> vmImageFilteredList =
                    vmImageHavingSoftwareCostList.FindAll(x => x.VMImagePublisherName.Equals(publisher, StringComparison.OrdinalIgnoreCase));

                // Get Filtered List by Offer Names, SKU Names
                VMImageHavingSoftwareCost vmImageHavingSoftwareCost = GetVMImageFilteredListbyOfferSKUNames(offer, sku, vmImageHavingSoftwareCostList);

                if (vmImageHavingSoftwareCost != null)
                {
                    int cores = -1, coresListIndex = -1;
                    cores = VMOnlineHelper.GetCoresForVmSize(cspCreds, vmSize, location);
                    if (cores != -1)
                    {
                        coresListIndex = GetLowestIndexForItemInSortedArray(vmImageHavingSoftwareCost.MeterSubCategoryCoresList, cores);
                    }

                    if (coresListIndex == -1)
                    {
                        throw new Exception(ExceptionLogger.GenerateLoggerTextForInternalError("Unsupported number of cores while calculating software cost for the VM Image"));
                    }
                    else
                    {
                        string[] templateParams = null;
                        templateParams = SetValuesForMeterSubCategoryTemplateParams(vmImageHavingSoftwareCost, coresListIndex, sku, cores);

                        if (templateParams != null)
                        {
                            meterSubCategory = coresListIndex == 0 ? string.Format(vmImageHavingSoftwareCost.VMMeterSubCategoryStrTemplateForFirst, templateParams)
                                : string.Format(vmImageHavingSoftwareCost.VMMeterSubCategoryStrTemplate, templateParams);
                        }
                        else
                        {
                            meterSubCategory = coresListIndex == 0 ? vmImageHavingSoftwareCost.VMMeterSubCategoryStrTemplateForFirst
                            : vmImageHavingSoftwareCost.VMMeterSubCategoryStrTemplate;
                        }
                    }
                }
            }
            catch (Exception)
            {             
                throw;
            }

            return meterSubCategory;
        }

        /// <summary>
        /// Get the Software details for the Specified Offer and SKU of the Azure VM Image
        /// </summary>
        /// <param name="offer">Offer of the Azure VM Image</param>
        /// <param name="sku">SKU of the Azure VM Image</param>
        /// <param name="vmImageList">List of VM Image Info with Software Cost</param>
        /// <returns> Returns the details of the VM Image having Software Cost if matched. If no match is found, null is returned</returns>
        private static VMImageHavingSoftwareCost GetVMImageFilteredListbyOfferSKUNames(string offer, string sku, List<VMImageHavingSoftwareCost> vmImageList)
        {
            VMImageHavingSoftwareCost vmImageFilteredList = null;

            foreach (VMImageHavingSoftwareCost vmImage in vmImageList)
            {
                // Check if in Exception list of Offer/SKU, If so continue with next listitem in foreach loop
                if (ListContainsStringIgnoreCase(vmImage.VMImageOfferNamesExceptions, offer) || ListContainsStringIgnoreCase(vmImage.VMImageSKUNamesExceptions, sku))
                {
                    continue;
                }

                // Check if In Offer List, If not continue with next listitem in foreach loop
                bool isInOfferList = false;
                isInOfferList = vmImage.VMImageOfferNames == null ? true : ListContainsStringIgnoreCase(vmImage.VMImageOfferNames, offer);
                if (!isInOfferList)
                {
                    continue;
                }

                // Check if In SKU List
                bool isInSKUList = false;
                isInSKUList = vmImage.VMImageSKUNames == null ? true : ListContainsStringIgnoreCase(vmImage.VMImageSKUNames, sku);

                // If found, set the object to be returned
                if (isInOfferList && isInSKUList)
                {
                    vmImageFilteredList = vmImage;
                    break;
                }
            }

            return vmImageFilteredList;
        }

        /// <summary>
        /// A method to return the lowest index for a given item in the sorted array of number. This is used to match the number of cores with the one in the Price Specs String
        /// </summary>
        /// <param name="sortedArrayOfNumbers">Sorted integer array of numbers</param>
        /// <param name="item">Integer number to be matched</param>
        /// <returns> Returns the lowest index for a match</returns>
        private static int GetLowestIndexForItemInSortedArray(int[] sortedArrayOfNumbers, int item)
        {
            int index = -1;
            for (int i = 0; i < sortedArrayOfNumbers.Length; i++)
            {
                if (item <= sortedArrayOfNumbers[i])
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        /// <summary>
        /// A method to check if a list of strings contains a certain string by ignoring case. This is used for comparing Offer and SKU Names.
        /// </summary>
        /// <param name="list">List of Strings</param>
        /// <param name="str">String to be matched</param>
        /// <returns> Returns true if String is found in list, false if not</returns>
        private static bool ListContainsStringIgnoreCase(List<string> list, string str)
        {
            bool isListContainsStr = false;
            if (list != null && list.Count > 0)
            {
                foreach (string listItem in list)
                {
                    if (listItem.Equals(str, StringComparison.OrdinalIgnoreCase))
                    {
                        isListContainsStr = true;
                        break;
                    }
                }
            }

            return isListContainsStr;
        }

        /// <summary>
        /// Gets the array of Strings having values for the template variables to form the Meter SubCategory for the Software cost component of the Azure VM Image
        /// </summary>
        /// <param name="vmImageHavingSoftwareCost">Details of the VM Image having software cost</param>
        /// <param name="coresListIndex">Index of the item in the list of Software Cost. This is used to check if its first in the list. </param>
        /// <param name="sku">SKU of the Azure VM Image.</param>
        /// <param name="cores">Number of cores of the Azure VM image. </param>
        /// <returns> Returns the array of Strings having values for the template variables to form the Meter SubCategory</returns>
        private static string[] SetValuesForMeterSubCategoryTemplateParams(VMImageHavingSoftwareCost vmImageHavingSoftwareCost, int coresListIndex, string sku, int cores)
        {
            string[] templateParams = null;
            OrderedDictionary strTemplateItems = coresListIndex == 0 ? vmImageHavingSoftwareCost.StrTemplateItemsForFirst : vmImageHavingSoftwareCost.StrTemplateItems;
            if (strTemplateItems != null && strTemplateItems.Count != 0)
            {
                if (strTemplateItems.Contains(StrTemplateItemKeys.Cores.ToString()))
                {
                    strTemplateItems[StrTemplateItemKeys.Cores.ToString()] = cores.ToString();
                }

                if (strTemplateItems.Contains(StrTemplateItemKeys.SKU.ToString()))
                {
                    strTemplateItems[StrTemplateItemKeys.SKU.ToString()] = sku;
                }

                templateParams = new string[strTemplateItems.Count];
                ICollection valueCollection = strTemplateItems.Values;
                valueCollection.CopyTo(templateParams, 0);
            }

            return templateParams;
        }

        /// <summary>
        /// Internal Class that contains the information of software cost associated with certain VM Images
        /// </summary>
        private class VMImageHavingSoftwareCost
        {
            /// <summary>
            /// Gets or sets the Azure VM Image Publisher Name
            /// </summary>
            public string VMImagePublisherName { get; set; }

            /// <summary>
            /// Gets or sets the Azure VM Image Offer Name
            /// </summary>
            public List<string> VMImageOfferNames { get; set; }

            /// <summary>
            /// Gets or sets the Azure VM Image Offer Name having exceptions
            /// </summary>
            public List<string> VMImageOfferNamesExceptions { get; set; }

            /// <summary>
            /// Gets or sets the Azure VM Image SKU Name List
            /// </summary>
            public List<string> VMImageSKUNames { get; set; }

            /// <summary>
            /// Gets or sets the Azure VM Image SKU Name List having exceptions
            /// </summary>
            public List<string> VMImageSKUNamesExceptions { get; set; }

            /// <summary>
            /// Gets or sets the list of cores associated with the software cost
            /// </summary>
            public int[] MeterSubCategoryCoresList { get; set; }

            /// <summary>
            /// Gets or sets the template for the VM Meter SubCategory
            /// </summary>
            public string VMMeterSubCategoryStrTemplate { get; set; }

            /// <summary>
            /// Gets or sets the template for the first VM Meter SubCategory value
            /// </summary>
            public string VMMeterSubCategoryStrTemplateForFirst { get; set; }

            /// <summary>
            /// Gets or sets the variables in the template for the first VM Meter SubCategory value
            /// </summary>
            public OrderedDictionary StrTemplateItemsForFirst { get; set; }

            /// <summary>
            /// Gets or sets the variables in the template for the VM Meter SubCategory value
            /// </summary>
            public OrderedDictionary StrTemplateItems { get; set; }
        }
    }
}
