using System;
using DVLD_BusinessLayer;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            LoadTestTypes();
            lblRecords.Text = dgvTestTypes.RowCount.ToString();
        }

        public void LoadTestTypes()
        {
            dgvTestTypes.DataSource = TestType.GetTestTypes();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestTypeID = Convert.ToInt32(dgvTestTypes.SelectedRows[0].Cells["ID"].Value);
            frmUpdateTestType frm = new frmUpdateTestType(TestTypeID);
            frm.TestUpdated += LoadTestTypes;
            frm.ShowDialog();
        }

        private void dgvTestTypes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dgvTestTypes.HitTest(e.X, e.Y);

                // Ensure the click is on a valid cell (not on headers or empty space)
                if (hitTestInfo.ColumnIndex >= 0 && hitTestInfo.RowIndex >= 0)
                {
                    dgvTestTypes.ClearSelection();
                    dgvTestTypes.CurrentCell = dgvTestTypes[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];
                    dgvTestTypes[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex].Selected = true;
                }
            }
        }
    }
}
