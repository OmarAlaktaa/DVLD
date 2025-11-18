namespace DVLD
{
    partial class frmPersonDetails
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
            this.PersonInfoCard = new DVLD.ctrlPersonInfoCard();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(305, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Person Details";
            // 
            // PersonInfoCard
            // 
            this.PersonInfoCard.Location = new System.Drawing.Point(12, 76);
            this.PersonInfoCard.MaximumSize = new System.Drawing.Size(832, 287);
            this.PersonInfoCard.MinimumSize = new System.Drawing.Size(832, 287);
            this.PersonInfoCard.Name = "PersonInfoCard";
            this.PersonInfoCard.Person = null;
            this.PersonInfoCard.Size = new System.Drawing.Size(832, 287);
            this.PersonInfoCard.TabIndex = 0;
            this.PersonInfoCard.OnPersonSaved += new System.Action<int>(this.PersonInfoCard_OnPersonSaved);
            // 
            // frmPersonDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 385);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PersonInfoCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(892, 432);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(892, 432);
            this.Name = "frmPersonDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Person Details";
            this.Load += new System.EventHandler(this.frmPersonDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlPersonInfoCard PersonInfoCard;
        private System.Windows.Forms.Label label1;
    }
}