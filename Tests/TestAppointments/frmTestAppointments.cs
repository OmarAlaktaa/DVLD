using System.Windows.Forms;
using DVLD_BusinessLayer;
using System;

namespace DVLD
{
    public partial class frmTestAppointments : Form
    {
        public frmTestAppointments(TestType.TestTypes testType, int DLAppID)
        {
            InitializeComponent();
            this.DLApplication = LDLApplication.Find(DLAppID);
            this.testType = testType;
        }

        private LDLApplication _DLApplication = null;

        private LDLApplication DLApplication
        {
            get
            {
                return this._DLApplication;
            }
            set
            {
                _DLApplication = value;
                DLAppInfo.LDLApplication = value;
            }
        }

        TestType.TestTypes testType { get; set; }

        public delegate void PersonEditedEventHandler(int PersonID);

        public event PersonEditedEventHandler PersonEdited;

        public delegate void FormClosedEventHandler();

        public event FormClosedEventHandler TestAppointmentsFormClosed;
        public void LoadAppointments(TestType.TestTypes testType, int DLAppID)
        {
            dgvAppointments.DataSource = TestAppointments.GetTestAppointments
                (testType, DLAppID);
        }

        private void ReloadAppointments()
        {
            if (_DLApplication != null)
            {
                LoadAppointments(this.testType, this.DLApplication.ID);
            }
        }

        private void frmTestAppointments_Load(object sender, System.EventArgs e)
        {
            LoadAppointments(this.testType, this.DLApplication.ID);
            Design();
        }

        private void Design()
        {
            lblRecords.Text = dgvAppointments.Rows.Count.ToString();
            lblHeader.Text = Header(this.testType);
            pbHeaderImage.Image = Image(this.testType);
        }

        private System.Drawing.Image Image(TestType.TestTypes testType)
        {
            switch (testType)
            {
                case TestType.TestTypes.VisionTest:
                    {
                        return Properties.Resources.Vision_512;
                    }
                case TestType.TestTypes.WrittenTest:
                    {
                        return Properties.Resources.Written_Test_512;
                    }
                case TestType.TestTypes.StreetTest:
                    {
                        return Properties.Resources.driving_test_512;
                    }
                default:
                    {
                        return null;
                    }
            }
        }
        
        private string Header(TestType.TestTypes testType)
        {
            switch(testType)
            {
                case TestType.TestTypes.VisionTest:
                    {
                        return "Vision Test Appointments";
                    }
                case TestType.TestTypes.WrittenTest:
                    {
                        return "Written Test Appointments";
                    }
                case TestType.TestTypes.StreetTest:
                    {
                        return "Street Test Appointments";
                    }
                default:
                    {
                        return "Header";
                    }
            }
        }


        private void DLAppInfo_OnPersonEdited(int obj)
        {
            PersonEdited?.Invoke(obj);
        }

        private void AddTestAppointmentProccess()
        {
            if (TestAppointments.CheckForActivateTestAppointment(this.DLAppInfo.LDLApplication.ID))
            {
                MessageBox.Show("Person Already has an active appointment for this test, you cannot add new appointment",
                      "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Test.CheckIfTestPassed(this.DLAppInfo.LDLApplication.ID, this.testType))
            {
                MessageBox.Show("You cannot take this test because test already taken!",
                    "Cannot appoint for this test!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmScheduleTest Form = Test.RetakeTest(DLAppInfo.LDLApplication.ID, testType) ?
                new frmScheduleTest(testType, DLAppInfo.LDLApplication.ID, true)
                : new frmScheduleTest(testType, DLAppInfo.LDLApplication.ID);
            Form.AppointmentTestSaved += ReloadAppointments;
            Form.ShowDialog();
        }

        private void btnAddTestAppointment_Click(object sender, System.EventArgs e)
        {
            AddTestAppointmentProccess();
        }

        private void dgvAppointments_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dgvAppointments.HitTest(e.X, e.Y);

                // Ensure the click is on a valid cell (not on headers or empty space)
                if (hitTestInfo.ColumnIndex >= 0 && hitTestInfo.RowIndex >= 0)
                {
                    dgvAppointments.ClearSelection();
                    dgvAppointments.CurrentCell = dgvAppointments[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];
                    dgvAppointments[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex].Selected = true;
                }
            }
        }

        private void EditAppointmentProccess()
        {
            int testAppointmentID = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["TestAppointmentID"].Value);
            frmScheduleTest Form = new frmScheduleTest(testAppointmentID);
            Form.AppointmentTestSaved += ReloadAppointments;
            Form.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            EditAppointmentProccess();
        }

        private void TakeTestProccess()
        {
            int testAppointmentID = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["TestAppointmentID"].Value);
            TestAppointment testAppointment = TestAppointment.Find(testAppointmentID);
            frmTakeTest Form;
            if (testAppointment.IsTaken(out int TestID))
            {
                Test test = Test.Find(TestID);
                Form = new frmTakeTest(test);
            }
            else
            {
                Form = new frmTakeTest(testAppointment);
            }
            Form.TestTaken += ReloadAppointments;
            Form.ShowDialog();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeTestProccess();
        }

        private void frmTestAppointments_FormClosed(object sender, FormClosedEventArgs e)
        {
            TestAppointmentsFormClosed?.Invoke();
        }
    }
}
