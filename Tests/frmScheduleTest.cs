using System;
using DVLD_BusinessLayer;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmScheduleTest : Form
    {
        public frmScheduleTest(TestType.TestTypes testType, int DLAppID, bool Retake = false) // opens form in add new mode
        {
            InitializeComponent();
            InitializeTestType(DLAppID, testType);
            this._testAppointment = ReadTestAppointmentInfo();
            if (Retake) InitializeRetake();
        }

        public frmScheduleTest(int testAppointmentID) // opens form in edit mode
        {
            InitializeComponent();
            this.testAppointment = TestAppointment.Find(testAppointmentID); // set property does load appointment
            this.DLAppID = testAppointment.LocalDrivingLicenseApplicationID;
            this.testType = (TestType.TestTypes)testAppointment.TestTypeID;
            this.testAppointment.TestAppointmentChanged += testAppointment_ModeChanged;
            this.testAppointment.Mode = TestAppointment.AppointmentModes.Update;
        }

        public TestType.TestTypes testType;
        public int DLAppID;

        private TestAppointment _testAppointment;

        public TestAppointment testAppointment
        {
            set
            {
                this._testAppointment = value;
                LoadTestAppointment(value);
            }
            get
            {
                return _testAppointment;
            }
        }

        public delegate void AppointmentTestSavedEventHandler();

        public event AppointmentTestSavedEventHandler AppointmentTestSaved;

        private void InitializeTestType(int DLAppID, TestType.TestTypes testType)
        {
            DisplayTestType(testType, DLAppID);
            this.testType = testType;
            this.DLAppID = DLAppID;
            DisplayLDLApp(testType, DLAppID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisplayTestType(TestType.TestTypes testType, int DLAppID)
        {
            switch(testType)
            {
                case TestType.TestTypes.VisionTest:
                    {
                        DisplayVisionTestType(DLAppID);
                        break;
                    }
                case TestType.TestTypes.WrittenTest:
                    {
                        DisplayWrittenTestType(DLAppID);
                        break;
                    }
                case TestType.TestTypes.StreetTest:
                    {
                        DisplayStreetTest(DLAppID);
                        break;
                    }
            }
        }

        private void DisplayVisionTestType(int DLAppID)
        {
            gpTest.Text = "Vision Test";
            pbImage.Image = Properties.Resources.Vision_512;
        }

        private void LockEditing()
        {
            dtpAppointmentDate.Enabled = false;
            lblUnderHeader.Visible = true;
            lblUnderHeader.Text = "Person Already sat for the test, appointment locked!";
            btnSave.Enabled = false;
        }

        private void UnlockEditing()
        {
            dtpAppointmentDate.Enabled = true;
            lblUnderHeader.Text = "Person Already sat for the test, appointment locked!";
            lblHeader.Visible = false;
            btnSave.Enabled = true;
        }

        private void LoadTestAppointment(TestAppointment testAppointment)
        {
            DisplayTestType((TestType.TestTypes)testAppointment.TestTypeID, 
                testAppointment.LocalDrivingLicenseApplicationID);
            dtpAppointmentDate.Value = testAppointment.AppointmentDate;
        }

        private void DisplayLDLApp(TestType.TestTypes testType, int DLAppID)
        {
            LDLApplication DLApplication = LDLApplication.Find(DLAppID);
            if (DLApplication == null) // Application Not Found
                return;
            else
            {
                lblDLAppID.Text = DLApplication.ID.ToString();
                lblDClass.Text = DLApplication.DrivingClass.ToString();
                lblName.Text = DLApplication.FullName.ToString();
                lblTrial.Text = TestType.GetTestTrials(testType, DLAppID).ToString();
                lblFees.Text = TestType.GetTestFees(testType).ToString();
            }
        }

        private void DisplayWrittenTestType(int DLAppID)
        {
            gpTest.Text = "Written Test";
            pbImage.Image = Properties.Resources.Written_Test_512;
        }

        private void DisplayStreetTest(int DLAppID)
        {
            gpTest.Text = "Street Test";
            pbImage.Image = Properties.Resources.driving_test_512;
        }

        private TestAppointment ReadTestAppointmentInfo()
        {
            int testTypeID = (int)this.testType;
            int LDLAppID = this.DLAppID;
            DateTime AppointmentDate = dtpAppointmentDate.Value;
            double PaidFees = Convert.ToDouble(lblFees.Text);
            int CreaterUserID = Globals.CurrentUser.UserID;
            return new TestAppointment(testTypeID, LDLAppID, AppointmentDate, PaidFees, CreaterUserID, false);
        }

        private void SaveTestAppointment()
        {
            if (TestAppointments.CheckForActivateTestAppointment
                (testAppointment.LocalDrivingLicenseApplicationID)
                && testAppointment.Mode != TestAppointment.AppointmentModes.Update) // there is an activate appointment already
            {
                MessageBox.Show("Sorry!, you cannot add new appointment because there is an activate one already!", "Cannot Add New Appointment!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int AppointmentID = -1;
            if (testAppointment.Save(ref AppointmentID))
            {
                AppointmentTestSaved?.Invoke();
                MessageBox.Show($"Appointment with ID={AppointmentID} Saved Successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error while saving data!", "faild to save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveTestAppointment();
            this.Close();
        }

        private void dtpAppointmentDate_ValueChanged(object sender, EventArgs e)
        {
            testAppointment.AppointmentDate = dtpAppointmentDate.Value;
        }

        private void testAppointment_ModeChanged(TestAppointment.AppointmentModes mode)
        {
            if (mode == TestAppointment.AppointmentModes.Update)
            {
                if (testAppointment.IsTaken(out int TestID))
                {
                    LockEditing();
                }
                else UnlockEditing();
            }
            else UnlockEditing();
        }
   
        private void InitializeRetake()
        {
            testAppointment.Mode = TestAppointment.AppointmentModes.RetakeTest;
            gbRetakeTest.Enabled = true;
            double RetakeFees = -1, TotalFees = -1;
            RetakeFees = ApplicationType.GetApplicationTypeFees(ApplicationType.ApplicationTypes.RetakeTest);
            TotalFees = TestType.GetTestFees(testType) + RetakeFees;
            testAppointment.PaidFees = TotalFees;
            lblRetakeAppFees.Text = RetakeFees.ToString();
            lblTotalFees.Text = TotalFees.ToString();
        }

    }
}
