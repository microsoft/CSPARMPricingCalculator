// -----------------------------------------------------------------------
// <copyright file="VMProperties.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalOps.DataModel.ComponentModel
{ 
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class that defines the deserialized data for the JSON in properties section for the ARM resource of "type": "Microsoft.Compute/virtualMachines" in an ARM Template
    /// ARM Resource API Version: 2015-06-15
    /// </summary>
    public class VMProperties
    {
        [JsonProperty("hardwareProfile")]
        public HardwareProfile HardwareProfile { get; set; }

        [JsonProperty("osProfile")]
        public OsProfile OsProfile { get; set; }

        [JsonProperty("storageProfile")]
        public StorageProfile StorageProfile { get; set; }

        [JsonProperty("networkProfile")]
        public NetworkProfile NetworkProfile { get; set; }

        [JsonProperty("diagnosticsProfile")]
        public DiagnosticsProfile DiagnosticsProfile { get; set; }
    }

    /// <summary>
    /// DataDisk class
    /// </summary>
    public class DataDisk
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("diskSizeGB")]
        public string DiskSizeGB { get; set; }

        [JsonProperty("lun")]
        public int Lun { get; set; }

        [JsonProperty("vhd")]
        public Vhd Vhd { get; set; }

        [JsonProperty("createOption")]
        public string CreateOption { get; set; }
    }

    /// <summary>
    /// StorageProfile class
    /// </summary>
    public class StorageProfile
    {
        [JsonProperty("imageReference")]
        public ImageReference ImageReference { get; set; }

        [JsonProperty("osDisk")]
        public OsDisk OsDisk { get; set; }

        [JsonProperty("dataDisks")]
        public List<DataDisk> DataDisks { get; set; }
    }

    /// <summary>
    /// NetworkInterface class
    /// </summary>
    public class NetworkInterface
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    /// <summary>
    /// NetworkProfile class
    /// </summary>
    public class NetworkProfile
    {
        [JsonProperty("networkInterfaces")]
        public List<NetworkInterface> NetworkInterfaces { get; set; }
    }

    /// <summary>
    /// BootDiagnostics class
    /// </summary>
    public class BootDiagnostics
    {
        [JsonProperty("enabled")]
        public string Enabled { get; set; }

        [JsonProperty("storageUri")]
        public string StorageUri { get; set; }
    }

    /// <summary>
    /// DiagnosticsProfile class
    /// </summary>
    public class DiagnosticsProfile
    {
        [JsonProperty("bootDiagnostics")]
        public BootDiagnostics BootDiagnostics { get; set; }
    }

    /// <summary>
    /// HardwareProfile class
    /// </summary>
    public class HardwareProfile
    {
        [JsonProperty("vmSize")]
        public string VmSize { get; set; }
    }

    /// <summary>
    /// OsProfile class
    /// </summary>
    public class OsProfile
    {
        [JsonProperty("computerName")]
        public string ComputerName { get; set; }

        [JsonProperty("adminUsername")]
        public string AdminUsername { get; set; }

        [JsonProperty("adminPassword")]
        public string AdminPassword { get; set; }
    }

    /// <summary>
    /// ImageReference class
    /// </summary>
    public class ImageReference
    {
        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("offer")]
        public string Offer { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    /// <summary>
    /// Vhd class
    /// </summary>
    public class Vhd
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    /// <summary>
    /// OsDisk class
    /// </summary>
    public class OsDisk
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("osType")]
        public string OsType { get; set; }

        [JsonProperty("vhd")]
        public Vhd Vhd { get; set; }

        [JsonProperty("caching")]
        public string Caching { get; set; }

        [JsonProperty("createOption")]
        public string CreateOption { get; set; }
    }
}
