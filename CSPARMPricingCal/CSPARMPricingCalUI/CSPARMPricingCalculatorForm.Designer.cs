// -----------------------------------------------------------------------
// <copyright file="CSPARMPricingCalculatorForm.Designer.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CSPARMPricingCalUI
{
    partial class CSPARMPricingCalculatorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GrpBoxAzureSubDetails = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFetchRateCard = new System.Windows.Forms.Button();
            this.ddlCurrency = new System.Windows.Forms.ComboBox();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.lblSubOfferType = new System.Windows.Forms.Label();
            this.lblStep1 = new System.Windows.Forms.Label();
            this.GrpBoxARMTmpDetails = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnBrowseParamFile = new System.Windows.Forms.Button();
            this.txtARMParamFileName = new System.Windows.Forms.TextBox();
            this.lblBrowseParam = new System.Windows.Forms.Label();
            this.ddlLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.btnBrowseTemplate = new System.Windows.Forms.Button();
            this.txtARMTemFileName = new System.Windows.Forms.TextBox();
            this.lblBrowseTemplate = new System.Windows.Forms.Label();
            this.lblStep2 = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.GridVwCSPARMPricing = new System.Windows.Forms.DataGridView();
            this.ResourceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResourceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MeterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsChargeable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostPerMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTotalRate = new System.Windows.Forms.TextBox();
            this.FileDialogObj = new System.Windows.Forms.OpenFileDialog();
            this.txtOutputMessage = new System.Windows.Forms.TextBox();
            this.BackGrndWorker = new System.ComponentModel.BackgroundWorker();
            this.lblStep3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.SaveFileDialogObj = new System.Windows.Forms.SaveFileDialog();
            this.GrpBoxAzureSubDetails.SuspendLayout();
            this.GrpBoxARMTmpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridVwCSPARMPricing)).BeginInit();
            this.SuspendLayout();
            // 
            // GrpBoxAzureSubDetails
            // 
            this.GrpBoxAzureSubDetails.Controls.Add(this.label2);
            this.GrpBoxAzureSubDetails.Controls.Add(this.btnFetchRateCard);
            this.GrpBoxAzureSubDetails.Controls.Add(this.ddlCurrency);
            this.GrpBoxAzureSubDetails.Controls.Add(this.lblCurrency);
            this.GrpBoxAzureSubDetails.Controls.Add(this.lblSubOfferType);
            this.GrpBoxAzureSubDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrpBoxAzureSubDetails.Location = new System.Drawing.Point(12, 27);
            this.GrpBoxAzureSubDetails.Name = "GrpBoxAzureSubDetails";
            this.GrpBoxAzureSubDetails.Size = new System.Drawing.Size(398, 128);
            this.GrpBoxAzureSubDetails.TabIndex = 1;
            this.GrpBoxAzureSubDetails.TabStop = false;
            this.GrpBoxAzureSubDetails.Text = "Azure Subscription Details";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(193, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cloud Solution Provider (CSP)";
            // 
            // btnFetchRateCard
            // 
            this.btnFetchRateCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFetchRateCard.Location = new System.Drawing.Point(136, 91);
            this.btnFetchRateCard.Name = "btnFetchRateCard";
            this.btnFetchRateCard.Size = new System.Drawing.Size(126, 29);
            this.btnFetchRateCard.TabIndex = 4;
            this.btnFetchRateCard.Text = "FETCH RATE CARD";
            this.btnFetchRateCard.UseVisualStyleBackColor = true;
            this.btnFetchRateCard.Click += new System.EventHandler(this.btnFetchRateCard_Click);
            // 
            // ddlCurrency
            // 
            this.ddlCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCurrency.FormattingEnabled = true;
            this.ddlCurrency.Items.AddRange(new object[] {
            "AU-AUD",
            "CA-CAD",
            "CH-CHF",
            "DK-DKK",
            "FR-EUR",
            "GB-GBP",
            "IN-INR",
            "JP-JPY",
            "KR-KRW",
            "NO-NOK",
            "NZ-NZD",
            "RU-RUB",
            "SE-SEK",
            "TW-TWD",
            "US-USD"});
            this.ddlCurrency.Location = new System.Drawing.Point(195, 56);
            this.ddlCurrency.Name = "ddlCurrency";
            this.ddlCurrency.Size = new System.Drawing.Size(176, 23);
            this.ddlCurrency.TabIndex = 3;
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrency.Location = new System.Drawing.Point(41, 54);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(135, 17);
            this.lblCurrency.TabIndex = 2;
            this.lblCurrency.Text = "Region && Currency :";
            // 
            // lblSubOfferType
            // 
            this.lblSubOfferType.AutoSize = true;
            this.lblSubOfferType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubOfferType.Location = new System.Drawing.Point(10, 28);
            this.lblSubOfferType.Name = "lblSubOfferType";
            this.lblSubOfferType.Size = new System.Drawing.Size(166, 17);
            this.lblSubOfferType.TabIndex = 0;
            this.lblSubOfferType.Text = "Subscription Offer Type :";
            // 
            // lblStep1
            // 
            this.lblStep1.AutoSize = true;
            this.lblStep1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep1.Location = new System.Drawing.Point(109, 7);
            this.lblStep1.Name = "lblStep1";
            this.lblStep1.Size = new System.Drawing.Size(131, 15);
            this.lblStep1.TabIndex = 5;
            this.lblStep1.Text = "Step 1: Fetch Ratecard";
            // 
            // GrpBoxARMTmpDetails
            // 
            this.GrpBoxARMTmpDetails.Controls.Add(this.btnClear);
            this.GrpBoxARMTmpDetails.Controls.Add(this.btnBrowseParamFile);
            this.GrpBoxARMTmpDetails.Controls.Add(this.txtARMParamFileName);
            this.GrpBoxARMTmpDetails.Controls.Add(this.lblBrowseParam);
            this.GrpBoxARMTmpDetails.Controls.Add(this.ddlLocation);
            this.GrpBoxARMTmpDetails.Controls.Add(this.lblLocation);
            this.GrpBoxARMTmpDetails.Controls.Add(this.btnBrowseTemplate);
            this.GrpBoxARMTmpDetails.Controls.Add(this.txtARMTemFileName);
            this.GrpBoxARMTmpDetails.Controls.Add(this.lblBrowseTemplate);
            this.GrpBoxARMTmpDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrpBoxARMTmpDetails.Location = new System.Drawing.Point(443, 24);
            this.GrpBoxARMTmpDetails.Name = "GrpBoxARMTmpDetails";
            this.GrpBoxARMTmpDetails.Size = new System.Drawing.Size(634, 131);
            this.GrpBoxARMTmpDetails.TabIndex = 2;
            this.GrpBoxARMTmpDetails.TabStop = false;
            this.GrpBoxARMTmpDetails.Text = "ARM Template";
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(431, 81);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(147, 25);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "Clear File Selections";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnBrowseParamFile
            // 
            this.btnBrowseParamFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseParamFile.Location = new System.Drawing.Point(536, 52);
            this.btnBrowseParamFile.Name = "btnBrowseParamFile";
            this.btnBrowseParamFile.Size = new System.Drawing.Size(73, 25);
            this.btnBrowseParamFile.TabIndex = 9;
            this.btnBrowseParamFile.Text = "BROWSE";
            this.btnBrowseParamFile.UseVisualStyleBackColor = true;
            this.btnBrowseParamFile.Click += new System.EventHandler(this.btnBrowseParamFile_Click);
            // 
            // txtARMParamFileName
            // 
            this.txtARMParamFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtARMParamFileName.Location = new System.Drawing.Point(213, 54);
            this.txtARMParamFileName.Name = "txtARMParamFileName";
            this.txtARMParamFileName.ReadOnly = true;
            this.txtARMParamFileName.Size = new System.Drawing.Size(317, 21);
            this.txtARMParamFileName.TabIndex = 8;
            this.txtARMParamFileName.Text = "";
            // 
            // lblBrowseParam
            // 
            this.lblBrowseParam.AutoSize = true;
            this.lblBrowseParam.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrowseParam.Location = new System.Drawing.Point(76, 56);
            this.lblBrowseParam.Name = "lblBrowseParam";
            this.lblBrowseParam.Size = new System.Drawing.Size(126, 17);
            this.lblBrowseParam.TabIndex = 7;
            this.lblBrowseParam.Text = "Select Param File :";
            // 
            // ddlLocation
            // 
            this.ddlLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlLocation.FormattingEnabled = true;
            this.ddlLocation.Items.AddRange(new object[] {
            "East Asia",
            "Southeast Asia",
            "Central US",
            "East US",
            "East US 2",
            "West US",
            "North Central US",
            "South Central US",
            "North Europe",
            "West Europe",
            "Japan West",
            "Japan East",
            "Brazil South",
            "Australia East",
            "Australia Southeast",
            "Canada Central",
            "Canada East",
            "West Central US",
            "West US 2"});
            this.ddlLocation.Location = new System.Drawing.Point(213, 82);
            this.ddlLocation.Name = "ddlLocation";
            this.ddlLocation.Size = new System.Drawing.Size(176, 23);
            this.ddlLocation.TabIndex = 4;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(10, 85);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(192, 17);
            this.lblLocation.TabIndex = 3;
            this.lblLocation.Text = "Select Deployment Location :";
            // 
            // btnBrowseTemplate
            // 
            this.btnBrowseTemplate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseTemplate.Location = new System.Drawing.Point(536, 20);
            this.btnBrowseTemplate.Name = "btnBrowseTemplate";
            this.btnBrowseTemplate.Size = new System.Drawing.Size(73, 25);
            this.btnBrowseTemplate.TabIndex = 2;
            this.btnBrowseTemplate.Text = "BROWSE";
            this.btnBrowseTemplate.UseVisualStyleBackColor = true;
            this.btnBrowseTemplate.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtARMTemFileName
            // 
            this.txtARMTemFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtARMTemFileName.Location = new System.Drawing.Point(212, 22);
            this.txtARMTemFileName.Name = "txtARMTemFileName";
            this.txtARMTemFileName.ReadOnly = true;
            this.txtARMTemFileName.Size = new System.Drawing.Size(317, 21);
            this.txtARMTemFileName.TabIndex = 1;
            this.txtARMTemFileName.Text = "";
            // 
            // lblBrowseTemplate
            // 
            this.lblBrowseTemplate.AutoSize = true;
            this.lblBrowseTemplate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrowseTemplate.Location = new System.Drawing.Point(24, 22);
            this.lblBrowseTemplate.Name = "lblBrowseTemplate";
            this.lblBrowseTemplate.Size = new System.Drawing.Size(178, 17);
            this.lblBrowseTemplate.TabIndex = 0;
            this.lblBrowseTemplate.Text = "Select ARM Template File :";
            // 
            // lblStep2
            // 
            this.lblStep2.AutoSize = true;
            this.lblStep2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep2.Location = new System.Drawing.Point(652, 8);
            this.lblStep2.Name = "lblStep2";
            this.lblStep2.Size = new System.Drawing.Size(167, 15);
            this.lblStep2.TabIndex = 6;
            this.lblStep2.Text = "Step 2: Select ARM Template";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalculate.Location = new System.Drawing.Point(347, 172);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(184, 31);
            this.btnCalculate.TabIndex = 3;
            this.btnCalculate.Text = "CALCULATE Cost";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // GridVwCSPARMPricing
            // 
            this.GridVwCSPARMPricing.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridVwCSPARMPricing.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridVwCSPARMPricing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridVwCSPARMPricing.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ResourceName,
            this.ResourceType,
            this.MeterName,
            this.IsChargeable,
            this.Quantity,
            this.Rate,
            this.CostPerMonth});
            this.GridVwCSPARMPricing.EnableHeadersVisualStyles = false;
            this.GridVwCSPARMPricing.Location = new System.Drawing.Point(12, 217);
            this.GridVwCSPARMPricing.Name = "GridVwCSPARMPricing";
            this.GridVwCSPARMPricing.ReadOnly = true;
            this.GridVwCSPARMPricing.Size = new System.Drawing.Size(1065, 330);
            this.GridVwCSPARMPricing.TabIndex = 4;
            // 
            // ResourceName
            // 
            this.ResourceName.DataPropertyName = "ResourceName";
            this.ResourceName.HeaderText = "Resource Name";
            this.ResourceName.Name = "ResourceName";
            this.ResourceName.ReadOnly = true;
            this.ResourceName.Width = 122;
            // 
            // ResourceType
            // 
            this.ResourceType.DataPropertyName = "ResourceType";
            this.ResourceType.HeaderText = "Resource Type";
            this.ResourceType.Name = "ResourceType";
            this.ResourceType.ReadOnly = true;
            this.ResourceType.Width = 114;
            // 
            // MeterName
            // 
            this.MeterName.DataPropertyName = "MeterName";
            this.MeterName.HeaderText = "Meter Name";
            this.MeterName.Name = "MeterName";
            this.MeterName.ReadOnly = true;
            this.MeterName.Width = 101;
            // 
            // IsChargeable
            // 
            this.IsChargeable.DataPropertyName = "IsChargeable";
            this.IsChargeable.HeaderText = "Is Chargeable?";
            this.IsChargeable.Name = "IsChargeable";
            this.IsChargeable.ReadOnly = true;
            this.IsChargeable.Width = 115;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle2;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 76;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N6";
            dataGridViewCellStyle3.NullValue = null;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle3;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.Width = 58;
            // 
            // CostPerMonth
            // 
            this.CostPerMonth.DataPropertyName = "CostPerMonth";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.CostPerMonth.DefaultCellStyle = dataGridViewCellStyle4;
            this.CostPerMonth.HeaderText = "Cost / Month";
            this.CostPerMonth.Name = "CostPerMonth";
            this.CostPerMonth.ReadOnly = true;
            // 
            // txtTotalRate
            // 
            this.txtTotalRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalRate.Location = new System.Drawing.Point(12, 548);
            this.txtTotalRate.Name = "txtTotalRate";
            this.txtTotalRate.ReadOnly = true;
            this.txtTotalRate.Size = new System.Drawing.Size(329, 20);
            this.txtTotalRate.TabIndex = 5;
            this.txtTotalRate.Text = "Total Cost:";
            // 
            // FileDialogObj
            // 
            this.FileDialogObj.Title = "Select ARM Template File";
            // 
            // txtOutputMessage
            // 
            this.txtOutputMessage.BackColor = System.Drawing.Color.Linen;
            this.txtOutputMessage.Location = new System.Drawing.Point(12, 597);
            this.txtOutputMessage.Multiline = true;
            this.txtOutputMessage.Name = "txtOutputMessage";
            this.txtOutputMessage.ReadOnly = true;
            this.txtOutputMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutputMessage.Size = new System.Drawing.Size(1065, 69);
            this.txtOutputMessage.TabIndex = 6;
            // 
            // BackGrndWorker
            // 
            this.BackGrndWorker.WorkerReportsProgress = true;
            this.BackGrndWorker.WorkerSupportsCancellation = true;
            this.BackGrndWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackGrndWorker_DoWork);
            this.BackGrndWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackGrndWorker_ProgressChanged);
            this.BackGrndWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackGrndWorker_RunWorkerCompleted);
            // 
            // lblStep3
            // 
            this.lblStep3.AutoSize = true;
            this.lblStep3.Location = new System.Drawing.Point(241, 184);
            this.lblStep3.Name = "lblStep3";
            this.lblStep3.Size = new System.Drawing.Size(88, 13);
            this.lblStep3.TabIndex = 7;
            this.lblStep3.Text = "Step 3: Calculate";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 579);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Operation Status:";
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(874, 553);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(184, 31);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "EXPORT  TO FILE";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // SaveFileDialogObj
            // 
            this.SaveFileDialogObj.Filter = "CSV Files (*.csv)|*.csv";
            this.SaveFileDialogObj.Title = "Export ";
            // 
            // CSPARMPricingCalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 672);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lblStep1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblStep2);
            this.Controls.Add(this.lblStep3);
            this.Controls.Add(this.txtOutputMessage);
            this.Controls.Add(this.txtTotalRate);
            this.Controls.Add(this.GridVwCSPARMPricing);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.GrpBoxARMTmpDetails);
            this.Controls.Add(this.GrpBoxAzureSubDetails);
            this.Name = "CSPARMPricingCalculatorForm";
            this.Text = "CSP ARM Pricing Calculator";
            this.Load += new System.EventHandler(this.CSPARMPricingCalculator_Load);
            this.GrpBoxAzureSubDetails.ResumeLayout(false);
            this.GrpBoxAzureSubDetails.PerformLayout();
            this.GrpBoxARMTmpDetails.ResumeLayout(false);
            this.GrpBoxARMTmpDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridVwCSPARMPricing)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpBoxAzureSubDetails;
        private System.Windows.Forms.Label lblSubOfferType;
        private System.Windows.Forms.ComboBox ddlCurrency;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.GroupBox GrpBoxARMTmpDetails;
        private System.Windows.Forms.Button btnBrowseTemplate;
        private System.Windows.Forms.TextBox txtARMTemFileName;
        private System.Windows.Forms.Label lblBrowseTemplate;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.DataGridView GridVwCSPARMPricing;
        private System.Windows.Forms.TextBox txtTotalRate;
        private System.Windows.Forms.OpenFileDialog FileDialogObj;
        private System.Windows.Forms.ComboBox ddlLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Button btnFetchRateCard;
        private System.Windows.Forms.TextBox txtOutputMessage;
        private System.ComponentModel.BackgroundWorker BackGrndWorker;
        private System.Windows.Forms.Label lblStep1;
        private System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.Label lblStep3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtARMParamFileName;
        private System.Windows.Forms.Label lblBrowseParam;
        private System.Windows.Forms.Button btnBrowseParamFile;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResourceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResourceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn MeterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsChargeable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostPerMonth;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog SaveFileDialogObj;
        private System.Windows.Forms.Label label2;
    }
}

