using System;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        public void LoadApplications()
        {
            dgvApplications.DataSource = LDLApplication.GetLDLApplications();
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            LoadApplications();
            DesigndgvApplications();
            LoadFilters();
            cbFilter.SelectedItem = "None";
            lblNumberOfApplications.Text = dgvApplications.Rows.Count.ToString();
        }

        private void LoadFilters()
        {
            cbFilter.Items.Clear();
            cbFilter.DataSource = Enum.GetValues(typeof(SearchFilter.LDLApplications));
        }

        private void DesigndgvApplications()
        {
            if (dgvApplications.DataSource == null)
                return;
            //dgvApplications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            
            dgvApplications.Columns["L.D.L.AppID"].Width = 80;
            dgvApplications.Columns["Driving Class"].Width = 180;
            dgvApplications.Columns["NationalNo"].Width = 80;
            dgvApplications.Columns["FullName"].Width = 200;
            dgvApplications.Columns["ApplicationDate"].Width = 110;
            dgvApplications.Columns["Status"].Width = 200;
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHidetbFilter();
        }

        private void ShowHidetbFilter()
        {
            if (cbFilter.SelectedItem.ToString() == "None")
            {
                tbFilter.Hide();
            }
            else
            {
                tbFilter.Show();
            }
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            if (tbFilter.Text.Length > 0)
            {
                dgvApplications.DataSource = LDLApplication.GetFilteredLDLApplications
                    ((SearchFilter.LDLApplications)cbFilter.SelectedItem, tbFilter.Text);
            }
            else
            {
                dgvApplications.DataSource = LDLApplication.GetLDLApplications();
            }
            DesigndgvApplications();
        }

        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((SearchFilter.LDLApplications)cbFilter.SelectedItem == SearchFilter.LDLApplications.LDLAppID)
            {
                Validations.ValidateInput.ValidateNumberInput(e);
            }
        }

        private void dgvApplications_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dgvApplications.HitTest(e.X, e.Y);

                // Ensure the click is on a valid cell (not on headers or empty space)
                if (hitTestInfo.ColumnIndex >= 0 && hitTestInfo.RowIndex >= 0)
                {
                    dgvApplications.ClearSelection();
                    dgvApplications.CurrentCell = dgvApplications[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];
                    dgvApplications[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex].Selected = true;
                }
            }
        }

        private void LoadApplications(int ID)
        {
            LoadApplications();
        }

        private void scheduleTestVisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DLAppID = Convert.ToInt32(dgvApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);
            ShowTestAppointmentsForm(TestType.TestTypes.VisionTest, DLAppID);
        }

        private void ShowTestAppointmentsForm(TestType.TestTypes testType, int DLAppID)
        {
            frmTestAppointments Form = new frmTestAppointments
                (testType, DLAppID);
            Form.PersonEdited += LoadApplications;
            Form.TestAppointmentsFormClosed += LoadApplications;
            Form.Show();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DLAppID = Convert.ToInt32(dgvApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);
            ShowTestAppointmentsForm(TestType.TestTypes.WrittenTest, DLAppID);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DLAppID = Convert.ToInt32(dgvApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);
            ShowTestAppointmentsForm(TestType.TestTypes.StreetTest, DLAppID);
        }

        private void scheduleTestToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            DisableTests();
        }

        private void EnableAll()
        {
            scheduleTestVisionToolStripMenuItem.Enabled = true; // 1
            scheduleWrittenTestToolStripMenuItem.Enabled = true; // 2
            scheduleStreetTestToolStripMenuItem.Enabled = true; // 3
        }

        private void DisableTests()
        {
            EnableAll();
            int LDLAppID = Convert.ToInt32(dgvApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);
            int PassedTests = LDLApplication.GetLDLApplicationPassedTests(LDLAppID);
            switch (PassedTests)
            {
                case 0:
                    {
                        scheduleWrittenTestToolStripMenuItem.Enabled = false; // 2
                        scheduleStreetTestToolStripMenuItem.Enabled = false; // 3
                        break;
                    }
                case 1:
                    {
                        scheduleTestVisionToolStripMenuItem.Enabled = false; // 1
                        scheduleStreetTestToolStripMenuItem.Enabled = false; // 3
                        break;
                    }
                case 2:
                    {
                        scheduleTestVisionToolStripMenuItem.Enabled = false; // 1
                        scheduleWrittenTestToolStripMenuItem.Enabled = false; // 2
                        break;
                    }
                case 3:
                    {
                        scheduleTestVisionToolStripMenuItem.Enabled = false; // 1
                        scheduleWrittenTestToolStripMenuItem.Enabled = false; // 2
                        scheduleStreetTestToolStripMenuItem.Enabled = false; // 3
                        break;
                    }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadApplications();
        }

        private void cmsApplications_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int LDLAppID = Convert.ToInt32(dgvApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);
            LDLApplication App = new LDLApplication(LDLAppID);
            DisableEnableButtons(App);
        }

        private void DisableEnableNotAllTestsPassed()
        {
            showApplicationDetailsToolStripMenuItem.Enabled = true;
            //editApplicationToolStripMenuItem.Enabled = true;
            deleteApplicationToolStripMenuItem.Enabled = true;
            cancelApplicationToolStripMenuItem.Enabled = true;
            scheduleTestToolStripMenuItem.Enabled = true;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            showLicenseToolStripMenuItem.Enabled = false;
            showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
        }

        private void DisableEnableAllTestsPassedLicenseIssued()
        {
            showApplicationDetailsToolStripMenuItem.Enabled = true;
            //editApplicationToolStripMenuItem.Enabled = false;
            deleteApplicationToolStripMenuItem.Enabled = false;
            cancelApplicationToolStripMenuItem.Enabled = false;
            scheduleTestToolStripMenuItem.Enabled = false;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            showLicenseToolStripMenuItem.Enabled = true;
            showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
        }

        private void DisableEnableAllTestsPassedLicenseIsntIssued()
        {
            showApplicationDetailsToolStripMenuItem.Enabled = true;
            //editApplicationToolStripMenuItem.Enabled = true;
            deleteApplicationToolStripMenuItem.Enabled = true;
            cancelApplicationToolStripMenuItem.Enabled = true;
            scheduleTestToolStripMenuItem.Enabled = false;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
            showLicenseToolStripMenuItem.Enabled = false;
            showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
        }

        private void DisableEnableApplicationCanceled()
        {
            showApplicationDetailsToolStripMenuItem.Enabled = true;
            //editApplicationToolStripMenuItem.Enabled = false;
            deleteApplicationToolStripMenuItem.Enabled = true;
            cancelApplicationToolStripMenuItem.Enabled = false;
            scheduleTestToolStripMenuItem.Enabled = false;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            showLicenseToolStripMenuItem.Enabled = false;
            showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
        }

        private void DisableEnableButtons(LDLApplication LDLApplication)
        {
            if (LDLApplication.Canceled())
            {
                DisableEnableApplicationCanceled();
                return;
            }
            if (LDLApplication.PassedTests < 3) // Not All Tests Passed
            {
                DisableEnableNotAllTestsPassed();
            }
            else if (LDLApplication.LicenseIssued()) // All Tests Passed & License Issued
            {
                DisableEnableAllTestsPassedLicenseIssued();
            }
            else // All Tests Passed & Licnese isn't Issued
            {
                DisableEnableAllTestsPassedLicenseIsntIssued();
            }
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DLAppID = Convert.ToInt32(dgvApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);
            LDLApplication LDLApp = LDLApplication.Find(DLAppID);
            frmIssueDrivingLicenseFirstTime Form = new frmIssueDrivingLicenseFirstTime(ref LDLApp);
            Form.LicenseIssued += LoadApplications;
            Form.Show();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DLAppID = Convert.ToInt32(dgvApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);
            License license = License.GetLicense(DLAppID);
            frmLicenseInfo Form = new frmLicenseInfo(ref license);
            Form.Show();
        }

        private void AddApplication_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication Form = new frmNewLocalDrivingLicenseApplication();
            Form.LocalDrivingLicenseApplicationAdded += LoadApplications;
            Form.ShowDialog();
        }

        private bool SureToDelete()
        {
            return MessageBox.Show("Are you sure you want to delete selected application?",
                "Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes;
        }

        private void DeleteSelectedApplicationProccess()
        {
            int DLAppID = Convert.ToInt32(dgvApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);
            LDLApplication LDLApp = LDLApplication.Find(DLAppID);
            if (LDLApp.HasTestAppointments())
            {
                MessageBox.Show("You cannot delete application because it has scheduled test appointments",
                    "DVLD", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (SureToDelete())
            {
                if (LDLApp == null) return;
                if (LDLApp.Delete())
                    MessageBox.Show($"LocalDrivingLicenseApplication with ID={DLAppID} Deleted Successfully!",
                        "Success!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Error While Deleting!",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadApplications();
            }
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedApplicationProccess();
        }

        private bool SureToCancel()
        {
            return MessageBox.Show("Are you sure you want to cancel selected application?",
                "Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes;
        }

        private void CancelSelectedApplicationProccess()
        {
            int DLAppID = Convert.ToInt32(dgvApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);
            LDLApplication LDLApp = LDLApplication.Find(DLAppID);
            if (!LDLApp.ValidToCancel())
            {
                MessageBox.Show(@"You cannot cancel this Application for one of those possible reasons:
Application is completed of already canceled
License for this Application is Issued", 
                                "Cannot Cancel", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (SureToCancel())
            {
                
                if (LDLApp == null) return;
                if (LDLApp.Cancel())
                    MessageBox.Show($"Local Driving License Application with ID={DLAppID} Canceled Successfully!", "Canceled!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else MessageBox.Show("Error while canceling", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadApplications();
            }
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelSelectedApplicationProccess();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DLAppID = Convert.ToInt32(dgvApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);
            LDLApplication LDLApp = LDLApplication.Find(DLAppID);
            frmApplicationDetails Form = new frmApplicationDetails(LDLApp.Application);
            Form.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DLAppID = Convert.ToInt32(dgvApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);
            LDLApplication LDLApp = LDLApplication.Find(DLAppID);
            if (LDLApp == null) return;
            frmLicenseHistory Form = new frmLicenseHistory(LDLApp.Application.ApplicantPersonID);
            Form.ShowDialog();
        }
    }
}
