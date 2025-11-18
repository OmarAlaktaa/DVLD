using System;
using DVLD_BusinessLayer;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlPersonInfoCardWithFilter : UserControl
    {
        public ctrlPersonInfoCardWithFilter()
        {
            InitializeComponent();
        }

        private Person _person;

        public Person person 
        {
            get { return _person; }
            set
            {
                _person = value;
                LoadPerson(); 
            } 
        }

        public void SearchForPerson()
        {
            if (cbFindBy.SelectedValue.ToString() == SearchFilter.PersonInfoCardWithFilters.NationalNo.ToString())
            {
                FindPersonByNationalNumber();
            }
            else if (cbFindBy.SelectedValue.ToString() == SearchFilter.PersonInfoCardWithFilters.PersonID.ToString())
            {
                FindPersonByID();
            }
            else
            {
                MessageBox.Show("Error!", "Error!");
            }
        }

        public void LoadPerson()
        {
            ctrlPersonInfoCard1.Person = person;
        }

        private void FindPersonByNationalNumber()
        {
            Person P = Person.Find(tbFind.Text);
            if (P == null)
            {
                person = null;
            }
            else
            {
                person = P;
            }
        }

        private void LoadPersonIfFound()
        {
            if (person == null)
            {
                ctrlPersonInfoCard1.ClearCard();
                if (cbFindBy.SelectedValue.ToString() == SearchFilter.PersonInfoCardWithFilters.NationalNo.ToString())
                {
                    MessageBox.Show($"Person With National Number: ({tbFind.Text}) Was Not Found!", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (cbFindBy.SelectedValue.ToString() == SearchFilter.PersonInfoCardWithFilters.PersonID.ToString())
                {
                    MessageBox.Show($"Person With ID: ({tbFind.Text}) Was Not Found!", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                ctrlPersonInfoCard1.LoadPersonInfo(person);
            }
        }

        public void SearchAndLoadPerson()
        {
            SearchForPerson();
            LoadPersonIfFound();
        }

        private void FindPersonByID()
        {
            person = Person.Find(Convert.ToInt32(tbFind.Text));
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            SearchForPerson();
            LoadPersonIfFound();
        }

        private void LoadFilter()
        {
            cbFindBy.DataSource = Enum.GetValues(typeof(SearchFilter.PersonInfoCardWithFilters));
        }

        private void ctrlPersonInfoCardWithFilter_Load(object sender, EventArgs e)
        {
            LoadFilter();
        }

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFindBy.SelectedValue.ToString() == 
                SearchFilter.PersonInfoCardWithFilters.PersonID.ToString())
            {
                Validations.ValidateInput.ValidateNumberInput(e);
            }
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmEditAddNewPerson frmAddPerson = new frmEditAddNewPerson();
            frmAddPerson.PersonSaved += DataBack;
            frmAddPerson.ShowDialog();
        }

        private void DataBack(int PersonID)
        {
            this.person = Person.Find(PersonID);
        }
    }
}
