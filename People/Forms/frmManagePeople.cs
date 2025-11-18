using System;
using System.Windows.Forms;
using DVLD_BusinessLayer;
using System.Data;

namespace DVLD
{
    public partial class frmManagePeople : Form
    {
        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadPeople()
        {
            DataTable dt = People.Get();
            if (dt == null)
            {
                MessageBox.Show("Error While Loading Data!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dgvPeople.DataSource = dt;
                lblNumberOfPeople.Text = dt.Rows.Count.ToString();
            }
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            LoadPeople();
            LoadFilters();
        }

        private void LoadFilters()
        {
            cbFilter.DataSource = Enum.GetValues(typeof(SearchFilter.ManagePeopleFilters));
        }

        private void AddNewPersonImplementaion()
        {
            frmEditAddNewPerson frmEditAddNewP = new frmEditAddNewPerson();
            frmEditAddNewP.MdiParent = this.MdiParent;
            frmEditAddNewP.Show();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            AddNewPersonImplementaion();
            LoadPeople();
        }

        private void dgvPeople_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dgvPeople.HitTest(e.X, e.Y);

                // Ensure the click is on a valid cell (not on headers or empty space)
                if (hitTestInfo.ColumnIndex >= 0 && hitTestInfo.RowIndex >= 0)
                {
                    dgvPeople.ClearSelection();
                    dgvPeople.CurrentCell = dgvPeople[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];
                    dgvPeople[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex].Selected = true;
                }
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails PersonDetailsForm = new frmPersonDetails(Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value));
            PersonDetailsForm.Show();
        }

        private bool ConfirmDelete(int DeletePersonID)
        {
            return (MessageBox.Show("Are you sure you want to Delete Person with ID=" + DeletePersonID, "Confirming", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
        }

        public void DeleteSelectedPerson()
        {
            int SelectedPersonID = Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value);
            if (!ConfirmDelete(SelectedPersonID))
            {
                return;
            }
            else if (Person.Delete(SelectedPersonID))
            {
                LoadPeople();
                MessageBox.Show($"Person with ID={SelectedPersonID} Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error while deleting person!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedPerson();
        }

        public void DeleteSelectedPeople()
        {
            bool Deleted = false;
            int[] IDsDeleted = new int[dgvPeople.SelectedRows.Count];
            for (int i = 0; i < dgvPeople.SelectedRows.Count; i++)
            {
                Person P = Person.Find(Convert.ToInt32(dgvPeople.SelectedRows[i].Cells["PersonID"].Value));
                if (P == null)
                {
                    Deleted = false;
                    break;
                }
                if (P.Delete())
                {
                    IDsDeleted[i] = P.PersonID;
                    Deleted = true;
                }
                else
                {
                    Deleted = false;
                    break;
                }
            }
            if (Deleted)
            {
                MessageBox.Show($"People with ID {string.Join(", ", IDsDeleted)} Deleted Successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error while deleting people!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadPeople();
        }

        private void btnDeleteSelectedPeople_Click(object sender, EventArgs e)
        {
            DeleteSelectedPeople();
        }
        
        private void dgvPeople_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPeople.SelectedRows.Count > 1)
            {
                btnDeleteSelectedPeople.Visible = true;
            }
            else
            {
                btnDeleteSelectedPeople.Visible = false;
            }
        }

        private void ShowUpdateSelectedPersonForm()
        {
            int SelectedPersonID = Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value);
            frmEditAddNewPerson Form = new frmEditAddNewPerson(SelectedPersonID);
            Form.Show();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowUpdateSelectedPersonForm();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewPersonImplementaion();
        }

        private void notImplementedMethod()
        {
            MessageBox.Show("This method is not implemented yet", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notImplementedMethod();
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notImplementedMethod();
        }

        private void textBoxFilterVisiblity()
        {
            if ((SearchFilter.ManagePeopleFilters)cbFilter.SelectedIndex
                == SearchFilter.ManagePeopleFilters.None)
            {
                tbFilter.Visible = false;
            }
            else
            {
                tbFilter.Visible = true;
            }
        }

        private void FilterPeopleByNationalNo()
        {
            string FilteringNationalNumber = tbFilter.Text;
            dgvPeople.DataSource = DVLD_BusinessLayer.People.GetWithNationalNumberFilter(FilteringNationalNumber);
        }

        private void FilterPeopleByPersonID()
        {
            int PersonID = Convert.ToInt32(tbFilter.Text);
            dgvPeople.DataSource = DVLD_BusinessLayer.People.GetWithPersonIDFilter(PersonID);
        }

        private void FilterPeopleByFirstName()
        {
            string FilteringFirstName = tbFilter.Text;
            dgvPeople.DataSource = DVLD_BusinessLayer.People.GetWithFirstNameFilter(FilteringFirstName);
        }

        private void FilterPeopleBySecondName()
        {
            string FilteringSecondName = tbFilter.Text;
            dgvPeople.DataSource = DVLD_BusinessLayer.People.GetWithSecondNameFilter(FilteringSecondName);
        }

        private void FilterPeopleByThirdName()
        {
            string FilteringThirdName = tbFilter.Text;
            dgvPeople.DataSource = DVLD_BusinessLayer.People.GetWithThirdNameFilter(FilteringThirdName);
        }

        private void FilterPeopleByLastName()
        {
            string FilteringLastName = tbFilter.Text;
            dgvPeople.DataSource = DVLD_BusinessLayer.People.GetWithLastNameFilter(FilteringLastName);
        }

        private void FilterPeopleByNationality()
        {
            string FilteringNationality = tbFilter.Text;
            dgvPeople.DataSource = DVLD_BusinessLayer.People.GetWithNationalityFilter(FilteringNationality);
        }

        private void FilterPeopleByPhone()
        {
            string FilteringPhone = tbFilter.Text;
            dgvPeople.DataSource = DVLD_BusinessLayer.People.GetWithPhoneFilter(FilteringPhone);
        }

        private void FilterPeopleByEmail()
        {
            string FilteringEmail = tbFilter.Text;
            dgvPeople.DataSource = DVLD_BusinessLayer.People.GetWithEmailFilter(FilteringEmail);
        }

        private void ShowAllPeople()
        {
            dgvPeople.DataSource = DVLD_BusinessLayer.People.Get();
            lblNumberOfPeople.Text = dgvPeople.Rows.Count.ToString();
        }

        private void Filter()
        {
            if (tbFilter.Text == string.Empty)
            {
                ShowAllPeople();
                return;
            }
            dgvPeople.CurrentCell = null;
            switch ((SearchFilter.ManagePeopleFilters)cbFilter.SelectedIndex)
            {
                case SearchFilter.ManagePeopleFilters.None:
                    {
                        break;
                    }
                case SearchFilter.ManagePeopleFilters.NationalNo:
                    {
                        FilterPeopleByNationalNo();
                        break;
                    }
                case SearchFilter.ManagePeopleFilters.PersonID:
                    {
                        FilterPeopleByPersonID();
                        break;
                    }
                case SearchFilter.ManagePeopleFilters.FirstName:
                    {
                        FilterPeopleByFirstName();
                        break;
                    }
                case SearchFilter.ManagePeopleFilters.SecondName:
                    {
                        FilterPeopleBySecondName();
                        break;
                    }
                case SearchFilter.ManagePeopleFilters.ThirdName:
                    {
                        FilterPeopleByThirdName();
                        break;
                    }
                case SearchFilter.ManagePeopleFilters.LastName:
                    {
                        FilterPeopleByLastName();
                        break;
                    }
                case SearchFilter.ManagePeopleFilters.Nationality:
                    {
                        FilterPeopleByNationality();
                        break;
                    }
                case SearchFilter.ManagePeopleFilters.Phone:
                    {
                        FilterPeopleByPhone();
                        break;
                    }
                case SearchFilter.ManagePeopleFilters.Email:
                    {
                        FilterPeopleByEmail();
                        break;
                    }
            }
            lblNumberOfPeople.Text = dgvPeople.Rows.Count.ToString();
        }

        private void cbFilter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            textBoxFilterVisiblity();
            tbFilter.Focus();
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedIndex == (int)DVLD_BusinessLayer.SearchFilter.ManagePeopleFilters.PersonID)
            {
                Validations.ValidateInput.ValidateNumberInput(e);
            }
        }
    }
}
