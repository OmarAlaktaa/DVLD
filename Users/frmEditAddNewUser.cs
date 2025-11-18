using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class frmEditAddNewUser : Form
    {
        public frmEditAddNewUser()
        {
            InitializeComponent();
        }

        public frmEditAddNewUser(int UserID)
        {
            InitializeComponent();
            user = User.Find(UserID);
            LoadUserInfo(user);
            HidePassword();
        }

        User user = null;

        Validations.UserValidations UserValidations = new Validations.UserValidations();

        public delegate void UserSavedEventHandler(int SavedUserID);

        public event UserSavedEventHandler UserSaved;

        private bool LoadPersonCard()
        {
            if (ctrlPersonInfoCardWithFilter.person == null)
            {
                ctrlPersonInfoCardWithFilter.SearchAndLoadPerson();
                return true;
            }
            return false;
        }

        private void LoadUserInfo(User User)
        {
            if (user == null)
            {
                MessageBox.Show("Error While loading User!", "Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ctrlPersonInfoCardWithFilter.person = Person.Find(User.PersonID);
                lblUserID.Text = User.UserID.ToString();
                tbUsername.Text = User.Username;
                tbPassword.Text = User.Password;
                tbConfirmPassword.Text = User.Password;
                chkIsActive.Checked = User.IsActive;
                UserValidations.ValidUsername = true;UserValidations.ValidPassword = true;UserValidations.ValidPasswordConfirmation = true;
            }
        }

        private void Next()
        {
            LoadPersonCard();
            if (ctrlPersonInfoCardWithFilter.person == null)
            {
                MessageBox.Show("No Person selected to add user, try to select a person!", "Not Valid Person!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (User.IsUser(ctrlPersonInfoCardWithFilter.person.PersonID)
                /*&& ctrlPersonInfoCardWithFilter1.person.Mode == Person.enMode.eAddNew*/)
            {
                UserValidations.IsAlreadyUser = true;
                MessageBox.Show("Selected person already have a user, choose another one!", "Select another person!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else UserValidations.IsAlreadyUser = false;
            if (ctrlPersonInfoCardWithFilter.person != null)
            {
                LoginInfo();
            }
            else
            {
                MessageBox.Show("No person selected to add!", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            Next();
        }

        private void LoginInfo()
        {
            tcManageUsers.SelectedIndex = 1;
        }

        private void DisableEnableSaveButton()
        {
            switch (tcManageUsers.SelectedIndex)
            {
                case 0: // tap page is person info
                    {
                        btnSave.Enabled = false;
                        break;
                    }
                case 1: // tap page is login info
                    {
                        btnSave.Enabled = true;
                        break;
                    }
            }
        }

        private void tcManageUsers_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DisableEnableSaveButton();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ValidatetbPassword()
        {
            if (tbPassword.Text.Length == 0)
            {
                UserValidations.ValidPassword = false;
                epPassword.SetError(tbPassword, "Password can't be blank!");
            }
            else
            {
                UserValidations.ValidPassword = true;
                epPassword.Dispose();
            }
        }

        private void tbPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidatetbPassword();
        }

        private void ValidatetbConfirmationPassword()
        {
            if (tbConfirmPassword.Text != tbPassword.Text)
            {
                UserValidations.ValidPasswordConfirmation = false;
                epConfirmPassword.SetError(tbConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                UserValidations.ValidPasswordConfirmation = true;
                epConfirmPassword.Dispose();
            }
        }

        private void tbConfirmPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidatetbConfirmationPassword();
        }

        private User GetUserInfo()
        {
            if (ctrlPersonInfoCardWithFilter.person == null)
            {
                MessageBox.Show("No person selected to Add User!", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            int personID = ctrlPersonInfoCardWithFilter.person.PersonID;
            string Username = tbUsername.Text;
            string Password = tbPassword.Text;
            bool isActive = chkIsActive.Checked;
            return new User(personID, Username, Password, isActive);
        }

        private void ReadUpdatedInfo()
        {
            user.PersonID = ctrlPersonInfoCardWithFilter.person.PersonID;
            user.Username = tbUsername.Text;
            user.Password = tbPassword.Text;
            user.IsActive = chkIsActive.Checked;
        }

        private void SaveUser()
        {
            if (this.user == null) // Add New User
            {
                user = GetUserInfo();
            }
            else // Update User
            {
                ReadUpdatedInfo();
            }
            int SavedUserID = -1;
            bool SaveUser = false;
            if (user != null)
            {
                SaveUser = user.Save(ref SavedUserID);
            }
            if (SaveUser)
            {
                lblUserID.Text = SavedUserID.ToString();
                MessageBox.Show($"User with ID={SavedUserID} Saved Successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UserSaved?.Invoke(SavedUserID);
            }
            else
            {
                MessageBox.Show("Error While saving new data!", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (UserValidations.VALID)
            {
                SaveUser();
            }
            else
            {
                MessageBox.Show("Make sure all fields are valid!", "Wrong Inputs!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbUsername_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tbUsername.Text.Length > 0)
            {
                UserValidations.ValidUsername = true;
            }
            else
            {
                UserValidations.ValidUsername = false;
            }
        }

        private void ShowHidePassword(CheckBox chk, TextBox tb)
        {
            if (chk.Checked)
            {
                tb.UseSystemPasswordChar = false;
            }
            else
            {
                tb.UseSystemPasswordChar = true;
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, System.EventArgs e)
        {
            ShowHidePassword(chkShowPassword, tbPassword);
        }

        private void chkShowPasswordConfirmation_CheckedChanged(object sender, System.EventArgs e)
        {
            ShowHidePassword(chkShowPasswordConfirmation, tbConfirmPassword);
        }

        private void HidePassword()
        {
            chkShowPassword.Checked = false; chkShowPassword.Enabled = false;
            chkShowPasswordConfirmation.Checked = false; chkShowPasswordConfirmation.Enabled = false;
            tbPassword.Enabled = false;
            tbConfirmPassword.Enabled = false;
        }

        private void frmEditAddNewUser_Load(object sender, System.EventArgs e)
        {
            SetHeader();
        }

        private void SetHeader()
        {
            if (this.user == null)
            {
                lblHeader.Text = "Add New User";
            }
            else
            {
                lblHeader.Text = "Update User";
            }
        }

    
    }
}
