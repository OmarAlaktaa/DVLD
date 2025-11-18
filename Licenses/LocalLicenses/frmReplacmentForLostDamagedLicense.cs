using System;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class frmReplacmentForLostDamagedLicense : Form
    {
        public frmReplacmentForLostDamagedLicense()
        {
            InitializeComponent();
        }

        public enum FormModes
        {
            Damaged = 1, Lost = 2
        }

        public FormModes FormMode;

        public DVLD_BusinessLayer.License oldLicense;
        public DVLD_BusinessLayer.License newLicense;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeDamagedLicense()
        {
            this.Text = "Replacment for Damaged License";
            lblHeader.Text = "Replacment for Damaged License";
            lblApplicationFees.Text = ApplicationType.GetApplicationTypeFees(ApplicationType.ApplicationTypes.ReplacementForDamagedDrivingLicense).ToString();
            this.FormMode = FormModes.Damaged;
        }

        private void InitializeLostLicense()
        {
            this.Text = "Replacment for Lost License";
            lblHeader.Text = "Replacment for Lost License";
            lblApplicationFees.Text = ApplicationType.GetApplicationTypeFees(ApplicationType.ApplicationTypes.ReplacementForLostDrivingLicense).ToString();
            this.FormMode = FormModes.Lost;
        }

        private void InitializeReplacment()
        {
            if (rbDamagedLicense.Checked)
                InitializeDamagedLicense();
            else if (rbLostLicense.Checked)
                InitializeLostLicense();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            InitializeReplacment();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            InitializeReplacment();
        }

        private void InitializeApplication()
        {
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = Globals.CurrentUser.Username;
        }

        private void frmReplacmentForLostDamagedLicense_Load(object sender, EventArgs e)
        {
            llShowLicenseHistory.Enabled = false;
            llShowNewLicenseInfo.Enabled = false;
            btnIssueReplacment.Enabled = false;
            InitializeApplication();
            rbDamagedLicense.Checked = true;
        }

        private void DisableIssueing()
        {
            btnIssueReplacment.Enabled = false;
        }

        private void EnableIssueing()
        {
            btnIssueReplacment.Enabled = true;
        }

        private void oldLicenseFound(DVLD_BusinessLayer.License license)
        {
            oldLicense = license;
            lblOldLicenseID.Text = license.LicenseID.ToString();
            llShowLicenseHistory.Enabled = true;
        }

        private void ctrlSearchLicense_LicenseFound(object sender, License e)
        {
            oldLicenseFound(ctrlSearchLicense.license);
            if (!e.IsActive)
            {
                DisableIssueing();
                MessageBox.Show("Selected License is not active, choose an active license!", "Not Allowed!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                EnableIssueing();
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlSearchLicense.license.Driver.PersonID;
            if (ctrlSearchLicense.license == null) return;
            frmLicenseHistory Form = new frmLicenseHistory(PersonID);
            Form.ShowDialog();
        }

        private void IssueProccess()
        {
            if (SureToReplaceLicense())
            {
                if (rbDamagedLicense.Checked)
                    IssueReplacmentForDamagedLicenseProccess();
                else if (rbLostLicense.Checked)
                    IssueReplacmentForLostLicenseProccess();
            }
        }

        private string ModeString()
        {
            return this.FormMode == FormModes.Lost ? "Lost" : "Damaged";
        }

        private bool SureToReplaceLicense()
        {
            return MessageBox.Show($"Are you sure you want to Replace License with ID={oldLicense.LicenseID} for {ModeString()}?", "Confirm!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        
        private void DisplayNewLicense()
        {
            if (newLicense == null) return;
            lblReplacmentApplicationID.Text = newLicense.ApplicationID.ToString();
            lblReplacedLicenseID.Text = newLicense.LicenseID.ToString();
        }

        private void LicenseReplaced()
        {
            if (newLicense != null) // License Replaced
            {
                ctrlSearchLicense.license = oldLicense; // Display oldLicense after editing
                MessageBox.Show($"License with LicenseID={oldLicense.LicenseID}, Replaced Successfully with LicenseID={newLicense.LicenseID}", "Replaced!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayNewLicense();
                gbReplacmentFor.Enabled = false;
                btnIssueReplacment.Enabled = false;
                llShowNewLicenseInfo.Enabled = true;
                return;
            }
            MessageBox.Show($"Error while replacing License", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void IssueReplacmentForDamagedLicenseProccess()
        {
            newLicense = oldLicense.ReplaceForDamaged();
            LicenseReplaced();
        }

        private void IssueReplacmentForLostLicenseProccess()
        {
            newLicense = oldLicense.ReplaceForLost();
            LicenseReplaced();
        }

        private void btnIssueReplacment_Click(object sender, EventArgs e)
        {
            IssueProccess();
        }

        private void ClearOldLicense()
        {
            lblOldLicenseID.Text = "[???]";
            this.oldLicense = null;
        }

        private void ctrlSearchLicense_LicenseNotFound(object sender, EventArgs e)
        {
            ClearOldLicense();
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (newLicense == null) return;
            else
            {
                frmLicenseInfo Form = new frmLicenseInfo(ref newLicense);
                Form.ShowDialog();
            }
        }
    }
}
