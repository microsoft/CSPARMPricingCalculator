// -----------------------------------------------------------------------
// <copyright file="VMComponentFetcher.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.Util.ResourceComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DataModel;
    using DataModel.ComponentModel;
    using Helper;
    using Helper.Online;

    /// <summary>
    /// Class that has method to fetch the resource components for the Virtual Machine resource: "Microsoft.Compute/virtualMachines" in an ARM template
    /// ARM Resource API Version: 2015-06-15
    /// </summary>
    public class VMComponentFetcher : IComponentFetcher
    {
        /// <summary>
        /// Variable to store the VM resource from the ARM Template
        /// </summary>
        private Resource resource;

        /// <summary>
        /// Variable to store the ARM Template
        /// </summary>
        private ARMTemplate template;

        /// <summary>
        /// Variable to store the values of parameters from the Parameter file
        /// </summary>
        private ARMParamValue paramValue;

        /// <summary>
        /// Variable to store the name of the resource
        /// </summary>
        private string nameOfResource = string.Empty;

        /// <summary>
        /// Variable to store the properties of the Public IP resource from the ARM Template
        /// </summary>
        private VMProperties prop;

        /// <summary>
        /// CSP Account credentials object. A token will be generated using these credentials and used for making the online ARM API call
        /// </summary>
        private CSPAccountCreds cspCreds;

        /// <summary>
        /// Azure Location
        /// </summary>
        private string location;

        /// <summary>
        /// Gets the resource components for the Virtual machine resource in an ARM template
        /// </summary>
        /// <param name="resource">The object containing the resource</param>
        /// <param name="template">The object containing the ARM Template</param>
        /// <param name="paramValue">The object containing the values in the Parameter file</param>
        /// <param name="location">The Azure Location</param>
        /// <param name="cspCreds">CSP Account credentials object. This is not used in current version.</param>
        /// <param name="log">The object that will contain the exception messages</param>
        /// <returns> Returns the list of resource components</returns>
        public List<ResourceComponent> GetResourceComponents(Resource resource, ARMTemplate template, ARMParamValue paramValue, string location, CSPAccountCreds cspCreds, out StringBuilder log)
        {
            this.resource = resource;
            this.template = template;
            this.paramValue = paramValue;
            this.cspCreds = cspCreds;
            this.location = location;

            List<ResourceComponent> componentList = new List<ResourceComponent>();
            log = new StringBuilder(string.Empty);

            try
            {
                if (resource != null && resource.Name != null)
                {
                    // Get the name of the resource
                    this.nameOfResource = PropertyHelper.GetValueIfVariableOrParam(resource.Name, template.Variables, template.Parameters, paramValue.Parameters);
                }

                // Convert Resource Properties to VMProperties
                if (resource != null && resource.Properties != null)
                {
                    this.prop = resource.Properties.ToObject<VMProperties>();

                    if (this.prop != null)
                    {
                        // Fetch resource components for Compute Hours
                        componentList.AddRange(this.GetResourceComponentForComputeHours());

                        // Fetch resource components for Storage 
                        componentList.AddRange(this.GetResourceComponentForStorage());

                        // Fetch resource components for Network      
                        componentList.AddRange(this.GetResourceComponentForNetwork());

                        // Fetch resource components for VM Diagnostics      
                        componentList.AddRange(this.GetResourceComponentForDiagnostics());
                    }
                    else
                    {
                        throw new Exception(ExceptionLogger.GenerateLoggerTextForFailedReadProperties(this.nameOfResource));
                    }
                }
                else
                {
                    throw new Exception(ExceptionLogger.GenerateLoggerTextForMissingField("Properties", this.nameOfResource));
                }
            }
            catch (Exception ex)
            {
                componentList = null;
                log.AppendLine(ex.Message);
            }

            return componentList;
        }

        /// <summary>
        /// Gets the resource components for the compute hours of the Virtual machine resource. This includes components for software cost if present.
        /// </summary>
        /// <returns> Returns the list of resource components </returns>
        private List<ResourceComponent> GetResourceComponentForComputeHours()
        {
            List<ResourceComponent> componentList = new List<ResourceComponent>();

            // Create the compute hours component, Meter SubCategory value will be set later
            ResourceComponent computeHoursComponent = new ResourceComponent
            {
                ResourceType = this.resource.Type,
                MeterCategory = VMResourceConstants.VMMeterCategory,
                MeterSubCategory = null,
                MeterName = VMResourceConstants.VMMeterName,
                Quantity = Constants.HoursinaMonth,
                IsChargeable = true
            };

            // Create the software cost component, Meter SubCategory value will be set later
            ResourceComponent softwareCostComponent = new ResourceComponent
            {
                ResourceType = this.resource.Type,
                MeterCategory = VMResourceConstants.VMMeterCategory,
                MeterSubCategory = null,
                MeterName = VMResourceConstants.VMMeterName,
                Quantity = Constants.HoursinaMonth,
                IsChargeable = true
            };

            string vmSize = null, osType = null, createOption = null;

            if (this.prop.HardwareProfile != null && this.prop.HardwareProfile.VmSize != null)
            {
                // Fetch the VM Size
                vmSize = PropertyHelper.GetValueIfVariableOrParam(this.prop.HardwareProfile.VmSize, this.template.Variables, this.template.Parameters, this.paramValue.Parameters);
            }
            else
            {
                throw new Exception(ExceptionLogger.GenerateLoggerTextForMissingField("properties.hardwareProfile.vmSize", this.nameOfResource));
            }

            if (vmSize.Equals(string.Empty))
            {
                throw new Exception(ExceptionLogger.GenerateLoggerTextForMissingField("properties.hardwareProfile.vmSize", this.nameOfResource));
            }

            if (this.prop.StorageProfile != null && this.prop.StorageProfile.OsDisk != null)
            {
                // Fetch the OS Disk related info
                osType = PropertyHelper.GetValueIfVariableOrParam(this.prop.StorageProfile.OsDisk.OsType, this.template.Variables, this.template.Parameters, this.paramValue.Parameters);
                createOption = PropertyHelper.GetValueIfVariableOrParam(this.prop.StorageProfile.OsDisk.CreateOption, this.template.Variables, this.template.Parameters, this.paramValue.Parameters);
            }
            else
            {
                throw new Exception(ExceptionLogger.GenerateLoggerTextForMissingField("properties.storageProfile.osDisk", this.nameOfResource));
            }

            if (createOption == null)
            {
                throw new Exception(ExceptionLogger.GenerateLoggerTextForMissingField("properties.storageProfile.osDisk.createOption", this.nameOfResource));
            }

            if (osType == null)
            {
                if (createOption != null && createOption.Equals("FromImage", StringComparison.OrdinalIgnoreCase))
                {
                    string vmImagePublisher = null, vmImageOffer = null, vmImageSKU = null;

                    // Fetch the VM Image info - Publisher, Offer and SKU
                    if (this.prop.StorageProfile != null && this.prop.StorageProfile.ImageReference != null)
                    {
                        vmImagePublisher = PropertyHelper.GetValueIfVariableOrParam(this.prop.StorageProfile.ImageReference.Publisher, this.template.Variables, this.template.Parameters, this.paramValue.Parameters);
                        vmImageOffer = PropertyHelper.GetValueIfVariableOrParam(this.prop.StorageProfile.ImageReference.Offer, this.template.Variables, this.template.Parameters, this.paramValue.Parameters);
                        vmImageSKU = PropertyHelper.GetValueIfVariableOrParam(this.prop.StorageProfile.ImageReference.Sku, this.template.Variables, this.template.Parameters, this.paramValue.Parameters);
                    }
                    else
                    {
                        throw new Exception(ExceptionLogger.GenerateLoggerTextForMissingField("properties.storageProfile.imageReference", this.nameOfResource));
                    }

                    if (vmImagePublisher != null && vmImageOffer != null && vmImageSKU != null)
                    {
                        // Get the OS Type of the VM Image from the Online Helper method
                        osType = VMOnlineHelper.GetVMImageOSType(this.cspCreds, vmImagePublisher, vmImageOffer, vmImageSKU, this.location);
                        string meterSubCategoryForVMImageWithSoftwareCost = null;

                        // Get the Meter SubCategory for software cost of the VM Image is applicable
                        meterSubCategoryForVMImageWithSoftwareCost = VMImageHelper.GetMeterSubCategoryForVMImageWithSoftwareCost(this.cspCreds, vmImagePublisher, vmImageOffer, vmImageSKU, vmSize, this.location);
                        if (meterSubCategoryForVMImageWithSoftwareCost != null)
                        {
                            // Set the Meter SubCategory for Software Cost component, Add to the List of resource components
                            softwareCostComponent.MeterSubCategory = meterSubCategoryForVMImageWithSoftwareCost;
                            componentList.Add(softwareCostComponent);
                        }
                    }
                    else
                    {
                        throw new Exception(ExceptionLogger.GenerateLoggerTextForMissingField("properties.storageProfile.publisher/offer/sku", this.nameOfResource));
                    }
                }
                else
                {
                    throw new Exception(ExceptionLogger.GenerateLoggerTextForInvalidField("storageProfile.osDisk.createOption", createOption, this.nameOfResource));
                }
            }

            string meterSubCategory = null;
            string modifiedVMSizeString = VMHelper.ModifyVMSizeStringAsPerPricingSpecs(vmSize);
            if (osType != null)
            {
                // Fetch the Meter SubCategory as per the OS Type
                switch (osType.ToUpper())
                {
                    case "WINDOWS":
                        meterSubCategory = string.Format(VMResourceConstants.VMMeterSubCategoryWindowsString, modifiedVMSizeString, osType);
                        break;

                    case "LINUX":
                        meterSubCategory = string.Format(VMResourceConstants.VMMeterSubCategoryLinuxString, modifiedVMSizeString);
                        break;

                    default:
                        throw new Exception(ExceptionLogger.GenerateLoggerTextForInvalidField("OSType", osType, this.nameOfResource));
                }
            }
            else
            {
                throw new Exception(ExceptionLogger.GenerateLoggerTextForInvalidField("OSType", string.Empty, this.nameOfResource));
            }

            // Set the Meter SubCategory for Compute Hours Cost component, Add to the List of resource components
            computeHoursComponent.MeterSubCategory = meterSubCategory;
            componentList.Add(computeHoursComponent);

            return componentList;
        }

        /// <summary>
        /// Gets the resource components for the Storage of the Virtual machine resource. 
        /// </summary>
        /// <returns> Returns the list of resource components </returns>
        private List<ResourceComponent> GetResourceComponentForStorage()
        {
            List<ResourceComponent> storageComponentList = new List<ResourceComponent>();

            // Get list of all storage resources in template
            List<Resource> storageResourceList = this.template.Resources.FindAll(x => ARMResourceTypeConstants.ARMStorageResourceType.Equals(x.Type, StringComparison.OrdinalIgnoreCase));

            // OS Disk Components
            string diskURI = null;
            if (this.prop.StorageProfile != null && this.prop.StorageProfile.OsDisk != null && this.prop.StorageProfile.OsDisk.Vhd != null)
            {
                // Fetch the OS Disk URI
                diskURI = this.prop.StorageProfile.OsDisk.Vhd.Uri;
            }

            if (diskURI != null && !diskURI.Equals(string.Empty))
            {
                // Fetch the list of resource components for OS Disk and Add to list
                storageComponentList.AddRange(this.GetResourceComponentForDiskStorage(diskURI, null, storageResourceList, true));
            }
            else
            {
                throw new Exception(ExceptionLogger.GenerateLoggerTextForMissingField("properties.storageProfile.osDisk.vhd.uri", this.nameOfResource));
            }

            // Data Disk Components
            if (this.prop.StorageProfile != null && this.prop.StorageProfile.DataDisks != null && this.prop.StorageProfile.DataDisks.Count != 0)
            {
                for (int i = 0; i < this.prop.StorageProfile.DataDisks.Count; i++)
                {
                    string dataDiskSize = null;
                    if (this.prop.StorageProfile.DataDisks[i] != null && this.prop.StorageProfile.DataDisks[i].Vhd != null)
                    {
                        // Fetch the Data Disk URI
                        diskURI = this.prop.StorageProfile.DataDisks[i].Vhd.Uri;

                        // Fetch the Data Disk Size
                        dataDiskSize = PropertyHelper.GetValueIfVariableOrParam(this.prop.StorageProfile.DataDisks[i].DiskSizeGB, this.template.Variables, this.template.Parameters, this.paramValue.Parameters);
                    }

                    if (diskURI != null && dataDiskSize != null)
                    {
                        // Fetch the list of resource components for Data Disk and Add to list
                        storageComponentList.AddRange(this.GetResourceComponentForDiskStorage(diskURI, dataDiskSize, storageResourceList, false));
                    }
                }
            }

            return storageComponentList;
        }

        /// <summary>
        /// Gets the resource components for the Disks of the Virtual machine resource. 
        /// </summary>
        /// <param name="diskURI">The URI of the disk</param>
        /// <param name="diskSize">The size of the Disk</param>
        /// <param name="storageResourceList">The list of storage resources in the ARM Template</param>
        /// <param name="isOSDisk">Set true for OS Disk and false for Data Disk</param>
        /// <returns> Returns the list of resource components </returns>
        private List<ResourceComponent> GetResourceComponentForDiskStorage(string diskURI, string diskSize, List<Resource> storageResourceList, bool isOSDisk)
        {
            List<ResourceComponent> storageComponentList = new List<ResourceComponent>();
            List<ResourceComponent> storageDiskComponentList = new List<ResourceComponent>();
            List<ResourceComponent> storageTransactionComponentList = new List<ResourceComponent>();

            // Get the Storage resource corresponding to the Storage of the Disk of the VM
            Resource diskStorageResource = PropertyHelper.SearchResourceInListByName(storageResourceList, diskURI);

            // Create the resource component for Storage of the VM disk, MeterSubCategory and MeterName will be set later
            ResourceComponent storageDiskComponent = new ResourceComponent
            {
                ResourceType = this.resource.Type,
                MeterCategory = StorageResourceConstants.StorageMeterCategory,
                MeterSubCategory = null,
                MeterName = null,
                IsChargeable = true
            };

            string storageAccountType = null;
            bool isPremiumStorage = false;

            StorageProperties storageProp = null;
            if (diskStorageResource != null && diskStorageResource.Properties != null)
            {
                // Get properties of the Storage resource
                storageProp = this.GetStoragePropertiesForStorageResource(diskStorageResource);
            }

            if (storageProp != null)
            {
                // Get Account Type of the Storage resource
                storageAccountType = PropertyHelper.GetValueIfVariableOrParam(storageProp.AccountType, this.template.Variables, this.template.Parameters, this.paramValue.Parameters);
            }

            // If Unable to fetch Storage Account Type, then, the template may be using existing storage account, Take Default Storage Account Type
            if (storageAccountType == null)
            {
                storageAccountType = StorageResourceConstants.StorageDefaultAccountType;
            }

            string meterSubCategory = null;

            // Set the Meter SubCategory based on the Storage Account Type
            if (StorageResourceConstants.StorageTypeAndMeterSubCategoryMap.TryGetValue(storageAccountType, out meterSubCategory))
            {
                storageDiskComponent.MeterSubCategory = meterSubCategory;
            }
            else
            {
                throw new Exception(ExceptionLogger.GenerateLoggerTextForInvalidField("StorageAccountType", storageAccountType, this.nameOfResource));
            }

            // Check if premium storage is used
            if (storageAccountType.Equals(StorageResourceConstants.StoragePremiumAccountType, StringComparison.OrdinalIgnoreCase))
            {
                isPremiumStorage = true;
            }

            double diskQuantity = 0;
            if (isOSDisk)
            {
                // Get default Storage for OS Disk from Constants/Config
                diskQuantity = isPremiumStorage ? VMResourceConstants.StoragePremiumOSDiskSize : VMResourceConstants.StorageOSDiskSize;
            }
            else
            {
                // Get the Storage size for the Data Disk
                double dataDiskQuantityValue = 0;
                if (diskSize != null && double.TryParse(diskSize, out dataDiskQuantityValue))
                {
                    if (isPremiumStorage)
                    {
                        // If Premium Storage, Map the disk size to the next greater available size of premium disk
                        for (int i = 0; i < StorageResourceConstants.StoragePremiumDiskValuesArray.Count(); i++)
                        {
                            double premiumDiskSizeValue = StorageResourceConstants.StoragePremiumDiskValuesArray[i];
                            if ((dataDiskQuantityValue <= premiumDiskSizeValue) || (i == StorageResourceConstants.StoragePremiumDiskValuesArray.Count() - 1))
                            {
                                diskQuantity = premiumDiskSizeValue;
                                break;
                            }
                        }
                    }
                    else
                    {
                        diskQuantity = dataDiskQuantityValue < VMResourceConstants.StorageDataDiskSize ? dataDiskQuantityValue : VMResourceConstants.StorageDataDiskSize;
                    }
                }
            }

            // Set the meter name for the Disk resource based on premium and standard
            if (isPremiumStorage)
            {
                string meterName = null;
                if (StorageResourceConstants.StoragePremiumDiskSizeAndMeterNameMap.TryGetValue(diskQuantity, out meterName))
                {
                    storageDiskComponent.MeterName = meterName;
                }
            }
            else
            {
                storageDiskComponent.MeterName = StorageResourceConstants.StorageMeterNameForVMDisk;
            }

            // Set the Quantity of the component to the Disk Size, Set 1 if its premium disk
            storageDiskComponent.Quantity = isPremiumStorage ? 1 : diskQuantity;
            storageComponentList.Add(storageDiskComponent);

            if (!isPremiumStorage)
            {
                // Add Storage transactions component if not a premium disk
                storageTransactionComponentList.Add(new ResourceComponent()
                {
                    ResourceType = this.resource.Type,
                    MeterCategory = StorageResourceConstants.DataManagementMeterCategory,
                    MeterSubCategory = StorageResourceConstants.DataManagementMeterSubCategoryForVMDisk,
                    MeterName = StorageResourceConstants.DataManagementMeterNameForStorageTrans,
                    Quantity = isOSDisk ? VMResourceConstants.DataManagementVMStorageOSDiskTrans : VMResourceConstants.DataManagementVMStorageDataDiskTrans,
                    IsChargeable = true
                });
            }

            // Add the list of components obtained for the Storage Disk of the VM resource
            storageComponentList.AddRange(storageDiskComponentList);
            storageComponentList.AddRange(storageTransactionComponentList);
            return storageComponentList;
        }

        /// <summary>
        /// Fetch the storage properties for a storage resource. 
        /// </summary>
        /// <param name="storageResource">The Storage resource</param>
        /// <returns> Returns the storage properties </returns>
        private StorageProperties GetStoragePropertiesForStorageResource(Resource storageResource)
        {
            StorageProperties storageProp = null;

            try
            {
                storageProp = storageResource.Properties.ToObject<StorageProperties>();
            }
            catch (Exception)
            {
                // Do nothing
                storageProp = null;
            }

            return storageProp;
        }

        /// <summary>
        /// Gets the resource components for the Network bandwidth of the Virtual machine resource. 
        /// </summary>
        /// <returns> Returns the list of resource components </returns>
        private List<ResourceComponent> GetResourceComponentForNetwork()
        {
            List<ResourceComponent> componentList = new List<ResourceComponent>();
            List<NetworkInterface> vmNetworkInterfaces = null;
            if (this.prop.NetworkProfile != null && this.prop.NetworkProfile.NetworkInterfaces != null)
            {
                vmNetworkInterfaces = this.prop.NetworkProfile.NetworkInterfaces;
            }
            else
            {
                throw new Exception(ExceptionLogger.GenerateLoggerTextForMissingField("properties.networkProfile.networkInterfaces", this.nameOfResource));
            }

            if (vmNetworkInterfaces != null && (vmNetworkInterfaces.Count >= 1))
            {
                // Create the resource component for network bandwidth of the VM and add to the list
                ResourceComponent networkComponent = new ResourceComponent
                {
                    ResourceType = this.resource.Type,
                    MeterCategory = NetworkingResourceConstants.NetworkingMeterCategory,
                    MeterSubCategory = NetworkingResourceConstants.NetworkingMeterSubCategoryForVMNetwork,
                    MeterName = NetworkingResourceConstants.NetworkingMeterNameForVMNetwork,
                    Quantity = VMResourceConstants.NetworkingVMNetwork,
                    IsChargeable = true
                };

                componentList.Add(networkComponent);
            }
            else
            {
                throw new Exception(ExceptionLogger.GenerateLoggerTextForMissingField("properties.networkProfile.networkInterfaces", this.nameOfResource));
            }

            return componentList;
        }

        /// <summary>
        /// Gets the resource components for the Diagnostics if applicable/enabled for the Virtual machine resource. 
        /// </summary>
        /// <returns> Returns the list of resource components </returns>
        private List<ResourceComponent> GetResourceComponentForDiagnostics()
        {
            List<ResourceComponent> componentList = new List<ResourceComponent>();

            string diagnosticsEnabled = null;
            if (this.prop.DiagnosticsProfile != null && this.prop.DiagnosticsProfile.BootDiagnostics != null)
            {
                // Fetch the Enabled property for Diagnostics of the VM resource
                diagnosticsEnabled = this.prop.DiagnosticsProfile.BootDiagnostics.Enabled;
            }

            if (diagnosticsEnabled != null && diagnosticsEnabled.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                // Get list of all storage resources in template
                List<Resource> storageResourceList = this.template.Resources.FindAll(x => ARMResourceTypeConstants.ARMStorageResourceType.Equals(x.Type, StringComparison.OrdinalIgnoreCase));
                string diagnosticsStorageURI = null;
                if (this.prop.DiagnosticsProfile != null && this.prop.DiagnosticsProfile.BootDiagnostics != null)
                {
                    // Fetch the URI of Diagnostics
                    diagnosticsStorageURI = this.prop.DiagnosticsProfile.BootDiagnostics.StorageUri;
                }

                Resource diagnosticsStorageResource = null;
                if (diagnosticsStorageResource != null)
                {
                    diagnosticsStorageResource = PropertyHelper.SearchResourceInListByName(storageResourceList, diagnosticsStorageURI);
                }

                string storageAccountType = null;
                StorageProperties storageProp = null;
                if (diagnosticsStorageResource != null && diagnosticsStorageResource.Properties != null)
                {
                    // Get the Properties of the Storage Resource associated with storing the Diagnostics of the VM
                    storageProp = this.GetStoragePropertiesForStorageResource(diagnosticsStorageResource);
                }

                if (storageProp != null)
                {
                    // Get the Storage Account Type
                    storageAccountType = PropertyHelper.GetValueIfVariableOrParam(storageProp.AccountType, this.template.Variables, this.template.Parameters, this.paramValue.Parameters);
                }

                // Use Default Storage Account Type, as template may be using existing storage account
                if (storageAccountType == null)
                {
                    storageAccountType = StorageResourceConstants.StorageDefaultAccountType;
                }

                if (storageAccountType != null && storageAccountType != StorageResourceConstants.StoragePremiumAccountType)
                {
                    // Create the resource component for Diagnostics
                    ResourceComponent diagnosticsComponent = new ResourceComponent
                    {
                        ResourceType = this.resource.Type,
                        MeterCategory = StorageResourceConstants.StorageMeterCategory,
                        MeterSubCategory = null,
                        MeterName = StorageResourceConstants.StorageMeterNameForTable,
                        Quantity = VMResourceConstants.StorageDiagnosticsTableSize,
                        IsChargeable = true
                    };

                    string meterSubCategory = null;

                    // Set the Meter SubCategory based on the Storage Account Type
                    if (StorageResourceConstants.StorageTypeAndMeterSubCategoryMap.TryGetValue(storageAccountType, out meterSubCategory))
                    {
                        diagnosticsComponent.MeterSubCategory = meterSubCategory;
                    }
                    else
                    {
                        throw new Exception(ExceptionLogger.GenerateLoggerTextForInvalidField("StorageAccountType", storageAccountType, this.nameOfResource));
                    }

                    componentList.Add(diagnosticsComponent);

                    // Add the Storage Transactions component
                    componentList.Add(new ResourceComponent()
                    {
                        ResourceType = this.resource.Type,
                        MeterCategory = StorageResourceConstants.DataManagementMeterCategory,
                        MeterSubCategory = StorageResourceConstants.DataManagementMeterSubCategoryForVMDisk,
                        MeterName = StorageResourceConstants.DataManagementMeterNameForStorageTrans,
                        Quantity = VMResourceConstants.DataManagementVMDiagnosticsStorageTrans,
                        IsChargeable = true
                    });
                }
                else
                {
                    throw new Exception(ExceptionLogger.GenerateLoggerTextForInvalidField("StorageAccountType for Diagnostics", storageAccountType, this.nameOfResource));
                }
            }

            return componentList;
        }
    }
}
