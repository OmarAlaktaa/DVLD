using System.Windows.Forms;
using DVLD_BusinessLayer;
using System;

namespace DVLD
{
    public partial class ctrlSearchLicense : UserControl
    {
        public ctrlSearchLicense()
        {
            InitializeComponent();
        }

        private License _license;
        public License license
        {
            get
            {
                return _license;
            }
            set
            {
                _license = value;
                LicenseInfo.License = value;
            }
        }

        public event EventHandler<License> LicenseFound;

        public event EventHandler LicenseNotFound;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbLicenseID.Text, out int LicenseID))
            {
                this.license = License.Find(LicenseID);
                if (license != null) // License found
                {
                    LicenseFound?.Invoke(this, license);
                }
                else // License Not Found
                {
                    LicenseNotFound?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show($"No License found with LicenseID={LicenseID}",
                        "Invalid LicensID", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void tbLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validations.ValidateInput.ValidateNumberInput(e);
        }

        public void EditLicenseDetained()
        {
            LicenseInfo.EditLicenseDetained();
        }

        public void LockSearching()
        {
            gbFilter.Enabled = false;
        }

        public void UnlockSearching()
        {
            gbFilter.Enabled = true;
        }
    }
}
