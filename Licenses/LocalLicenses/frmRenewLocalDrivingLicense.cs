using DVLD_BusinessLayer;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmRenewLocalDrivingLicense : Form
    {
        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
        }

        public DVLD_BusinessLayer.License oldLicense = null;

        public DVLD_BusinessLayer.License newLicense = null;

        private void EnableRenewing()
        {
            btnRenew.Enabled = true;
        }

        private void DisplayOldLicenseInfo(ref DVLD_BusinessLayer.License oLicense)
        {
            lblOldLicenseID.Text = oLicense.LicenseID.ToString();
            lblExpirationDate.Text = oLicense.ExpirationDate.ToShortDateString();
            double LicenseFees = oLicense.LicenseClass.ClassFees;
            lblLicenseFees.Text = LicenseFees.ToString();
            double ApplicationFees = ApplicationType.GetApplicationType(ApplicationType.ApplicationTypes.RenewDrivingLicenseService).ApplicationTypeFees;
            lblTotalFees.Text = (LicenseFees + ApplicationFees).ToString();
            llShowLicensesHistory.Enabled = true;
        }

        private void ctrlSearchLicense_LicenseFound(object sender, DVLD_BusinessLayer.License e)
        {
            DisplayOldLicenseInfo(ref e);
            if (!ctrlSearchLicense.license.IsExpired())
            {
                MessageBox.Show($"Selected License is not expired yet, it will expire on {e.ExpirationDate.ToShortDateString()}", "Not Allowed!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!ctrlSearchLicense.license.IsActive)
            {
                MessageBox.Show($"Selected License is not Active!, You can't Renew a deactived license.", "Not Allowed!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            EnableRenewing();
        }

        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            InitializeNewApplication();
        }

        private void InitializeNewApplication()
        {
            btnRenew.Enabled = false;
            llShowLicensesHistory.Enabled = false;
            llShowNewLicenseInfo.Enabled = false;

            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = ApplicationType.GetApplicationType(
                ApplicationType.ApplicationTypes.RenewDrivingLicenseService).ApplicationTypeFees.ToString();
            lblCreatedBy.Text = Globals.CurrentUser.Username;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlSearchLicense.license.Driver.PersonID;
            if (ctrlSearchLicense.license == null) return;
            frmLicenseHistory Form = new frmLicenseHistory(PersonID);
            Form.ShowDialog();
        }

        private void DisplayRenewedLicense(ref DVLD_BusinessLayer.License license)
        {
            if (license == null) return;
            lblRenewedLicenseApplicationID.Text = license.ApplicationID.ToString();
            lblRenewedLicenseID.Text = license.LicenseID.ToString();
            llShowNewLicenseInfo.Enabled = true;
        }

        private void RenewLicenseProccess()
        {
            oldLicense = ctrlSearchLicense.license;
            string newLicenseNotes = tbNotes.Text;
            newLicense = oldLicense.RenewLicense(newLicenseNotes);
            if (newLicense != null)
            {
                DisplayRenewedLicense(ref newLicense);
                MessageBox.Show($"License with LicenseID={oldLicense.LicenseID}, Renewed Successfully with LicenseID={newLicense.LicenseID}", "Renewed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
                MessageBox.Show("Error while renewing license!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnRenew.Enabled = false;
        }

        private bool SureToRenewLicense()
        {
            return MessageBox.Show("Are you sure you want to renew license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (SureToRenewLicense())
                RenewLicenseProccess();
        }

        private void ClearOldLicenseInfo()
        {
            lblOldLicenseID.Text = "[???]";
            lblExpirationDate.Text = "[???]";
            lblLicenseFees.Text = "[$$$]";
            lblTotalFees.Text = "[$$$]";
            llShowLicensesHistory.Enabled = false;
        }

        private void ctrlSearchLicense_LicenseNotFound(object sender, EventArgs e)
        {
            ClearOldLicenseInfo();
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (newLicense == null) return;
            frmLicenseInfo Form = new frmLicenseInfo(ref newLicense);
            Form.ShowDialog();
        }
    }
}
