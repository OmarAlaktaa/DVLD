using DVLD_BusinessLayer;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
                
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, System.EventArgs e)
        {
            InternationalApplicationInfo.InitializeNewInternationalApplication();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ctrlSearchLicense_LicenseFound(object sender, DVLD_BusinessLayer.License license)
        {
            InternationalApplicationInfo.LicenseFound(license);
            llLicenseHistory.Enabled = true;
        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InternationalLicense Ilicense = InternationalApplicationInfo.InternationalLicense;
            frmInternationalLicenseInfo Form = new frmInternationalLicenseInfo(ref Ilicense);
            Form.ShowDialog();
        }

        private void llLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlSearchLicense.license.Driver.PersonID;
            if (ctrlSearchLicense.license == null) return;
            frmLicenseHistory Form = new frmLicenseHistory(PersonID);
            Form.ShowDialog();
        }

        private void DisplayInternationalLicense(InternationalLicense Ilicense)
        {
            InternationalApplicationInfo.InternationalLicenseIssued(Ilicense);
            llLicenseInfo.Enabled = true;
        }

        private void IssueProccess()
        {
            DVLD_BusinessLayer.License license = ctrlSearchLicense.license;
            if (license == null) 
            {
                MessageBox.Show("No Available Local License!", "DVLD", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return; 
            }
            if (license.IsInternational())
            {
                MessageBox.Show("Can't Issue International License for this local license because it's already issued!", "Already Issued!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                InternationalLicense Ilicense = InternationalLicense.Find(license.LicenseID);
                DisplayInternationalLicense(Ilicense);
                return;
            }
            if (InternationalLicense.IssueNewInternationalLicense(license, out InternationalLicense ILicense))
            {
                MessageBox.Show($"New International License Issued Successfully!, InternationalLicenseID={ILicense.InternationalLicenseID}", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayInternationalLicense(ILicense);
            }
        }

        private void btnIssue_Click(object sender, System.EventArgs e)
        {
            IssueProccess();
        }

        private void ctrlSearchLicense_LicenseNotFound(object sender, System.EventArgs e)
        {
            InternationalApplicationInfo.ClearLocalLicense();
            InternationalApplicationInfo.InitializeNewInternationalApplication();
            llLicenseHistory.Enabled = false;
            llLicenseInfo.Enabled = false;
        }
    }
}
