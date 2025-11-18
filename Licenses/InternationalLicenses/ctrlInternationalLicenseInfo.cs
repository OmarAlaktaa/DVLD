using DVLD_BusinessLayer;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private InternationalLicense _internationalLicense;
        public InternationalLicense InternationalLicense
        {
            get
            {
                return _internationalLicense;
            }
            set
            {
                _internationalLicense = value;
                DisplayLicenseInfo(ref value);
            }
        }

        private void DisplayLicenseInfo(ref InternationalLicense license)
        {
            if (license == null) return;
            lblName.Text = license.Driver.FullName;
            lblInternationalLicenseID.Text = license.InternationalLicenseID.ToString();
            lblLicenseID.Text = license.LicenseID.ToString();
            lblNationalNo.Text = license.Driver.NationalNumber.ToString();
            lblGendor.Text = license.Driver.Gendor.ToString();
            lblIssueDate.Text = license.IssueDate.ToShortDateString();
            lblApplicationDate.Text = DVLD_BusinessLayer.Application.GetApplication(license.ApplicationID).ApplicationDate.ToShortDateString();
            lblIsActive.Text = license.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = license.Driver.DateOfBirth.ToShortDateString();
            lblDriverID.Text = license.Driver.DriverID.ToString();
            lblExpirationDate.Text = license.ExpirationDate.ToShortDateString();
            if (license.Driver.ImagePath == null)
            {
                if (license.Driver.Gendor == "Male")
                    pbDriverImage.Image = Properties.Resources.Male_512;
                else if (license.Driver.Gendor == "Female")
                    pbDriverImage.Image = Properties.Resources.Female_512;
            }
            else pbDriverImage.ImageLocation = license.Driver.ImagePath;
        }
    }
}
