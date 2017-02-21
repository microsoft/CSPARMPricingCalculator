// -----------------------------------------------------------------------
// <copyright file="CSPARMPricingCalculatorForm.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalUI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Text;
    using System.Windows.Forms;
    using CSPARMPricingCalOps;
    using CSPARMPricingCalOps.DataModel;
    using Newtonsoft.Json;
    using Util;

    /// <summary>
    /// Form Class for the Windows Form UI of the project
    /// </summary>
    public partial class CSPARMPricingCalculatorForm : Form
    {
        /// <summary>
        /// Variable stores the CSP Account credentials
        /// </summary>
        private CSPAccountCreds cspCreds = null;

        /// <summary>
        /// Variable to store the reference of the object of the class which will perform the CSP ARM Pricing Calculation
        /// </summary>
        private CSPARMPricingCalculator cspARMPricingCalc = new CSPARMPricingCalculator();

        /// <summary>
        /// Variable to store the reference of the object of the class which will perform the CSP ARM Pricing Calculation
        /// </summary>
        private CSPARMPricingInfo cspARMPricingInfoOutput;

        /// <summary>
        /// Variable to store the sum of monthly cost estimates
        /// </summary>
        private double totalCost;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSPARMPricingCalculatorForm" /> class.
        /// </summary>
        public CSPARMPricingCalculatorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Enum to store type of Background operations to be performed
        /// </summary>
        public enum OperationType
        {
            /// <summary>
            /// Operation Type - Rate Card Fetch
            /// </summary>
            FetchRateCard,

            /// <summary>
            /// Operation Type - CSP ARM Pricing Calculation
            /// </summary>
            CalculateCSPARMPricing
        }

        /// <summary>
        /// Button Click to Initiate Fetch Rate Card Async function
        /// </summary>
        /// <param name="sender">sender (button) </param>
        /// <param name="e">e (EventArgs)</param>
        private void btnFetchRateCard_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the Region/Currency Selection made by the user
                string currencyRegionSelection = this.ddlCurrency.SelectedItem.ToString();

                this.AddOperationStatusMessage(string.Format(UIMessageConstants.RateCardFetchInitiateMsgL1, currencyRegionSelection));
                this.AddOperationStatusMessage(UIMessageConstants.RateCardFetchInitiateMsgL2);

                // Extract the Region and Currency from the UI Selection, Set the values in the Creds object
                char[] charSeparators = new char[] { '-' };
                string[] selectedRegionDetails = currencyRegionSelection.Split(charSeparators, 2, StringSplitOptions.RemoveEmptyEntries);

                this.cspCreds.CSPRegion = selectedRegionDetails[0];
                this.cspCreds.CSPCurrency = selectedRegionDetails[1];

                // Initiate the Async operation to fetch the Azure CSP Rate Card
                this.BackGrndWorker.RunWorkerAsync(new OperationData()
                {
                    Operation = OperationType.FetchRateCard,
                    Data = new FetchRateCardInput()
                    {
                        CSPARMPricingCalc = this.cspARMPricingCalc,
                        CSPCreds = this.cspCreds
                    }
                });
            }
            catch (Exception ex)
            {
                this.AddOperationStatusMessage(string.Format(UIMessageConstants.RateCardFetchFailedMsg, ex.Message));
            }

            this.ToggleButtonStatus(false);
        }

        /// <summary>
        /// Enables/ Disables buttons on the UI
        /// </summary>
        /// <param name="isCompleted">Set the value to true to enable buttons, false to disable buttons</param>
        private void ToggleButtonStatus(bool isCompleted)
        {
            if (isCompleted)
            {
                // Set the enabled status of the buttons to true
                this.btnFetchRateCard.Enabled = true;
                this.btnBrowseTemplate.Enabled = true;
                this.btnCalculate.Enabled = true;
                this.btnBrowseParamFile.Enabled = true;
                this.btnExport.Enabled = true;
                this.btnClear.Enabled = true;
            }
            else
            {
                // Set the enabled status of the buttons to false
                this.btnFetchRateCard.Enabled = false;
                this.btnBrowseTemplate.Enabled = false;
                this.btnCalculate.Enabled = false;
                this.btnBrowseParamFile.Enabled = false;
                this.btnExport.Enabled = false;
                this.btnClear.Enabled = false;
            }
        }

        /// <summary>
        /// Button Click to Initiate ARM CSP Pricing Calculation
        /// </summary>
        /// <param name="sender">sender (button) </param>
        /// <param name="e">e (EventArgs)</param>
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if Selections are made and valid
                if (this.txtARMTemFileName.Text.Equals(string.Empty))
                {
                    // No ARM Template specified, Display Validation Msg, return
                    this.AddOperationStatusMessage(UIMessageConstants.NoARMTemplateProvided);
                    return;
                }
                else if (this.ddlLocation.SelectedIndex == -1)
                {
                    // Deployment Selection not made, Display Validation Msg, return
                    this.AddOperationStatusMessage(UIMessageConstants.NoDeploymentLocationProvided);
                    return;
                }

                // Fetch the inputs provided: ARM Template file, Parameter file and Azure Location selection
                string armTemplateSelection = this.txtARMTemFileName.Text;
                string armParamValueSelection = this.txtARMParamFileName.Text;
                string locationSelection = this.ddlLocation.SelectedItem.ToString();

                this.AddOperationStatusMessage(string.Format(UIMessageConstants.CSPARMPricingCalculationInitiateMsg, armTemplateSelection, locationSelection));

                // Initiate the ASync call to perform CSP ARM Pricing Calculation
                this.BackGrndWorker.RunWorkerAsync(new OperationData()
                {
                    Operation = OperationType.CalculateCSPARMPricing,
                    Data = new CalculateCSPARMPricingInput()
                    {
                        CSPARMPricingCalc = this.cspARMPricingCalc,
                        ARMTemplateFilePath = armTemplateSelection,
                        ARMParamValueFilePath = armParamValueSelection,
                        Location = locationSelection,
                        CSPCreds = this.cspCreds
                    }
                });
            }
            catch (Exception ex)
            {
                this.AddOperationStatusMessage(string.Format(UIMessageConstants.CSPARMPricingCalculationFailedMsg, ex.Message));
                throw;
            }

            // Enable the buttons on the UI
            this.ToggleButtonStatus(false);
        }

        /// <summary>
        /// Function that will be asynchronously called - that calls the CSP ARM Pricing Calculation module
        /// </summary>
        /// <param name="cspARMPricingCalc">Object of the class whose method will be called to perform CSP ARM Pricing Calculation</param>
        /// <param name="armTemplateFilePath">Path to the ARM Template File</param>
        /// <param name="armParamValueFilePath">Path to the ARM Parameter File</param>
        /// <param name="location">Azure Location selection</param>
        /// <param name="cspCreds">CSP Account credentials object. A token will be generated using these credentials and used for making the online ARM API call</param>
        /// <returns> Returns the Result of the CSP ARM Pricing Calculation with resource components and monthly cost estimates</returns>
        private CSPARMPricingInfo CalculateCSPARMPricing(CSPARMPricingCalculator cspARMPricingCalc, string armTemplateFilePath, string armParamValueFilePath, string location, CSPAccountCreds cspCreds)
        {
           CSPARMPricingInfo info = null;
            try
            {
                // Read the ARM Template file and get the object
                ARMTemplate template = FileUtil.GetResourceList(armTemplateFilePath);

                // Read the ARM Template file and get the object, Parameter file is optional
                ARMParamValue paramValue = new ARMParamValue() { ContentVersion = null, Parameters = null };
                if (armParamValueFilePath != null && !armParamValueFilePath.Equals(string.Empty))
                {
                    paramValue = FileUtil.GetParamValueList(armParamValueFilePath);
                }

                // Report progress message
                this.BackGrndWorker.ReportProgress(50, UIMessageConstants.CSPARMPricingCalculationProgressFileReadCompleteMsg);

                // Calculate
                info = cspARMPricingCalc.CalculateCSPARMPricing(template, paramValue, location, cspCreds);

                // Report progress message
                this.BackGrndWorker.ReportProgress(100, UIMessageConstants.CSPARMPricingCalculationProgressAllCompleteMsg);
            }
            catch (JsonSerializationException ex)
            {
                info = new CSPARMPricingInfo();
                info.Log.Append(string.Format(UIMessageConstants.CSPARMPricingCalculationInvalidJSONMsg, ex.Message));
            }
            catch (Exception ex)
            {
                info = new CSPARMPricingInfo();
                info.Log.Append(string.Format("{0}", ex.Message));
            }

            return info;
        }

        /// <summary>
        /// Load the Config Settings from the app.config
        /// </summary>
        private void LoadConfigSettings()
        {
            try
            {
                this.cspCreds = new CSPAccountCreds()
                {
                    // Fetch the app.config values
                    CSPClientId = ConfigurationManager.AppSettings[Constants.ConfigSettingsFieldCSPPCNativeAppClientId],
                    CSPResellerTenantID = ConfigurationManager.AppSettings[Constants.ConfigSettingsFieldCSPPartnerTenantID],
                    CSPRegion = null,
                    CSPCurrency = null,
                    CSPNativeAppClientId = ConfigurationManager.AppSettings[Constants.ConfigSettingsFieldCSPARMNativeAppClientId],
                    CSPAdminAgentUserName = ConfigurationManager.AppSettings[Constants.ConfigSettingsFieldCSPAdminAgentUserName],
                    CSPAdminAgentPassword = ConfigurationManager.AppSettings[Constants.ConfigSettingsFieldCSPAdminAgentPassword],
                    CSPCustomerTenantId = ConfigurationManager.AppSettings[Constants.ConfigSettingsFieldCSPCustomerTenantId],
                    CSPAzureSubscriptionId = ConfigurationManager.AppSettings[Constants.ConfigSettingsFieldCSPAzureSubscriptionId]
                };
            }
            catch (Exception e)
            {
                this.AddOperationStatusMessage(string.Format(UIMessageConstants.LoadConfigFailedMsg, e.Message));
            }
        }

        /// <summary>
        /// Button Click for browsing ARM Template File Path
        /// </summary>
        /// <param name="sender">sender (button) </param>
        /// <param name="e">e (EventArgs)</param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string fileName = this.ShowBrowseDialog();
            if (fileName != null && !fileName.Equals(string.Empty))
            {
                // Set the filename browsed
                this.txtARMTemFileName.Text = fileName;
            }
        }

        /// <summary>
        /// Button Click for browsing ARM Parameter File Path
        /// </summary>
        /// <param name="sender">sender (button) </param>
        /// <param name="e">e (EventArgs)</param>
        private void btnBrowseParamFile_Click(object sender, EventArgs e)
        {
            string fileName = this.ShowBrowseDialog();
            if (fileName != null && !fileName.Equals(string.Empty))
            {
                // Set the filename browsed
                this.txtARMParamFileName.Text = fileName;
            }
        }

        /// <summary>
        /// Open the browse dialog box for user to browse the file path
        /// </summary>
        /// <returns> Returns the file path of the file browsed</returns> 
        private string ShowBrowseDialog()
        {
            string fileName = string.Empty;
            try
            {
                DialogResult result = this.FileDialogObj.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = this.FileDialogObj.FileName;
                }
            }
            catch (Exception ex)
            {
                this.AddOperationStatusMessage(string.Format(UIMessageConstants.BrowseFailedMsg, ex.Message));            
            }

            return fileName;
        }

        /// <summary>
        /// Background worker function
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">e (DoWorkEventArgs)</param>
        private void BackGrndWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Create a background worker object
            BackgroundWorker worker = sender as BackgroundWorker;

            // Set the input data
            OperationData opData = (OperationData)e.Argument;

            // Switch to choose the operation to be performed
            switch (opData.Operation)
            {
                // Rate Card Fetch Operation
                case OperationType.FetchRateCard:
                    FetchRateCardInput rateCardInput = (FetchRateCardInput)opData.Data;
                    this.FetchRateCard(rateCardInput.CSPARMPricingCalc, rateCardInput.CSPCreds);
                    e.Result = new OperationData() { Operation = OperationType.FetchRateCard, Data = null };
                    break;

                // CSP ARM Pricing Calculation Operation
                case OperationType.CalculateCSPARMPricing:
                    CalculateCSPARMPricingInput calculateCSPARMPricingInput = (CalculateCSPARMPricingInput)opData.Data;
                    CSPARMPricingInfo info = this.CalculateCSPARMPricing(calculateCSPARMPricingInput.CSPARMPricingCalc, calculateCSPARMPricingInput.ARMTemplateFilePath, calculateCSPARMPricingInput.ARMParamValueFilePath, calculateCSPARMPricingInput.Location, calculateCSPARMPricingInput.CSPCreds);
                    e.Result = new OperationData() { Operation = OperationType.CalculateCSPARMPricing, Data = info };
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Background worker run completed function
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">e (RunWorkerCompletedEventArgs)</param>
        private void BackGrndWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Enable the status of the buttons
            this.ToggleButtonStatus(true);

            // Check if error occured while processing operation or if user cancelled, Display log messages accordingly
            if (e.Error != null)
            {
                this.AddOperationStatusMessage(string.Format(UIMessageConstants.BackgroundOperationFailedMsg, e.Error.Message));
            }
            else if (e.Cancelled)
            {
                this.AddOperationStatusMessage(UIMessageConstants.BackgroundOperationCancelledMsg);
            }
            else
            {
                // Background Operation complete and successful
                OperationData data = (OperationData)e.Result;
                switch (data.Operation)
                {
                    case OperationType.FetchRateCard:
                        StringBuilder str = new StringBuilder();
                        this.AddOperationStatusMessage(UIMessageConstants.RateCardFetchCompleteMsg);
                        break;

                    case OperationType.CalculateCSPARMPricing:
                        // CSP ARM Pricing Calculation Complete
                        OperationData cspARMPricingOutput = (OperationData)e.Result;
                        CSPARMPricingInfo info = (CSPARMPricingInfo)cspARMPricingOutput.Data;
                        this.cspARMPricingInfoOutput = info;

                        if (info != null && info.Log != null)
                        {
                            this.AddOperationStatusMessage(info.Log.ToString());
                        }

                        this.totalCost = 0;

                        if (info != null & info.CSPARMPricingList != null)
                        {
                            // Load grid with result
                            this.AddOperationStatusMessage(UIMessageConstants.GridShowingResultsMsg);
                            this.GridVwCSPARMPricing.DataSource = info.CSPARMPricingList;
                            this.GridVwCSPARMPricing.Refresh();

                            // Get the total cost and Display
                            foreach (ResourceComponent component in (List<ResourceComponent>)info.CSPARMPricingList)
                            {
                                this.totalCost = this.totalCost + component.CostPerMonth;
                            }

                            this.txtTotalRate.Text = string.Format(UIMessageConstants.TotalCostStringMsg, this.totalCost, info.Currency);
                        }

                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Method to fetch the Azure CSP Rate Card, this will be called by the Background worker
        /// </summary>
        /// <param name="cspARMPricingCalc">The object of the class to perform the CSP ARM Pricing Calculation</param>
        /// <param name="cspCreds">CSP Account credentials object. A token will be generated using these credentials and used for making the online Partner Center API call</param>
        private void FetchRateCard(CSPARMPricingCalculator cspARMPricingCalc, CSPAccountCreds cspCreds)
        {
            cspARMPricingCalc.FetchRateCard(cspCreds);
        }

        /// <summary>
        /// Method to add the status messages to the UI
        /// </summary>
        /// <param name="text">Text of the message(s) to be added</param>
        private void AddOperationStatusMessage(string text)
        {
            if (text != null && !text.Equals(string.Empty))
            {
                string msg = string.Empty;
                if (text.ToUpper().Contains(UIMessageConstants.ErrorStringToCheck))
                {
                    msg = string.Format("{0}", text);
                }
                else
                {
                    msg = string.Format("{0} : {1}", DateTime.Now.ToString(), text);
                }

                this.txtOutputMessage.AppendText(msg);
            }
        }

        /// <summary>
        /// Background worker progress change function
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">e (ProgressChangedEventArgs)</param>
        private void BackGrndWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Get the current progress
            string calculationState = (string)e.UserState;

            // Display the progress on the screen
            this.AddOperationStatusMessage(string.Format(UIMessageConstants.ProgressChangeMsg, e.ProgressPercentage, calculationState));
        }

        /// <summary>
        /// Button Click event to clear the file path selections
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">e (EventArgs)</param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear the File Path textboxes
            this.txtARMParamFileName.Text = string.Empty;
            this.txtARMTemFileName.Text = string.Empty;
        }

        /// <summary>
        /// Button Click event to Export results
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">e (EventArgs)</param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            // Show Browse File Path
            string fileName = this.ShowSaveDialog();

            try
            {
                if (fileName != null && !fileName.Equals(string.Empty))
                {
                    // Export the results to a CSV File
                    FileUtil.ExportCSPARMPricingToCSV(fileName, this.cspARMPricingInfoOutput, this.totalCost);
                    this.AddOperationStatusMessage(string.Format(UIMessageConstants.ExportCSVCompleteMsg, fileName));
                }
            }
            catch (Exception ex)
            {
                this.AddOperationStatusMessage(string.Format(UIMessageConstants.ExportCSVFailedMsg, ex.Message));
            }
        }

        /// <summary>
        /// Method that will show the Save Dialog to choose a file path to save a new file
        /// </summary>
        /// <returns> Path of the file to be created</returns>
        private string ShowSaveDialog()
        {
            string fileName = string.Empty;
            try
            {
                DialogResult result = this.SaveFileDialogObj.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = this.SaveFileDialogObj.FileName;
                }
            }
            catch (Exception ex)
            {
                this.AddOperationStatusMessage(string.Format(UIMessageConstants.BrowseFailedMsg, ex.Message));
            }

            return fileName;
        }

        /// <summary>
        /// Form Load function
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">e (EventArgs)</param>
        private void CSPARMPricingCalculator_Load(object sender, EventArgs e)
        {
            // Load Config values if not already loaded
            if (this.cspCreds == null)
            {
                this.LoadConfigSettings();
            }

            // Set a currency as default - USD
            this.ddlCurrency.SelectedIndex = 14;

            // Set a default value for the Azure Location dropdown - 
            this.ddlLocation.SelectedIndex = 5;

            this.GridVwCSPARMPricing.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Class whose instances will store the input and operation type info for the background worker operations
        /// </summary>
        public class OperationData
        {
            /// <summary>
            /// Gets or sets the Operation Type
            /// </summary>
            public OperationType Operation { get; set; }

            /// <summary>
            /// Gets or sets the Input Data
            /// </summary>
            public object Data { get; set; }
        }

        /// <summary>
        /// Class to store the input data for Rate Card Fetch background operation
        /// </summary>
        public class FetchRateCardInput
        {
            /// <summary>
            /// Gets or sets the CSP ARM Pricing Calculator Object
            /// </summary>
            public CSPARMPricingCalculator CSPARMPricingCalc { get; set; }

            /// <summary>
            /// Gets or sets the CSP Account credentials object. A token will be generated using these credentials and used for making the online Partner Center API call
            /// </summary>
            public CSPAccountCreds CSPCreds { get; set; }
        }

        /// <summary>
        /// Class to store the input data for CSP ARM Pricing Calculation background operation
        /// </summary>
        public class CalculateCSPARMPricingInput
        {
            /// <summary>
            /// Gets or sets the CSP ARM Pricing Calculator Object
            /// </summary>
            public CSPARMPricingCalculator CSPARMPricingCalc { get; set; }

            /// <summary>
            /// Gets or sets the ARM Template File Path
            /// </summary>
            public string ARMTemplateFilePath { get; set; }

            /// <summary>
            /// Gets or sets the ARM Parameter File Path
            /// </summary>
            public string ARMParamValueFilePath { get; set; }

            /// <summary>
            /// Gets or sets the Azure Location
            /// </summary>
            public string Location { get; set; }

            /// <summary>
            /// Gets or sets the CSP Account credentials object. A token will be generated using these credentials and used for making the online ARM API call
            /// </summary>
            public CSPAccountCreds CSPCreds { get; set; }
        }
    }
}
