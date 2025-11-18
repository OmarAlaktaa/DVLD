using DVLD_BusinessLayer;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmInternationalLicenseInfo : Form
    {
        public frmInternationalLicenseInfo(ref InternationalLicense Ilicense)
        {
            InitializeComponent();
            if (Ilicense == null) return;
            else ctrlInternationalLicenseInfo.InternationalLicense = Ilicense;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
