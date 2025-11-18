namespace DVLD
{
    partial class frmUserDetails
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
            this.UserInfoCard = new DVLD.ctrlUserInfoCard();
            this.SuspendLayout();
            // 
            // UserInfoCard
            // 
            this.UserInfoCard.Location = new System.Drawing.Point(12, 12);
            this.UserInfoCard.Name = "UserInfoCard";
            this.UserInfoCard.Size = new System.Drawing.Size(841, 366);
            this.UserInfoCard.TabIndex = 0;
            // 
            // frmUserDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 393);
            this.Controls.Add(this.UserInfoCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserDetails";
            this.Text = "User Info";
            this.ResumeLayout(false);

        }

        #endregion

        public ctrlUserInfoCard UserInfoCard;
    }
}