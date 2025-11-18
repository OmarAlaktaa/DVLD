using System.Windows.Forms;
using DVLD_BusinessLayer;
using System;

namespace DVLD
{
    public partial class ctrlInternationalApplicationInfo : UserControl
    {
        public ctrlInternationalApplicationInfo()
        {
            InitializeComponent();
        }

        public void InitializeNewInternationalApplication()
        {
            DateTime ApplicationDate = DateTime.Now;
            ApplicationType appType = ApplicationType.GetApplicationType(ApplicationType.ApplicationTypes.NewInternationalLicense);
            double Fees = appType.ApplicationTypeFees;
            string Username = Globals.CurrentUser.Username;
            lblApplicationDate.Text = ApplicationDate.ToShortDateString();
            lblFees.Text = Fees.ToString();
            lblCreatedBy.Text = Username;
        }

        private InternationalLicense _internationalApplication;
        public InternationalLicense InternationalLicense
        {
            get { return _internationalApplication; }
            set { _internationalApplication = value; }
        }

        private void ClearInternational()
        {
            lblInternationalLicenseID.Text = "[???]";
            lblInternationalLicenseApplicationID.Text = "[???]";
        }

        public void ClearLocalLicense()
        {
            lblIssueDate.Text = "[???]";
            lblExpirationDate.Text = "[???]";
            lblLocalLicenseID.Text = "[???]";
            ClearInternational();
        }

        public void LicenseFound(License license)
        {
            if (license == null) return;
            lblLocalLicenseID.Text = license.LicenseID.ToString();
            lblIssueDate.Text = license.IssueDate.ToShortDateString();
            lblExpirationDate.Text = license.ExpirationDate.ToShortDateString();
            lblCreatedBy.Text = User.Find(license.CreatedByUserID).Username;

            ClearInternational();
        }

        public void InternationalLicenseIssued(InternationalLicense ILicense)
        {
            this.InternationalLicense = ILicense;
            lblInternationalLicenseApplicationID.Text = ILicense.ApplicationID.ToString();
            lblInternationalLicenseID.Text = ILicense.InternationalLicenseID.ToString();
        }
    }
}
