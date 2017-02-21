// -----------------------------------------------------------------------
// <copyright file="Constants.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util
{
    using System.Collections.Generic;

    /// <summary>
    /// Class contains the constant values used in the application
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// CSP Locale of the CSP Partner
        /// </summary>
        public const string CSPLocale = "en-US";

        /// <summary>
        /// String to get Default value of a parameter for a property
        /// </summary>
        public const string ParamDefValueString = ".defaultValue";

        /// <summary>
        /// String to get value of a parameter for a property
        /// </summary>
        public const string ParamValueString = ".value";

        /// <summary>
        /// Regex to fetch variable Name from a Property
        /// </summary>
        public const string VariableNameRegexString = @"^\[variables\('(.+)'\)\]$";

        /// <summary>
        /// Regex to fetch Parameter Name from a Property
        /// </summary>
        public const string ParamNameRegexString = @"^\[parameters\('(.+)'\)\]$";

        /// <summary>
        /// Hours in a month, used for calculating monthly cost estimates
        /// </summary>
        public const double HoursinaMonth = 744;
    }

    /// <summary>
    /// Class contains the constant values for custom exception messages generated 
    /// </summary>
    public class LogMessagesConstants
    {
        /// <summary>
        /// Exception message for Missing Field
        /// </summary>
        public const string MissingFieldMsg = "Error Occured, Field: '{0}' missing or unable to read value in Resource:'{1}'";

        /// <summary>
        /// Exception message for Invalid value for a Field
        /// </summary>
        public const string InvalidFieldMsg = "Error Occured, Field: '{0}' invalid value:'{1}' in Resource:'{2}'";

        /// <summary>
        /// Exception message for Unsupported resource type
        /// </summary>
        public const string ResourceNotSupportedMsg = "Error Occured, Resource:'{0}' not supported by the tool";

        /// <summary>
        /// Exception message for no resource components returned
        /// </summary>
        public const string ResourceComponentsNoOutputMsg = "Error Occured, Unable to fetch components for Resource:'{0}'";

        /// <summary>
        /// Exception message for Read failed for a field
        /// </summary>
        public const string PropertiesReadFailedMsg = "Error Occured, Unable to read/convert Properties section for Resource:'{0}'";

        /// <summary>
        /// Exception message for Online API Call Failure
        /// </summary>
        public const string PropertiesOnlineCallFailedMsg = "Error Occured while fetching '{0}' Online. {1}";

        /// <summary>
        /// Exception message for Internal Error
        /// </summary>
        public const string InternalErrorMsg = "Internal Error Occured {0}";

        /// <summary>
        /// Exception message for Meter Rate Fetch Failure
        /// </summary>
        public const string MeterRateFetchFailedMsg = "Error Occured while fetching MeterRate as '{0}' for Resource: '{1}'";
    }

    /// <summary>
    /// Class contains the constant values for URLs of the API
    /// </summary>
    public class APIURLConstants
    {
        /// <summary>
        /// URL to fetch the Azure AD Token
        /// </summary>
        public const string GraphAPILoginURL = "https://login.windows.net";

        /// <summary>
        /// Resource of Graph API
        /// </summary>
        public const string GraphAPIResourceURL = "https://graph.windows.net";

        /// <summary>
        /// Resource of ARM API
        /// </summary>
        public const string ARMAPIResourceURL = "https://management.azure.com/";

        /// <summary>
        /// ARM API URL
        /// </summary>
        public const string ARMAPIURL = "https://management.azure.com";

        /// <summary>
        /// Compute Version for ARM API calls
        /// </summary>
        public const string ARMComputeAPIVersion = "2016-03-30";

        /// <summary>
        /// Partner Center API URL
        /// </summary>
        public const string PCAPIUrl = "https://api.partnercenter.microsoft.com";

        /// <summary>
        /// ARM Compute API URL to get VM SKU Versions
        /// </summary>
        public const string VMSKUGetVersionsAPIsUrl = "{0}/subscriptions/{1}/providers/Microsoft.Compute/locations/{2}/publishers/{3}/artifacttypes/vmimage/offers/{4}/skus/{5}/versions?api-version={6}";

        /// <summary>
        /// ARM Compute API URL to get VM SKU Version details of a specific SKU version
        /// </summary>
        public const string VMSKUGetVersionDetailsAPIsUrl = "{0}{1}?api-version={2}";

        /// <summary>
        /// ARM Compute API URL to get VM Sizes for a given location
        /// </summary>
        public const string VMGetVMSizesAPIsUrl = "{0}/subscriptions/{1}/providers/Microsoft.Compute/locations/{2}/vmSizes?api-version={3}";
    }

    /// <summary>
    /// Class contains the constant values for Response Time Out values for the API Calls
    /// </summary>
    public class APIResponseTimeLimitConstants
    {
        /// <summary>
        /// TimeOut Limit for Rate Card Fetch from Partner Center API in minutes
        /// </summary>
        public const int RateCardFetchLimit = 3;

        /// <summary>
        /// TimeOut Limit for for fetching Azure AD Token in minutes
        /// </summary>
        public const int TokenFetchLimit = 1;

        /// <summary>
        /// TimeOut Limit for ARM API calls in minutes
        /// </summary>
        public const int APICallDefaultLimit = 2;
    }

    /// <summary>
    /// Class that contains the constants for ARM Resource Types supported
    /// </summary>
    public class ARMResourceTypeConstants
    {
        /// <summary>
        /// Storage Account Resource Type
        /// </summary>
        public const string ARMStorageResourceType = "Microsoft.Storage/storageAccounts";

        /// <summary>
        /// Public IP Resource Type
        /// </summary>
        public const string ARMPublicIPResourceType = "Microsoft.Network/publicIPAddresses";

        /// <summary>
        /// Virtual Network Resource Type
        /// </summary>
        public const string ARMVirtualNetworkResourceType = "Microsoft.Network/virtualNetworks";

        /// <summary>
        /// Network Interface Resource Type
        /// </summary>
        public const string ARMNetworkInterfaceResourceType = "Microsoft.Network/networkInterfaces";

        /// <summary>
        /// Virtual Machine Resource Type
        /// </summary>
        public const string ARMVMResourceType = "Microsoft.Compute/virtualMachines";

        /// <summary>
        /// Availability Sets Resource Type
        /// </summary>
        public const string ARMAvailabilitySetsResourceType = "Microsoft.Compute/availabilitySets";

        /// <summary>
        /// Load Balancers Resource Type
        /// </summary>
        public const string ARMLoadBalancerResourceType = "Microsoft.Network/loadBalancers";
    }

    /// <summary>
    /// Class that contains the location mapping between Displayed values, ARM location and Location as per Pricing specifications
    /// </summary>
    public class LocationConstants
    {
        /// <summary>
        /// Mapping between Display names of Azure locations and the Pricing Specs
        /// </summary>
        public static readonly Dictionary<string, string> LocationAsPerPricingSpecsMap = new Dictionary<string, string>
        {
            { "East Asia", "AP East" },
            { "Southeast Asia", "AP Southeast" },
            { "Central US", "US Central" },
            { "East US", "US East" },
            { "East US 2", "US East 2" },
            { "West US", "US West" },
            { "North Central US", "US North Central" },
            { "South Central US", "US South Central" },
            { "North Europe", "EU North" },
            { "West Europe", "EU West" },
            { "Japan West", "JA West" },
            { "Japan East", "JA East" },
            { "Brazil South", "BR South" },
            { "Australia East", "AU East" },
            { "Australia Southeast", "AU Southeast" },
            { "Canada Central", "CA Central" },
            { "Canada East", "CA East" },

            // {"UK South", "UK South" 
            // {"UK West", "UK West" },
            { "West Central US", "US West Central" },
            { "West US 2", "US West 2" }
        };

        /// <summary>
        /// Mapping between Display names of Azure locations and the ARM Specs
        /// </summary>
        public static readonly Dictionary<string, string> LocationAsPerARMSpecsMap = new Dictionary<string, string>
        {
            { "East Asia", "eastasia" },
            { "Southeast Asia", "southeastasia" },
            { "Central US", "centralus" },
            { "East US", "eastus" },
            { "East US 2", "eastus2" },
            { "West US", "westus" },
            { "North Central US", "northcentralus" },
            { "South Central US", "southcentralus" },
            { "North Europe", "northeurope" },
            { "West Europe", "westeurope" },
            { "Japan West", "japanwest" },
            { "Japan East", "japaneast" },
            { "Brazil South", "brazilsouth" },
            { "Australia East", "australiaeast" },
            { "Australia Southeast", "australiasoutheast" },
            { "Canada Central", "canadacentral" },
            { "Canada East", "canadaeast" },
            { "West Central US", "westcentralus" },
            { "West US 2", "westus2" }

            // {"UK South", "uksouth" },
            // {"UK West", "ukwest" }
        };

        /// <summary>
        /// Mapping between the ARM Pricing Specs of Azure locations and the Network Zones
        /// </summary>
        public static readonly Dictionary<string, string> LocationZoneMap = new Dictionary<string, string>()
        {
            { "US West", "Zone 1" },
            { "US West 2", "Zone 1" },
            { "US West Central", "Zone 1" },
            { "US East", "Zone 1" },
            { "US North Central", "Zone 1" },
            { "US South Central", "Zone 1" },
            { "US East 2", "Zone 1" },
            { "US Central", "Zone 1" },
            { "EU West", "Zone 1" },
            { "EU North", "Zone 1" },
            { "CA East", "Zone 1" },
            { "CA Central", "Zone 1" },
            { "AP East", "Zone 2" },
            { "AP Southeast", "Zone 2" },
            { "JA East", "Zone 2" },
            { "JA West", "Zone 2" },
            { "AU Southeast", "Zone 2" },
            { "AU East", "Zone 2" },
            { "BR South", "Zone 3" }

          // , {"","" }
          // , {"","" }
        };
    }

    /// <summary>
    /// Class contains the Virtual Machines Resource Constants including default values for usage
    /// </summary>
    public class VMResourceConstants
    {
        /// <summary>
        /// OS Disk Size in GB for Storage Capacity Pricing of a VM Disk
        /// </summary>
        public const double StorageOSDiskSize = 20;

        /// <summary>
        /// OS Disk Size in GB for Storage Capacity Pricing of a VM Premium Disk
        /// </summary>
        public const double StoragePremiumOSDiskSize = 128;

        /// <summary>
        /// Data Disk Size in GB for Storage Capacity Pricing of a VM
        /// </summary>
        public const double StorageDataDiskSize = 100;

        /// <summary>
        /// Table Size for Storage Capacity Pricing of a VM's Diagnostics
        /// </summary>
        public const double StorageDiagnosticsTableSize = 10;

        /// <summary>
        /// Number of Transactions (in 10000) of a OS Disk for Storage Transactions Pricing of a VM Disk
        /// </summary>
        public const double DataManagementVMStorageOSDiskTrans = 100;

        /// <summary>
        /// Number of Transactions (in 10000) of a Data Disk for Storage Transactions Pricing of a VM Disk
        /// </summary>
        public const double DataManagementVMStorageDataDiskTrans = 200;

        /// <summary>
        /// Number of Transactions (in 10000) for Storage Transactions Pricing of a VM's Diagnostics
        /// </summary>
        public const double DataManagementVMDiagnosticsStorageTrans = 20;

        /// <summary>
        /// Networking Bandwidth Usage (Out) in GB
        /// </summary>
        public const double NetworkingVMNetwork = 500;

        /// <summary>
        /// Meters Field values for Virtual Machine Compute Hours Pricing
        /// </summary>
        public const string VMMeterCategory = "Virtual Machines";

        /// <summary>
        /// Meters Field values for Virtual Machine Compute Hours Pricing
        /// </summary>
        public const string VMMeterName = "Compute Hours";

        /// <summary>
        /// Meters Field values for Virtual Machine Compute Hours Pricing
        /// </summary>
        public const string VMMeterSubCategoryWindowsString = "{0} VM ({1})";

        /// <summary>
        /// Meters Field values for Virtual Machine Compute Hours Pricing
        /// </summary>
        public const string VMMeterSubCategoryLinuxString = "{0} VM";

        /// <summary>
        /// Regex Strings for VM Size A
        /// </summary>
        public const string VMASizeRegexString = @"^(Basic|Standard)_(A.+)$";

        /// <summary>
        /// VM Size Basic String
        /// </summary>
        public const string VMSizeBasicString = "Basic";

        /// <summary>
        /// Regex Strings for VM Size Premium D and G
        /// </summary>
        public const string VMDGPremiumSSizeRegexString = @"^(Standard_D|Standard_G)S(.+)$";

        /// <summary>
        /// Regex Strings for VM Size Premium F
        /// </summary>
        public const string VMFPremiumSSizeRegexString = @"^(Standard_F)(.+)S$";
    }

    /// <summary>
    /// Class contains the Storage Resource Constants
    /// </summary>
    public class StorageResourceConstants
    {
        /// <summary>
        /// Account Type for Premium Storage
        /// </summary>
        public const string StoragePremiumAccountType = "Premium_LRS";

        /// <summary>
        /// Account Type for Standard Storage
        /// </summary>
        public const string StorageDefaultAccountType = "Standard_LRS";

        /// <summary>
        /// Meters Field values for Storage Pricing
        /// </summary>
        public const string StorageMeterCategory = "Storage";

        /// <summary>
        /// Meters Field values for Storage Pricing
        /// </summary>
        public const string StorageMeterNameForVMDisk = "Standard IO - Page Blob/Disk (GB)";

        /// <summary>
        /// Meters Field values for Storage Pricing
        /// </summary>
        public const string StorageMeterNameForTable = "Standard IO - Table (GB)";

        /// <summary>
        /// Meters Field values for Storage Pricing
        /// </summary>
        public const string DataManagementMeterCategory = "Data Management";

        /// <summary>
        /// Meters Field values for Storage Pricing
        /// </summary>
        public const string DataManagementMeterSubCategoryForVMDisk = "";

        /// <summary>
        /// Meters Field values for Storage Pricing
        /// </summary>
        public const string DataManagementMeterNameForStorageTrans = "Storage Transactions (in 10,000s)";

        /// <summary>
        /// Mapping between Storage Account Type and Meter SubCategory of Storage Pricing
        /// </summary>
        public static readonly Dictionary<string, string> StorageTypeAndMeterSubCategoryMap = new Dictionary<string, string>
        {
            { "Standard_LRS", "Locally Redundant" },
            { "Standard_GRS", "Geo Redundant" },
            { "Standard_ZRS", "Zone Redundant" },
            { "Premium_LRS", "Locally Redundant" }
        };

        /// <summary>
        /// Premium Storage Disk Size Options
        /// </summary>
        public static readonly double[] StoragePremiumDiskValuesArray = new double[] { 128, 512, 1024 };

        /// <summary>
        /// Mapping of Premium Storage Disk Size Options with Pricing Meter Names
        /// </summary>
        public static readonly Dictionary<double, string> StoragePremiumDiskSizeAndMeterNameMap = new Dictionary<double, string>
        {
            { 128, "Premium Storage - Page Blob/P10 (Units)" },
            { 512, "Premium Storage - Page Blob/P20 (Units)" },
            { 1024, "Premium Storage - Page Blob/P30 (Units)" }
        };
    }

    /// <summary>
    /// Class contains the Networking Resource Constants
    /// </summary>
    public class NetworkingResourceConstants
    {
        /// <summary>
        /// Meters Field values for Networking Pricing
        /// </summary>
        public const string NetworkingMeterCategory = "Networking";

        /// <summary>
        /// Meters Field values for Networking Pricing
        /// </summary>
        public const string NetworkingMeterSubCategoryForVMNetwork = "";

        /// <summary>
        /// Meters Field values for Networking Pricing
        /// </summary>
        public const string NetworkingMeterNameForVMNetwork = "Data Transfer Out (GB)";
    }

    /// <summary>
    /// Class contains the Public IP Resource Constants
    /// </summary>
    public class PublicIPResourceConstants
    {
        /// <summary>
        /// Static Allocation Method String
        /// </summary>
        public const string PublicPublicIPAllocationMethodStatic = "Static";

        /// <summary>
        /// Meters Field values for Public IP Pricing
        /// </summary>
        public const string NetworkingMeterSubCategoryForPublicIP = "Public IP Addresses";

        /// <summary>
        /// Meters Field values for Public IP Pricing
        /// </summary>
        public const string NetworkingMeterNameForPublicIP = "IP Address Hours";
    }
    }
