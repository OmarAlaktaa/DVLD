using System;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        public frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        public delegate void LocalDrivingLicenseApplicationAddedEventHandler(int LDLAppID);

        public event LocalDrivingLicenseApplicationAddedEventHandler LocalDrivingLicenseApplicationAdded;

        private void btnNext_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void Next()
        {
            PersonInfoCardWithFilter.SearchAndLoadPerson();
            if (PersonInfoCardWithFilter.person == null)
            {
                return;
            }
            else
            {
                tabControl1.SelectedIndex = 1;
            }
        }

        private void SetApplicationInfo()
        {
            cbLicenseClass.DataSource = LicenseClass.GetLicenseClasses();
            cbLicenseClass.SelectedItem = "Class 3 - Ordinary driving license";
            lblApplicationDate.Text= DateTime.Now.ToShortDateString();
            lblUsername.Text = Globals.CurrentUser.Username;
            lblFees.Text = ApplicationType.GetApplicationTypeFees(ApplicationType.ApplicationTypes.NewLocalDrivingLicenseService).ToString();
        }

        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            SetApplicationInfo();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1 && PersonInfoCardWithFilter.person != null)
            {
                btnSave.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SaveApplication()
        {
            int LDLApplicationID = -1;
            DVLD_BusinessLayer.Application application = ReadApplicationInfo();
            if (LDLApplication.SaveNewLDLApplication(ref LDLApplicationID, application, cbLicenseClass.SelectedValue.ToString()))
            {
                lblDLAID.Text = LDLApplicationID.ToString();
                LocalDrivingLicenseApplicationAdded?.Invoke(LDLApplicationID);
                MessageBox.Show($"Application Saved Succefully!\nApplication ID={LDLApplicationID.ToString()}", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error while saving data", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DVLD_BusinessLayer.Application ReadApplicationInfo()
        {
            int ApplicantPersonID = PersonInfoCardWithFilter.person.PersonID;
            DateTime ApplicationDate = DateTime.Now;
            ApplicationType appType = new ApplicationType(ApplicationType.ApplicationTypes.NewLocalDrivingLicenseService);
            int ApplicationStatus = 1;// status (New) [static]
            DateTime LastStatusDate = DateTime.Now;
            double PaidFees = appType.ApplicationTypeFees;
            int CreatedByUserID = Globals.CurrentUser.UserID;
            return new DVLD_BusinessLayer.Application(ApplicantPersonID, ApplicationDate,
                appType, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
        }

        private bool ConfirmSave()
        {
            return MessageBox.Show("Are you sure you want to save new Application?",
                "Confirmation", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) 
                == DialogResult.Yes;
        }

        private bool ValidToSave()
        {
            LicenseClass licenseClass = LicenseClass.GetLicenseClass(cbLicenseClass.SelectedValue.ToString());
            int licenseClassID = licenseClass.LicenseClassID;
            return !PersonInfoCardWithFilter.person.HasLicense(licenseClassID);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidToSave())
            {
                if (ConfirmSave())
                    SaveApplication();
            }
            else
                MessageBox.Show("Person already has a license with the same applied driving class", 
                    "Not Allowd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
