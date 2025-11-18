using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class ctrlTestAppointmentInfo : UserControl
    {
        public ctrlTestAppointmentInfo()
        {
            InitializeComponent();
        }

        private TestAppointment _testAppointment;

        public TestAppointment testAppointment
        {
            get
            {
                return _testAppointment;
            }
            set
            {
                if (value == null) return;
                _testAppointment = value;
                LoadTestAppointment(value);
            }
        }

        private void LoadTestAppointment(TestAppointment testAppointment)
        {
            if (testAppointment == null) return;
            DisplayTestType((TestType.TestTypes)testAppointment.TestTypeID,
                testAppointment.LocalDrivingLicenseApplicationID);

            lblAppointmentDate.Text = testAppointment.AppointmentDate.ToShortDateString();
            lblFees.Text = testAppointment.PaidFees.ToString();

            DisplayLDLApp(TestType.TestTypes.VisionTest, 
                testAppointment.LocalDrivingLicenseApplicationID);
        }

        private void DisplayTestType(TestType.TestTypes testType, int DLAppID)
        {
            switch (testType)
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
            }
        }

        private void DisplayVisionTestType(int DLAppID)
        {
            gbTestInfo.Text = "Vision Test";
            pbImage.Image = Properties.Resources.Vision_512;
        }

        private void DisplayWrittenTestType(int DLAppID)
        {
            gbTestInfo.Text = "Written Test";
            pbImage.Image = Properties.Resources.Written_Test_512;
        }

        private void DisplayStreetTest(int DLAppID)
        {
            gbTestInfo.Text = "Street Test";
            pbImage.Image = Properties.Resources.driving_test_512;
        }

    }
}
