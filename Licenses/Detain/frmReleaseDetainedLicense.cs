using System;
using System.ComponentModel;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class frmReleaseDetainedLicense : Form
    {
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(DVLD_BusinessLayer.License licenseToRelease)
        {
            InitializeComponent();
            ctrlSearchLicense.LockSearching();
            this.License = licenseToRelease; // this automaticlly load license and detain info
        }

        private DVLD_BusinessLayer.License _license;
        public DVLD_BusinessLayer.License License
        {
            get
            {
                return _license;
            }
            set
            {
                _license = value;
                _LoadLicenseInfo(value);
            }
        }

        public Detain detain { get; set; }

        private void _LoadLicenseInfo(DVLD_BusinessLayer.License license)
        {
            if (license == null)
            {
                MessageBox.Show("Passed license is empty!", "Cannot Load License!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            // Check if licese is already detained
            if(!license.IsDetained(out int detainID))
            {
                // License is not detained to release
                MessageBox.Show("Selected License is not detained, you can just release a detain license", "License is not detained!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            detain = Detain.Find(detainID);
            ctrlSearchLicense.license = license;
            lblLicenseID.Text = license.LicenseID.ToString();
            if (detain == null) MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else _LoadDetainInfo(detain);
        }

        private void _LoadDetainInfo(Detain detain)
        {
            if (detain == null) return;

            lblDetainID.Text = detain.DetainID.ToString();
            lblDetainDate.Text = detain.DetainDate.ToShortDateString();
            lblCreatedBy.Text = Globals.CurrentUser.Username;
            double AppFees = ApplicationType.GetApplicationTypeFees(ApplicationType.ApplicationTypes.ReleaseDetainedDrivingLicsense);
            lblApplicationFees.Text = AppFees.ToString();
            lblFineFees.Text = detain.FineFees.ToString();
            lblTotalFees.Text = (detain.FineFees + AppFees).ToString();

        }

        private void ctrlSearchLicense_LicenseFound(object sender, DVLD_BusinessLayer.License e)
        {
            this._license = ctrlSearchLicense.license;

            int detainID = e.GetDetainID();
            detain = Detain.Find(detainID);
            if (detain == null) return;

            lblLicenseID.Text = e.LicenseID.ToString();
            if (detain == null) MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else _LoadDetainInfo(detain);

            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (this.License == null) return;
            DVLD_BusinessLayer.Detain detain = this.License.Release();
            if (detain != null)
            {
                // License Released
                lblReleaseApplicationID.Text = detain.Released.ReleaseApplicationID.ToString();
                MessageBox.Show($"License with LicenseID={detain.DetainedLicense.LicenseID}, Released Successfully, Release applicationID={detain.Released.ReleaseApplicationID}", "Released!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnRelease.Enabled = false;
            }

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory Form = new frmLicenseHistory(this.License.Driver.PersonID);
            Form.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo Form = new frmLicenseInfo(this.License);
            Form.ShowDialog();
        }

        private void _ClearDetainInfo()
        {
            this.detain = null;
            lblDetainID.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblDetainID.Text = "[???]";
            lblDetainDate.Text = "[???]";
            lblCreatedBy.Text = "[???]";
            lblApplicationFees.Text = "[???]";
            lblFineFees.Text = "[???]";
            lblTotalFees.Text = "[???]";
        }

        private void ctrlSearchLicense_LicenseNotFound(object sender, EventArgs e)
        {
            btnRelease.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            this.License = null;
            _ClearDetainInfo();
        }
    }
}
