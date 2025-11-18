namespace DVLD
{
    partial class frmEditAddNewPerson
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
            DVLD.ctrlPersonCard ctrlPersonCard1 =new ctrlPersonCard();
            this.lblEditAddNewPersonHeader = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.ctrlPersonCard1 = new DVLD.ctrlPersonCard();
            this.SuspendLayout();
            // 
            // lblEditAddNewPersonHeader
            // 
            this.lblEditAddNewPersonHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEditAddNewPersonHeader.Font = new System.Drawing.Font("Microsoft YaHei", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditAddNewPersonHeader.ForeColor = System.Drawing.Color.Red;
            this.lblEditAddNewPersonHeader.Location = new System.Drawing.Point(0, 0);
            this.lblEditAddNewPersonHeader.Name = "lblEditAddNewPersonHeader";
            this.lblEditAddNewPersonHeader.Size = new System.Drawing.Size(860, 57);
            this.lblEditAddNewPersonHeader.TabIndex = 1;
            this.lblEditAddNewPersonHeader.Text = "Add New Person";
            this.lblEditAddNewPersonHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Person ID : ";
            // 
            // lblID
            // 
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(144, 66);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(100, 23);
            this.lblID.TabIndex = 4;
            this.lblID.Text = "N / A";
            // 
            // ctrlPersonCard1
            // 
            ctrlPersonCard1.Location = new System.Drawing.Point(13, 114);
            ctrlPersonCard1.MaximumSize = new System.Drawing.Size(835, 390);
            ctrlPersonCard1.MinimumSize = new System.Drawing.Size(835, 390);
            ctrlPersonCard1.Name = "ctrlPersonCard1";
            ctrlPersonCard1.Size = new System.Drawing.Size(835, 390);
            ctrlPersonCard1.TabIndex = 5;
            ctrlPersonCard1.OnNewPersonSaved += new System.Action<int>(this.ctrlPersonCard1_OnPersonSaved);
            ctrlPersonCard1.OnCloseButtonClick += new System.Action(this.ctrlPersonCard1_OnCloseButtonClick);
            // 
            // frmEditAddNewPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 516);
            this.Controls.Add(ctrlPersonCard1);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblEditAddNewPersonHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(878, 563);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(878, 563);
            this.Name = "frmEditAddNewPerson";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit / Add New Person";
            this.Load += new System.EventHandler(this.frmEditAddNewPerson_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.Label lblEditAddNewPersonHeader;
        private ctrlPersonCard ctrlPersonCard1 = new ctrlPersonCard();
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblID;
    }
}