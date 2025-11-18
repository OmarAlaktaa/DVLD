using System;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class frmMain : Form
    {
        public frmMain(frmLoginScreen LoginScreen)
        {
            InitializeComponent();
            this.LoginScreen = LoginScreen;
        }

        frmLoginScreen LoginScreen;
        Form CurrentForm = null;

        private void OpenForm(Form form, bool Fill = false, bool MdiParent = true)
        {
            if (CurrentForm == null || CurrentForm.IsDisposed)
            {
                CurrentForm = form;
                if (MdiParent) CurrentForm.MdiParent = this;
                CurrentForm.Show();
                if (Fill) CurrentForm.Dock = DockStyle.Fill;
            }
            else
            {
                MessageBox.Show("You should Close current form before opening new one!", "Can't Open Form!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagePeople frm = new frmManagePeople();
            OpenForm(frm, true);
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageUsers frm = new frmManageUsers();
            frm.MdiParent = this;
            OpenForm(frm, true);
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Globals.CurrentUser != null)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void currentUserIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCurrentUserInfoScreen();
        }

        private void ShowCurrentUserInfoScreen()
        {
            frmUserDetails frmUserInfo = new frmUserDetails();
            frmUserInfo.MdiParent = this;
            frmUserInfo.UserInfoCard.User = Globals.CurrentUser;
            this.OpenForm(frmUserInfo);
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangeUserPassword Form = new frmChangeUserPassword(Globals.CurrentUser.UserID);
            Form.MdiParent = this;
            this.OpenForm(Form);
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Users.Logout())
            {
                this.Close();
                LoginScreen.Show();
            }
            else
            {
                MessageBox.Show("Error while Loging out!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes Form = new frmManageApplicationTypes();
            Form.MdiParent = this;
            this.OpenForm(Form);
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes Form = new frmManageTestTypes();
            Form.MdiParent = this;
            this.OpenForm(Form);
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication Form = new frmNewLocalDrivingLicenseApplication();
            Form.MdiParent = this;
            this.OpenForm(Form);
        }

        private void localDrivingLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications Form = new frmLocalDrivingLicenseApplications();
            Form.MdiParent = this;
            this.OpenForm(Form);
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Escape:
                    {
                        CloseCurrentForm();
                        break;
                    }
            }
        }

        private bool ConfirmToCloseCurrentForm()
        {
            return MessageBox.Show("Are you sure you want to close current from?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;
        }

        public bool CloseCurrentForm()
        {
            if (CurrentForm == null)
                return false;
            else
            {
                if (ConfirmToCloseCurrentForm())
                {
                    CurrentForm.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDriversList Form = new frmDriversList();
            Form.MdiParent = this;
            this.OpenForm(Form);
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication Form = new frmNewInternationalLicenseApplication();
            OpenForm(Form);
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicense Form = new frmRenewLocalDrivingLicense();
            OpenForm(Form);
        }

        private void replacementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacmentForLostDamagedLicense Form = new frmReplacmentForLostDamagedLicense();
            OpenForm(Form);
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense Form = new frmDetainLicense();
            OpenForm(Form);
        }

        private void releaseDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense Form = new frmReleaseDetainedLicense();
            OpenForm(Form);
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense Form = new frmReleaseDetainedLicense();
            OpenForm(Form);
        }

        private void internationalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicenses Form = new frmListInternationalLicenses();
            OpenForm(Form);
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications Form = new frmLocalDrivingLicenseApplications();
            OpenForm(Form);
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDetainedLicenses Form = new frmManageDetainedLicenses();
            OpenForm(Form);
        }
    }
}
