using System;
using DVLD_BusinessLayer;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmTakeTest : Form
    {
        public frmTakeTest(TestAppointment testAppointment)
        {
            InitializeComponent();
            this.TestAppointment = testAppointment;
        }

        public frmTakeTest(Test test)
        {
            InitializeComponent();
            this.Test = test;
            DisableEditing();
        }

        private TestAppointment _testAppointment;

        public TestAppointment TestAppointment
        {
            get
            {
                return _testAppointment; 
            }
            set 
            {
                LoadAppointment(ref value);
                _testAppointment = value;
            }
        }

        private Test _test;

        public Test Test
        {
            get { return _test; }
            set
            {
                _test = value;
                DisplayTest(ref value);
            }
        }

        public delegate void TestTakenEventHandler();

        public event TestTakenEventHandler TestTaken;

        private void DisplayTest(ref Test test)
        {
            if (test == null) return;
            TestAppointmentInfo.testAppointment = TestAppointment.Find(test.TestAppointmentID);
            lblTestID.Text = test.TestID.ToString();
            // Display Result
            if (test.TestResult) rbPass.Checked = true;
            else rbFail.Checked = true;
        }

        private void LoadAppointment(ref TestAppointment appointment)
        {
            TestAppointmentInfo.testAppointment = appointment;        
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ConfirmSave()
        {
            return MessageBox.Show("Are you sure you want to save?\nNote : you cannot change test result Pass/Fail After saving!",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void DisableEditing()
        {
            TestAppointmentInfo.lblUnderHeader.Text = "This test already taken!, You cannot edit it.";
            TestAppointmentInfo.lblUnderHeader.Visible = true;
            rbPass.Enabled = false; rbFail.Enabled = false;
            tbNotes.ReadOnly = true; lblTestID.Text = Test.TestID.ToString();
            btnSave.Enabled = false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ConfirmSave()) SaveTest();this.Close();
        }

        private void SaveTest()
        {
            Test test = this.GetTest();
            int TestID = -1;
            if (test.Save(ref TestID))
            {
                TestTaken?.Invoke();
                MessageBox.Show($"Test with ID={TestID} Saved Successfully!",
                    "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error!");
            lblTestID.Text = TestID.ToString();
        }

        Test GetTest()
        {
            int TestAppointmentID = TestAppointmentInfo.testAppointment.ID;
            bool TestResult = rbPass.Checked;
            string Notes = tbNotes.Text;
            int CreatedByUserID = Globals.CurrentUser.UserID;
            return new Test(TestAppointmentID, TestResult, Notes, CreatedByUserID);
        }
    }
}
