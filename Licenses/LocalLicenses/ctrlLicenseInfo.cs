using DVLD_BusinessLayer;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlLicenseInfo : UserControl
    {
        public ctrlLicenseInfo()
        {
            InitializeComponent();
        }

        private DVLD_BusinessLayer.License _license;

        public DVLD_BusinessLayer.License License
        {
            get { return _license; }
            set
            {
                _license = value;
                DisplayLicenseInfo(ref value);
            }
        }

        private string IssueReasonString(byte IssueReason)
        {
            //1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost
            switch (IssueReason)
            {
                case 1:
                    return "First Time";
                case 2:
                    return "Renew";
                case 3:
                    return "Repalcment for Damaged";
                case 4:
                    return "Replacment for Lost";
                default:
                    return "";
            }
        }

        private System.Drawing.Image DriverImageLocation(string DriverImagePath, string Gendor)
        {
            if (string.IsNullOrEmpty(DriverImagePath))
            {
                switch(Gendor)
                {
                    case "Male":
                        {
                            return DVLD.Properties.Resources.Male_512;
                        }
                    case "Female":
                        {
                            return DVLD.Properties.Resources.Female_512;
                        }
                }
            }
            if (File.Exists(DriverImagePath))
                return System.Drawing.Image.FromFile(DriverImagePath);
            return null;
        }
      
        private void DisplayLicenseInfo(ref DVLD_BusinessLayer.License license)
        {
            if (license == null) {ClearLicense(); return; }
            lblClass.Text = license.LicenseClass.ClassName;
            lblName.Text = license.Driver.FullName;
            lblLicenseID.Text = license.LicenseID.ToString();
            lblNationalNumber.Text = license.Driver.NationalNumber;
            lblGendor.Text = license.Driver.Gendor;
            lblIssueDate.Text = license.IssueDate.ToShortDateString();
            lblIssueReason.Text = IssueReasonString(license.IssueReason);
            lblNotes.Text = (license.Notes == string.Empty ? "No Notes" : license.Notes);
            lblIsActive.Text = (license.IsActive ? "Yes" : "No");
            lblDateOfBirth.Text = license.Driver.DateOfBirth.ToShortDateString();
            lblDriverID.Text = license.Driver.DriverID.ToString();
            lblExpirationDate.Text = license.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = (license.IsDetained(out int DetainID) ? "Yes" : "No");
            pbDriverImage.Image = DriverImageLocation(license.Driver.ImagePath, license.Driver.Gendor);
        }

        public void ClearLicense()
        {
            lblClass.Text = "[???]";
            lblName.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblNationalNumber.Text = "[???]";
            lblGendor.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblIssueReason.Text = "[???]";
            lblNotes.Text = "[???]";
            lblIsActive.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblDriverID.Text = "[???]";
            lblExpirationDate.Text = "[???]";
            lblIsDetained.Text = "[???]";
            pbDriverImage.Image = null;
        }

        public void EditLicenseDetained()
        {
            lblIsDetained.Text = (License.IsDetained(out int DetainID) ? "Yes" : "No");
        }
    }
}
