using DVLD_BusinessLayer;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmLicenseInfo : Form
    {
        public frmLicenseInfo(ref License license)
        {
            InitializeComponent();
            LicenseInfo.License = license;
        }

        public frmLicenseInfo(License license)
        {
            InitializeComponent();
            LicenseInfo.License = license;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
