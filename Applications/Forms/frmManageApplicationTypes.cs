using System;
using System.Data;
using DVLD_BusinessLayer;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmManageApplicationTypes : Form
    {
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadApplicationTypes()
        {
            dgvApplicationTypes.DataSource = Applications.GetApplicationTypes();
            lblRecords.Text = dgvApplicationTypes.RowCount.ToString();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            LoadApplicationTypes();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvApplicationTypes.SelectedRows[0].Cells["ID"].Value.ToString(), out int ApplicationTypeID))
            {
                frmUpdateApplicationType form = new frmUpdateApplicationType(ApplicationTypeID);
                form.ApplicationUpdated += LoadApplicationTypes;
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Error!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvApplicationTypes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dgvApplicationTypes.HitTest(e.X, e.Y);

                // Ensure the click is on a valid cell (not on headers or empty space)
                if (hitTestInfo.ColumnIndex >= 0 && hitTestInfo.RowIndex >= 0)
                {
                    dgvApplicationTypes.ClearSelection();
                    dgvApplicationTypes.CurrentCell = dgvApplicationTypes[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];
                    dgvApplicationTypes[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex].Selected = true;
                }
            }
        }
    }
}
