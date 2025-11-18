using System;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class frmIssueDrivingLicenseFirstTime : Form
    {
        public frmIssueDrivingLicenseFirstTime(ref LDLApplication LDLApp)
        {
            InitializeComponent();
            DrivingLicenseApplicationInfo.LDLApplication = LDLApp;
        }

        public delegate void LicenseIssuedEventHandler(int LicenseID);

        public event LicenseIssuedEventHandler LicenseIssued;

        private void IssueLicenseProccess()
        {
            if (DrivingLicenseApplicationInfo.LDLApplication == null) return;
            string Notes = tbNotes.Text;
            DVLD_BusinessLayer.Application application = DrivingLicenseApplicationInfo.LDLApplication.Application;
            if (License.IssueLocalLicenseFirstTime(application, DrivingLicenseApplicationInfo
                .LDLApplication.DrivingClass, Notes, out int LicenseID))
            {
                MessageBox.Show($"License Issued Successfully, License ID={LicenseID}", "DVLD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LicenseIssued?.Invoke(LicenseID);
                this.Close();
            }
            else
                MessageBox.Show("Error!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            IssueLicenseProccess();
        }

    }
}
