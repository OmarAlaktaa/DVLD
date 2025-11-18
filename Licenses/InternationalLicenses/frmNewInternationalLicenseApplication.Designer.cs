namespace DVLD
{
    partial class frmNewInternationalLicenseApplication
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnIssue = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.llLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.llLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.ctrlSearchLicense = new DVLD.ctrlSearchLicense();
            this.InternationalApplicationInfo = new DVLD.ctrlInternationalApplicationInfo();
            this.ctrlSearchLicenseInfo1 = new DVLD.ctrlSearchLicense();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(197, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(593, 36);
            this.label2.TabIndex = 2;
            this.label2.Text = "International License Application";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnIssue
            // 
            this.btnIssue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIssue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIssue.Image = global::DVLD.Properties.Resources.International_32;
            this.btnIssue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssue.Location = new System.Drawing.Point(823, 691);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(109, 37);
            this.btnIssue.TabIndex = 3;
            this.btnIssue.Text = "Issue";
            this.btnIssue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIssue.UseVisualStyleBackColor = true;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(708, 691);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 37);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // llLicenseHistory
            // 
            this.llLicenseHistory.AutoSize = true;
            this.llLicenseHistory.Enabled = false;
            this.llLicenseHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llLicenseHistory.Location = new System.Drawing.Point(12, 702);
            this.llLicenseHistory.Name = "llLicenseHistory";
            this.llLicenseHistory.Size = new System.Drawing.Size(173, 20);
            this.llLicenseHistory.TabIndex = 5;
            this.llLicenseHistory.TabStop = true;
            this.llLicenseHistory.Text = "Show License History";
            this.llLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llLicenseHistory_LinkClicked);
            // 
            // llLicenseInfo
            // 
            this.llLicenseInfo.AutoSize = true;
            this.llLicenseInfo.Enabled = false;
            this.llLicenseInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llLicenseInfo.Location = new System.Drawing.Point(212, 702);
            this.llLicenseInfo.Name = "llLicenseInfo";
            this.llLicenseInfo.Size = new System.Drawing.Size(146, 20);
            this.llLicenseInfo.TabIndex = 6;
            this.llLicenseInfo.TabStop = true;
            this.llLicenseInfo.Text = "Show License Info";
            this.llLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llLicenseInfo_LinkClicked);
            // 
            // ctrlSearchLicense
            // 
            this.ctrlSearchLicense.license = null;
            this.ctrlSearchLicense.Location = new System.Drawing.Point(12, 48);
            this.ctrlSearchLicense.MaximumSize = new System.Drawing.Size(926, 441);
            this.ctrlSearchLicense.MinimumSize = new System.Drawing.Size(926, 441);
            this.ctrlSearchLicense.Name = "ctrlSearchLicense";
            this.ctrlSearchLicense.Size = new System.Drawing.Size(926, 441);
            this.ctrlSearchLicense.TabIndex = 9;
            this.ctrlSearchLicense.LicenseFound += new System.EventHandler<DVLD_BusinessLayer.License>(this.ctrlSearchLicense_LicenseFound);
            this.ctrlSearchLicense.LicenseNotFound += new System.EventHandler(this.ctrlSearchLicense_LicenseNotFound);
            // 
            // InternationalApplicationInfo
            // 
            this.InternationalApplicationInfo.InternationalLicense = null;
            this.InternationalApplicationInfo.Location = new System.Drawing.Point(12, 495);
            this.InternationalApplicationInfo.MaximumSize = new System.Drawing.Size(920, 190);
            this.InternationalApplicationInfo.MinimumSize = new System.Drawing.Size(920, 190);
            this.InternationalApplicationInfo.Name = "InternationalApplicationInfo";
            this.InternationalApplicationInfo.Size = new System.Drawing.Size(920, 190);
            this.InternationalApplicationInfo.TabIndex = 8;
            // 
            // ctrlSearchLicenseInfo1
            // 
            this.ctrlSearchLicenseInfo1.license = null;
            this.ctrlSearchLicenseInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctrlSearchLicenseInfo1.MaximumSize = new System.Drawing.Size(926, 441);
            this.ctrlSearchLicenseInfo1.MinimumSize = new System.Drawing.Size(926, 441);
            this.ctrlSearchLicenseInfo1.Name = "ctrlSearchLicenseInfo1";
            this.ctrlSearchLicenseInfo1.Size = new System.Drawing.Size(926, 441);
            this.ctrlSearchLicenseInfo1.TabIndex = 0;
            // 
            // frmNewInternationalLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 737);
            this.Controls.Add(this.ctrlSearchLicense);
            this.Controls.Add(this.InternationalApplicationInfo);
            this.Controls.Add(this.llLicenseInfo);
            this.Controls.Add(this.llLicenseHistory);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnIssue);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(968, 784);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(968, 784);
            this.Name = "frmNewInternationalLicenseApplication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Iinternational License Application";
            this.Load += new System.EventHandler(this.frmNewInternationalLicenseApplication_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlSearchLicense ctrlSearchLicenseInfo1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel llLicenseHistory;
        private System.Windows.Forms.LinkLabel llLicenseInfo;
        private ctrlInternationalApplicationInfo InternationalApplicationInfo;
        private ctrlSearchLicense ctrlSearchLicense;
    }
}