namespace freeArve
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fdRegNrMapping = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenFileDialogue1 = new System.Windows.Forms.Button();
            this.label_file_2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.selectedSourceFileLabel = new System.Windows.Forms.Label();
            this.label_file_1 = new System.Windows.Forms.Label();
            this.btnConvertAndSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelFilePath2 = new System.Windows.Forms.Label();
            this.buttonMappingFile = new System.Windows.Forms.Button();
            this.fdPaymentsFile = new System.Windows.Forms.OpenFileDialog();
            this.buttonExit = new System.Windows.Forms.Button();
            this.resultTextBox = new System.Windows.Forms.RichTextBox();
            this.labelConfigPathTitle = new System.Windows.Forms.Label();
            this.labelSellerName = new System.Windows.Forms.Label();
            this.labelSellerAccountNumber = new System.Windows.Forms.Label();
            this.configFileLink = new System.Windows.Forms.LinkLabel();
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.labelCompanyAccountNumber = new System.Windows.Forms.Label();
            this.labelAccountNumber = new System.Windows.Forms.Label();
            this.linkLabelOutputFile = new System.Windows.Forms.LinkLabel();
            this.linkLabelLogFile = new System.Windows.Forms.LinkLabel();
            this.comboConfigFiles = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenFileDialogue1
            // 
            this.btnOpenFileDialogue1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnOpenFileDialogue1.Location = new System.Drawing.Point(3, 5);
            this.btnOpenFileDialogue1.Name = "btnOpenFileDialogue1";
            this.btnOpenFileDialogue1.Size = new System.Drawing.Size(169, 29);
            this.btnOpenFileDialogue1.TabIndex = 0;
            this.btnOpenFileDialogue1.Text = "Browse...";
            this.btnOpenFileDialogue1.UseVisualStyleBackColor = true;
            this.btnOpenFileDialogue1.Click += new System.EventHandler(this.btnOpenFileDialogue1_Click);
            // 
            // label_file_2
            // 
            this.label_file_2.AutoSize = true;
            this.label_file_2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_file_2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label_file_2.Location = new System.Drawing.Point(17, 120);
            this.label_file_2.Name = "label_file_2";
            this.label_file_2.Size = new System.Drawing.Size(202, 20);
            this.label_file_2.TabIndex = 2;
            this.label_file_2.Text = "Isikukoodide ja kontode fail";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.selectedSourceFileLabel);
            this.panel1.Controls.Add(this.btnOpenFileDialogue1);
            this.panel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(17, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(921, 46);
            this.panel1.TabIndex = 4;
            // 
            // selectedSourceFileLabel
            // 
            this.selectedSourceFileLabel.AutoSize = true;
            this.selectedSourceFileLabel.Location = new System.Drawing.Point(178, 12);
            this.selectedSourceFileLabel.Name = "selectedSourceFileLabel";
            this.selectedSourceFileLabel.Size = new System.Drawing.Size(119, 20);
            this.selectedSourceFileLabel.TabIndex = 6;
            this.selectedSourceFileLabel.Text = "No file selected..";
            // 
            // label_file_1
            // 
            this.label_file_1.AutoSize = true;
            this.label_file_1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_file_1.Location = new System.Drawing.Point(22, 15);
            this.label_file_1.Name = "label_file_1";
            this.label_file_1.Size = new System.Drawing.Size(151, 20);
            this.label_file_1.TabIndex = 2;
            this.label_file_1.Text = "Arvete faili asukoht:";
            // 
            // btnConvertAndSave
            // 
            this.btnConvertAndSave.Location = new System.Drawing.Point(656, 247);
            this.btnConvertAndSave.Name = "btnConvertAndSave";
            this.btnConvertAndSave.Size = new System.Drawing.Size(272, 83);
            this.btnConvertAndSave.TabIndex = 7;
            this.btnConvertAndSave.Text = "Konverteeri ja salvesta e-arve fail";
            this.btnConvertAndSave.UseVisualStyleBackColor = true;
            this.btnConvertAndSave.Click += new System.EventHandler(this.btnConvertAndSave_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.labelFilePath2);
            this.panel2.Controls.Add(this.buttonMappingFile);
            this.panel2.ForeColor = System.Drawing.SystemColors.Control;
            this.panel2.Location = new System.Drawing.Point(17, 143);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(921, 43);
            this.panel2.TabIndex = 6;
            // 
            // labelFilePath2
            // 
            this.labelFilePath2.AutoSize = true;
            this.labelFilePath2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelFilePath2.Location = new System.Drawing.Point(178, 7);
            this.labelFilePath2.Name = "labelFilePath2";
            this.labelFilePath2.Size = new System.Drawing.Size(119, 20);
            this.labelFilePath2.TabIndex = 6;
            this.labelFilePath2.Text = "No file selected..";
            // 
            // buttonMappingFile
            // 
            this.buttonMappingFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonMappingFile.Location = new System.Drawing.Point(3, 3);
            this.buttonMappingFile.Name = "buttonMappingFile";
            this.buttonMappingFile.Size = new System.Drawing.Size(169, 29);
            this.buttonMappingFile.TabIndex = 0;
            this.buttonMappingFile.Text = "Browse...";
            this.buttonMappingFile.UseVisualStyleBackColor = true;
            this.buttonMappingFile.Click += new System.EventHandler(this.buttonMappingFile_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(801, 540);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(127, 37);
            this.buttonExit.TabIndex = 7;
            this.buttonExit.Text = "Välju";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // resultTextBox
            // 
            this.resultTextBox.CausesValidation = false;
            this.resultTextBox.Location = new System.Drawing.Point(17, 232);
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.Size = new System.Drawing.Size(633, 217);
            this.resultTextBox.TabIndex = 10;
            this.resultTextBox.Text = "";
            // 
            // labelConfigPathTitle
            // 
            this.labelConfigPathTitle.AutoSize = true;
            this.labelConfigPathTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelConfigPathTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelConfigPathTitle.Location = new System.Drawing.Point(17, 499);
            this.labelConfigPathTitle.Name = "labelConfigPathTitle";
            this.labelConfigPathTitle.Size = new System.Drawing.Size(81, 20);
            this.labelConfigPathTitle.TabIndex = 2;
            this.labelConfigPathTitle.Text = "Config file:";
            this.labelConfigPathTitle.Click += new System.EventHandler(this.configFileLink_Clicked);
            // 
            // labelSellerName
            // 
            this.labelSellerName.AutoSize = true;
            this.labelSellerName.Location = new System.Drawing.Point(16, 461);
            this.labelSellerName.Name = "labelSellerName";
            this.labelSellerName.Size = new System.Drawing.Size(82, 20);
            this.labelSellerName.TabIndex = 11;
            this.labelSellerName.Text = "Firma nimi:";
            // 
            // labelSellerAccountNumber
            // 
            this.labelSellerAccountNumber.AutoSize = true;
            this.labelSellerAccountNumber.Location = new System.Drawing.Point(17, 540);
            this.labelSellerAccountNumber.Name = "labelSellerAccountNumber";
            this.labelSellerAccountNumber.Size = new System.Drawing.Size(107, 20);
            this.labelSellerAccountNumber.TabIndex = 12;
            this.labelSellerAccountNumber.Text = "Konto number:";
            // 
            // configFileLink
            // 
            this.configFileLink.Location = new System.Drawing.Point(469, 501);
            this.configFileLink.Name = "configFileLink";
            this.configFileLink.Size = new System.Drawing.Size(431, 23);
            this.configFileLink.TabIndex = 0;
            this.configFileLink.Click += new System.EventHandler(this.configFileLink_Clicked);
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.AutoSize = true;
            this.labelCompanyName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCompanyName.Location = new System.Drawing.Point(128, 461);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(0, 20);
            this.labelCompanyName.TabIndex = 11;
            // 
            // labelCompanyAccountNumber
            // 
            this.labelCompanyAccountNumber.AutoSize = true;
            this.labelCompanyAccountNumber.Location = new System.Drawing.Point(130, 540);
            this.labelCompanyAccountNumber.Name = "labelCompanyAccountNumber";
            this.labelCompanyAccountNumber.Size = new System.Drawing.Size(0, 20);
            this.labelCompanyAccountNumber.TabIndex = 11;
            // 
            // labelAccountNumber
            // 
            this.labelAccountNumber.AutoSize = true;
            this.labelAccountNumber.Location = new System.Drawing.Point(130, 540);
            this.labelAccountNumber.Name = "labelAccountNumber";
            this.labelAccountNumber.Size = new System.Drawing.Size(0, 20);
            this.labelAccountNumber.TabIndex = 11;
            // 
            // linkLabelOutputFile
            // 
            this.linkLabelOutputFile.AutoSize = true;
            this.linkLabelOutputFile.Location = new System.Drawing.Point(656, 355);
            this.linkLabelOutputFile.Name = "linkLabelOutputFile";
            this.linkLabelOutputFile.Size = new System.Drawing.Size(120, 20);
            this.linkLabelOutputFile.TabIndex = 13;
            this.linkLabelOutputFile.TabStop = true;
            this.linkLabelOutputFile.Text = "Genereeritud fail";
            this.linkLabelOutputFile.Visible = false;
            this.linkLabelOutputFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelOutputFile_LinkClicked);
            // 
            // linkLabelLogFile
            // 
            this.linkLabelLogFile.AutoSize = true;
            this.linkLabelLogFile.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.linkLabelLogFile.Location = new System.Drawing.Point(656, 398);
            this.linkLabelLogFile.Name = "linkLabelLogFile";
            this.linkLabelLogFile.Size = new System.Drawing.Size(33, 17);
            this.linkLabelLogFile.TabIndex = 13;
            this.linkLabelLogFile.TabStop = true;
            this.linkLabelLogFile.Text = "Logi";
            this.linkLabelLogFile.Visible = false;
            this.linkLabelLogFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLogFile_LinkClicked);
            // 
            // comboConfigFiles
            // 
            this.comboConfigFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboConfigFiles.DropDownWidth = 300;
            this.comboConfigFiles.FormattingEnabled = true;
            this.comboConfigFiles.Location = new System.Drawing.Point(128, 496);
            this.comboConfigFiles.Name = "comboConfigFiles";
            this.comboConfigFiles.Size = new System.Drawing.Size(335, 28);
            this.comboConfigFiles.TabIndex = 14;
            this.comboConfigFiles.SelectedIndexChanged += new System.EventHandler(this.comboConfigFiles_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(949, 592);
            this.Controls.Add(this.comboConfigFiles);
            this.Controls.Add(this.linkLabelLogFile);
            this.Controls.Add(this.linkLabelOutputFile);
            this.Controls.Add(this.configFileLink);
            this.Controls.Add(this.labelSellerAccountNumber);
            this.Controls.Add(this.labelCompanyAccountNumber);
            this.Controls.Add(this.labelAccountNumber);
            this.Controls.Add(this.labelCompanyName);
            this.Controls.Add(this.labelSellerName);
            this.Controls.Add(this.labelConfigPathTitle);
            this.Controls.Add(this.label_file_2);
            this.Controls.Add(this.resultTextBox);
            this.Controls.Add(this.label_file_1);
            this.Controls.Add(this.btnConvertAndSave);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "Form1";
            this.Text = "freeEArve";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public OpenFileDialog fdRegNrMapping;
        private Button btnOpenFileDialogue1;
        private Label label_file_2;
        private Panel panel1;
        private Label selectedSourceFileLabel;
        private Panel panel2;
        private Label labelFilePath2;
        public OpenFileDialog fdPaymentsFile;
        private Button btnConvertAndSave;
        private Button buttonMappingFile;
        private Label label_file_1;
        private Button buttonExit;
        public RichTextBox resultTextBox;
        private Label labelStatusLine;
        private Label labelStatus2;
        private Label labelStatus3;
        private LinkLabel configFileLink;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel3;
        private Label labelConfigTitle;
        private Label labelSellerName;
        private Label labelSellerAccountNumber;
        private Label labelCompanyName;
        private Label labelCompanyAccountNumber;
        private Label labelConfigPathTitle;
        private Label labelAccountNumber;
        private LinkLabel linkLabelOutputFile;
        private LinkLabel linkLabelLogFile;
        private ComboBox comboConfigFiles;
    }
}