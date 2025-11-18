using System;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD
{
    public partial class frmManageUsers : Form
    {
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {             
            LoadUsers();
            LoadFilters();
            LoadDefaults();
        }

        private void LoadDefaults()
        {
            tbFilterVisibility();
        }

        private void tbFilterVisibility()
        {
            if ((SearchFilter.ManageUsersFilters)cbFilters.SelectedIndex
                == SearchFilter.ManageUsersFilters.None)
            {
                tbFilter.Visible = false;
                cbUserActivation.Visible = false;
            } 
            else
            {
                tbFilter.Visible = true;
                ShowHideActivationRadios();
            }
        }

        private void LoadUsers()
        {
            dgvUsers.DataSource = Users.Get();
            lblNumberOfUsers.Text = dgvUsers.Rows.Count.ToString();
        }

        private void LoadFilters()
        {
            cbFilters.DataSource = Enum.GetValues(typeof(SearchFilter.ManageUsersFilters));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddNewUser()
        {
            frmEditAddNewUser frm = new frmEditAddNewUser();
            frm.MdiParent = this.MdiParent;
            frm.UserSaved += RefreshUsers;
            frm.Show();
        }

        private void RefreshUsers(int UserID)
        {
            LoadUsers();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            AddNewUser();
        }

        private void tbFilterFocus()
        {
            if (tbFilter.Visible)
            {
                tbFilter.Focus();
            }
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFilterVisibility();
            tbFilterFocus();
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            FilterUsers();
        }

        private void FilterUsersByPersonID()
        {
            int PersonID = int.Parse(tbFilter.Text);
            dgvUsers.DataSource = Users.GetWithPersonIDFilter(PersonID);
        }

        private void FilterUsersByUserID()
        {
            int UserID = int.Parse(tbFilter.Text);
            dgvUsers.DataSource = Users.GetWithUserIDFilter(UserID);
        }

        private void FilterUsersByUsername()
        {
            string Username = tbFilter.Text;
            dgvUsers.DataSource = Users.GetWithUsernameFilter(Username);
        }

        private void FilterUsersByFullName()
        {
            string FullName = tbFilter.Text;
            dgvUsers.DataSource = Users.GetWithFullNameFilter(FullName);
        }

        private void FilterUsersByActivation()
        {
            if ((string)cbUserActivation.SelectedItem == "All")
            {
                dgvUsers.DataSource = Users.Get();
                return;
            }
            bool ActiveUser = ((string)cbUserActivation.SelectedItem == "Yes");
            dgvUsers.DataSource = Users.GetWithActiveFilter(ActiveUser);
        }

        private void ShowHideActivationRadios()
        {
            if ((SearchFilter.ManageUsersFilters)cbFilters.SelectedIndex == SearchFilter.ManageUsersFilters.IsActive)
            {
                tbFilter.Visible = false;
                cbUserActivation.Visible = true;
            }
            else
            {
                cbUserActivation.Visible = false;
                tbFilter.Visible = true;
            }
        }

        private void FilterUsers()
        {
            if (tbFilter.Text.Length > 0 || 
                (SearchFilter.ManageUsersFilters)cbFilters.SelectedIndex 
                == SearchFilter.ManageUsersFilters.IsActive)
            {
                switch ((SearchFilter.ManageUsersFilters)cbFilters.SelectedIndex)
                {
                    case SearchFilter.ManageUsersFilters.PersonID:
                        {
                            FilterUsersByPersonID();
                            break;
                        }
                    case SearchFilter.ManageUsersFilters.UserID:
                        {
                            FilterUsersByUserID();
                            break;
                        }
                    case SearchFilter.ManageUsersFilters.Username:
                        {
                            FilterUsersByUsername();
                            break;
                        }
                    case SearchFilter.ManageUsersFilters.FullName:
                        {
                            FilterUsersByFullName();
                            break;
                        }
                    case SearchFilter.ManageUsersFilters.IsActive:
                        {
                            FilterUsersByActivation();
                            break;
                        }
                }
            }
            else
            {
                dgvUsers.DataSource = Users.Get();
            }
        }

        private void cbUserActivation_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterUsers();
        }

        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.SelectedIndex == 
                (int)DVLD_BusinessLayer.SearchFilter.ManageUsersFilters.UserID
                || cbFilters.SelectedIndex == 
                (int)DVLD_BusinessLayer.SearchFilter.ManageUsersFilters.PersonID)
            {
                Validations.ValidateInput.ValidateNumberInput(e);
            }
        }

        private void dgvUsers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dgvUsers.HitTest(e.X, e.Y);

                // Ensure the click is on a valid cell (not on headers or empty space)
                if (hitTestInfo.ColumnIndex >= 0 && hitTestInfo.RowIndex >= 0)
                {
                    dgvUsers.ClearSelection();
                    dgvUsers.CurrentCell = dgvUsers[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];
                    dgvUsers[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex].Selected = true;

                }
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(dgvUsers.Rows[0].Cells["PersonID"].Value.ToString(), out int PersonID))
            {
                MessageBox.Show("ERROR!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            frmPersonDetails Form = new frmPersonDetails(PersonID);
            Form.ShowDialog();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewUser();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(dgvUsers.Rows[0].Cells["UserID"].Value.ToString(), out int UserID))
            {
                MessageBox.Show("ERROR!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            frmEditAddNewUser frmEditUser = new frmEditAddNewUser(UserID);
            frmEditUser.UserSaved += RefreshUser;
            frmEditUser.ShowDialog();
        }

        private void RefreshUser(int UserID)
        {
            LoadUsers();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedPerson();
            LoadUsers();
        }

        private void DeleteSelectedPerson()
        {
            int UserID = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);
            if (Users.DeleteUser(UserID))
            {
                MessageBox.Show($"User with ID={UserID} Deleted Successfully!",
                    "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("User was not Deleted!", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);
            frmChangeUserPassword Form = new frmChangeUserPassword(UserID);
            Form.MdiParent = this.MdiParent;
            Form.Show();
        }

    
    }
}
