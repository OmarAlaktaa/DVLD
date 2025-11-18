using DVLD_BusinessLayer;
using System.Windows.Forms;
using System;

namespace DVLD
{
    public partial class ctrlDriverLicenses : UserControl
    {
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        public void LoadLicenses(int PersonID)
        {
            if (PersonID <= 0) return;
            if (Driver.IsDriver(PersonID, out int DriverID))
            {
                dgvLocalLicenses.DataSource = License.GetLocalLicenses(DriverID);
                dgvInternationalLicenses.DataSource = InternationalLicense.GetInterantionalLicensesByDriverID(DriverID);
            }
            lblLocalRecords.Text = dgvLocalLicenses.Rows.Count.ToString();
            lblInternationalRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void dgvLocalLicenses_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dgvLocalLicenses.HitTest(e.X, e.Y);

                // Ensure the click is on a valid cell (not on headers or empty space)
                if (hitTestInfo.ColumnIndex >= 0 && hitTestInfo.RowIndex >= 0)
                {
                    dgvLocalLicenses.ClearSelection();
                    dgvLocalLicenses.CurrentCell = dgvLocalLicenses[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];
                    dgvLocalLicenses[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex].Selected = true;
                }
            }
        }

        private void showLicenseHistoryToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int LicenseID = Convert.ToInt32(dgvLocalLicenses.SelectedRows[0].Cells["LicenseID"].Value);
            License license = License.Find(LicenseID);
            if (license == null) return;
            frmLicenseInfo Form = new frmLicenseInfo(ref license);
            Form.ShowDialog();
        }
    }
}
