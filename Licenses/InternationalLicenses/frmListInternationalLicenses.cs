using System;
using System.Data;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class frmListInternationalLicenses : Form
    {
        public frmListInternationalLicenses()
        {
            InitializeComponent();
        }

        private DataTable _dtListInternationalLicenseApplications;

        public DataTable dtListInternationalLicenseApplications
        {
            get {  return _dtListInternationalLicenseApplications; }
            set
            {
                _dtListInternationalLicenseApplications = value;
                _LoadListInternationalLicenseApplications(value);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _LoadListInternationalLicenseApplications(DataTable dt)
        {
            dgvApplications.DataSource = dt;
        }

        private void _LoadListInternationalLicenseApplications()
        {
            this._dtListInternationalLicenseApplications = InternationalLicense.GetInternationalLicenseApplications();
        }

        private void frmListInternationalLicenses_Load(object sender, EventArgs e)
        {
            _LoadListInternationalLicenseApplications();
        }
    }
}
