using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmChangeUserPassword : Form
    {
        public frmChangeUserPassword(int UserID)
        {
            InitializeComponent();
            LoadUser(UserID);
        }

        private void LoadUser(int UserID)
        {
            UserInfoCard.LoadUserInfo(UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            if (tbCurrentPassword.Text.Length > 0)
            {
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
        }

        private bool ArePasswordsValid()
        {
            DVLD_BusinessLayer.Validations.ChangePassword changepassword
                = new DVLD_BusinessLayer.Validations.ChangePassword
                (UserInfoCard.User, tbCurrentPassword.Text, tbNewPassword.Text, tbConfirmNewPassword.Text);
            if (!changepassword.CurrentPswrdMatchOldPswrd)
            {
                MessageBox.Show("Current Password does not match old Password!", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!changepassword.NewPasswordConfirmed)
            {
                MessageBox.Show("Please Confirm your new password!", "Confirm Password!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!changepassword.PasswordChanged)
            {
                MessageBox.Show("You didn't Change password!", "Nothing Changed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ArePasswordsValid())
            {
                int UserID = -1;
                UserInfoCard.User.Password = tbNewPassword.Text;
                if (UserInfoCard.User.Save(ref UserID))
                {
                    MessageBox.Show($"User with ID={UserID}, Saved Successfully!",
                        "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Save operation failed.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
